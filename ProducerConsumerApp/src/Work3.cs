//SemaphoreSlim + ConcurrentQueue
using System.Collections.Concurrent;

namespace ProducerConsumer
{
    public class Work3 : IWork
    {
        private readonly ConcurrentQueue<int> _buffer = new ConcurrentQueue<int>();

        private readonly SemaphoreSlim _empty;
        private readonly SemaphoreSlim _full;

        public Work3(int capacity)
        {
            int realCapacity = capacity > 0 ? capacity : 1;
            _empty = new SemaphoreSlim(realCapacity);
            _full = new SemaphoreSlim(0);
        }
        public void Produce(int value)
        {
            _empty.Wait();
            _buffer.Enqueue(value);
            _full.Release();
        }
        public int Consume()
        {
            int ret;
            _full.Wait();
            _buffer.TryDequeue(out ret);
            _empty.Release();
            return ret;
        }
    }
}