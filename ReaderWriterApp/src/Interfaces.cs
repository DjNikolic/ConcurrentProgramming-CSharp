namespace ReaderWriter
{
    public interface ISharedResource
    {
        public int read();
        public void write(int value);
    }

    public interface IAsyncSharedResource
    {
        public int read();
        public void write(int value);
    }
}