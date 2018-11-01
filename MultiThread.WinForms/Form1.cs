using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using MultiThread.Common;

namespace MultiThread.WinForms
{
    public partial class Form1 : Form
    {
        private CancellationTokenSource _cancellationTokenSource;
        public Form1()
        {
            InitializeComponent();
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            _cancellationTokenSource = new CancellationTokenSource();
            var token = _cancellationTokenSource.Token;
            var tasks = new List<Task<int>>();

            for (var i = 0; i <= 5; i++)
            {
                var worker = new WorkerMultiTask(i);
                worker.ProgressChanged += OnProgressChanged;

                var task = Task<int>.Factory.StartNew(worker.Work, new TaskEntry { Delay = i * 10, Token = token }, token);
                tasks.Add(task);
            }

            try
            {
                var result = await Task<int>.Factory.ContinueWhenAll(tasks.ToArray(), ts => ts.Sum(t => t.Result));

                MessageBox.Show($"Процесс завершен. Результат {result}");
            }
            catch (AggregateException ex)
            {
                // So that we have 5 tasks with the same token, when we put Cancel, there are 5 OperationCancelledExceptions called,
                // AggregateException contains all of them and with the hepl of Flatter/Handle methods we can handle all of them with the same logic
                // in this case we just show textbox with message
                ex.Flatten().Handle(exc =>
                {
                    string message = exc is TaskCanceledException ? "Процесс отменен" : exc.Message;
                    MessageBox.Show(message);
                    return true;
                });
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            _cancellationTokenSource.Cancel();
        }

        private void OnProgressChanged(int value, int threadId)
        {
            var progressBar = 
                Controls.Find($"progressBar{threadId}", false).SingleOrDefault() as ProgressBar;

            if (progressBar != null)
            {
                Invoke(new Action(() => 
                {
                    progressBar.Value = value + 1;
                    progressBar.Value = value;
                }));
            }
        }
    }
}
