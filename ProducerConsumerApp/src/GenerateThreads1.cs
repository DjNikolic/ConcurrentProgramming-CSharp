// Thread
using System.Threading;

namespace ProducerConsumer
{
    public class GenerateThreads1 : IGenerateThreads
    {
        public void Generate(IWork work, int producers, int consumers)
        {
            for (int i = 0; i < producers; i++)
            {
                Producer producer = new Producer(work);
                Thread t = new Thread(producer.Run);
                t.Name = "Producer " + i;
                t.Start();
            }
            for (int i = 0; i < consumers; i++)
            {
                Consumer consumer = new Consumer(work);
                Thread t = new Thread(consumer.Run);
                t.Name = "Consumer " + i;
                t.Start();
            }
        }
    }
}