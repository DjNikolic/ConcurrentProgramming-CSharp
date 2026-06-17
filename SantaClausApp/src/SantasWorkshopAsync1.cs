//Channel async
using System.Threading.Channels;

namespace SantaClaus
{
    public class SantasWorkshopAsync1 : SantasWorkshopAsync
    {
        private readonly Channel<TaskCompletionSource<bool>> _reindeersRequest = Channel.CreateUnbounded<TaskCompletionSource<bool>>(
                new UnboundedChannelOptions
                {
                    SingleReader = true,
                    SingleWriter = false
                });
        private readonly Channel<TaskCompletionSource<bool>> _elvesRequest = Channel.CreateUnbounded<TaskCompletionSource<bool>>(
                new UnboundedChannelOptions
                {
                    SingleReader = true,
                    SingleWriter = false
                });

        private readonly List<TaskCompletionSource<bool>> _elvesWaiting = new();
        private readonly List<TaskCompletionSource<bool>> _reindeersWaiting = new();

        public async override Task ElfNeedsHelpAsync()
        {
            var tcs = new TaskCompletionSource<bool>(TaskCreationOptions.RunContinuationsAsynchronously);
            await _elvesRequest.Writer.WriteAsync(tcs);
            await tcs.Task;
        }

        public async override Task ReindeerIsReadyAsync()
        {
            var tcs = new TaskCompletionSource<bool>(TaskCreationOptions.RunContinuationsAsynchronously);
            await _reindeersRequest.Writer.WriteAsync(tcs);
            await tcs.Task;
            await Task.Delay(500); //riding()
        }

        public async override Task SantaWorkAsync()
        {
            var waitForElf = _elvesRequest.Reader.WaitToReadAsync().AsTask();
            var waitForReindeer = _reindeersRequest.Reader.WaitToReadAsync().AsTask();
            await Task.WhenAny(waitForElf, waitForReindeer);

            while (_reindeersRequest.Reader.TryRead(out var temp))
            {
                _reindeersWaiting.Add(temp);
            }
            while (_elvesWaiting.Count < 3 && _elvesRequest.Reader.TryRead(out var temp))
            {
                _elvesWaiting.Add(temp);
            }

            if (_reindeersWaiting.Count == 9)
            {
                await PrepareSledsAsync();
                foreach (var temp in _reindeersWaiting)
                {
                    temp.SetResult(true);
                }
                _reindeersWaiting.Clear();
            }
            else if (_elvesWaiting.Count == 3)
            {
                await HelpElvesAsync();
                foreach (var temp in _elvesWaiting)
                {
                    temp.SetResult(true);
                }
                _elvesWaiting.Clear();
            }
        }
    }
}