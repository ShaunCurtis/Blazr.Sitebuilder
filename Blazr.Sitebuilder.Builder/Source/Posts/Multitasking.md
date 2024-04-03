There's a common misconception in async programming that a thread can muti-task: do more that one thing at once.  It monitors something for an event [and execute when it does] while running other tasks.  Wrapping code in an async/await block makes it non-blocking.

Here's a recent example in a question I answered on StackOverflow.  I've refactored the code a little to make it easier to see thr changes.

It opens a new shell process, runs a *ping* and passes back the output.

```sharp
@page "/"
@using System.Diagnostics
@using System.Text

<div class="mb-2">
    <button class="btn btn-primary" disabled="@_processing" @onclick="ProcessSomething">execute...</button>
</div>

<div class="bg-dark text-white m-2 p-2">
    <pre>@_output</pre>
</div>

@code {
    private StringBuilder _output = new();
    private bool _processing;

    private async Task ProcessSomething()
    {
        _output.Clear();
        await this.RunPingAsync();
    }

    private async Task RunPingAsync()
    {
        _processing = true;
        Process p = new Process();
        p.StartInfo.UseShellExecute = false;
        p.StartInfo.RedirectStandardOutput = true;
        p.StartInfo.FileName = "cmd.exe";
        p.StartInfo.Arguments = "/c ping 8.8.8.8";
        p.OutputDataReceived += OnDataReceived;
        p.Start();
        p.BeginOutputReadLine();
        p.WaitForExit();
        _processing = false;
        await InvokeAsync(StateHasChanged);
    }

    private async void OnDataReceived(object? sender, DataReceivedEventArgs args)
    {
        Console.WriteLine(args.Data);
        _output.AppendLine(args.Data);
        await InvokeAsync(StateHasChanged);
    }
}
```

When you run this code, the console updates at each ping, but the Blazor application will only update at the end.  If the console updates properly, why doesn't the App. 

> Surely, the console updates prove the code is non blocking?

Consider this:

If your code is waiting for something to happen, that's all it's doing.  It's blocked.  In our code block the thread running the shell process is waiting for output.  When it gets it it raises the `OutputDataReceived` event and then goes back to waiting for the next output [or the exit].

This code is running on the UI thread [the synchronisation context].  When the shell process raises `OutputDataReceived` it runs `OnDataReceived` which writes to the console, updates the component state and then queues a Render.  The problem is here.  The Renderer queues the render on the *synchronisation context*.  Unfortunately, that's busy running `RunPingAsync`, which doesn't complete until the shell process completes.

The *synchronisation context* is blocked until `RunPingAsync` completes.  The render is then executed and updates the UI.

## The solution

The solution is to run the shell process elsewhere: on the Threadpool.

We can change `ProcessSomething` like this to run the shell process on the Threadpool.

```csharp
    private Task ProcessSomething()
    {
        _output.Clear();
         Task.Run(RunPingAsync);
        return Task.CompletedTask;
    }
```
