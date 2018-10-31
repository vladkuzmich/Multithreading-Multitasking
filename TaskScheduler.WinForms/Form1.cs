using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TaskFactory.Common;

namespace TaskScheduler.WinForms
{
    public partial class Form1 : Form
    {
        private ProgressBarSample progressBarSample;
        private System.Threading.Tasks.TaskScheduler taskScheduler;
        public Form1()
        {
            InitializeComponent();

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            taskScheduler = System.Threading.Tasks.TaskScheduler.FromCurrentSynchronizationContext();
            System.Threading.Tasks.Task.Factory.StartNew(() =>
            {
                while (true)
                {
                    label1.Text = DateTime.Now.ToLongTimeString();
                }
            });
        }

        private void button1_Click(object sender, EventArgs e)
        {
            progressBarSample = new ProgressBarSample();
            progressBarSample.ProgressChanged += OnProgressChanged;

            button1.Enabled = false;
            // Do work execute in new thread, but work completed will be start in main thread (ui-thread)
            System.Threading.Tasks.Task.Factory
                .StartNew(() => progressBarSample.DoWork())
                .ContinueWith((t, o) => WorkCompleted(t.Result), null, taskScheduler);
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }
        private void WorkCompleted(bool cancelled)
        {
            string message = cancelled ? "Was cancelled" : "Was done";
            MessageBox.Show(message);
            button1.Enabled = true;
        }

        private void OnProgressChanged(int value)
        {
            progressBar1.Value = value;
        }
    }
}
