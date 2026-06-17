//Channel
using System.Threading.Channels;

namespace RollerCoaster
{
    public class RollerCoaster3 : RollerCoaster
    {
        private readonly Channel<TaskCompletionSource<bool>> _enterChannel;
        private readonly Channel<TaskCompletionSource<bool>> _exitChannel;
        public RollerCoaster3(int capacity) : base(capacity)
        {
            _enterChannel = Channel.CreateUnbounded<TaskCompletionSource<bool>>(
                new UnboundedChannelOptions
                {
                    SingleReader = true,
                    SingleWriter = false
                });
            _exitChannel = Channel.CreateUnbounded<TaskCompletionSource<bool>>(
                new UnboundedChannelOptions
                {
                    SingleReader = true,
                    SingleWriter = false
                });
            StartRideThread();
        }

        private void StartRideThread()
        {
            Task.Run(async () =>
            {
                while (true)
                {
                    for (int i = 0; i < Capacity; i++)
                    {
                        var tcs = await _enterChannel.Reader.ReadAsync();
                        tcs.SetResult(true);
                    }
                    Console.WriteLine("All passengers boarded the ride");
                    Ride();
                    for (int i = 0; i < Capacity; i++)
                    {
                        var tcs = await _exitChannel.Reader.ReadAsync();
                        tcs.SetResult(true);
                    }
                    Console.WriteLine("All passengers got off the ride");
                }
            });
        }

        public override void BoardRide()
        {
            var tcs = new TaskCompletionSource<bool>();
            _enterChannel.Writer.WriteAsync(tcs).AsTask().GetAwaiter().GetResult();
            tcs.Task.GetAwaiter().GetResult();
            //Console.WriteLine(Thread.CurrentThread.Name + " boarded");
        }

        public override void LeaveRide()
        {
            var tcs = new TaskCompletionSource<bool>();
            _exitChannel.Writer.WriteAsync(tcs).AsTask().GetAwaiter().GetResult();
            tcs.Task.GetAwaiter().GetResult();
            //Console.WriteLine(Thread.CurrentThread.Name + " left");
        }
    }
}