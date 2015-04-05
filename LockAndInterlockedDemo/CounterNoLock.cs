using System.Threading;

namespace LockAndInterlockedDemo
{
    class CounterNoLock:CounterBase
    {
        private int _count;

        public int Count
        {
            get {return _count; }
        }

        public override void Increment()
        {
            Interlocked.Increment(ref _count);
        }

        public override void Decrement()
        {
            Interlocked.Decrement(ref _count);
        }
    }
}
