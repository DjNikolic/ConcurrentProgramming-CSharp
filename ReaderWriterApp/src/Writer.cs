namespace ReaderWriter
{
    public class Writer
    {
        private readonly ISharedResource _sharedResource;
        private readonly Random _random = new Random();
        private int data;
        public Writer(ISharedResource sharedResource)
        {
            _sharedResource = sharedResource;
        }
        public void Run()
        {
            while (true)
            {
                Thread.Sleep(_random.Next(1000));
                data = _random.Next(1000);
                Console.WriteLine(Thread.CurrentThread.Name + " wants to write");
                _sharedResource.write(data);
                Console.WriteLine(Thread.CurrentThread.Name + " has finished writing (" + data + ")");
            }
        }
    }
}