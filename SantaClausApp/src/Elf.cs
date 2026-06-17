namespace SantaClaus
{
    public class Elf
    {
        private readonly ISantasWorkshop _workshop;
        private readonly Random _random = new Random();
        public Elf(ISantasWorkshop workshop)
        {
            _workshop = workshop;
        }

        private readonly SantasWorkshopAsync _workshopAsync;
        private readonly string _name;
        public Elf(SantasWorkshopAsync workshopAsync, string name)
        {
            _workshopAsync = workshopAsync;
            _name = name;
        }

        public void Run()
        {
            while (true)
            {
                Thread.Sleep(_random.Next(1000)); //work()
                Console.WriteLine(Thread.CurrentThread.Name + " needs Santas help");
                _workshop.ElfNeedsHelp();
                Console.WriteLine(Thread.CurrentThread.Name + " got back to work");
            }
        }

        public async Task RunAsync()
        {
            while (true)
            {
                await Task.Delay(_random.Next(1000)); //work()
                Console.WriteLine(_name + " needs Santas help");
                await _workshopAsync.ElfNeedsHelpAsync();
                Console.WriteLine(_name + " got back to work");
            }
        }
    }
}