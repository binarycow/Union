namespace Union
{
    public interface IUnion
    {
        object Value { get ; }
        int Index { get; }
    }
}