// Copyright (c) Mike Christiansen. All rights reserved.
// Licensed under the MIT license.
// See license.txt file in the project root for full license information.

using System;
using System.Diagnostics.CodeAnalysis;

namespace Union
{
    public interface IUnion<T0, T1> : IUnion
        where T0 : notnull
        where T1 : notnull
    {
        public void Switch(Action<T0> f0, Action<T1> f1);
        public TResult Switch<TResult>(Func<T0, TResult> f0, Func<T1, TResult> f1);
        
        #region As Methods 
        public T0? AsT0(); 
        public T1? AsT1(); 
        #endregion As Methods
        
        #region Is Methods
        public bool IsT0([NotNullWhen(true)] out T0? value); 
        public bool IsT1([NotNullWhen(true)] out T1? value); 
        #endregion Is Methods
        
        #region Select Methods
        public Union<TResult, T1> Select0<TResult>(Func<T0, TResult> selector) where TResult : notnull;
        public Union<T0, TResult> Select1<TResult>(Func<T1, TResult> selector) where TResult : notnull;
        #endregion Select Methods

        public Union<T0, T1, TResult> With<TResult>(TResult value) where TResult : notnull;
    }
}

