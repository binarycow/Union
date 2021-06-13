// Copyright (c) Mike Christiansen. All rights reserved.
// Licensed under the MIT license.
// See license.txt file in the project root for full license information.

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Union
{
    public class UnionClass<T0, T1, T2> : IUnion, IEquatable<UnionClass<T0, T1, T2>>
        where T0 : notnull
        where T1 : notnull
        where T2 : notnull
    {
        private UnionClass(UnionIndexT2 index, T0? value0 = default, T1? value1 = default, T2? value2 = default)
        {
            this.Index = index;
            this.value0 = default;
            this.value1 = default;
            this.value2 = default;
            switch (this.Index)
            {

                case UnionIndexT2.T0:
                    this.value0 = value0 ?? throw new ArgumentNullException(nameof(value0));
                    break;
                case UnionIndexT2.T1:
                    this.value1 = value1 ?? throw new ArgumentNullException(nameof(value1));
                    break;
                case UnionIndexT2.T2:
                    this.value2 = value2 ?? throw new ArgumentNullException(nameof(value2));
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(index));
            }
        }

        public UnionClass(T0 value) : this(UnionIndexT2.T0, value0: value) { }
        public UnionClass(T1 value) : this(UnionIndexT2.T1, value1: value) { }
        public UnionClass(T2 value) : this(UnionIndexT2.T2, value2: value) { }

        public UnionIndexT2 Index { get; }

        private readonly T0? value0; 
        private readonly T1? value1; 
        private readonly T2? value2; 
        int IUnion.Index => (int) Index;

        public object Value => Index switch
        {
            UnionIndexT2.T0 when value0 is not null => value0,
            UnionIndexT2.T1 when value1 is not null => value1,
            UnionIndexT2.T2 when value2 is not null => value2,
            _ => throw new InvalidOperationException(),
        };

        public void Switch(Action<T0> f0, Action<T1> f1, Action<T2> f2)
        {
            f0 = f0 ?? throw new ArgumentNullException(nameof(f0));
            f1 = f1 ?? throw new ArgumentNullException(nameof(f1));
            f2 = f2 ?? throw new ArgumentNullException(nameof(f2));
            switch (this.Index)
            {
                case UnionIndexT2.T0 when value0 is not null:
                    f0(this.value0);
                    return;
                case UnionIndexT2.T1 when value1 is not null:
                    f1(this.value1);
                    return;
                case UnionIndexT2.T2 when value2 is not null:
                    f2(this.value2);
                    return;
            }
        }

        public TResult Switch<TResult>(Func<T0, TResult> f0, Func<T1, TResult> f1, Func<T2, TResult> f2)
        {
            f0 = f0 ?? throw new ArgumentNullException(nameof(f0));
            f1 = f1 ?? throw new ArgumentNullException(nameof(f1));
            f2 = f2 ?? throw new ArgumentNullException(nameof(f2));
            return this.Index switch
            {
                UnionIndexT2.T0 when value0 is not null => f0(this.value0),
                UnionIndexT2.T1 when value1 is not null => f1(this.value1),
                UnionIndexT2.T2 when value2 is not null => f2(this.value2),
                _ => throw new InvalidOperationException(),
            };
        }
        
        #region As Methods 
        public T0? AsT0() 
            => this.Index == UnionIndexT2.T0 
                ? value0 
                : throw new InvalidOperationException($"Cannot return as T0 as result is T{this.Index}");
        public T1? AsT1() 
            => this.Index == UnionIndexT2.T1 
                ? value1 
                : throw new InvalidOperationException($"Cannot return as T1 as result is T{this.Index}");
        public T2? AsT2() 
            => this.Index == UnionIndexT2.T2 
                ? value2 
                : throw new InvalidOperationException($"Cannot return as T2 as result is T{this.Index}");
        #endregion As Methods
        
        #region Is Methods
        public bool IsT0([NotNullWhen(true)] out T0? value)
        {
            value = this.Index == UnionIndexT2.T0 ? value0 : default;
            return this.Index == UnionIndexT2.T0 && !(value is null);
        }
        public bool IsT1([NotNullWhen(true)] out T1? value)
        {
            value = this.Index == UnionIndexT2.T1 ? value1 : default;
            return this.Index == UnionIndexT2.T1 && !(value is null);
        }
        public bool IsT2([NotNullWhen(true)] out T2? value)
        {
            value = this.Index == UnionIndexT2.T2 ? value2 : default;
            return this.Index == UnionIndexT2.T2 && !(value is null);
        }
        #endregion Is Methods

        #region From Methods
        public static UnionClass<T0, T1, T2> FromT0(T0 t) => new (UnionIndexT2.T0, value0: t);
        public static UnionClass<T0, T1, T2> FromT1(T1 t) => new (UnionIndexT2.T1, value1: t);
        public static UnionClass<T0, T1, T2> FromT2(T2 t) => new (UnionIndexT2.T2, value2: t);
        #endregion From Methods

        #region Conversions
        public static implicit operator UnionClass<T0, T1, T2>(T0 t) => FromT0(t);
        public static implicit operator UnionClass<T0, T1, T2>(T1 t) => FromT1(t);
        public static implicit operator UnionClass<T0, T1, T2>(T2 t) => FromT2(t);
        #endregion Conversions
        
        #region Select Methods
        public Union<TResult, T1, T2> Select0<TResult>(Func<T0, TResult> selector)
            where TResult : notnull
        {
            selector = selector ?? throw new ArgumentNullException(nameof(selector));
            return this.Index switch
            {
                UnionIndexT2.T0 when this.value0 is not null => selector(this.value0),
                UnionIndexT2.T1 when this.value1 is not null => Union<TResult, T1, T2>.FromT1(this.value1),
                UnionIndexT2.T2 when this.value2 is not null => Union<TResult, T1, T2>.FromT2(this.value2),
                _ => throw new InvalidOperationException(),
            };
        }
        public Union<T0, TResult, T2> Select1<TResult>(Func<T1, TResult> selector)
            where TResult : notnull
        {
            selector = selector ?? throw new ArgumentNullException(nameof(selector));
            return this.Index switch
            {
                UnionIndexT2.T0 when this.value0 is not null => Union<T0, TResult, T2>.FromT0(this.value0),
                UnionIndexT2.T1 when this.value1 is not null => selector(this.value1),
                UnionIndexT2.T2 when this.value2 is not null => Union<T0, TResult, T2>.FromT2(this.value2),
                _ => throw new InvalidOperationException(),
            };
        }
        public Union<T0, T1, TResult> Select2<TResult>(Func<T2, TResult> selector)
            where TResult : notnull
        {
            selector = selector ?? throw new ArgumentNullException(nameof(selector));
            return this.Index switch
            {
                UnionIndexT2.T0 when this.value0 is not null => Union<T0, T1, TResult>.FromT0(this.value0),
                UnionIndexT2.T1 when this.value1 is not null => Union<T0, T1, TResult>.FromT1(this.value1),
                UnionIndexT2.T2 when this.value2 is not null => selector(this.value2),
                _ => throw new InvalidOperationException(),
            };
        }
        #endregion Select Methods
        
        public override string ToString() => this.Index switch
        {
            UnionIndexT2.T0 => $"{typeof(T0)}: {value0}",
            UnionIndexT2.T1 => $"{typeof(T1)}: {value1}",
            UnionIndexT2.T2 => $"{typeof(T2)}: {value2}",
            _ => throw new InvalidOperationException(),
        };

        #region Equality
        
        public bool Equals(UnionClass<T0, T1, T2>? other) => this.Index switch
        {
            { } when other is null => false,
            { } when this.Index != other.Index => false,
            UnionIndexT2.T0 => EqualityComparer<T0?>.Default.Equals(this.value0, other.value0),
            UnionIndexT2.T1 => EqualityComparer<T1?>.Default.Equals(this.value1, other.value1),
            UnionIndexT2.T2 => EqualityComparer<T2?>.Default.Equals(this.value2, other.value2),
            _ => throw new InvalidOperationException($"Invalid index {this.Index}"),
        };

        public override int GetHashCode() => this.Index switch
        {
            UnionIndexT2.T0 => HashCode.Combine(this.Index, this.value0),
            UnionIndexT2.T1 => HashCode.Combine(this.Index, this.value1),
            UnionIndexT2.T2 => HashCode.Combine(this.Index, this.value2),
            _ => throw new InvalidOperationException($"Invalid index {this.Index}"),
        };

        public override bool Equals(object? obj) => obj is UnionClass<T0, T1, T2> other && Equals(other);
        public static bool operator ==(UnionClass<T0, T1, T2> left, UnionClass<T0, T1, T2> right) => left.Equals(right);
        public static bool operator !=(UnionClass<T0, T1, T2> left, UnionClass<T0, T1, T2> right) => !left.Equals(right);
        #endregion Equality
    }
}

