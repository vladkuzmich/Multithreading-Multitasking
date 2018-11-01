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

namespace TaskFactory.WinForms
{
    public partial class Form1 : Form
    {
        private ProgressBarSample progressBarSample;
        public Form1()
        {
            InitializeComponent();
            ExecuteTime();
        }

        public void ExecuteTime()
        {
            Task.Factory.StartNew(() =>
            {
                while(true)
                {
                    label1.Text = DateTime.Now.ToLongTimeString();
                }
            });
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            button1.Enabled = false;
            progressBarSample = new ProgressBarSample();
            progressBarSample.ProgressChanged += OnProgressChanged;

            //progressBarSample.WorkCompleted += OnWorkCompleted;
            var cancelled = await Task<bool>.Factory.StartNew(() => progressBarSample.DoWork());
            
            string message = cancelled ? "Work was cancelled" : "Work was done";
            MessageBox.Show(message);
            button1.Enabled = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (progressBarSample != null)
            {
                progressBarSample.Cancel();
            }
        }

        public void OnProgressChanged(int value)
        {
            progressBar1.Value = value;
        }
    }
}
