# Union

## Summary 

C# library for unions.  Similar to [OneOf](https://github.com/mcintyre321/OneOf), with a few conceptual changes.

- Better handling of nullable reference types
- Change methods to match C# idioms, rather than adopting F# idioms.

This library comes in two forms:

- [Regular C# library](#library-reference)
- [Source generator](#c-source-generator)
  - Note: The source generator **does not** have a dependency on the regular `Union` library.

## Library reference

You can reference the `Union` library via nuget package, source, etc.

Doing so gives you the following types:

- Interface (`IUnion`), implemented by:
  - _Note:_ All of these types come in 8 'arities'.  For example, `Union<T0>`, `Union<T1>`, through `Union<T7>`
  - Interfaces: `IUnion<T0..Tn>`
  - Structs: `Union<T0..Tn>`
  - Classes: `UnionClass<T0..Tn>`
- Misc types:
  - `Yes`
  - `No`
  - `Maybe`
  - `Unknown`
  - `True`
  - `False`
  - `All`
  - `Some`
  - `None`
  - `NotFound`
  - `Success`
  - `Success<T>`
  - `Result<T>`
  - `Error`
  - `Error<T>`

### Examples

```csharp
private static void Main()
{
    ReadFileText("C:\\Path\\to\\file.txt").Switch(
        _ => Console.WriteLine($"File doesn't exist."),
        error => Console.WriteLine($"Error: {error.Value}"),
        contents => Console.WriteLine($"File contents: {contents}")
    );
}

private static Union<None, Error<string>, string> ReadFileText(string path)
{
    try
    {
        if (File.Exists(path) == false)
            return new None();
        return File.ReadAllText(path);
    }
    catch (Exception e)
    {
        return new Error<string>(e.Message);
    }
}
```

### Nulls

Null values are not allowed.

If you have the need to return a `null` value, I suggest a different pattern - using the `None` type instead of returning `null`.  See the above example - instead of
returning `null`, `Union.None` is returned.  Thus, all other options are guarenteed to not be `null`.


## C# Source Generator

1. Add a reference to the `Union.SourceGenerator` package
2. Create a `struct` and apply the `GenerateUnion` attribute to it.
3. Add a `field` for each value you'd like to store in the type.
4. See the [complete documentation](Union.SourceGenerator/readme.md) for all restrictions, requirements, and options


### Example

```csharp
[GenerateUnion]
public readonly partial struct FileResult
{
    private readonly None none;
    private readonly Error<string> errorMessage;
    private readonly string? result;
}

internal static class Program
{
    private static void Main()
    {
        ReadFileText("C:\\Path\\to\\file.txt").Switch(
            _ => Console.WriteLine($"File doesn't exist."),
            error => Console.WriteLine($"Error: {error.Value}"),
            contents => Console.WriteLine($"File contents: {contents}")
        );
    }
    private static FileResult ReadFileText(string path)
    {
        try
        {
            if (File.Exists(path) == false)
                return new None();
            return File.ReadAllText(path);
        }
        catch (Exception e)
        {
            return new Error<string>(e.Message);
        }
    }
}
```