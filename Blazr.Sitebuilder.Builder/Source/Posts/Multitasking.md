There's a common misconception in async programming that a thread can do more that one thing at once.  It can monitor for something to happen [and execute when it does] and at the same time run other tasks.  Wrapping code in an async/await block makes it non-blocking.

Here's a recent example in a question I answered on StackOverflow.  I've refactored thr code a little to make it easier to see thr changes.

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

When you run this, the console updates at each ping, but the Blazor application will only update at the end.

If the console updates properly, why doesn't the App. Surely, the console updates prove the code is non blocking.

The important question to answer here is: 

> Where is the shell process running?

Consider where we're starting it.  On the UI thread [the synchronisation context].  It's not started asynchronously, so it all runs on the same thread.  And it blocks that thread for the duration.

