namespace SantaClaus
{
    public class SantasWorkshopAsync2 : SantasWorkshopAsync
    {
        private int _elfCount = 0;
        private int _reindeerCount = 0;
        private readonly SemaphoreSlim _mutex = new SemaphoreSlim(1, 1);
        private readonly SemaphoreSlim _santaSem = new SemaphoreSlim(0, 2);
        private readonly SemaphoreSlim _reindeerSem = new SemaphoreSlim(0, 9);

        private readonly SemaphoreSlim _elfEnterSem = new SemaphoreSlim(3, 3);
        private readonly SemaphoreSlim _elfExitSem = new SemaphoreSlim(0, 3);
        public async override Task ElfNeedsHelpAsync()
        {
            await _elfEnterSem.WaitAsync();
            await _mutex.WaitAsync();
            if (++_elfCount == 3)
            {
                Console.WriteLine("3 elves want help");
                _santaSem.Release();
            }
            _mutex.Release();

            await _elfExitSem.WaitAsync();
            await _mutex.WaitAsync();
            if (--_elfCount == 0)
            {
                _elfEnterSem.Release(3);
            }
            _mutex.Release();
        }

        public async override Task ReindeerIsReadyAsync()
        {
            await _mutex.WaitAsync();
            if (++_reindeerCount == 9)
            {
                Console.WriteLine("All 9 rendeers are ready");
                _santaSem.Release();
            }
            _mutex.Release();
            await _reindeerSem.WaitAsync(); // waiting for Santa to prepare sleads
            await Task.Delay(500); // riding()
        }

        public async override Task SantaWorkAsync()
        {
            bool reindeerCase;
            await _santaSem.WaitAsync();
            await _mutex.WaitAsync();
            reindeerCase = _reindeerCount == 9;
            _mutex.Release();
            if (reindeerCase)
            {
                await PrepareSledsAsync();
                await _mutex.WaitAsync();
                _reindeerCount = 0;
                _mutex.Release();
                _reindeerSem.Release(9);
            }
            else
            {
                await HelpElvesAsync();
                _elfExitSem.Release(3);
            }
        }
    }
}