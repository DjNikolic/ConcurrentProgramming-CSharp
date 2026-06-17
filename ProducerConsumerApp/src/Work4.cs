//BlockingCollection
using System.Collections.Concurrent;

namespace ProducerConsumer
{
    public class Work4 : IWork
    {
        private readonly BlockingCollection<int> _buffer;
        public Work4(int capacity)
        {
            int realCapacity = capacity > 0 ? capacity : 1;
            _buffer = new BlockingCollection<int>(
                 new ConcurrentQueue<int>(),
                 realCapacity
            );
        }
        public void Produce(int value)
        {
            _buffer.Add(value);
        }
        public int Consume()
        {
            return _buffer.Take();
        }
    }
}