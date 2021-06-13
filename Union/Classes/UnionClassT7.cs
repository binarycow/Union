// Copyright (c) Mike Christiansen. All rights reserved.
// Licensed under the MIT license.
// See license.txt file in the project root for full license information.

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Union
{
    public class UnionClass<T0, T1, T2, T3, T4, T5, T6, T7> : IUnion, IEquatable<UnionClass<T0, T1, T2, T3, T4, T5, T6, T7>>
        where T0 : notnull
        where T1 : notnull
        where T2 : notnull
        where T3 : notnull
        where T4 : notnull
        where T5 : notnull
        where T6 : notnull
        where T7 : notnull
    {
        private UnionClass(UnionIndexT7 index, T0? value0 = default, T1? value1 = default, T2? value2 = default, T3? value3 = default, T4? value4 = default, T5? value5 = default, T6? value6 = default, T7? value7 = default)
        {
            this.Index = index;
            this.value0 = default;
            this.value1 = default;
            this.value2 = default;
            this.value3 = default;
            this.value4 = default;
            this.value5 = default;
            this.value6 = default;
            this.value7 = default;
            switch (this.Index)
            {

                case UnionIndexT7.T0:
                    this.value0 = value0 ?? throw new ArgumentNullException(nameof(value0));
                    break;
                case UnionIndexT7.T1:
                    this.value1 = value1 ?? throw new ArgumentNullException(nameof(value1));
                    break;
                case UnionIndexT7.T2:
                    this.value2 = value2 ?? throw new ArgumentNullException(nameof(value2));
                    break;
                case UnionIndexT7.T3:
                    this.value3 = value3 ?? throw new ArgumentNullException(nameof(value3));
                    break;
                case UnionIndexT7.T4:
                    this.value4 = value4 ?? throw new ArgumentNullException(nameof(value4));
                    break;
                case UnionIndexT7.T5:
                    this.value5 = value5 ?? throw new ArgumentNullException(nameof(value5));
                    break;
                case UnionIndexT7.T6:
                    this.value6 = value6 ?? throw new ArgumentNullException(nameof(value6));
                    break;
                case UnionIndexT7.T7:
                    this.value7 = value7 ?? throw new ArgumentNullException(nameof(value7));
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(index));
            }
        }

        public UnionClass(T0 value) : this(UnionIndexT7.T0, value0: value) { }
        public UnionClass(T1 value) : this(UnionIndexT7.T1, value1: value) { }
        public UnionClass(T2 value) : this(UnionIndexT7.T2, value2: value) { }
        public UnionClass(T3 value) : this(UnionIndexT7.T3, value3: value) { }
        public UnionClass(T4 value) : this(UnionIndexT7.T4, value4: value) { }
        public UnionClass(T5 value) : this(UnionIndexT7.T5, value5: value) { }
        public UnionClass(T6 value) : this(UnionIndexT7.T6, value6: value) { }
        public UnionClass(T7 value) : this(UnionIndexT7.T7, value7: value) { }

        public UnionIndexT7 Index { get; }

        private readonly T0? value0; 
        private readonly T1? value1; 
        private readonly T2? value2; 
        private readonly T3? value3; 
        private readonly T4? value4; 
        private readonly T5? value5; 
        private readonly T6? value6; 
        private readonly T7? value7; 
        int IUnion.Index => (int) Index;

        public object Value => Index switch
        {
            UnionIndexT7.T0 when value0 is not null => value0,
            UnionIndexT7.T1 when value1 is not null => value1,
            UnionIndexT7.T2 when value2 is not null => value2,
            UnionIndexT7.T3 when value3 is not null => value3,
            UnionIndexT7.T4 when value4 is not null => value4,
            UnionIndexT7.T5 when value5 is not null => value5,
            UnionIndexT7.T6 when value6 is not null => value6,
            UnionIndexT7.T7 when value7 is not null => value7,
            _ => throw new InvalidOperationException(),
        };

        public void Switch(Action<T0> f0, Action<T1> f1, Action<T2> f2, Action<T3> f3, Action<T4> f4, Action<T5> f5, Action<T6> f6, Action<T7> f7)
        {
            f0 = f0 ?? throw new ArgumentNullException(nameof(f0));
            f1 = f1 ?? throw new ArgumentNullException(nameof(f1));
            f2 = f2 ?? throw new ArgumentNullException(nameof(f2));
            f3 = f3 ?? throw new ArgumentNullException(nameof(f3));
            f4 = f4 ?? throw new ArgumentNullException(nameof(f4));
            f5 = f5 ?? throw new ArgumentNullException(nameof(f5));
            f6 = f6 ?? throw new ArgumentNullException(nameof(f6));
            f7 = f7 ?? throw new ArgumentNullException(nameof(f7));
            switch (this.Index)
            {
                case UnionIndexT7.T0 when value0 is not null:
                    f0(this.value0);
                    return;
                case UnionIndexT7.T1 when value1 is not null:
                    f1(this.value1);
                    return;
                case UnionIndexT7.T2 when value2 is not null:
                    f2(this.value2);
                    return;
                case UnionIndexT7.T3 when value3 is not null:
                    f3(this.value3);
                    return;
                case UnionIndexT7.T4 when value4 is not null:
                    f4(this.value4);
                    return;
                case UnionIndexT7.T5 when value5 is not null:
                    f5(this.value5);
                    return;
                case UnionIndexT7.T6 when value6 is not null:
                    f6(this.value6);
                    return;
                case UnionIndexT7.T7 when value7 is not null:
                    f7(this.value7);
                    return;
            }
        }

        public TResult Switch<TResult>(Func<T0, TResult> f0, Func<T1, TResult> f1, Func<T2, TResult> f2, Func<T3, TResult> f3, Func<T4, TResult> f4, Func<T5, TResult> f5, Func<T6, TResult> f6, Func<T7, TResult> f7)
        {
            f0 = f0 ?? throw new ArgumentNullException(nameof(f0));
            f1 = f1 ?? throw new ArgumentNullException(nameof(f1));
            f2 = f2 ?? throw new ArgumentNullException(nameof(f2));
            f3 = f3 ?? throw new ArgumentNullException(nameof(f3));
            f4 = f4 ?? throw new ArgumentNullException(nameof(f4));
            f5 = f5 ?? throw new ArgumentNullException(nameof(f5));
            f6 = f6 ?? throw new ArgumentNullException(nameof(f6));
            f7 = f7 ?? throw new ArgumentNullException(nameof(f7));
            return this.Index switch
            {
                UnionIndexT7.T0 when value0 is not null => f0(this.value0),
                UnionIndexT7.T1 when value1 is not null => f1(this.value1),
                UnionIndexT7.T2 when value2 is not null => f2(this.value2),
                UnionIndexT7.T3 when value3 is not null => f3(this.value3),
                UnionIndexT7.T4 when value4 is not null => f4(this.value4),
                UnionIndexT7.T5 when value5 is not null => f5(this.value5),
                UnionIndexT7.T6 when value6 is not null => f6(this.value6),
                UnionIndexT7.T7 when value7 is not null => f7(this.value7),
                _ => throw new InvalidOperationException(),
            };
        }
        
        #region As Methods 
        public T0? AsT0() 
            => this.Index == UnionIndexT7.T0 
                ? value0 
                : throw new InvalidOperationException($"Cannot return as T0 as result is T{this.Index}");
        public T1? AsT1() 
            => this.Index == UnionIndexT7.T1 
                ? value1 
                : throw new InvalidOperationException($"Cannot return as T1 as result is T{this.Index}");
        public T2? AsT2() 
            => this.Index == UnionIndexT7.T2 
                ? value2 
                : throw new InvalidOperationException($"Cannot return as T2 as result is T{this.Index}");
        public T3? AsT3() 
            => this.Index == UnionIndexT7.T3 
                ? value3 
                : throw new InvalidOperationException($"Cannot return as T3 as result is T{this.Index}");
        public T4? AsT4() 
            => this.Index == UnionIndexT7.T4 
                ? value4 
                : throw new InvalidOperationException($"Cannot return as T4 as result is T{this.Index}");
        public T5? AsT5() 
            => this.Index == UnionIndexT7.T5 
                ? value5 
                : throw new InvalidOperationException($"Cannot return as T5 as result is T{this.Index}");
        public T6? AsT6() 
            => this.Index == UnionIndexT7.T6 
                ? value6 
                : throw new InvalidOperationException($"Cannot return as T6 as result is T{this.Index}");
        public T7? AsT7() 
            => this.Index == UnionIndexT7.T7 
                ? value7 
                : throw new InvalidOperationException($"Cannot return as T7 as result is T{this.Index}");
        #endregion As Methods
        
        #region Is Methods
        public bool IsT0([NotNullWhen(true)] out T0? value)
        {
            value = this.Index == UnionIndexT7.T0 ? value0 : default;
            return this.Index == UnionIndexT7.T0 && !(value is null);
        }
        public bool IsT1([NotNullWhen(true)] out T1? value)
        {
            value = this.Index == UnionIndexT7.T1 ? value1 : default;
            return this.Index == UnionIndexT7.T1 && !(value is null);
        }
        public bool IsT2([NotNullWhen(true)] out T2? value)
        {
            value = this.Index == UnionIndexT7.T2 ? value2 : default;
            return this.Index == UnionIndexT7.T2 && !(value is null);
        }
        public bool IsT3([NotNullWhen(true)] out T3? value)
        {
            value = this.Index == UnionIndexT7.T3 ? value3 : default;
            return this.Index == UnionIndexT7.T3 && !(value is null);
        }
        public bool IsT4([NotNullWhen(true)] out T4? value)
        {
            value = this.Index == UnionIndexT7.T4 ? value4 : default;
            return this.Index == UnionIndexT7.T4 && !(value is null);
        }
        public bool IsT5([NotNullWhen(true)] out T5? value)
        {
            value = this.Index == UnionIndexT7.T5 ? value5 : default;
            return this.Index == UnionIndexT7.T5 && !(value is null);
        }
        public bool IsT6([NotNullWhen(true)] out T6? value)
        {
            value = this.Index == UnionIndexT7.T6 ? value6 : default;
            return this.Index == UnionIndexT7.T6 && !(value is null);
        }
        public bool IsT7([NotNullWhen(true)] out T7? value)
        {
            value = this.Index == UnionIndexT7.T7 ? value7 : default;
            return this.Index == UnionIndexT7.T7 && !(value is null);
        }
        #endregion Is Methods

        #region From Methods
        public static UnionClass<T0, T1, T2, T3, T4, T5, T6, T7> FromT0(T0 t) => new (UnionIndexT7.T0, value0: t);
        public static UnionClass<T0, T1, T2, T3, T4, T5, T6, T7> FromT1(T1 t) => new (UnionIndexT7.T1, value1: t);
        public static UnionClass<T0, T1, T2, T3, T4, T5, T6, T7> FromT2(T2 t) => new (UnionIndexT7.T2, value2: t);
        public static UnionClass<T0, T1, T2, T3, T4, T5, T6, T7> FromT3(T3 t) => new (UnionIndexT7.T3, value3: t);
        public static UnionClass<T0, T1, T2, T3, T4, T5, T6, T7> FromT4(T4 t) => new (UnionIndexT7.T4, value4: t);
        public static UnionClass<T0, T1, T2, T3, T4, T5, T6, T7> FromT5(T5 t) => new (UnionIndexT7.T5, value5: t);
        public static UnionClass<T0, T1, T2, T3, T4, T5, T6, T7> FromT6(T6 t) => new (UnionIndexT7.T6, value6: t);
        public static UnionClass<T0, T1, T2, T3, T4, T5, T6, T7> FromT7(T7 t) => new (UnionIndexT7.T7, value7: t);
        #endregion From Methods

        #region Conversions
        public static implicit operator UnionClass<T0, T1, T2, T3, T4, T5, T6, T7>(T0 t) => FromT0(t);
        public static implicit operator UnionClass<T0, T1, T2, T3, T4, T5, T6, T7>(T1 t) => FromT1(t);
        public static implicit operator UnionClass<T0, T1, T2, T3, T4, T5, T6, T7>(T2 t) => FromT2(t);
        public static implicit operator UnionClass<T0, T1, T2, T3, T4, T5, T6, T7>(T3 t) => FromT3(t);
        public static implicit operator UnionClass<T0, T1, T2, T3, T4, T5, T6, T7>(T4 t) => FromT4(t);
        public static implicit operator UnionClass<T0, T1, T2, T3, T4, T5, T6, T7>(T5 t) => FromT5(t);
        public static implicit operator UnionClass<T0, T1, T2, T3, T4, T5, T6, T7>(T6 t) => FromT6(t);
        public static implicit operator UnionClass<T0, T1, T2, T3, T4, T5, T6, T7>(T7 t) => FromT7(t);
        #endregion Conversions
        
        #region Select Methods
        public Union<TResult, T1, T2, T3, T4, T5, T6, T7> Select0<TResult>(Func<T0, TResult> selector)
            where TResult : notnull
        {
            selector = selector ?? throw new ArgumentNullException(nameof(selector));
            return this.Index switch
            {
                UnionIndexT7.T0 when this.value0 is not null => selector(this.value0),
                UnionIndexT7.T1 when this.value1 is not null => Union<TResult, T1, T2, T3, T4, T5, T6, T7>.FromT1(this.value1),
                UnionIndexT7.T2 when this.value2 is not null => Union<TResult, T1, T2, T3, T4, T5, T6, T7>.FromT2(this.value2),
                UnionIndexT7.T3 when this.value3 is not null => Union<TResult, T1, T2, T3, T4, T5, T6, T7>.FromT3(this.value3),
                UnionIndexT7.T4 when this.value4 is not null => Union<TResult, T1, T2, T3, T4, T5, T6, T7>.FromT4(this.value4),
                UnionIndexT7.T5 when this.value5 is not null => Union<TResult, T1, T2, T3, T4, T5, T6, T7>.FromT5(this.value5),
                UnionIndexT7.T6 when this.value6 is not null => Union<TResult, T1, T2, T3, T4, T5, T6, T7>.FromT6(this.value6),
                UnionIndexT7.T7 when this.value7 is not null => Union<TResult, T1, T2, T3, T4, T5, T6, T7>.FromT7(this.value7),
                _ => throw new InvalidOperationException(),
            };
        }
        public Union<T0, TResult, T2, T3, T4, T5, T6, T7> Select1<TResult>(Func<T1, TResult> selector)
            where TResult : notnull
        {
            selector = selector ?? throw new ArgumentNullException(nameof(selector));
            return this.Index switch
            {
                UnionIndexT7.T0 when this.value0 is not null => Union<T0, TResult, T2, T3, T4, T5, T6, T7>.FromT0(this.value0),
                UnionIndexT7.T1 when this.value1 is not null => selector(this.value1),
                UnionIndexT7.T2 when this.value2 is not null => Union<T0, TResult, T2, T3, T4, T5, T6, T7>.FromT2(this.value2),
                UnionIndexT7.T3 when this.value3 is not null => Union<T0, TResult, T2, T3, T4, T5, T6, T7>.FromT3(this.value3),
                UnionIndexT7.T4 when this.value4 is not null => Union<T0, TResult, T2, T3, T4, T5, T6, T7>.FromT4(this.value4),
                UnionIndexT7.T5 when this.value5 is not null => Union<T0, TResult, T2, T3, T4, T5, T6, T7>.FromT5(this.value5),
                UnionIndexT7.T6 when this.value6 is not null => Union<T0, TResult, T2, T3, T4, T5, T6, T7>.FromT6(this.value6),
                UnionIndexT7.T7 when this.value7 is not null => Union<T0, TResult, T2, T3, T4, T5, T6, T7>.FromT7(this.value7),
                _ => throw new InvalidOperationException(),
            };
        }
        public Union<T0, T1, TResult, T3, T4, T5, T6, T7> Select2<TResult>(Func<T2, TResult> selector)
            where TResult : notnull
        {
            selector = selector ?? throw new ArgumentNullException(nameof(selector));
            return this.Index switch
            {
                UnionIndexT7.T0 when this.value0 is not null => Union<T0, T1, TResult, T3, T4, T5, T6, T7>.FromT0(this.value0),
                UnionIndexT7.T1 when this.value1 is not null => Union<T0, T1, TResult, T3, T4, T5, T6, T7>.FromT1(this.value1),
                UnionIndexT7.T2 when this.value2 is not null => selector(this.value2),
                UnionIndexT7.T3 when this.value3 is not null => Union<T0, T1, TResult, T3, T4, T5, T6, T7>.FromT3(this.value3),
                UnionIndexT7.T4 when this.value4 is not null => Union<T0, T1, TResult, T3, T4, T5, T6, T7>.FromT4(this.value4),
                UnionIndexT7.T5 when this.value5 is not null => Union<T0, T1, TResult, T3, T4, T5, T6, T7>.FromT5(this.value5),
                UnionIndexT7.T6 when this.value6 is not null => Union<T0, T1, TResult, T3, T4, T5, T6, T7>.FromT6(this.value6),
                UnionIndexT7.T7 when this.value7 is not null => Union<T0, T1, TResult, T3, T4, T5, T6, T7>.FromT7(this.value7),
                _ => throw new InvalidOperationException(),
            };
        }
        public Union<T0, T1, T2, TResult, T4, T5, T6, T7> Select3<TResult>(Func<T3, TResult> selector)
            where TResult : notnull
        {
            selector = selector ?? throw new ArgumentNullException(nameof(selector));
            return this.Index switch
            {
                UnionIndexT7.T0 when this.value0 is not null => Union<T0, T1, T2, TResult, T4, T5, T6, T7>.FromT0(this.value0),
                UnionIndexT7.T1 when this.value1 is not null => Union<T0, T1, T2, TResult, T4, T5, T6, T7>.FromT1(this.value1),
                UnionIndexT7.T2 when this.value2 is not null => Union<T0, T1, T2, TResult, T4, T5, T6, T7>.FromT2(this.value2),
                UnionIndexT7.T3 when this.value3 is not null => selector(this.value3),
                UnionIndexT7.T4 when this.value4 is not null => Union<T0, T1, T2, TResult, T4, T5, T6, T7>.FromT4(this.value4),
                UnionIndexT7.T5 when this.value5 is not null => Union<T0, T1, T2, TResult, T4, T5, T6, T7>.FromT5(this.value5),
                UnionIndexT7.T6 when this.value6 is not null => Union<T0, T1, T2, TResult, T4, T5, T6, T7>.FromT6(this.value6),
                UnionIndexT7.T7 when this.value7 is not null => Union<T0, T1, T2, TResult, T4, T5, T6, T7>.FromT7(this.value7),
                _ => throw new InvalidOperationException(),
            };
        }
        public Union<T0, T1, T2, T3, TResult, T5, T6, T7> Select4<TResult>(Func<T4, TResult> selector)
            where TResult : notnull
        {
            selector = selector ?? throw new ArgumentNullException(nameof(selector));
            return this.Index switch
            {
                UnionIndexT7.T0 when this.value0 is not null => Union<T0, T1, T2, T3, TResult, T5, T6, T7>.FromT0(this.value0),
                UnionIndexT7.T1 when this.value1 is not null => Union<T0, T1, T2, T3, TResult, T5, T6, T7>.FromT1(this.value1),
                UnionIndexT7.T2 when this.value2 is not null => Union<T0, T1, T2, T3, TResult, T5, T6, T7>.FromT2(this.value2),
                UnionIndexT7.T3 when this.value3 is not null => Union<T0, T1, T2, T3, TResult, T5, T6, T7>.FromT3(this.value3),
                UnionIndexT7.T4 when this.value4 is not null => selector(this.value4),
                UnionIndexT7.T5 when this.value5 is not null => Union<T0, T1, T2, T3, TResult, T5, T6, T7>.FromT5(this.value5),
                UnionIndexT7.T6 when this.value6 is not null => Union<T0, T1, T2, T3, TResult, T5, T6, T7>.FromT6(this.value6),
                UnionIndexT7.T7 when this.value7 is not null => Union<T0, T1, T2, T3, TResult, T5, T6, T7>.FromT7(this.value7),
                _ => throw new InvalidOperationException(),
            };
        }
        public Union<T0, T1, T2, T3, T4, TResult, T6, T7> Select5<TResult>(Func<T5, TResult> selector)
            where TResult : notnull
        {
            selector = selector ?? throw new ArgumentNullException(nameof(selector));
            return this.Index switch
            {
                UnionIndexT7.T0 when this.value0 is not null => Union<T0, T1, T2, T3, T4, TResult, T6, T7>.FromT0(this.value0),
                UnionIndexT7.T1 when this.value1 is not null => Union<T0, T1, T2, T3, T4, TResult, T6, T7>.FromT1(this.value1),
                UnionIndexT7.T2 when this.value2 is not null => Union<T0, T1, T2, T3, T4, TResult, T6, T7>.FromT2(this.value2),
                UnionIndexT7.T3 when this.value3 is not null => Union<T0, T1, T2, T3, T4, TResult, T6, T7>.FromT3(this.value3),
                UnionIndexT7.T4 when this.value4 is not null => Union<T0, T1, T2, T3, T4, TResult, T6, T7>.FromT4(this.value4),
                UnionIndexT7.T5 when this.value5 is not null => selector(this.value5),
                UnionIndexT7.T6 when this.value6 is not null => Union<T0, T1, T2, T3, T4, TResult, T6, T7>.FromT6(this.value6),
                UnionIndexT7.T7 when this.value7 is not null => Union<T0, T1, T2, T3, T4, TResult, T6, T7>.FromT7(this.value7),
                _ => throw new InvalidOperationException(),
            };
        }
        public Union<T0, T1, T2, T3, T4, T5, TResult, T7> Select6<TResult>(Func<T6, TResult> selector)
            where TResult : notnull
        {
            selector = selector ?? throw new ArgumentNullException(nameof(selector));
            return this.Index switch
            {
                UnionIndexT7.T0 when this.value0 is not null => Union<T0, T1, T2, T3, T4, T5, TResult, T7>.FromT0(this.value0),
                UnionIndexT7.T1 when this.value1 is not null => Union<T0, T1, T2, T3, T4, T5, TResult, T7>.FromT1(this.value1),
                UnionIndexT7.T2 when this.value2 is not null => Union<T0, T1, T2, T3, T4, T5, TResult, T7>.FromT2(this.value2),
                UnionIndexT7.T3 when this.value3 is not null => Union<T0, T1, T2, T3, T4, T5, TResult, T7>.FromT3(this.value3),
                UnionIndexT7.T4 when this.value4 is not null => Union<T0, T1, T2, T3, T4, T5, TResult, T7>.FromT4(this.value4),
                UnionIndexT7.T5 when this.value5 is not null => Union<T0, T1, T2, T3, T4, T5, TResult, T7>.FromT5(this.value5),
                UnionIndexT7.T6 when this.value6 is not null => selector(this.value6),
                UnionIndexT7.T7 when this.value7 is not null => Union<T0, T1, T2, T3, T4, T5, TResult, T7>.FromT7(this.value7),
                _ => throw new InvalidOperationException(),
            };
        }
        public Union<T0, T1, T2, T3, T4, T5, T6, TResult> Select7<TResult>(Func<T7, TResult> selector)
            where TResult : notnull
        {
            selector = selector ?? throw new ArgumentNullException(nameof(selector));
            return this.Index switch
            {
                UnionIndexT7.T0 when this.value0 is not null => Union<T0, T1, T2, T3, T4, T5, T6, TResult>.FromT0(this.value0),
                UnionIndexT7.T1 when this.value1 is not null => Union<T0, T1, T2, T3, T4, T5, T6, TResult>.FromT1(this.value1),
                UnionIndexT7.T2 when this.value2 is not null => Union<T0, T1, T2, T3, T4, T5, T6, TResult>.FromT2(this.value2),
                UnionIndexT7.T3 when this.value3 is not null => Union<T0, T1, T2, T3, T4, T5, T6, TResult>.FromT3(this.value3),
                UnionIndexT7.T4 when this.value4 is not null => Union<T0, T1, T2, T3, T4, T5, T6, TResult>.FromT4(this.value4),
                UnionIndexT7.T5 when this.value5 is not null => Union<T0, T1, T2, T3, T4, T5, T6, TResult>.FromT5(this.value5),
                UnionIndexT7.T6 when this.value6 is not null => Union<T0, T1, T2, T3, T4, T5, T6, TResult>.FromT6(this.value6),
                UnionIndexT7.T7 when this.value7 is not null => selector(this.value7),
                _ => throw new InvalidOperationException(),
            };
        }
        #endregion Select Methods
        
        public override string ToString() => this.Index switch
        {
            UnionIndexT7.T0 => $"{typeof(T0)}: {value0}",
            UnionIndexT7.T1 => $"{typeof(T1)}: {value1}",
            UnionIndexT7.T2 => $"{typeof(T2)}: {value2}",
            UnionIndexT7.T3 => $"{typeof(T3)}: {value3}",
            UnionIndexT7.T4 => $"{typeof(T4)}: {value4}",
            UnionIndexT7.T5 => $"{typeof(T5)}: {value5}",
            UnionIndexT7.T6 => $"{typeof(T6)}: {value6}",
            UnionIndexT7.T7 => $"{typeof(T7)}: {value7}",
            _ => throw new InvalidOperationException(),
        };

        #region Equality
        
        public bool Equals(UnionClass<T0, T1, T2, T3, T4, T5, T6, T7>? other) => this.Index switch
        {
            { } when other is null => false,
            { } when this.Index != other.Index => false,
            UnionIndexT7.T0 => EqualityComparer<T0?>.Default.Equals(this.value0, other.value0),
            UnionIndexT7.T1 => EqualityComparer<T1?>.Default.Equals(this.value1, other.value1),
            UnionIndexT7.T2 => EqualityComparer<T2?>.Default.Equals(this.value2, other.value2),
            UnionIndexT7.T3 => EqualityComparer<T3?>.Default.Equals(this.value3, other.value3),
            UnionIndexT7.T4 => EqualityComparer<T4?>.Default.Equals(this.value4, other.value4),
            UnionIndexT7.T5 => EqualityComparer<T5?>.Default.Equals(this.value5, other.value5),
            UnionIndexT7.T6 => EqualityComparer<T6?>.Default.Equals(this.value6, other.value6),
            UnionIndexT7.T7 => EqualityComparer<T7?>.Default.Equals(this.value7, other.value7),
            _ => throw new InvalidOperationException($"Invalid index {this.Index}"),
        };

        public override int GetHashCode() => this.Index switch
        {
            UnionIndexT7.T0 => HashCode.Combine(this.Index, this.value0),
            UnionIndexT7.T1 => HashCode.Combine(this.Index, this.value1),
            UnionIndexT7.T2 => HashCode.Combine(this.Index, this.value2),
            UnionIndexT7.T3 => HashCode.Combine(this.Index, this.value3),
            UnionIndexT7.T4 => HashCode.Combine(this.Index, this.value4),
            UnionIndexT7.T5 => HashCode.Combine(this.Index, this.value5),
            UnionIndexT7.T6 => HashCode.Combine(this.Index, this.value6),
            UnionIndexT7.T7 => HashCode.Combine(this.Index, this.value7),
            _ => throw new InvalidOperationException($"Invalid index {this.Index}"),
        };

        public override bool Equals(object? obj) => obj is UnionClass<T0, T1, T2, T3, T4, T5, T6, T7> other && Equals(other);
        public static bool operator ==(UnionClass<T0, T1, T2, T3, T4, T5, T6, T7> left, UnionClass<T0, T1, T2, T3, T4, T5, T6, T7> right) => left.Equals(right);
        public static bool operator !=(UnionClass<T0, T1, T2, T3, T4, T5, T6, T7> left, UnionClass<T0, T1, T2, T3, T4, T5, T6, T7> right) => !left.Equals(right);
        #endregion Equality
    }
}

