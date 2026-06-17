namespace SantaClaus
{
    public class Santa
    {
        private readonly ISantasWorkshop _workshop;
        private readonly SantasWorkshopAsync _workshopAsync;
        public Santa(ISantasWorkshop workshop)
        {
            _workshop = workshop;
        }
        public Santa(SantasWorkshopAsync workshopAsync)
        {
            _workshopAsync = workshopAsync;
        }
        public void Run()
        {
            while (true)
            {
                _workshop.SantaWork();
            }
        }
        public async Task RunAsync()
        {
            while (true)
            {
                await _workshopAsync.SantaWorkAsync();
            }
        }
    }
}