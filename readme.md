C# library for unions.  Similar to [OneOf](https://github.com/mcintyre321/OneOf), with a few conceptual changes.

- Better handling of nullable reference types
- Change methods to match C# idioms, rather than adopting F# idioms.

### Examples

```
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

