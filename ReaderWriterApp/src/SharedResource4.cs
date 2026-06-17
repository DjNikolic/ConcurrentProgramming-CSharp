//ReaderWriterLockSlim
namespace ReaderWriter
{
    public class SharedResource4 : ISharedResource
    {
        private int _data = 0;
        private readonly ReaderWriterLockSlim _lock = new ReaderWriterLockSlim();
        public int read()
        {
            _lock.EnterReadLock();
            try
            {
                Console.WriteLine(Thread.CurrentThread.Name + " has started reading");
                int value = _data;
                Thread.Sleep(200);
                return value;
            }
            finally
            {
                _lock.ExitReadLock();
            }
        }

        public void write(int value)
        {
            _lock.EnterWriteLock();
            try
            {
                Console.WriteLine(Thread.CurrentThread.Name + " has started writing");
                _data = value;
                Thread.Sleep(300);
            }
            finally
            {
                _lock.ExitWriteLock();
            }
        }
    }
}