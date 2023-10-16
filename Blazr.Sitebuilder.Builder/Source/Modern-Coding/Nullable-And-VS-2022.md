Nulls have always presented programming challenges.

To define a null, I'll quote MS-Docs:

> The null keyword is a literal that represents a null reference, one that does not refer to any object. null is the default value of reference-type variables. Ordinary value types cannot be null, except for nullable value types.

C# 8 introduced nullable reference types.  A system where reference types could no longer be null unless explicitly declared with the `?` operator.

```csharp
// not nullable fields/properties must be assigned a value
string notNullableValue = string.Empty;

string? NullableValue; 

// nullable is controlled by the return value from the method 
var value = SomeMethod();
```
For many programmers this had no consequences because the project templates by default did not enable *Nullable*.  We sailed on serenely in ignorance.

With *Nullable* enabled the compiler throws warnings whenever it considers you've broken the rules.  It's pretty good, but there are certain circumstances where you want to break the rules, and edge cases where it gets things wrong.  We'll look at both of those later.

## Visual Studio 2022

Visual Studio 2022 moved the goalposts: `Nullable` is now enabled by default.  Here's the project file for a console app:

```xml
  <PropertyGroup>
    <OutputType>Exe</OutputType>
    <TargetFramework>net6.0</TargetFramework>
    <ImplicitUsings>enable</ImplicitUsings>
    <Nullable>enable</Nullable>
  </PropertyGroup>
```

You now have to explicitly disable it.  Write code the old way, or import existing code and unless you were a miracle coder,  you will see a lot of null code warnings and errors.  It has certainly impacted my coding style and practices. 

Let's look at some code to see what I mean.

## Simple Nullable

Here's some simple old style code.

```csharp
string value;

value = getHello(0);

Console.WriteLine(value);

string getHello(int test)
    =>  test == 1
        ? "Hello World"
        : null;
```

Disable Nullable and this code flies: no exceptions or warnings.  So what's the problem?  

String can be a null, so we can pass null strings around.  Unless we explicitly check, we don't know if `Console.WriteLine` handles nulls correctly.  If it doesn't we will get an exception when we pass it a null.  Everything flies because `Console.WriteLine` knows how to handle nulls.  It's definition looks like this:

```csharp
Console.WriteLine(string? value) 
```

Enable Nullable and we see a warning - *Possible Null Reference Return* on `getHello`.  Why?  
1. `getHello` declares it's return value as `string`.  No `?` and therefore not nullable, yet in the body we can return `null`.  
2. `string value` also declares `value` as not nullable.

To fix this:

```csharp
string? value;

value = getHello(0);

Console.WriteLine(value);

string? getHello(int test)
    =>  test == 1
        ? "Hello World"
        : null;
```

We've added the nullable `?` operator to `value` and the return declaration of `getHello`.

We're now declaring and handling nullables correctly and the compiler is happy,

## Nullable Detection

Our new block of code

```csharp
string? value;

var sometest = true;

value = sometest
        ? "Hello World"
        : "Bye World";

WriteLine(value);

value = sometest
        ? "Hello World"
        : null;

// Get a possible null warning
WriteLine(value);

void WriteLine(string value)
    => Console.WriteLine(value);
```

We have our own `WriteLine` method with a *not nullable* string argument.  note that we only get a compiler warning after yhe second assignment to value where we try and assign a `null`.  The compiler is analysing the code, seeing a possble null and throwing a warning.

We fix this by changing `WriteLne` to accept a nullable string.  The compiler knows `Console.WriteLine` accepts nullable strings so everything is good. 

```csharp
void WriteLine(string? value)
    => Console.WriteLine(value);
```

## Edge Case

The contrived code below illustrates one common problem.  We know once we've called `GetAsync` and assigned `value` it can't be null because we throw an error if it is.  However, the analyser isn't that clever so still throws the warning. 

```csharp
string? value = null;

var myClass = new MyClass();

await myClass.GetAsync();

value = myClass.Value;

// We know it's safe to pass Value because it can't be null
// but the code doesn't so still throws a warning
WriteLine(value);

void WriteLine(string value)
    => Console.WriteLine(value);

class MyClass
{
    public string? Value { get; set; }

    public async Task GetAsync()
    {
        await Task.Yield();
        // Get the value from a service
        this.Value = "Hello World";
        if (this.Value is null)
            throw new ArgumentNullException("Value is null and shouldn't be!");
    }
}
```

We can suppress these messages using the ! null forgiving operator.  In the code above, the correct place to apply it is where the assignment takes place. 

```csharp
value = myClass.Value!;
```

Don't apply it to where it's being used.

```csharp
WriteLine(value!);
```
Two notes:

> Don't use it unless you have to.  Use null coalesing - covered below - wherever you can. 

> The null forgiving operator has no effect in compilation.  It's just a succinct message from the programmer to the compiler/interpreter saying "No worries mate, bin the warnings, I've got it covered!". 

## Real World Edge Case

The example above may be contrived, but consider the following code from a Blazor page.

```csharp
[Inject] MyService? myService { get; set; }

void DoSomething()
{
    // nullable warning on myService
    var value = myService.Value;
}
```

Blazor ensures `MyService` is registered: it throws an exception if it isn't.  We could code:

```csharp
[Inject] MyService myService { get; set; } = new MyService();
```

Which works, but is just *wrong*.  We're creating an object for a contrived reason, and it's absolutely useless created outside the DI container context.

We could use null forgiving when we use the object, which is ok when you use ut once, but say you use the service ten times in you code block, it doesn't seem right.

My current best solution is to use a local variable in a code block like this:

```csharp
    void DoSomething()
    {
        MyService service = myService!;
        var value = service.Value;
        ....
    }
```
## Breaking The Rules

Not often, but testing is one area where you may break the rules to test null handling and boundary conditions.

## Null Coalescing

The normal approach to the "boxing" problem of switching from nullable to non nullable is to use null coalescing `??`.

We can use it on  `Writeline` like this.  `Writeline` now receives `value` if it isn't `null`, or `string,Empty` if it is. 

```csharp
WriteLine(value ?? string.Empty);
```

We can also use the very useful null coalescing assignment operator `??=` like this whenever we need to make sure a value isn't null: 

```csharp
value ??= string.Empty;
WriteLine(value);
```

More succinct than:

```csharp
if (value is null)
{
    value = string.Empty;
}
```

## Some Observations and Suggestions

1. Don't chicken out, leave **Nullable** enabled.
2. A lot of null checking code becomes obselete by ensuring you pass not nullable objects into methods.
3. Get familiar with *Null Coalescing*.
4. Don't splatter *null forgiving !* marks across your code.  Ask yourself Why?  Is this a boundary condition or just lazy coding?  