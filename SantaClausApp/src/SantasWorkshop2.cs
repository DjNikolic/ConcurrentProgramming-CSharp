//SemaphoreSlim
namespace SantaClaus
{
    public class SantasWorkshop2 : ISantasWorkshop
    {
        private int _elfCount = 0;
        private int _reindeerCount = 0;
        private readonly SemaphoreSlim _mutex = new SemaphoreSlim(1, 1);
        private readonly SemaphoreSlim _santaSem = new SemaphoreSlim(0, 2);
        private readonly SemaphoreSlim _reindeerSem = new SemaphoreSlim(0, 9);

        private readonly SemaphoreSlim _elfEnterSem = new SemaphoreSlim(3, 3);
        private readonly SemaphoreSlim _elfExitSem = new SemaphoreSlim(0, 3);


        public void ElfNeedsHelp()
        {
            _elfEnterSem.Wait();
            _mutex.Wait();
            if (++_elfCount == 3)
            {
                Console.WriteLine("3 elves want help");
                _santaSem.Release();
            }
            _mutex.Release();
            _elfExitSem.Wait();
            _mutex.Wait();
            if (--_elfCount == 0)
            {
                _elfEnterSem.Release(3);
            }
            _mutex.Release();
        }

        public void ReindeerIsReady()
        {
            _mutex.Wait();
            if (++_reindeerCount == 9)
            {
                Console.WriteLine("All 9 rendeers are ready");
                _santaSem.Release();
            }
            _mutex.Release();
            _reindeerSem.Wait(); // waiting for Santa to prepare sleads
            Thread.Sleep(500); // riding()
        }

        public void SantaWork()
        {
            bool reindeerCase;
            _santaSem.Wait();
            _mutex.Wait();
            reindeerCase = _reindeerCount == 9;
            _mutex.Release();
            if (reindeerCase)
            {
                Console.WriteLine(Thread.CurrentThread.Name + " is preparing the sleds");
                Thread.Sleep(200); // preparing the sleds
                Console.WriteLine(Thread.CurrentThread.Name + " finished preparing the sleds");
                _mutex.Wait();
                _reindeerCount = 0;
                _mutex.Release();
                _reindeerSem.Release(9);
            }
            else
            {
                Console.WriteLine(Thread.CurrentThread.Name + " is helping the elves");
                Thread.Sleep(300);
                Console.WriteLine(Thread.CurrentThread.Name + " finished helping the elves");
                _elfExitSem.Release(3);
            }
        }
    }
}