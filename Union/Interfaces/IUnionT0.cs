// Copyright (c) Mike Christiansen. All rights reserved.
// Licensed under the MIT license.
// See license.txt file in the project root for full license information.

using System;
using System.Diagnostics.CodeAnalysis;

namespace Union
{
    public interface IUnion<T0> : IUnion
        where T0 : notnull
    {
        public void Switch(Action<T0> f0);
        public TResult Switch<TResult>(Func<T0, TResult> f0);
        
        #region As Methods 
        public T0? AsT0(); 
        #endregion As Methods
        
        #region Is Methods
        public bool IsT0([NotNullWhen(true)] out T0? value); 
        #endregion Is Methods
        
        #region Select Methods
        public Union<TResult> Select0<TResult>(Func<T0, TResult> selector) where TResult : notnull;
        #endregion Select Methods
    }
}

