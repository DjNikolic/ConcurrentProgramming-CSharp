//SpinLock
namespace ProducerConsumer
{
    public class Work5 : IWork
    {
        private readonly Queue<int> _buffer = new Queue<int>();
        private readonly int _capacity;
        private SpinLock _spinLock = new SpinLock();

        public Work5(int capacity)
        {
            _capacity = capacity > 0 ? capacity : 1;
        }
        public void Produce(int value)
        {
            SpinWait spinWait = new SpinWait();
            while (true)
            {
                bool gotLuck = false;
                try
                {
                    _spinLock.Enter(ref gotLuck);
                    if (_buffer.Count < _capacity)
                    {
                        _buffer.Enqueue(value);
                        return;
                    }
                }
                finally
                {
                    if (gotLuck)
                    {
                        _spinLock.Exit();
                    }
                }
                spinWait.SpinOnce();
            }
        }
        public int Consume()
        {
            SpinWait spinWait = new SpinWait();
            while (true)
            {
                bool gotLuck = false;
                try
                {
                    _spinLock.Enter(ref gotLuck);
                    if (_buffer.Count > 0)
                    {
                        return _buffer.Dequeue();
                    }
                }
                finally
                {
                    if (gotLuck)
                    {
                        _spinLock.Exit();
                    }
                }
                spinWait.SpinOnce();
            }
        }
    }
}