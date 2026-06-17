namespace ProducerConsumer
{
    public class Consumer
    {
        private readonly IConsuming _work;
        private string _name;
        private readonly Random _random = new Random();

        public Consumer(IConsuming work, string name)
        {
            _work = work;
            _name = name;
        }
        public Consumer(IConsuming work)
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
                int value = _work.Consume();
                Console.WriteLine(_name + " has consumed an item " + value);
                Thread.Sleep(_random.Next(1000));
            }
        }
    }
}