namespace LockAndInterlockedDemo
{
    class CounterWithLock:CounterBase
    {
        private static object _lockObj = new object();
        private int _count;
        public int Count { get { return _count; } }
        public override void Increment()
        {
            lock (_lockObj)
            {
                _count += 1;
            }

        }

        public override void Decrement()
        {
            lock (_lockObj)
            {
                _count -= 1;
            }
        }
    }
}
