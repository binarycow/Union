using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Union
{
    public readonly struct Union<T0, T1> : IEquatable<Union<T0, T1>>, IUnion<T0, T1>
    {
        public UnionIndexT1 Index { get; }
        private readonly T0? value0;
        private readonly T1? value1;
        
        int IUnion.Index => (int) Index;
        private Union(UnionIndexT1 index, T0? value0 = default, T1? value1 = default)
        {
            this.Index = index;
            this.value0 = default;
            this.value1 = default;
            switch (this.Index)
            {
                case UnionIndexT1.T0:
                    this.value0 = value0 ?? throw new ArgumentNullException(nameof(value0));
                    break;
                case UnionIndexT1.T1:
                    this.value1 = value1 ?? throw new ArgumentNullException(nameof(value1));
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(index));
            }
        }
        public void Switch(Action<T0> f0, Action<T1> f1)
        {
            f0 = f0 ?? throw new ArgumentNullException(nameof(f0));
            f1 = f1 ?? throw new ArgumentNullException(nameof(f1));
            switch (this.Index)
            {
                case UnionIndexT1.T0 when value0 is not null:
                    f0(this.value0);
                    return;
                case UnionIndexT1.T1 when value1 is not null:
                    f1(this.value1);
                    return;
            }
        }
        
        public TResult Switch<TResult>(Func<T0, TResult> f0, Func<T1, TResult> f1)
        {
            f0 = f0 ?? throw new ArgumentNullException(nameof(f0));
            f1 = f1 ?? throw new ArgumentNullException(nameof(f1));
            return this.Index switch
            {
                UnionIndexT1.T0 when value0 is not null => f0(this.value0),
                UnionIndexT1.T1 when value1 is not null => f1(this.value1),
                _ => throw new InvalidOperationException(),
            };
        }
        
        public object Value => Index switch
        {
            UnionIndexT1.T0 when value0 is not null => value0,
            UnionIndexT1.T1 when value1 is not null => value1,
            _ => throw new InvalidOperationException(),
        };
        
        #region As Methods
        public T0? AsT0()
        {
            return this.Index == UnionIndexT1.T0 ? value0 : throw new InvalidOperationException($"Cannot return as T0 as result is T{this.Index}");
        }
        public T1? AsT1()
        {
            return this.Index == UnionIndexT1.T1 ? value1 : throw new InvalidOperationException($"Cannot return as T1 as result is T{this.Index}");
        }
        #endregion As Methods
        
        #region Is Methods
        public bool IsT0([NotNullWhen(true)] out T0? value)
        {
            value = this.Index == UnionIndexT1.T0 ? value0 : default;
            return this.Index == UnionIndexT1.T0 && !(value is null);
        }
        public bool IsT1([NotNullWhen(true)] out T1? value)
        {
            value = this.Index == UnionIndexT1.T1 ? value1 : default;
            return this.Index == UnionIndexT1.T1 && !(value is null);
        }
        #endregion Is Methods
        
        #region Select Methods
        public Union<TResult, T1> Select0<TResult>(Func<T0, TResult> selector)
        {
            selector = selector ?? throw new ArgumentNullException(nameof(selector));
            return this.Index switch
            {
                UnionIndexT1.T0 when this.value0 is not null => selector(this.value0),
                UnionIndexT1.T1 when this.value1 is not null => Union<TResult, T1>.FromT1(this.value1),
                _ => throw new InvalidOperationException(),
            };
        }
        public Union<T0, TResult> Select1<TResult>(Func<T1, TResult> selector)
        {
            selector = selector ?? throw new ArgumentNullException(nameof(selector));
            return this.Index switch
            {
                UnionIndexT1.T0 when this.value0 is not null => Union<T0, TResult>.FromT0(this.value0),
                UnionIndexT1.T1 when this.value1 is not null => selector(this.value1),
                _ => throw new InvalidOperationException(),
            };
        }
        #endregion Select Methods
        
        public override string ToString() => this.Index switch
        {
            UnionIndexT1.T0 => $"{typeof(T0)}: {value0}",
            UnionIndexT1.T1 => $"{typeof(T1)}: {value1}",
            _ => throw new InvalidOperationException(),
        };
        #region Conversion
        public static implicit operator Union<T0, T1>(T0 t) => FromT0(t);
        public static implicit operator Union<T0, T1>(T1 t) => FromT1(t);
        #endregion Conversion
        
        #region Static factory methods
        public static Union<T0, T1> FromT0(T0 t) => new (UnionIndexT1.T0, value0: t);
        public static Union<T0, T1> FromT1(T1 t) => new (UnionIndexT1.T1, value1: t);
        #endregion Static factory methods
        
        #region Equality
        public bool Equals(Union<T0, T1> other)
        {
            return this.Index switch
            {
                { } when this.Index != other.Index => false,
                UnionIndexT1.T0 => EqualityComparer<T0?>.Default.Equals(this.value0, other.value0),
                UnionIndexT1.T1 => EqualityComparer<T1?>.Default.Equals(this.value1, other.value1),
                _ => throw new InvalidOperationException($"Invalid index {this.Index}"),
            };
        }
        public override int GetHashCode()
        {
            return this.Index switch
            {
                UnionIndexT1.T0 => HashCode.Combine(this.Index, this.value0),
                UnionIndexT1.T1 => HashCode.Combine(this.Index, this.value1),
                _ => throw new InvalidOperationException($"Invalid index {this.Index}"),
            };
        }
        public override bool Equals(object? obj) => obj is Union<T0, T1> other && Equals(other);
        public static bool operator ==(Union<T0, T1> left, Union<T0, T1> right) => left.Equals(right);
        public static bool operator !=(Union<T0, T1> left, Union<T0, T1> right) => !left.Equals(right);
        #endregion Equality
        
    }
}
