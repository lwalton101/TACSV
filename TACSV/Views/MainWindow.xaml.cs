using System;
using System.Windows;
using TACSV.Config;
using TACSV.ViewModels;

namespace TACSV
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
	    public MainWindow()
        {
            InitializeComponent();

            var viewModel = new MainWindowViewModel();
            DataContext = viewModel;
        }

		private void Window_Closed(object sender, EventArgs e)
		{
			Environment.Exit(Environment.ExitCode);
		}

		private void Window_Loaded(object sender, RoutedEventArgs e)
		{
			TACSVConsole.Log("TACSV Loaded!"); 
			ConfigManager.Initialise();
			Program.Ground.OnMessageRecieved += InfluxDbManager.OnMessage;
			Sidebar.SelectedIndex = 0;
		}
	}
}