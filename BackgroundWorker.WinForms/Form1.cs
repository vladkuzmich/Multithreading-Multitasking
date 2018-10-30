using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BackgroundWorker.WinForms
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            button1.Click += button1_Click;
            button2.Click += button2_Click;

            backgroundWorker1.DoWork += backgroundWorker1_DoWork;
            backgroundWorker1.ProgressChanged += backgroundWorker1_ProgressChanged;
            backgroundWorker1.RunWorkerCompleted += backgroundWorker1_RunWorkerCompleted;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            backgroundWorker1.CancelAsync();
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            button2.Enabled = false;
            if (!backgroundWorker1.IsBusy)
            {
                backgroundWorker1.RunWorkerAsync();
            }
        }

        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressBar1.Value = e.ProgressPercentage;
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Error != null) // check if Error
            {
                MessageBox.Show($"Error: {e.Error.Message}");
            }
            else
            {
                string message = e.Cancelled ? "Process was cancelled" : "Process was done";
                MessageBox.Show(message);
                
            }
            button2.Enabled = true;
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            for (var i = 0; i <= 99; i++)
            {
                if (backgroundWorker1.CancellationPending)
                {
                    e.Cancel = true;
                    break;
                }

                // Throw some exception from non-ui thread
                //if (i == 30)
                //{
                //    throw new Exception("Opps, something went wrong...");
                //}
                 
                backgroundWorker1.ReportProgress(i);
                System.Threading.Thread.Sleep(50); //some work
            }
        }
    }
}
