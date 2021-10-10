using System;

namespace Union
{
    public readonly struct None 
    {
        public static Union<T, None> Of<T>(T t) where T : notnull => new None();
    }
}