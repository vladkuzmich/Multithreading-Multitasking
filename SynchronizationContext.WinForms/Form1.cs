using System;
using System.Threading;
using System.Windows.Forms;
using SynchronizationContext.Common.Samples;

namespace SynchronizationContext.WinForms
{
    public partial class Form1 : Form
    {
        private ProgressBarSample _progressBarSample;
        private WindowsFormsSynchronizationContext synchronizationContext; // SynchronizationContext special for winforms

        public Form1()
        {
            InitializeComponent();
            synchronizationContext = System.Threading.SynchronizationContext.Current as WindowsFormsSynchronizationContext; 
        }

        private void button1_Click(object sender, EventArgs e)
        {
            _progressBarSample = new ProgressBarSample();
            _progressBarSample.ProgressChanged += OnProgressChanged;
            _progressBarSample.WorkCompleted += OnWorkCompleted;

            var thread = new Thread(() => _progressBarSample.Work(synchronizationContext));
            thread.Start();
            button1.Enabled = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var thread = new Thread(() => _progressBarSample.Cancel(synchronizationContext));
            thread.Start();
        }

        private void OnProgressChanged(int i)
        {
            progressBar1.Value = i + 1;
            progressBar1.Value = i;
        }

        private void OnWorkCompleted(bool _cancelled)
        {
            string message = _cancelled ? "Process was canceled" : "Process is done";
            MessageBox.Show(message);
            button1.Enabled = true;
        }
    }
}
