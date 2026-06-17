// SemaphoreSlim + lock - reader priority
// ne moze Mutex umesto Semaphore-a jer mora ista nit da ga otkljuca
namespace ReaderWriter
{
    public class SharedResource2 : ISharedResource
    {
        private int _data = 0;
        private readonly SemaphoreSlim _resource = new SemaphoreSlim(1, 1);
        private readonly object _lock = new object();
        private int _countReaders = 0;

        public int read()
        {
            lock (_lock)
            {
                if (++_countReaders == 1)
                {
                    _resource.Wait();
                }
            }

            Console.WriteLine(Thread.CurrentThread.Name + " has started reading");
            int value = _data;
            Thread.Sleep(200);

            lock (_lock)
            {
                if (--_countReaders == 0)
                {
                    _resource.Release();
                }
            }

            return value;
        }

        public void write(int value)
        {
            _resource.Wait();

            Console.WriteLine(Thread.CurrentThread.Name + " has started writing");
            _data = value;
            Thread.Sleep(300);

            _resource.Release();
        }
    }
}