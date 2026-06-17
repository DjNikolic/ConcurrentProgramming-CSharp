namespace ReaderWriter
{
    public class Reader
    {
        private readonly ISharedResource _sharedResource;
        private readonly Random _random = new Random();
        private int data;
        public Reader(ISharedResource sharedResource)
        {
            _sharedResource = sharedResource;
        }
        public void Run()
        {
            while (true)
            {
                Thread.Sleep(_random.Next(1000));
                Console.WriteLine(Thread.CurrentThread.Name + " wants to read");
                data = _sharedResource.read();
                Console.WriteLine(Thread.CurrentThread.Name + " has finished reading (" + data + ")");
            }
        }
    }
}