using System;
using System.Threading;

namespace SynchronizationContext.Common.Samples
{
    public class ProgressBarSample
    {
        private bool _cancelled = false;

        public Action<int> ProgressChanged;
        public Action<bool> WorkCompleted;

        public void Work(System.Threading.SynchronizationContext synchronizationContext)
        {
            for (var i = 0; i < 100; i++)
            {
                if (_cancelled)
                {
                    break;
                }
                Thread.Sleep(50);

                synchronizationContext.Send(OnProgressChanged, i); //swith method calling in main thread
            }
            synchronizationContext.Send(OnWorkCompleted, _cancelled); //switch method calling in main thread
        }

        public void Cancel(System.Threading.SynchronizationContext synchronizationContext) => _cancelled = true;

        private void OnProgressChanged(object i) => ProgressChanged?.Invoke((int)i);

        private void OnWorkCompleted(object cancelled) => WorkCompleted?.Invoke((bool)cancelled);
    }
}
