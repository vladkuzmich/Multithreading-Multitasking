using System.Threading;

namespace MultiThread.Common
{
    public class TaskEntry
    {
        public int Delay { get; set; }
        public CancellationToken Token { get; set; }
    }
}
