//Monitor
namespace RollerCoaster
{
    public class RollerCoaster5 : RollerCoaster
    {
        private int _currentCount = 0;
        private int _leftCount = 0;
        private bool _canStartRide = false;
        private bool _canLeave = false;
        private readonly object _lock = new object();
        public RollerCoaster5(int capacity) : base(capacity)
        {
            StartRideThread();
        }
        private void StartRideThread()
        {
            Thread t = new Thread(() =>
            {
                while (true)
                {
                    lock (_lock)
                    {
                        while (!_canStartRide)
                        {
                            Monitor.Wait(_lock);
                        }
                        _canStartRide = false;
                    }
                    Console.WriteLine("All passengers boarded the ride");
                    Ride();
                    lock (_lock)
                    {
                        _canLeave = true;
                        Monitor.PulseAll(_lock);
                        while (_leftCount < Capacity)
                        {
                            Monitor.Wait(_lock);
                        }
                        _canLeave = false;
                        _leftCount = 0;
                        _currentCount = 0;
                        Monitor.PulseAll(_lock);
                        Console.WriteLine("All passengers got off the ride");
                    }
                }
            });
            t.Start();
        }

        public override void BoardRide()
        {
            lock (_lock)
            {
                while (_currentCount == Capacity)
                {
                    Monitor.Wait(_lock);
                }
                Console.WriteLine(Thread.CurrentThread.Name + " boarded");
                _currentCount++;
                if (_currentCount == Capacity)
                {
                    _canStartRide = true;
                    Monitor.PulseAll(_lock);
                }
            }
        }

        public override void LeaveRide()
        {
            lock (_lock)
            {
                while (!_canLeave)
                {
                    Monitor.Wait(_lock);
                }
                Console.WriteLine(Thread.CurrentThread.Name + " left");
                _leftCount++;
                if (_leftCount == Capacity)
                {
                    Monitor.PulseAll(_lock);
                }
            }
        }
    }
}