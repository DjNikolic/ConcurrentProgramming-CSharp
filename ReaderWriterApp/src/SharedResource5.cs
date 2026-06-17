//Task + ConcurrentExclusiveSchedulerPair
namespace ReaderWriter
{
    public class SharedResource5 : ISharedResource
    {
        private int _data = 0;
        private readonly ConcurrentExclusiveSchedulerPair _schedulerPair = new ConcurrentExclusiveSchedulerPair();
        public int read()
        {
            string name = Thread.CurrentThread.Name;
            Task<int> task = Task.Factory.StartNew(() =>
            {
                Console.WriteLine(name + " has started reading");
                int value = _data;
                Thread.Sleep(200);
                return value;

            }, CancellationToken.None,
            TaskCreationOptions.None,
            _schedulerPair.ConcurrentScheduler);

            return task.Result;
        }

        public void write(int value)
        {
            string name = Thread.CurrentThread.Name;
            Task task = Task.Factory.StartNew(() =>
            {
                Console.WriteLine(name + " has started writing");
                _data = value;
                Thread.Sleep(300);
            }, CancellationToken.None,
            TaskCreationOptions.None,
            _schedulerPair.ExclusiveScheduler);

            task.Wait();
        }
    }
}