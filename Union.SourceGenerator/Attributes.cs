using System;

#nullable enable

namespace Union.SourceGenerator
{
    [Flags]
    public enum UnionOptions
    {
        None        = 0b_0000_0000_0000_0000,
        Value       = 0b_0000_0000_0000_0001,
        ToString    = 0b_0000_0000_0000_0010,
        Switch      = 0b_0000_0000_0000_0100,
        As          = 0b_0000_0000_0000_1000,
        Is          = 0b_0000_0000_0001_0000,
        Conversions = 0b_0000_0000_0010_0000,
        From        = 0b_0000_0000_0100_0000,
        
        
        Index       = 0b_1000_0000_0000_0000,
        
        Default     = Value | ToString | Switch | As | Is | Conversions | From | Index,
    }
    
    public static class FlagsExtensions
    {
        public static bool HasAnyFlag(this UnionOptions composite, UnionOptions flags) => (composite & flags) != 0;
        public static bool HasAllFlags(this UnionOptions composite, UnionOptions flags) => (composite & flags) == flags;
        public static void SetFlags(ref this UnionOptions composite, UnionOptions flags) => composite |= flags;
        public static void ClearFlags(ref this UnionOptions composite, UnionOptions flags) => composite &= ~flags;
        
    }
    
    internal static class Attributes
    {
        internal const string ATTRIBUTE_TEXT = @"
using System;

namespace Union
{
    [Flags]
    public enum UnionOptions
    {
        None        = 0b_0000_0000_0000_0000,
        Value       = 0b_0000_0000_0000_0001,
        ToString    = 0b_0000_0000_0000_0010,
        Switch      = 0b_0000_0000_0000_0100,
        As          = 0b_0000_0000_0000_1000,
        Is          = 0b_0000_0000_0001_0000,
        Conversions = 0b_0000_0000_0010_0000,
        From        = 0b_0000_0000_0100_0000,

        Default     = Value | ToString | Switch | As | Is | Conversions | From,
    }

    [AttributeUsage(AttributeTargets.Struct)]
    internal class GenerateUnionAttribute : Attribute
    {
        public GenerateUnionAttribute(UnionOptions options = UnionOptions.Default)
        {

        }
    }

    [AttributeUsage(AttributeTargets.Field)]
    internal class UnionIndexAttribute : Attribute
    {
        
    }
}";
    }
}