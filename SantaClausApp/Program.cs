using SantaClaus;

namespace RollerCoaster
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Choose implemetation:");
            Console.WriteLine("1 - Monitor:");
            Console.WriteLine("2 - SemaphoreSlim:");
            Console.WriteLine("3 - Channel async:");
            Console.WriteLine("4 - SemaphoreSlim async:");

            int number = int.Parse(Console.ReadLine());
            switch (number)
            {
                case 1:
                    RunSync(new SantasWorkshop1());
                    break;
                case 2:
                    RunSync(new SantasWorkshop2());
                    break;
                case 3:
                    await RunAsync(new SantasWorkshopAsync1());
                    break;
                case 4:
                    await RunAsync(new SantasWorkshopAsync2());
                    break;
                default:
                    Console.WriteLine("Invalid choise.");
                    return;
            }
        }

        private static void RunSync(ISantasWorkshop workshop)
        {
            Santa santa = new Santa(workshop);
            Thread t = new Thread(santa.Run);
            t.Name = "Santa";
            t.Start();

            for (int i = 1; i < 11; i++)
            {
                Elf elf = new Elf(workshop);
                t = new Thread(elf.Run);
                t.Name = "Elf " + i;
                t.Start();
            }
            for (int i = 1; i < 10; i++)
            {
                Reindeer reindeer = new Reindeer(workshop);
                t = new Thread(reindeer.Run);
                t.Name = "Reindeer " + i;
                t.Start();
            }
        }

        private static async Task RunAsync(SantasWorkshopAsync workshopAsync)
        {
            List<Task> tasks = new List<Task>();

            Santa santa = new Santa(workshopAsync);
            tasks.Add(santa.RunAsync());

            for (int i = 1; i < 11; i++)
            {
                Elf elf = new Elf(workshopAsync, "Elf " + i);
                tasks.Add(elf.RunAsync());
            }
            for (int i = 1; i < 10; i++)
            {
                Reindeer reindeer = new Reindeer(workshopAsync, "Reindeer " + i);
                tasks.Add(reindeer.RunAsync());
            }

            await Task.WhenAll(tasks);
        }
    }
}

