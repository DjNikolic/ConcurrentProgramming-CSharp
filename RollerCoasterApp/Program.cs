namespace RollerCoaster
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Enter the number of passengers: ");
            int passengers = int.Parse(Console.ReadLine());
            Console.Write("Enter the rollercoaster capacity: ");
            int capacity = int.Parse(Console.ReadLine());

            Console.WriteLine("Choose implemetation:");
            Console.WriteLine("1 - Barrier + SemaphoreSlim:");
            Console.WriteLine("2 - CountdownEvent + SemaphoreSlim:");
            Console.WriteLine("3 - Channel:");
            Console.WriteLine("4 - Monitor:");
            Console.WriteLine("5 - ManualResetEventSlim + Interlocked + SemaphoreSlim:");
            int number = int.Parse(Console.ReadLine());

            RollerCoaster? rollerCoaster = null;
            switch (number)
            {
                case 1:
                    rollerCoaster = new RollerCoaster1(capacity);
                    break;
                case 2:
                    rollerCoaster = new RollerCoaster2(capacity);
                    break;
                case 3:
                    rollerCoaster = new RollerCoaster3(capacity);
                    break;
                case 4:
                    rollerCoaster = new RollerCoaster5(capacity);
                    break;
                case 5:
                    rollerCoaster = new RollerCoaster6(capacity);
                    break;
                default:
                    Console.WriteLine("Invalid choise.");
                    return;
            }

            for (int i = 0; i < passengers; i++)
            {
                Passenger passenger = new Passenger(rollerCoaster);
                Thread t = new Thread(passenger.Run);
                t.Name = "Passenger " + i;
                t.Start();
            }
        }
    }
}
