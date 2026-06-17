// ThreadPool
namespace ProducerConsumer
{
    public class GenerateThreads2 : IGenerateThreads
    {
        public void Generate(IWork work, int producers, int consumers)
        {
            for (int i = 0; i < producers; i++)
            {
                int index = i;
                ThreadPool.QueueUserWorkItem(_ =>
                {
                    Producer producer = new Producer(work, "Producer " + index);
                    producer.Run();
                });
            }
            for (int i = 0; i < consumers; i++)
            {
                int index = i;
                ThreadPool.QueueUserWorkItem(_ =>
                {
                    Consumer consumer = new Consumer(work, "Consumer " + index);
                    consumer.Run();
                });
            }
        }
    }
}