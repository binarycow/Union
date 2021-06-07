using System;
using System.Diagnostics.CodeAnalysis;

namespace Union
{
    public interface IUnion<T0, T1, T2, T3> : IUnion
    {
        public void Switch(Action<T0> f0, Action<T1> f1, Action<T2> f2, Action<T3> f3);
        
        public TResult Switch<TResult>(Func<T0, TResult> f0, Func<T1, TResult> f1, Func<T2, TResult> f2, Func<T3, TResult> f3);
        
        #region As Methods
        public T0? AsT0();
        public T1? AsT1();
        public T2? AsT2();
        public T3? AsT3();
        #endregion As Methods
        
        #region Is Methods
        public bool IsT0([NotNullWhen(true)] out T0? value);
        public bool IsT1([NotNullWhen(true)] out T1? value);
        public bool IsT2([NotNullWhen(true)] out T2? value);
        public bool IsT3([NotNullWhen(true)] out T3? value);
        #endregion Is Methods
        
        #region Select Methods
        public Union<TResult, T1, T2, T3> Select0<TResult>(Func<T0, TResult> selector);
        public Union<T0, TResult, T2, T3> Select1<TResult>(Func<T1, TResult> selector);
        public Union<T0, T1, TResult, T3> Select2<TResult>(Func<T2, TResult> selector);
        public Union<T0, T1, T2, TResult> Select3<TResult>(Func<T3, TResult> selector);
        #endregion Select Methods
        
    }
}
