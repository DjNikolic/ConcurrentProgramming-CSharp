namespace ProducerConsumer
{
    public class Producer
    {
        private readonly IProducing _work;
        private string _name;
        private readonly Random _random = new Random();
        public Producer(IProducing work, string name)
        {
            _work = work;
            _name = name;
        }
        public Producer(IProducing work)
        {
            _work = work;
        }
        public void Run()
        {
            if (_name == null)
            {
                _name = Thread.CurrentThread.Name;
            }
            while (true)
            {
                int value = _random.Next(1000);
                _work.Produce(value);
                Console.WriteLine(_name + " has produced an item " + value);
                Thread.Sleep(_random.Next(1000));
            }
        }
    }
}