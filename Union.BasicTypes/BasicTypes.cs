using System;

namespace Union
{
    public readonly struct Yes { }
    public readonly struct No { }
    public readonly struct Maybe { }

    public readonly struct Unknown { }
    public readonly struct True { }
    public readonly struct False { }

    public readonly struct All { }
    public readonly struct Some { }

    public readonly struct NotFound { }

    public readonly struct Success { }

    public readonly struct Success<T>
    {
        public Success(T value)
        {
            Value = value;
        }
        public T Value { get; }
    }

    public readonly struct Result<T>
    {
        public Result(T value)
        {
            Value = value;
        }
        public T Value { get; }
    }

    public readonly struct Error { }
    public readonly struct Error<T>
    {
        public Error(T value)
        {
            Value = value;
        }
        public T Value { get; }
    }
}