using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Union
{
    public readonly struct Union<T0, T1, T2, T3> : IEquatable<Union<T0, T1, T2, T3>>, IUnion<T0, T1, T2, T3>
    {
        public UnionIndexT3 Index { get; }
        private readonly T0? value0;
        private readonly T1? value1;
        private readonly T2? value2;
        private readonly T3? value3;
        
        int IUnion.Index => (int) Index;
        private Union(UnionIndexT3 index, T0? value0 = default, T1? value1 = default, T2? value2 = default, T3? value3 = default)
        {
            this.Index = index;
            this.value0 = default;
            this.value1 = default;
            this.value2 = default;
            this.value3 = default;
            switch (this.Index)
            {
                case UnionIndexT3.T0:
                    this.value0 = value0 ?? throw new ArgumentNullException(nameof(value0));
                    break;
                case UnionIndexT3.T1:
                    this.value1 = value1 ?? throw new ArgumentNullException(nameof(value1));
                    break;
                case UnionIndexT3.T2:
                    this.value2 = value2 ?? throw new ArgumentNullException(nameof(value2));
                    break;
                case UnionIndexT3.T3:
                    this.value3 = value3 ?? throw new ArgumentNullException(nameof(value3));
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(index));
            }
        }
        public void Switch(Action<T0> f0, Action<T1> f1, Action<T2> f2, Action<T3> f3)
        {
            f0 = f0 ?? throw new ArgumentNullException(nameof(f0));
            f1 = f1 ?? throw new ArgumentNullException(nameof(f1));
            f2 = f2 ?? throw new ArgumentNullException(nameof(f2));
            f3 = f3 ?? throw new ArgumentNullException(nameof(f3));
            switch (this.Index)
            {
                case UnionIndexT3.T0 when value0 is not null:
                    f0(this.value0);
                    return;
                case UnionIndexT3.T1 when value1 is not null:
                    f1(this.value1);
                    return;
                case UnionIndexT3.T2 when value2 is not null:
                    f2(this.value2);
                    return;
                case UnionIndexT3.T3 when value3 is not null:
                    f3(this.value3);
                    return;
            }
        }
        
        public TResult Switch<TResult>(Func<T0, TResult> f0, Func<T1, TResult> f1, Func<T2, TResult> f2, Func<T3, TResult> f3)
        {
            f0 = f0 ?? throw new ArgumentNullException(nameof(f0));
            f1 = f1 ?? throw new ArgumentNullException(nameof(f1));
            f2 = f2 ?? throw new ArgumentNullException(nameof(f2));
            f3 = f3 ?? throw new ArgumentNullException(nameof(f3));
            return this.Index switch
            {
                UnionIndexT3.T0 when value0 is not null => f0(this.value0),
                UnionIndexT3.T1 when value1 is not null => f1(this.value1),
                UnionIndexT3.T2 when value2 is not null => f2(this.value2),
                UnionIndexT3.T3 when value3 is not null => f3(this.value3),
                _ => throw new InvalidOperationException(),
            };
        }
        
        public object Value => Index switch
        {
            UnionIndexT3.T0 when value0 is not null => value0,
            UnionIndexT3.T1 when value1 is not null => value1,
            UnionIndexT3.T2 when value2 is not null => value2,
            UnionIndexT3.T3 when value3 is not null => value3,
            _ => throw new InvalidOperationException(),
        };
        
        #region As Methods
        public T0? AsT0()
        {
            return this.Index == UnionIndexT3.T0 ? value0 : throw new InvalidOperationException($"Cannot return as T0 as result is T{this.Index}");
        }
        public T1? AsT1()
        {
            return this.Index == UnionIndexT3.T1 ? value1 : throw new InvalidOperationException($"Cannot return as T1 as result is T{this.Index}");
        }
        public T2? AsT2()
        {
            return this.Index == UnionIndexT3.T2 ? value2 : throw new InvalidOperationException($"Cannot return as T2 as result is T{this.Index}");
        }
        public T3? AsT3()
        {
            return this.Index == UnionIndexT3.T3 ? value3 : throw new InvalidOperationException($"Cannot return as T3 as result is T{this.Index}");
        }
        #endregion As Methods
        
        #region Is Methods
        public bool IsT0([NotNullWhen(true)] out T0? value)
        {
            value = this.Index == UnionIndexT3.T0 ? value0 : default;
            return this.Index == UnionIndexT3.T0 && !(value is null);
        }
        public bool IsT1([NotNullWhen(true)] out T1? value)
        {
            value = this.Index == UnionIndexT3.T1 ? value1 : default;
            return this.Index == UnionIndexT3.T1 && !(value is null);
        }
        public bool IsT2([NotNullWhen(true)] out T2? value)
        {
            value = this.Index == UnionIndexT3.T2 ? value2 : default;
            return this.Index == UnionIndexT3.T2 && !(value is null);
        }
        public bool IsT3([NotNullWhen(true)] out T3? value)
        {
            value = this.Index == UnionIndexT3.T3 ? value3 : default;
            return this.Index == UnionIndexT3.T3 && !(value is null);
        }
        #endregion Is Methods
        
        #region Select Methods
        public Union<TResult, T1, T2, T3> Select0<TResult>(Func<T0, TResult> selector)
        {
            selector = selector ?? throw new ArgumentNullException(nameof(selector));
            return this.Index switch
            {
                UnionIndexT3.T0 when this.value0 is not null => selector(this.value0),
                UnionIndexT3.T1 when this.value1 is not null => Union<TResult, T1, T2, T3>.FromT1(this.value1),
                UnionIndexT3.T2 when this.value2 is not null => Union<TResult, T1, T2, T3>.FromT2(this.value2),
                UnionIndexT3.T3 when this.value3 is not null => Union<TResult, T1, T2, T3>.FromT3(this.value3),
                _ => throw new InvalidOperationException(),
            };
        }
        public Union<T0, TResult, T2, T3> Select1<TResult>(Func<T1, TResult> selector)
        {
            selector = selector ?? throw new ArgumentNullException(nameof(selector));
            return this.Index switch
            {
                UnionIndexT3.T0 when this.value0 is not null => Union<T0, TResult, T2, T3>.FromT0(this.value0),
                UnionIndexT3.T1 when this.value1 is not null => selector(this.value1),
                UnionIndexT3.T2 when this.value2 is not null => Union<T0, TResult, T2, T3>.FromT2(this.value2),
                UnionIndexT3.T3 when this.value3 is not null => Union<T0, TResult, T2, T3>.FromT3(this.value3),
                _ => throw new InvalidOperationException(),
            };
        }
        public Union<T0, T1, TResult, T3> Select2<TResult>(Func<T2, TResult> selector)
        {
            selector = selector ?? throw new ArgumentNullException(nameof(selector));
            return this.Index switch
            {
                UnionIndexT3.T0 when this.value0 is not null => Union<T0, T1, TResult, T3>.FromT0(this.value0),
                UnionIndexT3.T1 when this.value1 is not null => Union<T0, T1, TResult, T3>.FromT1(this.value1),
                UnionIndexT3.T2 when this.value2 is not null => selector(this.value2),
                UnionIndexT3.T3 when this.value3 is not null => Union<T0, T1, TResult, T3>.FromT3(this.value3),
                _ => throw new InvalidOperationException(),
            };
        }
        public Union<T0, T1, T2, TResult> Select3<TResult>(Func<T3, TResult> selector)
        {
            selector = selector ?? throw new ArgumentNullException(nameof(selector));
            return this.Index switch
            {
                UnionIndexT3.T0 when this.value0 is not null => Union<T0, T1, T2, TResult>.FromT0(this.value0),
                UnionIndexT3.T1 when this.value1 is not null => Union<T0, T1, T2, TResult>.FromT1(this.value1),
                UnionIndexT3.T2 when this.value2 is not null => Union<T0, T1, T2, TResult>.FromT2(this.value2),
                UnionIndexT3.T3 when this.value3 is not null => selector(this.value3),
                _ => throw new InvalidOperationException(),
            };
        }
        #endregion Select Methods
        
        public override string ToString() => this.Index switch
        {
            UnionIndexT3.T0 => $"{typeof(T0)}: {value0}",
            UnionIndexT3.T1 => $"{typeof(T1)}: {value1}",
            UnionIndexT3.T2 => $"{typeof(T2)}: {value2}",
            UnionIndexT3.T3 => $"{typeof(T3)}: {value3}",
            _ => throw new InvalidOperationException(),
        };
        #region Conversion
        public static implicit operator Union<T0, T1, T2, T3>(T0 t) => FromT0(t);
        public static implicit operator Union<T0, T1, T2, T3>(T1 t) => FromT1(t);
        public static implicit operator Union<T0, T1, T2, T3>(T2 t) => FromT2(t);
        public static implicit operator Union<T0, T1, T2, T3>(T3 t) => FromT3(t);
        #endregion Conversion
        
        #region Static factory methods
        public static Union<T0, T1, T2, T3> FromT0(T0 t) => new (UnionIndexT3.T0, value0: t);
        public static Union<T0, T1, T2, T3> FromT1(T1 t) => new (UnionIndexT3.T1, value1: t);
        public static Union<T0, T1, T2, T3> FromT2(T2 t) => new (UnionIndexT3.T2, value2: t);
        public static Union<T0, T1, T2, T3> FromT3(T3 t) => new (UnionIndexT3.T3, value3: t);
        #endregion Static factory methods
        
        #region Equality
        public bool Equals(Union<T0, T1, T2, T3> other)
        {
            return this.Index switch
            {
                { } when this.Index != other.Index => false,
                UnionIndexT3.T0 => EqualityComparer<T0?>.Default.Equals(this.value0, other.value0),
                UnionIndexT3.T1 => EqualityComparer<T1?>.Default.Equals(this.value1, other.value1),
                UnionIndexT3.T2 => EqualityComparer<T2?>.Default.Equals(this.value2, other.value2),
                UnionIndexT3.T3 => EqualityComparer<T3?>.Default.Equals(this.value3, other.value3),
                _ => throw new InvalidOperationException($"Invalid index {this.Index}"),
            };
        }
        public override int GetHashCode()
        {
            return this.Index switch
            {
                UnionIndexT3.T0 => HashCode.Combine(this.Index, this.value0),
                UnionIndexT3.T1 => HashCode.Combine(this.Index, this.value1),
                UnionIndexT3.T2 => HashCode.Combine(this.Index, this.value2),
                UnionIndexT3.T3 => HashCode.Combine(this.Index, this.value3),
                _ => throw new InvalidOperationException($"Invalid index {this.Index}"),
            };
        }
        public override bool Equals(object? obj) => obj is Union<T0, T1, T2, T3> other && Equals(other);
        public static bool operator ==(Union<T0, T1, T2, T3> left, Union<T0, T1, T2, T3> right) => left.Equals(right);
        public static bool operator !=(Union<T0, T1, T2, T3> left, Union<T0, T1, T2, T3> right) => !left.Equals(right);
        #endregion Equality
        
    }
}
