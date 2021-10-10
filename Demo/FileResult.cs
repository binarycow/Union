using Union;

namespace Union.Demo
{
    [GenerateUnion(UnionOptions.Conversions | UnionOptions.Switch)]
    public readonly partial struct FileResult
    {
        [UnionIndex]
        private readonly int index;
        private readonly NotFound notFound;
        private readonly Error<string> errorMessage;
        private readonly string? result;
    }
}
