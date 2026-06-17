namespace RollerCoaster
{
    public class Passenger
    {
        private readonly RollerCoaster _rollerCoaster;
        private readonly Random _random = new Random();

        public Passenger(RollerCoaster rollerCoaster)
        {
            _rollerCoaster = rollerCoaster;
        }
        public void Run()
        {
            while (true)
            {
                Thread.Sleep(_random.Next(1000)); // walkAround()
                Console.WriteLine(Thread.CurrentThread.Name + " wants to board the ride");
                _rollerCoaster.BoardRide();
                // riding();
                _rollerCoaster.LeaveRide();
            }
        }
    }
}