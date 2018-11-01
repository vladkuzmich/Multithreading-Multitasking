using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace MultiThread.Common
{
    public class WorkerMultiTask
    {
        private readonly int _workId;

        public WorkerMultiTask(int workId)
        {
            _workId = workId;
        }

        public int Work(object obj)
        {
            var taskEntry = obj as TaskEntry;

            int delay = taskEntry.Delay;

            CancellationToken token = taskEntry.Token;

            for (var i = 0; i <= 99; i++)
            {
                token.ThrowIfCancellationRequested();

                Thread.Sleep(delay);

                ProgressChanged(i, _workId);
            }

            return delay;
        }

        public event Action<int, int> ProgressChanged;
    }
}
