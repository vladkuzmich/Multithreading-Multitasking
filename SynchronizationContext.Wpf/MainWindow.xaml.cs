using System.Threading;
using System.Windows;
using System.Windows.Threading;
using SynchronizationContext.Common.Samples;

namespace SynchronizationContext.Wpf
{
    public partial class MainWindow : Window
    {
        private ProgressBarSample _progressBarSample;
        private readonly DispatcherSynchronizationContext synchronizationContext; // SynchronizationContext special for WPF

        public MainWindow()
        {
            InitializeComponent();
            synchronizationContext = System.Threading.SynchronizationContext.Current as DispatcherSynchronizationContext;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            _progressBarSample = new ProgressBarSample();
            _progressBarSample.ProgressChanged += OnProgressChanged;
            _progressBarSample.WorkCompleted += OnWorkCompleted;

            var thread = new Thread(() => _progressBarSample.Work(synchronizationContext));
            thread.Start();
            StartBut.IsEnabled = false;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            var thread = new Thread(() => _progressBarSample.Cancel(synchronizationContext));
            thread.Start();
        }

        private void OnProgressChanged(int i)
        {
            progressbar1.Value = i + 1;
            progressbar1.Value = i;
        }

        private void OnWorkCompleted(bool _cancelled)
        {
            string message = _cancelled ? "Process was canceled" : "Process is done";
            MessageBox.Show(message);
            StartBut.IsEnabled = true;
        }
    }
}
