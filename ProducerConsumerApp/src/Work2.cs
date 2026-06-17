//Semaphore + lock
namespace ProducerConsumer
{
    public class Work2 : IWork
    {
        private readonly Queue<int> _buffer = new Queue<int>();
        private readonly int _capacity;
        private readonly object _lock = new object();

        private readonly Semaphore _empty;
        private readonly Semaphore _full;

        public Work2(int capacity)
        {
            _capacity = capacity > 0 ? capacity : 1;
            _empty = new Semaphore(_capacity, _capacity);
            _full = new Semaphore(0, _capacity);
        }
        public void Produce(int value)
        {
            _empty.WaitOne();
            lock (_lock)
            {
                _buffer.Enqueue(value);
                //Console.WriteLine(Thread.CurrentThread.Name + " has produced an item " + value);
            }
            _full.Release();
        }
        public int Consume()
        {
            int ret;
            _full.WaitOne();
            lock (_lock)
            {
                ret = _buffer.Dequeue();
                //Console.WriteLine(Thread.CurrentThread.Name + " has consumed an item " + ret);
            }
            _empty.Release();
            return ret;
        }
    }
}