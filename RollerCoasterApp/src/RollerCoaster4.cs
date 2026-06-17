// VISAK
namespace RollerCoaster
{
    public class RollerCoaster4 : RollerCoaster
    {
        private int _currentCount = 0;
        private readonly object _lock = new object();
        private readonly SemaphoreSlim _semEnter;
        private readonly SemaphoreSlim _semExit;
        private readonly SemaphoreSlim _semStartRide;
        private readonly SemaphoreSlim _semEndRide;
        public RollerCoaster4(int capacity) : base(capacity)
        {
            _semEnter = new SemaphoreSlim(capacity, capacity);
            _semExit = new SemaphoreSlim(0, capacity);
            _semStartRide = new SemaphoreSlim(0, 1);
            _semEndRide = new SemaphoreSlim(0, 1);
            StartRideThread();
        }
        private void StartRideThread()
        {
            Thread t = new Thread(() =>
            {
                while (true)
                {
                    _semStartRide.Wait();
                    Console.WriteLine("All passengers boarded the ride");
                    Ride();
                    _semExit.Release(Capacity);
                    _semEndRide.Wait();
                    Console.WriteLine("All passengers got off the ride");
                    _semEnter.Release(Capacity);
                }
            });
            t.Start();
        }
        public override void BoardRide()
        {
            _semEnter.Wait();
            lock (_lock)
            {
                _currentCount++;
                if (_currentCount == Capacity)
                {
                    _semStartRide.Release();
                }
            }
        }
        public override void LeaveRide()
        {
            _semExit.Wait();
            lock (_lock)
            {
                _currentCount--;
                if (_currentCount == 0)
                {
                    _semEndRide.Release();
                }
            }
        }
    }
}