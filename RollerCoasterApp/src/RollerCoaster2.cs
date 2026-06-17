//CountdownEvent + SemaphoreSlim
namespace RollerCoaster
{
    public class RollerCoaster2 : RollerCoaster
    {
        private readonly SemaphoreSlim _barSem;
        private readonly SemaphoreSlim _exitSem;
        // private readonly object _lock = new object();
        private CountdownEvent _enterCountdownEvent;
        private CountdownEvent _exitCountdownEvent;

        public RollerCoaster2(int capacity) : base(capacity)
        {
            _enterCountdownEvent = new CountdownEvent(capacity);
            _exitCountdownEvent = new CountdownEvent(capacity);
            _exitSem = new SemaphoreSlim(0, capacity);
            _barSem = new SemaphoreSlim(capacity, capacity);
            StartRideThread();
        }

        private void StartRideThread()
        {
            Thread t = new Thread(() =>
            {
                while (true)
                {
                    _enterCountdownEvent.Wait();
                    Console.WriteLine("All passengers boarded the ride");
                    //    lock (_lock)
                    //    {
                    _enterCountdownEvent = new CountdownEvent(Capacity);
                    //    }
                    Ride();
                    _exitSem.Release(Capacity);
                    _exitCountdownEvent.Wait();
                    Console.WriteLine("All passengers got off the ride");
                    //    lock (_lock)
                    //    {
                    _exitCountdownEvent = new CountdownEvent(Capacity);
                    //    }
                }
            });
            t.Start();
        }

        public override void BoardRide()
        {
            _barSem.Wait();
            //Console.WriteLine(Thread.CurrentThread.Name + " boarded");
            _enterCountdownEvent.Signal();
        }

        public override void LeaveRide()
        {
            _exitSem.Wait();
            //Console.WriteLine(Thread.CurrentThread.Name + " left");
            _exitCountdownEvent.Signal();
            _barSem.Release();
        }
    }
}