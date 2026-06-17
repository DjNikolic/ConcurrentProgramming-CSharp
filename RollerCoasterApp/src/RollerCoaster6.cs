//ManualResetEventSlim + Interlocked + SemaphoreSlim
namespace RollerCoaster
{
    public class RollerCoaster6 : RollerCoaster
    {
        private int curPassengers = 0;
        private readonly SemaphoreSlim _semEnter;
        private readonly ManualResetEventSlim _startRideEvent = new ManualResetEventSlim(false);
        private readonly ManualResetEventSlim _rideFinishedEvent = new ManualResetEventSlim(false);
        private readonly ManualResetEventSlim _passengersLeftEvent = new ManualResetEventSlim(false);
        public RollerCoaster6(int capacity) : base(capacity)
        {
            _semEnter = new SemaphoreSlim(capacity, capacity);
            StartRideThread();
        }

        private void StartRideThread()
        {
            Thread t = new Thread(() =>
            {
                while (true)
                {
                    _startRideEvent.Wait();

                    Console.WriteLine("All passengers boarded the ride");
                    Ride();

                    _rideFinishedEvent.Set();
                    _passengersLeftEvent.Wait();
                    Console.WriteLine("All passengers got off the ride");

                    _startRideEvent.Reset();
                    _rideFinishedEvent.Reset();
                    _passengersLeftEvent.Reset();
                    _semEnter.Release(Capacity);
                }
            });
            t.Start();
        }

        public override void BoardRide()
        {
            _semEnter.Wait();
            //Console.WriteLine(Thread.CurrentThread.Name + " boarded");
            int temp = Interlocked.Increment(ref curPassengers);
            if (temp == Capacity)
                _startRideEvent.Set();

        }

        public override void LeaveRide()
        {
            _rideFinishedEvent.Wait();
            //Console.WriteLine(Thread.CurrentThread.Name + " left");
            int temp = Interlocked.Decrement(ref curPassengers);
            if (temp == 0)
                _passengersLeftEvent.Set();
        }
    }
}