using System;

namespace TaskFactory.Common
{
    public class ProgressBarSample
    {
        private bool _cancelled = false;

        public Action<bool> WorkCompleted;
        public Action<int> ProgressChanged;

        public bool DoWork()
        {
            for (var i = 0; i < 100; i++)
            {
                if (_cancelled)
                {
                    break;
                }
                System.Threading.Thread.Sleep(50);
                OnProgressChanged(i);
            }
            OnWorkCompleted(_cancelled);
            return _cancelled;
        }

        public void Cancel() => _cancelled = true;

        public void OnProgressChanged(int i) => ProgressChanged?.Invoke(i);

        public void OnWorkCompleted(bool cancelled) => WorkCompleted?.Invoke(cancelled);
    }
}
