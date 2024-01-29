How often have you needed to tweak one of Blazor's input controls?

In this post I'll show how to add maximum and minimum value validation checking.

The key to tweaking is inheriting from the original code.  Don't create a new component and try and incorporate the original into it.   

Here's my base new input component.

```csharp
public class InputIntegerConstrained: InputNumber<int>
{
}
```

This will do exactly the same as the base `InputNumber<int>`.

All we want to do is check the input value and add validation messages if the value falls outside the set range.

First we add `Max` and `Min` Parameters.  Note they are nullable: if we don't set them they aren't checked.

```csharp
    [Parameter] public int? Min { get; set; }
    [Parameter] public int? Max { get; set; }
```

All `InputBase` components provide the `TryParseValueFromString` protected method that we can override.

This is the base implementation which checks to see that the submitted string value can be parsed to `TValue`.

```csharp
    protected override bool TryParseValueFromString(string? value, [MaybeNullWhen(false)] out TValue result, [NotNullWhen(false)] out string? validationErrorMessage)
    {
        if (BindConverter.TryConvertTo<TValue>(value, CultureInfo.InvariantCulture, out result))
        {
            validationErrorMessage = null;
            return true;
        }
        else
        {
            validationErrorMessage = string.Format(CultureInfo.InvariantCulture, ParsingErrorMessage, DisplayName ?? FieldIdentifier.FieldName);
            return false;
        }
    }
```

We can use this and add some extra range checking.

```csharp
    protected override bool TryParseValueFromString(string? value, [MaybeNullWhen(false)] out int result, [NotNullWhen(false)] out string? validationErrorMessage)
    {
        if (BindConverter.TryConvertTo<int>(value, CultureInfo.InvariantCulture, out result))
        {
            if ( this.Min is not null && result < this.Min)
            {
                validationErrorMessage = $"{DisplayName ?? FieldIdentifier.FieldName} must be greater than {this.Min} ";
                return false;
            }

            if (this.Max is not null && result > this.Max)
            {
                validationErrorMessage = $"{DisplayName ?? FieldIdentifier.FieldName} must be less than {this.Max} ";
                return false;
            }

            validationErrorMessage = null;
            return true;
        }
        else
        {
            validationErrorMessage = string.Format(CultureInfo.InvariantCulture, ParsingErrorMessage, DisplayName ?? FieldIdentifier.FieldName);
            return false;
        }
    }
```

And that's it.  Our finished component looks like this:

```csharp
public class InputIntegerConstrained: InputNumber<int>
{
    [Parameter] public int? Min { get; set; }
    [Parameter] public int? Max { get; set; }

    protected override bool TryParseValueFromString(string? value, [MaybeNullWhen(false)] out int result, [NotNullWhen(false)] out string? validationErrorMessage)
    {
        if (BindConverter.TryConvertTo<int>(value, CultureInfo.InvariantCulture, out result))
        {
            if ( this.Min is not null && result < this.Min)
            {
                validationErrorMessage = $"{DisplayName ?? FieldIdentifier.FieldName} must be greater than {this.Min} ";
                return false;
            }

            if (this.Max is not null && result > this.Max)
            {
                validationErrorMessage = $"{DisplayName ?? FieldIdentifier.FieldName} must be less than {this.Max} ";
                return false;
            }

            validationErrorMessage = null;
            return true;
        }
        else
        {
            validationErrorMessage = string.Format(CultureInfo.InvariantCulture, ParsingErrorMessage, DisplayName ?? FieldIdentifier.FieldName);
            return false;
        }
    }
}
```

Which we can use in a demo page:

```csharp
@page "/"

<PageTitle>Home</PageTitle>

<h1>Hello, world!</h1>

<EditForm EditContext="_editContext">
    <InputIntegerConstrained class="form-control" @bind-Value=_model.IntValue Max="100"/>
    <ValidationMessage For="() => _model.IntValue" />
</EditForm>

@code {
    private Model _model = new();
    private EditContext? _editContext;

    protected override void OnInitialized()
    {
        _editContext = new(_model);
    }

    public class Model
    {
        public int IntValue { get; set; }
    }
}
```
