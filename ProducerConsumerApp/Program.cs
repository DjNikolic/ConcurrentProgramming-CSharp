namespace ProducerConsumer
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Enter the number of producers: ");
            int producers = int.Parse(Console.ReadLine());
            Console.Write("Enter the number of consumers: ");
            int consumers = int.Parse(Console.ReadLine());
            Console.Write("Enter the buffer size: ");
            int size = int.Parse(Console.ReadLine());

            Console.WriteLine("Choose implemetation:");
            Console.WriteLine("1 - Monitor + lock:");
            Console.WriteLine("2 - Semaphore + lock:");
            Console.WriteLine("3 - SemaphoreSlim + ConcurrentQueue:");
            Console.WriteLine("4 - BlockingCollection:");
            Console.WriteLine("5 - SpinLock:");
            int number = int.Parse(Console.ReadLine());

            IWork? work = null;
            switch (number)
            {
                case 1:
                    work = new Work1(size);
                    break;
                case 2:
                    work = new Work2(size);
                    break;
                case 3:
                    work = new Work3(size);
                    break;
                case 4:
                    work = new Work4(size);
                    break;
                case 5:
                    work = new Work5(size);
                    break;
                default:
                    Console.WriteLine("Invalid choise.");
                    return;
            }

            Console.WriteLine("Choose thread generetion:");
            Console.WriteLine("1 - Thread:");
            Console.WriteLine("2 - ThreadPool:");
            Console.WriteLine("3 - Task:");
            number = int.Parse(Console.ReadLine());

            IGenerateThreads? generator = null;
            switch (number)
            {
                case 1:
                    generator = new GenerateThreads1();
                    break;
                case 2:
                    generator = new GenerateThreads2();
                    break;
                case 3:
                    generator = new GenerateThreads3();
                    break;
                default:
                    Console.WriteLine("Invalid choise.");
                    return;
            }
            generator.Generate(work, producers, consumers);
            Console.ReadLine(); // da se Main nit ne ugasi
        }
    }
}

