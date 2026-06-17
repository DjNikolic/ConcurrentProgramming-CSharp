namespace SantaClaus
{
    public class Reindeer
    {
        private readonly ISantasWorkshop _workshop;
        private readonly Random _random = new Random();
        public Reindeer(ISantasWorkshop workshop)
        {
            _workshop = workshop;
        }

        private readonly SantasWorkshopAsync _workshopAsync;
        private readonly string _name;
        public Reindeer(SantasWorkshopAsync workshopAsync, string name)
        {
            _workshopAsync = workshopAsync;
            _name = name;
        }
        public void Run()
        {
            while (true)
            {
                Thread.Sleep(_random.Next(1000)); //rest()
                Console.WriteLine(Thread.CurrentThread.Name + " is ready for delivery");
                _workshop.ReindeerIsReady();
                Console.WriteLine(Thread.CurrentThread.Name + " is back from delivery");
            }
        }
        public async Task RunAsync()
        {
            while (true)
            {
                await Task.Delay(_random.Next(1000)); //rest()
                Console.WriteLine(_name + " is ready for delivery");
                await _workshopAsync.ReindeerIsReadyAsync();
                Console.WriteLine(_name + " is back from delivery");
            }
        }
    }
}