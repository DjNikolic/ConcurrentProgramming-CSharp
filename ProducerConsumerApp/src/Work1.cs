//Monitor + lock
namespace ProducerConsumer
{
    public class Work1 : IWork
    {
        private readonly Queue<int> _buffer = new Queue<int>();
        private readonly int _capacity;
        private readonly object _lock = new object();

        public Work1(int capacity)
        {
            _capacity = capacity > 0 ? capacity : 1;
        }
        public void Produce(int value)
        {
            lock (_lock)
            {
                while (_buffer.Count == _capacity)
                {
                    //Console.WriteLine(Thread.CurrentThread.Name + " is waiting because buffer is full");
                    Monitor.Wait(_lock);
                }
                _buffer.Enqueue(value);
                //Console.WriteLine(Thread.CurrentThread.Name + " has produced an item " + value);
                Monitor.PulseAll(_lock);
            }
        }
        public int Consume()
        {
            lock (_lock)
            {
                while (_buffer.Count == 0)
                {
                    //Console.WriteLine(Thread.CurrentThread.Name + " is waiting because buffer is empty");
                    Monitor.Wait(_lock);
                }
                int ret = _buffer.Dequeue();
                //Console.WriteLine(Thread.CurrentThread.Name + " has consumed an item " + ret);
                Monitor.PulseAll(_lock);
                return ret;
            }
        }
    }
}