namespace LockAndInterlockedDemo
{
    class Counter:CounterBase
    {
        private int _count;
        public int Count { get { return _count; } }
        public override void Increment()
        {
            _count += 1;
        }

        public override void Decrement()
        {
            _count -= 1;
        }
    }
}
