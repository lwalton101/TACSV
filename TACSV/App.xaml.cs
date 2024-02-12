using System;
using System.Windows;
using System.Windows.Threading;

namespace TACSV
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void App_OnDispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            MessageBox.Show("Error" + Environment.NewLine + e.Exception.Message, "Error");
            e.Handled = true;
        }
    }
}