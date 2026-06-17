// Task
namespace ProducerConsumer
{
    public class GenerateThreads3 : IGenerateThreads
    {
        public void Generate(IWork work, int producers, int consumers)
        {
            for (int i = 0; i < producers; i++)
            {
                int index = i;
                Task.Factory.StartNew(() =>
                    {
                        Producer producer = new Producer(work, "Producer " + index);
                        producer.Run();
                    },
                    TaskCreationOptions.LongRunning
                );
            }
            for (int i = 0; i < consumers; i++)
            {
                int index = i;
                Task.Factory.StartNew(() =>
                    {
                        Consumer consumer = new Consumer(work, "Consumer " + index);
                        consumer.Run();
                    },
                    TaskCreationOptions.LongRunning
                );
            }
        }
    }
}