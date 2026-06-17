//Monitor
namespace SantaClaus
{
    public class SantasWorkshop1 : ISantasWorkshop
    {
        private readonly object _lock = new object();
        private int _elfCount = 0;
        private int _reindeerCount = 0;
        private bool _santaDoneE = false;
        private bool _santaDoneR = false;
        private bool _elvesCanEnter = true;
        private bool _reindeersCanEnter = true;

        public void ElfNeedsHelp()
        {
            lock (_lock)
            {
                while (!_elvesCanEnter || _elfCount == 3)
                {
                    Monitor.Wait(_lock);
                }
                if (++_elfCount == 3)
                {
                    _elvesCanEnter = false;
                    Console.WriteLine("3 elves want help");
                    Monitor.PulseAll(_lock);
                }
                while (!_santaDoneE)
                {
                    Monitor.Wait(_lock);
                }
                if (--_elfCount == 0)
                {
                    _santaDoneE = false;
                    _elvesCanEnter = true;
                    Monitor.PulseAll(_lock);
                }
            }
        }

        public void ReindeerIsReady()
        {
            lock (_lock)
            {
                while (!_reindeersCanEnter)
                {
                    Monitor.Wait(_lock);
                }
                if (++_reindeerCount == 9)
                {
                    _reindeersCanEnter = false;
                    Console.WriteLine("All 9 rendeers are ready");
                    Monitor.PulseAll(_lock);
                }
                while (!_santaDoneR)
                {
                    Monitor.Wait(_lock); // waiting for Santa to prepare sleads
                }
                if (--_reindeerCount == 0)
                {
                    _santaDoneR = false;
                    _reindeersCanEnter = true;
                    Monitor.PulseAll(_lock);
                }
            }
            Thread.Sleep(500); // riding()
        }

        public void SantaWork()
        {
            Boolean reindeerCase = false;
            lock (_lock)
            {
                while (_elfCount < 3 && _reindeerCount < 9)
                {
                    Monitor.Wait(_lock);
                }
                reindeerCase = _reindeerCount == 9;
            }
            if (reindeerCase)
            {
                Console.WriteLine(Thread.CurrentThread.Name + " is preparing the sleds");
                Thread.Sleep(200); // preparing the sleds
                Console.WriteLine(Thread.CurrentThread.Name + " finished preparing the sleds");
                lock (_lock)
                {
                    _santaDoneR = true;
                    Monitor.PulseAll(_lock);
                }
            }
            else
            {
                Console.WriteLine(Thread.CurrentThread.Name + " is helping the elves");
                Thread.Sleep(300);
                Console.WriteLine(Thread.CurrentThread.Name + " finished helping the elves");
                lock (_lock)
                {
                    _santaDoneE = true;
                    Monitor.PulseAll(_lock);
                }
            }
        }
    }
}