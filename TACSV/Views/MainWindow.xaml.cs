using System;
using System.Collections.Generic;
using System.IO;
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
			InfluxDbManager.StartThread();
			
			string path = "C:\\Users\\lukew\\Downloads\\TEST.txt";
			List<string> lines = new List<string>();
			// Open the file and read lines
				using (StreamReader sr = new StreamReader(path))
				{
					string line;
					// Read until the end of the file
					while ((line = sr.ReadLine()) != null)
					{
						// Add each line to the list
						lines.Add(line);
					}
				}

				// Output the lines read from the file
				foreach (string line in lines)
				{
					InfluxDbManager.OnMessage(null, line);
				}
				
			
		}
	}
}