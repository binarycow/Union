using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Union
{
    public readonly struct Union<T0> : IEquatable<Union<T0>>, IUnion<T0>
    {
        public UnionIndexT0 Index { get; }
        private readonly T0? value0;
        
        int IUnion.Index => (int) Index;
        private Union(UnionIndexT0 index, T0? value0 = default)
        {
            this.Index = index;
            this.value0 = default;
            switch (this.Index)
            {
                case UnionIndexT0.T0:
                    this.value0 = value0 ?? throw new ArgumentNullException(nameof(value0));
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(index));
            }
        }
        public void Switch(Action<T0> f0)
        {
            f0 = f0 ?? throw new ArgumentNullException(nameof(f0));
            switch (this.Index)
            {
                case UnionIndexT0.T0 when value0 is not null:
                    f0(this.value0);
                    return;
            }
        }
        
        public TResult Switch<TResult>(Func<T0, TResult> f0)
        {
            f0 = f0 ?? throw new ArgumentNullException(nameof(f0));
            return this.Index switch
            {
                UnionIndexT0.T0 when value0 is not null => f0(this.value0),
                _ => throw new InvalidOperationException(),
            };
        }
        
        public object Value => Index switch
        {
            UnionIndexT0.T0 when value0 is not null => value0,
            _ => throw new InvalidOperationException(),
        };
        
        #region As Methods
        public T0? AsT0()
        {
            return this.Index == UnionIndexT0.T0 ? value0 : throw new InvalidOperationException($"Cannot return as T0 as result is T{this.Index}");
        }
        #endregion As Methods
        
        #region Is Methods
        public bool IsT0([NotNullWhen(true)] out T0? value)
        {
            value = this.Index == UnionIndexT0.T0 ? value0 : default;
            return this.Index == UnionIndexT0.T0 && !(value is null);
        }
        #endregion Is Methods
        
        #region Select Methods
        public Union<TResult> Select0<TResult>(Func<T0, TResult> selector)
        {
            selector = selector ?? throw new ArgumentNullException(nameof(selector));
            return this.Index switch
            {
                UnionIndexT0.T0 when this.value0 is not null => selector(this.value0),
                _ => throw new InvalidOperationException(),
            };
        }
        #endregion Select Methods
        
        public override string ToString() => this.Index switch
        {
            UnionIndexT0.T0 => $"{typeof(T0)}: {value0}",
            _ => throw new InvalidOperationException(),
        };
        #region Conversion
        public static implicit operator Union<T0>(T0 t) => FromT0(t);
        #endregion Conversion
        
        #region Static factory methods
        public static Union<T0> FromT0(T0 t) => new (UnionIndexT0.T0, value0: t);
        #endregion Static factory methods
        
        #region Equality
        public bool Equals(Union<T0> other)
        {
            return this.Index switch
            {
                { } when this.Index != other.Index => false,
                UnionIndexT0.T0 => EqualityComparer<T0?>.Default.Equals(this.value0, other.value0),
                _ => throw new InvalidOperationException($"Invalid index {this.Index}"),
            };
        }
        public override int GetHashCode()
        {
            return this.Index switch
            {
                UnionIndexT0.T0 => HashCode.Combine(this.Index, this.value0),
                _ => throw new InvalidOperationException($"Invalid index {this.Index}"),
            };
        }
        public override bool Equals(object? obj) => obj is Union<T0> other && Equals(other);
        public static bool operator ==(Union<T0> left, Union<T0> right) => left.Equals(right);
        public static bool operator !=(Union<T0> left, Union<T0> right) => !left.Equals(right);
        #endregion Equality
        
    }
}
