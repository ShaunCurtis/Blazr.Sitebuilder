Consider the following code:

```csharp
private Task OnButtonClick()
{
   //do some sync work
   return Task.CompletedTask;
}
```

Many will tell you this is expensive for one of two reasons:

1. The compiler has to build out an async state machine to handle the Task.
2. The compiler needs to build a Task to return.

Neither are true.

1. The method is not async - it has no `Async/Await` signature - and the low level code looks exactly the same as the high level code.  There's no need for async state machines because there's no awaiters.  You can check out the code at [SharpLab](https://sharplab.io/).

2. `Task.Completed` is cached as a singleton.  Everyone gets the same object returned.  The Task code looks like this:

```csharp
private static Task s_completedTask;
 
public static Task CompletedTask
{
    get
    {
        var completedTask = s_completedTask;
        if (completedTask == null)
            s_completedTask = completedTask = new Task(false, (TaskCreationOptions)InternalTaskOptions.DoNotDispose, default(CancellationToken)); // benign initialization ----
        return completedTask;
    }
}
```
[You can view this code here](https://referencesource.microsoft.com/#mscorlib/system/threading/Tasks/Task.cs,66f1c3e3e272f591)

