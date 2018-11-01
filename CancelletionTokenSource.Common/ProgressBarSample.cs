using System;
using System.Threading;

namespace CancelletionTokenSource.Common
{
    public class ProgressBarSample
    {
        private bool _cancel;
        public event Action<int> ProgressChanged;


        public bool Work(CancellationToken cancelletionToken)
        {
            
            for (var i = 0; i < 100; i++)
            {
                cancelletionToken.ThrowIfCancellationRequested();
         
                Thread.Sleep(50);

                OnProgressChanged(i);
            }
            return _cancel;
        }

        public void Cancel() => _cancel = true;

        private void OnProgressChanged(int value)
        {
            if (ProgressChanged != null)
            {
                ProgressChanged(value);
            }
        }
    }
}
