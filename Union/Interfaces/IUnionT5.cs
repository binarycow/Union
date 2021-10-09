// Copyright (c) Mike Christiansen. All rights reserved.
// Licensed under the MIT license.
// See license.txt file in the project root for full license information.

using System;
using System.Diagnostics.CodeAnalysis;

namespace Union
{
    public interface IUnion<T0, T1, T2, T3, T4, T5> : IUnion
        where T0 : notnull
        where T1 : notnull
        where T2 : notnull
        where T3 : notnull
        where T4 : notnull
        where T5 : notnull
    {
        public void Switch(Action<T0> f0, Action<T1> f1, Action<T2> f2, Action<T3> f3, Action<T4> f4, Action<T5> f5);
        public TResult Switch<TResult>(Func<T0, TResult> f0, Func<T1, TResult> f1, Func<T2, TResult> f2, Func<T3, TResult> f3, Func<T4, TResult> f4, Func<T5, TResult> f5);
        
        #region As Methods 
        public T0? AsT0(); 
        public T1? AsT1(); 
        public T2? AsT2(); 
        public T3? AsT3(); 
        public T4? AsT4(); 
        public T5? AsT5(); 
        #endregion As Methods
        
        #region Is Methods
        public bool IsT0([NotNullWhen(true)] out T0? value); 
        public bool IsT1([NotNullWhen(true)] out T1? value); 
        public bool IsT2([NotNullWhen(true)] out T2? value); 
        public bool IsT3([NotNullWhen(true)] out T3? value); 
        public bool IsT4([NotNullWhen(true)] out T4? value); 
        public bool IsT5([NotNullWhen(true)] out T5? value); 
        #endregion Is Methods
        
        #region Select Methods
        public Union<TResult, T1, T2, T3, T4, T5> Select0<TResult>(Func<T0, TResult> selector) where TResult : notnull;
        public Union<T0, TResult, T2, T3, T4, T5> Select1<TResult>(Func<T1, TResult> selector) where TResult : notnull;
        public Union<T0, T1, TResult, T3, T4, T5> Select2<TResult>(Func<T2, TResult> selector) where TResult : notnull;
        public Union<T0, T1, T2, TResult, T4, T5> Select3<TResult>(Func<T3, TResult> selector) where TResult : notnull;
        public Union<T0, T1, T2, T3, TResult, T5> Select4<TResult>(Func<T4, TResult> selector) where TResult : notnull;
        public Union<T0, T1, T2, T3, T4, TResult> Select5<TResult>(Func<T5, TResult> selector) where TResult : notnull;
        #endregion Select Methods

        public Union<T0, T1, T2, T3, T4, T5, TResult> With<TResult>(TResult value) where TResult : notnull;
    }
}

