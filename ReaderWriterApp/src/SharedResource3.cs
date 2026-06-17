//Mutex + SemaphoreSlim - fair
namespace ReaderWriter
{
    public class SharedResource3 : ISharedResource
    {
        private int _data = 0;
        private int _countReaders = 0;
        private readonly SemaphoreSlim _resource = new SemaphoreSlim(1, 1);
        private readonly Mutex _queueMutex = new Mutex();
        private readonly Mutex _countReaderMutex = new Mutex();

        public int read()
        {
            _queueMutex.WaitOne();
            _countReaderMutex.WaitOne();
            if (++_countReaders == 1)
            {
                _resource.Wait();
            }
            _countReaderMutex.ReleaseMutex();
            _queueMutex.ReleaseMutex();

            Console.WriteLine(Thread.CurrentThread.Name + " has started reading");
            int value = _data;
            Thread.Sleep(200);

            _countReaderMutex.WaitOne();
            if (--_countReaders == 0)
            {
                _resource.Release();
            }
            _countReaderMutex.ReleaseMutex();
            return value;
        }

        public void write(int value)
        {
            _queueMutex.WaitOne();
            _resource.Wait();
            _queueMutex.ReleaseMutex();

            Console.WriteLine(Thread.CurrentThread.Name + " has started writing");
            _data = value;
            Thread.Sleep(300);

            _resource.Release();
        }
    }
}