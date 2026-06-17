// Monitor - writer priority
namespace ReaderWriter
{
    public class SharedResource1 : ISharedResource
    {
        private int _data = 0;
        private readonly object _lock = new object();
        private bool _isWriter = false;
        private int _countReaders = 0;
        private int _countWritersWaiting = 0;
        public int read()
        {
            lock (_lock)
            {
                while (_isWriter || _countWritersWaiting > 0)
                {
                    Monitor.Wait(_lock);
                }
                _countReaders++;
            }
            Console.WriteLine(Thread.CurrentThread.Name + " has started reading");
            int value = _data;
            Thread.Sleep(200);
            lock (_lock)
            {
                _countReaders--;
                if (_countReaders == 0)
                {
                    Monitor.PulseAll(_lock);
                }
            }
            return value;
        }

        public void write(int value)
        {
            lock (_lock)
            {
                _countWritersWaiting++;
                while (_isWriter || _countReaders > 0)
                {
                    Monitor.Wait(_lock);
                }
                _countWritersWaiting--;
                _isWriter = true;
            }
            Console.WriteLine(Thread.CurrentThread.Name + " has started writing");
            _data = value;
            Thread.Sleep(300);
            lock (_lock)
            {
                _isWriter = false;
                Monitor.PulseAll(_lock);
            }
        }
    }
}