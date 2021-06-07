using System;

namespace Union.Generator
{
    public class Disposer : IDisposable
    {
        private readonly Action action;

        public Disposer(Action action)
        {
            this.action = action;
        }
        public void Dispose()
        {
            action();
        }
    }
}