namespace SantaClaus
{
    public interface ISantasWorkshop
    {
        void SantaWork();
        void ElfNeedsHelp();
        void ReindeerIsReady();
    }

    public abstract class SantasWorkshopAsync
    {
        public abstract Task SantaWorkAsync();
        public abstract Task ElfNeedsHelpAsync();
        public abstract Task ReindeerIsReadyAsync();

        protected async Task PrepareSledsAsync()
        {
            Console.WriteLine("Santa is preparing the sleds");
            await Task.Delay(200); // preparing the sleds
            Console.WriteLine("Santa finished preparing the sleds");
        }

        protected async Task HelpElvesAsync()
        {
            Console.WriteLine("Santa is helping the elves");
            await Task.Delay(300);
            Console.WriteLine("Santa finished helping the elves");
        }
    }
}