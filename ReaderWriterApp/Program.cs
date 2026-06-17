namespace ReaderWriter
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Enter the number of readers: ");
            int readers = int.Parse(Console.ReadLine());
            Console.Write("Enter the number of writers: ");
            int writers = int.Parse(Console.ReadLine());

            Console.WriteLine("Choose implemetation:");
            Console.WriteLine("1 - Monitor - writer priority:");
            Console.WriteLine("2 - SemaphoreSlim + lock - reader priority:");
            Console.WriteLine("3 - Mutex + SemaphoreSlim - fair:");
            Console.WriteLine("4 - ReaderWriterLockSlim:");
            Console.WriteLine("5 - Task + ConcurrentExclusiveSchedulerPair:");
            int number = int.Parse(Console.ReadLine());

            ISharedResource? sharedResource = null;
            switch (number)
            {
                case 1:
                    sharedResource = new SharedResource1();
                    break;
                case 2:
                    sharedResource = new SharedResource2();
                    break;
                case 3:
                    sharedResource = new SharedResource3();
                    break;
                case 4:
                    sharedResource = new SharedResource4();
                    break;
                case 5:
                    sharedResource = new SharedResource5();
                    break;
                default:
                    Console.WriteLine("Invalid choise.");
                    return;
            }

            for (int i = 0; i < readers; i++)
            {
                Reader producer = new Reader(sharedResource);
                Thread t = new Thread(producer.Run);
                t.Name = "Reader " + i;
                t.Start();
            }
            for (int i = 0; i < writers; i++)
            {
                Writer consumer = new Writer(sharedResource);
                Thread t = new Thread(consumer.Run);
                t.Name = "Writer " + i;
                t.Start();
            }
        }
    }
}
