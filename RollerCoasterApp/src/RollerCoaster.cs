namespace RollerCoaster
{
    public abstract class RollerCoaster
    {
        public int Capacity { get; }
        protected RollerCoaster(int capacity)
        {
            Capacity = capacity;
        }
        public abstract void BoardRide();
        public abstract void LeaveRide();
        protected void Ride()
        {
            Console.WriteLine("The ride has started");
            Thread.Sleep(300);
            Console.WriteLine("The ride has finished");
        }
    }
}