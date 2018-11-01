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
using CancelletionTokenSource.Common;

namespace CancelletionTokenSource.WinForms
{
    public partial class Form1 : Form
    {
        private ProgressBarSample progressBarSample;
        private System.Threading.CancellationTokenSource cancellationTokenSource;
        private bool first;
        public Form1()
        {
            InitializeComponent();
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            progressBarSample = new ProgressBarSample();
            progressBarSample.ProgressChanged += OnProgressChanged;
            cancellationTokenSource = new System.Threading.CancellationTokenSource();
            var token = cancellationTokenSource.Token;
           
            string message = "";
            bool isError = false;

            try
            {
                var task = await Task<bool>.Factory.StartNew(() => progressBarSample.Work(token), token);
                //await task;
            }
            catch (OperationCanceledException)
            {

            }
            catch (Exception ex)
            {
                isError = true;
                message = $"Произошла ошибка {ex.Message}";
            }

            if (!isError)
            {
                message = true ? "Процесс отменет" : "Процесс завершен";
            }

            MessageBox.Show(message);
            
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void OnProgressChanged(int i)
        {
            //this.Invoke(new Action(() => */
            //progressBar1.Value = i+1; 
            if (!first)
            {
                first = true;
                progressBar1.Value = Thread.CurrentThread.ManagedThreadId; 
            }            
        }
        private void OnWorkCompleted(bool cancel)
        {
            string message = cancel ? "Операция была отменета" : "Операция завершена";
            MessageBox.Show(message);
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
