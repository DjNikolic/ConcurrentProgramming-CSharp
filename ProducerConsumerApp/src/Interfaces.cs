namespace ProducerConsumer
{
    public interface IProducing
    {
        void Produce(int value);
    }
    public interface IConsuming
    {
        int Consume();
    }
    public interface IWork : IProducing, IConsuming
    { }

    public interface IGenerateThreads
    {
        void Generate(IWork work, int producers, int consumers);
    }
}