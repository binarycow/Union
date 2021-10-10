using System;
using System.IO;
using System.Reflection;

namespace Union.Demo
{
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
                return new NotFound();
            return File.ReadAllText(path);
        }
        catch (Exception e)
        {
            return new Error<string>(e.Message);
        }
    }
}
}