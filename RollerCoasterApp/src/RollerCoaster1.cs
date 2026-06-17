//Barrier + SemaphoreSlim
namespace RollerCoaster
{
    public class RollerCoaster1 : RollerCoaster
    {
        private readonly SemaphoreSlim _barSem;
        private readonly Barrier _enterBarrier;
        private readonly Barrier _exitBarrier;
        public RollerCoaster1(int capacity) : base(capacity)
        {
            _enterBarrier = new Barrier(capacity + 1);
            _exitBarrier = new Barrier(capacity + 1);
            _barSem = new SemaphoreSlim(capacity, capacity);
            StartRideThread();
        }

        private void StartRideThread()
        {
            Thread t = new Thread(() =>
            {
                while (true)
                {
                    _enterBarrier.SignalAndWait();
                    Console.WriteLine("All passengers boarded the ride");
                    Ride();
                    _exitBarrier.SignalAndWait();
                    Console.WriteLine("All passengers got off the ride");
                }
            });
            t.Start();
        }

        public override void BoardRide()
        {
            _barSem.Wait();
            //Console.WriteLine(Thread.CurrentThread.Name + " boarded");
            _enterBarrier.SignalAndWait();
        }

        public override void LeaveRide()
        {
            _exitBarrier.SignalAndWait();
            //Console.WriteLine(Thread.CurrentThread.Name + " left");
            _barSem.Release();
        }
    }
}