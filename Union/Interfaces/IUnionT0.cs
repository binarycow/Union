using System;
using System.Diagnostics.CodeAnalysis;

namespace Union
{
    public interface IUnion<T0> : IUnion
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
        public Union<TResult> Select0<TResult>(Func<T0, TResult> selector);
        #endregion Select Methods
        
    }
}
