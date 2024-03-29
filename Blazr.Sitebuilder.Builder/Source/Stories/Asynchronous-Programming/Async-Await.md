Async/Await is the fundamental building material of asynchronous operations in modern C#.

On the plus side, it abstracts the programmer from the nitty gritty of the *Task Processing Library*.  On the downside is deception, what you see is not what you get below the surface.

To quote Stephen Tomb, one of the authors of Async/Await:

> [It's] both viable and extremely common to utilize the functionality without understanding exactly what's going on under the covers. You start with a synchronous method ...  sprinkle a few keywords, change a few method names, and you end up with [an] asynchronous method instead. 

There are several very good articles available on the subject, I've included references to those that were the source material for this article.  Unfortunately most assume a level of knowledge mortal programmers don't have.  In this short article I'll attempt to bring that required knowledge down to the level that most will understand.

**Async**

Async is a modifier.  It labels a method as containing one or more awaitable async calls.

**Await**

Defines an async call that should be awaited.  Any code following the call should only execute once the awaitable has completed.  Only methods implementing the *awaitable* pattern can be awaited.  `Task` is the most common awaitable, but there are others.

**Yielding**

Yielding occurs when the background operation behind an async call shifts to a different thread and frees the current context to continue processing it's queue. The process returns a reference to an awaitable object that the background operation updates when complete.  

**Continuation**

A continuation is the block of code following an await statement.  It encapsulates the code to run after the await completes.  It may or may not consume the result of the await.

## Awaitables and Awaiters

You can only `await` a method that implements the *awaitable* pattern: a `GetAwaiter` method that returns an object implementing the *awaiter* pattern.

The *Awaiter* pattern.

```csharp
public struct MyAwaiter : INotifyCompletion
{
    public bool IsCompleted;
    public void OnCompleted(Action continuation);
    public void GetResult();
}
```

You can't await an `Int32`.  Or can you?

Can:

```csharp
  await Task.Delay(500);
```

be coded as:

```csharp
   await 500;
```

It's not particularly obvious what it does out of context, but it's certainly succinct.

It turns out you can.  You just need to implement the awaitable pattern on `Int32`.

It's this simple. 

```csharp
public static TaskAwaiter GetAwaiter(this Int32 milliseconds)
{
    return Task.Delay(milliseconds).GetAwaiter();
}
```

Add `GetAwaiter` as an extension method, call `Task.Delay(milliseconds)` and return it's awaiter.

We'll look into *awaiters* and *awaitable* in more detail in the *Awaitable* article.  

## Tasks

Tasks are another fundimental *TPL* building block.

`Task`, in all it's guises, is an implementation of an awaitable.  It returns a `TaskAwaiter` that implements the *awaiter* pattern.

A `Task` is a simple `struct` that represents an asynchronous operation. It's a handle that provides a communications channel between the caller and the asynchronous background operation.

It's returned to the caller in one of four states:

1. Completed - probably the most common.  It's safe to get the result.
2. Not Completed - there's a background task running somewhere else that's in-process.  The Task's result isn't yet set.  If you try and get it, you will block your thread.
3. Faulted - A exception has occured which the task returns.
4. Cancelled - A cancellation token request was successful.  The operation was cancelled.

It's important to understand that the state of the returned `Task` is unrelated to the state of the code block that returned it.  If your code block is handed a `Task`, the immediate code behind the call has completed.  Code may have been parcelled up as a continuation or as a block of code within the *Async State Machine*, but the thread your code is running on is free. The continuation or state machine code will be scheduled to run when appropriate.  We'll look at how this works shortly.

The asyncronous background operation holds a reference to the task.  When it completes it:

1. Sets the task's state to Completed 
2. Sets the task's result [if there is one].
3. Schedules any registered continuations.

You can attach a continuation to any task regardless of who created it.  That continuation will be executed immediately if the task has already completed, or added to the awaiter's continuation collection if not.

Where continuations run is based on ConfigureAwait: 

1. false - on any threadpool thread. 
2. true - on the synchronisation context if one exists. 

The `public` side of `Task` is for consumers: there's no control mechanisms. The control side is `internal`.  The state machine accesses this functionality through `AsyncTaskMethodBuilder`.  We normally use a `TaskCompletionSource` object. You'll see this used in our example state machine shortly.

## Async/Await

To demonstrate how opaque *Async/Await* really is, let's look at the code generated by the compiler.

Go to [SharpLab](https://sharplab.io/).  Set the output to *C#* and enter the following code:

```csharp
using System;
using System.Threading.Tasks;

public class C {
    public async Task DoSomeWorkAsync() {
        Console.WriteLine("Starting");
        await DoSomethingAsync();
        Console.WriteLine("Finished");
    }
    
    private Task DoSomethingAsync()
    {
        return Task.Delay(500);
    }
}
```

The generated code is complex and unrecognisable.  Let's break it down.  You now have:

1. A private *Async State Machine* within you parent class.
2. A refactored `DoSomeWorkAsync`.
 
`Async` and `await` have disappeared.

Look at the state machine.  The original code block has been split into `n+1` states and code blocks based on `awaits`.

The state machine provides a public Task object [through the `AsyncTaskMethodBuilder`] which is returned to the caller when the state machine yields control.

The refactored `DoSomeWorkAsync` creates and starts the state machine, and on a yield, returns the state machine's Task to the caller.

```csharp
    [AsyncStateMachine(typeof(<DoSomeWorkAsync>d__0))]
    [DebuggerStepThrough]
    public Task DoSomeWorkAsync()
    {
        <DoSomeWorkAsync>d__0 stateMachine = new <DoSomeWorkAsync>d__0();
        stateMachine.<>t__builder = AsyncTaskMethodBuilder.Create();
        stateMachine.<>4__this = this;
        stateMachine.<>1__state = -1;
        stateMachine.<>t__builder.Start(ref stateMachine);
        return stateMachine.<>t__builder.Task;
    }
```

`__builder.Start` internally calls `MoveNext`,  the first block runs synchronously to the final async operation [the *await* line] and increments the state. The block either completes or yields control.

If the async operation completes, then execution falls through to the next block, and so on... with everything executing synchronously on the same thread. 

If the async operation yields [returns a not complete awaitable such as a Task], the state machine adds a continuation to the awaitable to call `MoveNext` and completes.

When the async operation completes on it's background thread it queues the continuation to run [normally on the synchronisation context].  The continuation "re-enters" the state machine and executes the next state code block.

The final state block has no final async operation so falls through to the bottom where it sets the state machine's own Task result and state to completed.


### Our Own State Machine

We've seen what the compiler produces, but we can build our own state machine as a learning exercise.

Consider this simple Blazor `Home` page:

```csharp
@page "/"

<PageTitle>Home</PageTitle>

<h1>Hello, world!</h1>

Welcome to your new app.

<div class="mb-3">
    <button class="btn btn-success" @onclick="Clicked">Click</button>
</div>

<div class="bg-dark text-white m-2 p-2">
    @_message
</div>

@code {
    private string? _message;

    private async Task Clicked()
    {
        _message = $"Processing at {DateTime.Now.ToLongTimeString()}";
        await TaskHelper.DoSomethingAsync();
        _message = $"Completed Processing at {DateTime.Now.ToLongTimeString()}";
    }
}
```

`TaskHelper` looks like this:

```csharp
public static class TaskHelper
{
    public static Task DoSomethingAsync()
        => Task.Delay(1000);
}
```

Here's the skeleton class.

1. We capture a reference to the parent class so we have access to any class variables, properties and methods.  Being a class within the parent gives us access to all the privates.
2. `TaskCompletionSource` provides a `Task` that we control.  This is the `Task` we return to the caller.
3. `_state` holds the current state of the machine.  It gets incremented as we step through the states.
4. `Task` is the actual task the `TaskCompletionSource` provides.
5. `MoveNext` is the method we call to start and increment the state. 

```csharp
class Clicked_StateMachine
{
    private readonly Home _parent;

    private readonly TaskCompletionSource _tcs = new();
    private int _state = 0;
    private TaskAwaiter _state1_Awaiter = default!;

    public Task Task => _tcs.Task;

    public Clicked_StateMachine(Home parent)
    {
        _parent = parent;
    }

    public void MoveNext()
    { }
}
```

The `MoveNext` detail.  

Execution is wrapped in a `try` to capture exceptions and report them to the caller through the  `TaskCompletionSource` task.

```csharp
public void MoveNext()
{
    try
    {
        //...
    }
    // Something went wrong.  Pass the error to the caller through the completion task
    catch (Exception e)
    {
        _tcs.SetException(e);
    }
}
```

The *State 0* step runs the first code block.

It:

1. Runs the sync code. 
1. Runs the awaitable and captures the returned `Task`.
1. Increments the state.

Finally it checks the state of `task`.  
   
 - If it's incomplete, it adds a continuation to the task to call `MoveNext` on completion.  
 - If it's complete, it falls through to the next state and execution continues synchronously on the same context.

```csharp
    if (_state == 0)
    {
        // The code from the start of the method to the first 'await'.
        {
            _parent._log.AppendLine($"State Machine Processing at {DateTime.Now.ToLongTimeString()}");
        }

        var task = TaskHelper.DoSomethingAsync();

        _state = 1;

        if (!task.IsCompleted)
        {
            task.ContinueWith(_ => MoveNext());
            return;
        }
    }
```

Step 1 only runs once state 0's async operation has completed.  It runs state 1 code.  As there's no further awaits it falls out of the bottom to the finalization process.

```csharp
    // Step 1 - the first await block
    if (_state == 1)
    {
        // The code from the first await to the next await or end of the method.
        {
            _parent._log.AppendLine($"State Machine Processing completed at {DateTime.Now.ToLongTimeString()}");
        }

        //No more await tasks so fall thro to bottom
    }
```

The finalization process is to set the task manager to complete.

```csharp
// No more steps, job done.  Set the Task to complete and finish.
_taskManager.SetResult();
```

Now we can refactor `Clicked` in `Home`. It's no longer `async` and just returns the state machine `Task` to the UI event handler.

```csharp
    private Task Clicked()
    {
        var stateMachine = new Clicked_StateMachine(this);
        stateMachine.MoveNext();
        return stateMachine.Task;
    }
```

## Takaways

My example state machine is a gross oversimplification of the real thing.  I've removed all the exception and cancellation code.

In SharpLab toggle the mode from *Debug* to *Release*.  The state machine changes from a `class` to a `struct` for performance purposes. 

## References

The primary resources for this article were:

[Stephen Toub's how await works](https://devblogs.microsoft.com/dotnet/how-async-await-really-works/)

[Sergey Tepliakov's dissecting async](https://devblogs.microsoft.com/premier-developer/dissecting-the-async-methods-in-c/)

[Stephen Toub's Blog await anything](https://devblogs.microsoft.com/pfxteam/await-anything/)

[Stephen Cleary's various airings on the topic such as this one](https://blog.stephencleary.com/2023/11/configureawait-in-net-8.html)

The code example is based on Sergey Tepliakov's code. 