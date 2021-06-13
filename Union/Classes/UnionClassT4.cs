// Copyright (c) Mike Christiansen. All rights reserved.
// Licensed under the MIT license.
// See license.txt file in the project root for full license information.

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Union
{
    public class UnionClass<T0, T1, T2, T3, T4> : IUnion, IEquatable<UnionClass<T0, T1, T2, T3, T4>>
        where T0 : notnull
        where T1 : notnull
        where T2 : notnull
        where T3 : notnull
        where T4 : notnull
    {
        private UnionClass(UnionIndexT4 index, T0? value0 = default, T1? value1 = default, T2? value2 = default, T3? value3 = default, T4? value4 = default)
        {
            this.Index = index;
            this.value0 = default;
            this.value1 = default;
            this.value2 = default;
            this.value3 = default;
            this.value4 = default;
            switch (this.Index)
            {

                case UnionIndexT4.T0:
                    this.value0 = value0 ?? throw new ArgumentNullException(nameof(value0));
                    break;
                case UnionIndexT4.T1:
                    this.value1 = value1 ?? throw new ArgumentNullException(nameof(value1));
                    break;
                case UnionIndexT4.T2:
                    this.value2 = value2 ?? throw new ArgumentNullException(nameof(value2));
                    break;
                case UnionIndexT4.T3:
                    this.value3 = value3 ?? throw new ArgumentNullException(nameof(value3));
                    break;
                case UnionIndexT4.T4:
                    this.value4 = value4 ?? throw new ArgumentNullException(nameof(value4));
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(index));
            }
        }

        public UnionClass(T0 value) : this(UnionIndexT4.T0, value0: value) { }
        public UnionClass(T1 value) : this(UnionIndexT4.T1, value1: value) { }
        public UnionClass(T2 value) : this(UnionIndexT4.T2, value2: value) { }
        public UnionClass(T3 value) : this(UnionIndexT4.T3, value3: value) { }
        public UnionClass(T4 value) : this(UnionIndexT4.T4, value4: value) { }

        public UnionIndexT4 Index { get; }

        private readonly T0? value0; 
        private readonly T1? value1; 
        private readonly T2? value2; 
        private readonly T3? value3; 
        private readonly T4? value4; 
        int IUnion.Index => (int) Index;

        public object Value => Index switch
        {
            UnionIndexT4.T0 when value0 is not null => value0,
            UnionIndexT4.T1 when value1 is not null => value1,
            UnionIndexT4.T2 when value2 is not null => value2,
            UnionIndexT4.T3 when value3 is not null => value3,
            UnionIndexT4.T4 when value4 is not null => value4,
            _ => throw new InvalidOperationException(),
        };

        public void Switch(Action<T0> f0, Action<T1> f1, Action<T2> f2, Action<T3> f3, Action<T4> f4)
        {
            f0 = f0 ?? throw new ArgumentNullException(nameof(f0));
            f1 = f1 ?? throw new ArgumentNullException(nameof(f1));
            f2 = f2 ?? throw new ArgumentNullException(nameof(f2));
            f3 = f3 ?? throw new ArgumentNullException(nameof(f3));
            f4 = f4 ?? throw new ArgumentNullException(nameof(f4));
            switch (this.Index)
            {
                case UnionIndexT4.T0 when value0 is not null:
                    f0(this.value0);
                    return;
                case UnionIndexT4.T1 when value1 is not null:
                    f1(this.value1);
                    return;
                case UnionIndexT4.T2 when value2 is not null:
                    f2(this.value2);
                    return;
                case UnionIndexT4.T3 when value3 is not null:
                    f3(this.value3);
                    return;
                case UnionIndexT4.T4 when value4 is not null:
                    f4(this.value4);
                    return;
            }
        }

        public TResult Switch<TResult>(Func<T0, TResult> f0, Func<T1, TResult> f1, Func<T2, TResult> f2, Func<T3, TResult> f3, Func<T4, TResult> f4)
        {
            f0 = f0 ?? throw new ArgumentNullException(nameof(f0));
            f1 = f1 ?? throw new ArgumentNullException(nameof(f1));
            f2 = f2 ?? throw new ArgumentNullException(nameof(f2));
            f3 = f3 ?? throw new ArgumentNullException(nameof(f3));
            f4 = f4 ?? throw new ArgumentNullException(nameof(f4));
            return this.Index switch
            {
                UnionIndexT4.T0 when value0 is not null => f0(this.value0),
                UnionIndexT4.T1 when value1 is not null => f1(this.value1),
                UnionIndexT4.T2 when value2 is not null => f2(this.value2),
                UnionIndexT4.T3 when value3 is not null => f3(this.value3),
                UnionIndexT4.T4 when value4 is not null => f4(this.value4),
                _ => throw new InvalidOperationException(),
            };
        }
        
        #region As Methods 
        public T0? AsT0() 
            => this.Index == UnionIndexT4.T0 
                ? value0 
                : throw new InvalidOperationException($"Cannot return as T0 as result is T{this.Index}");
        public T1? AsT1() 
            => this.Index == UnionIndexT4.T1 
                ? value1 
                : throw new InvalidOperationException($"Cannot return as T1 as result is T{this.Index}");
        public T2? AsT2() 
            => this.Index == UnionIndexT4.T2 
                ? value2 
                : throw new InvalidOperationException($"Cannot return as T2 as result is T{this.Index}");
        public T3? AsT3() 
            => this.Index == UnionIndexT4.T3 
                ? value3 
                : throw new InvalidOperationException($"Cannot return as T3 as result is T{this.Index}");
        public T4? AsT4() 
            => this.Index == UnionIndexT4.T4 
                ? value4 
                : throw new InvalidOperationException($"Cannot return as T4 as result is T{this.Index}");
        #endregion As Methods
        
        #region Is Methods
        public bool IsT0([NotNullWhen(true)] out T0? value)
        {
            value = this.Index == UnionIndexT4.T0 ? value0 : default;
            return this.Index == UnionIndexT4.T0 && !(value is null);
        }
        public bool IsT1([NotNullWhen(true)] out T1? value)
        {
            value = this.Index == UnionIndexT4.T1 ? value1 : default;
            return this.Index == UnionIndexT4.T1 && !(value is null);
        }
        public bool IsT2([NotNullWhen(true)] out T2? value)
        {
            value = this.Index == UnionIndexT4.T2 ? value2 : default;
            return this.Index == UnionIndexT4.T2 && !(value is null);
        }
        public bool IsT3([NotNullWhen(true)] out T3? value)
        {
            value = this.Index == UnionIndexT4.T3 ? value3 : default;
            return this.Index == UnionIndexT4.T3 && !(value is null);
        }
        public bool IsT4([NotNullWhen(true)] out T4? value)
        {
            value = this.Index == UnionIndexT4.T4 ? value4 : default;
            return this.Index == UnionIndexT4.T4 && !(value is null);
        }
        #endregion Is Methods

        #region From Methods
        public static UnionClass<T0, T1, T2, T3, T4> FromT0(T0 t) => new (UnionIndexT4.T0, value0: t);
        public static UnionClass<T0, T1, T2, T3, T4> FromT1(T1 t) => new (UnionIndexT4.T1, value1: t);
        public static UnionClass<T0, T1, T2, T3, T4> FromT2(T2 t) => new (UnionIndexT4.T2, value2: t);
        public static UnionClass<T0, T1, T2, T3, T4> FromT3(T3 t) => new (UnionIndexT4.T3, value3: t);
        public static UnionClass<T0, T1, T2, T3, T4> FromT4(T4 t) => new (UnionIndexT4.T4, value4: t);
        #endregion From Methods

        #region Conversions
        public static implicit operator UnionClass<T0, T1, T2, T3, T4>(T0 t) => FromT0(t);
        public static implicit operator UnionClass<T0, T1, T2, T3, T4>(T1 t) => FromT1(t);
        public static implicit operator UnionClass<T0, T1, T2, T3, T4>(T2 t) => FromT2(t);
        public static implicit operator UnionClass<T0, T1, T2, T3, T4>(T3 t) => FromT3(t);
        public static implicit operator UnionClass<T0, T1, T2, T3, T4>(T4 t) => FromT4(t);
        #endregion Conversions
        
        #region Select Methods
        public Union<TResult, T1, T2, T3, T4> Select0<TResult>(Func<T0, TResult> selector)
            where TResult : notnull
        {
            selector = selector ?? throw new ArgumentNullException(nameof(selector));
            return this.Index switch
            {
                UnionIndexT4.T0 when this.value0 is not null => selector(this.value0),
                UnionIndexT4.T1 when this.value1 is not null => Union<TResult, T1, T2, T3, T4>.FromT1(this.value1),
                UnionIndexT4.T2 when this.value2 is not null => Union<TResult, T1, T2, T3, T4>.FromT2(this.value2),
                UnionIndexT4.T3 when this.value3 is not null => Union<TResult, T1, T2, T3, T4>.FromT3(this.value3),
                UnionIndexT4.T4 when this.value4 is not null => Union<TResult, T1, T2, T3, T4>.FromT4(this.value4),
                _ => throw new InvalidOperationException(),
            };
        }
        public Union<T0, TResult, T2, T3, T4> Select1<TResult>(Func<T1, TResult> selector)
            where TResult : notnull
        {
            selector = selector ?? throw new ArgumentNullException(nameof(selector));
            return this.Index switch
            {
                UnionIndexT4.T0 when this.value0 is not null => Union<T0, TResult, T2, T3, T4>.FromT0(this.value0),
                UnionIndexT4.T1 when this.value1 is not null => selector(this.value1),
                UnionIndexT4.T2 when this.value2 is not null => Union<T0, TResult, T2, T3, T4>.FromT2(this.value2),
                UnionIndexT4.T3 when this.value3 is not null => Union<T0, TResult, T2, T3, T4>.FromT3(this.value3),
                UnionIndexT4.T4 when this.value4 is not null => Union<T0, TResult, T2, T3, T4>.FromT4(this.value4),
                _ => throw new InvalidOperationException(),
            };
        }
        public Union<T0, T1, TResult, T3, T4> Select2<TResult>(Func<T2, TResult> selector)
            where TResult : notnull
        {
            selector = selector ?? throw new ArgumentNullException(nameof(selector));
            return this.Index switch
            {
                UnionIndexT4.T0 when this.value0 is not null => Union<T0, T1, TResult, T3, T4>.FromT0(this.value0),
                UnionIndexT4.T1 when this.value1 is not null => Union<T0, T1, TResult, T3, T4>.FromT1(this.value1),
                UnionIndexT4.T2 when this.value2 is not null => selector(this.value2),
                UnionIndexT4.T3 when this.value3 is not null => Union<T0, T1, TResult, T3, T4>.FromT3(this.value3),
                UnionIndexT4.T4 when this.value4 is not null => Union<T0, T1, TResult, T3, T4>.FromT4(this.value4),
                _ => throw new InvalidOperationException(),
            };
        }
        public Union<T0, T1, T2, TResult, T4> Select3<TResult>(Func<T3, TResult> selector)
            where TResult : notnull
        {
            selector = selector ?? throw new ArgumentNullException(nameof(selector));
            return this.Index switch
            {
                UnionIndexT4.T0 when this.value0 is not null => Union<T0, T1, T2, TResult, T4>.FromT0(this.value0),
                UnionIndexT4.T1 when this.value1 is not null => Union<T0, T1, T2, TResult, T4>.FromT1(this.value1),
                UnionIndexT4.T2 when this.value2 is not null => Union<T0, T1, T2, TResult, T4>.FromT2(this.value2),
                UnionIndexT4.T3 when this.value3 is not null => selector(this.value3),
                UnionIndexT4.T4 when this.value4 is not null => Union<T0, T1, T2, TResult, T4>.FromT4(this.value4),
                _ => throw new InvalidOperationException(),
            };
        }
        public Union<T0, T1, T2, T3, TResult> Select4<TResult>(Func<T4, TResult> selector)
            where TResult : notnull
        {
            selector = selector ?? throw new ArgumentNullException(nameof(selector));
            return this.Index switch
            {
                UnionIndexT4.T0 when this.value0 is not null => Union<T0, T1, T2, T3, TResult>.FromT0(this.value0),
                UnionIndexT4.T1 when this.value1 is not null => Union<T0, T1, T2, T3, TResult>.FromT1(this.value1),
                UnionIndexT4.T2 when this.value2 is not null => Union<T0, T1, T2, T3, TResult>.FromT2(this.value2),
                UnionIndexT4.T3 when this.value3 is not null => Union<T0, T1, T2, T3, TResult>.FromT3(this.value3),
                UnionIndexT4.T4 when this.value4 is not null => selector(this.value4),
                _ => throw new InvalidOperationException(),
            };
        }
        #endregion Select Methods
        
        public override string ToString() => this.Index switch
        {
            UnionIndexT4.T0 => $"{typeof(T0)}: {value0}",
            UnionIndexT4.T1 => $"{typeof(T1)}: {value1}",
            UnionIndexT4.T2 => $"{typeof(T2)}: {value2}",
            UnionIndexT4.T3 => $"{typeof(T3)}: {value3}",
            UnionIndexT4.T4 => $"{typeof(T4)}: {value4}",
            _ => throw new InvalidOperationException(),
        };

        #region Equality
        
        public bool Equals(UnionClass<T0, T1, T2, T3, T4>? other) => this.Index switch
        {
            { } when other is null => false,
            { } when this.Index != other.Index => false,
            UnionIndexT4.T0 => EqualityComparer<T0?>.Default.Equals(this.value0, other.value0),
            UnionIndexT4.T1 => EqualityComparer<T1?>.Default.Equals(this.value1, other.value1),
            UnionIndexT4.T2 => EqualityComparer<T2?>.Default.Equals(this.value2, other.value2),
            UnionIndexT4.T3 => EqualityComparer<T3?>.Default.Equals(this.value3, other.value3),
            UnionIndexT4.T4 => EqualityComparer<T4?>.Default.Equals(this.value4, other.value4),
            _ => throw new InvalidOperationException($"Invalid index {this.Index}"),
        };

        public override int GetHashCode() => this.Index switch
        {
            UnionIndexT4.T0 => HashCode.Combine(this.Index, this.value0),
            UnionIndexT4.T1 => HashCode.Combine(this.Index, this.value1),
            UnionIndexT4.T2 => HashCode.Combine(this.Index, this.value2),
            UnionIndexT4.T3 => HashCode.Combine(this.Index, this.value3),
            UnionIndexT4.T4 => HashCode.Combine(this.Index, this.value4),
            _ => throw new InvalidOperationException($"Invalid index {this.Index}"),
        };

        public override bool Equals(object? obj) => obj is UnionClass<T0, T1, T2, T3, T4> other && Equals(other);
        public static bool operator ==(UnionClass<T0, T1, T2, T3, T4> left, UnionClass<T0, T1, T2, T3, T4> right) => left.Equals(right);
        public static bool operator !=(UnionClass<T0, T1, T2, T3, T4> left, UnionClass<T0, T1, T2, T3, T4> right) => !left.Equals(right);
        #endregion Equality
    }
}

