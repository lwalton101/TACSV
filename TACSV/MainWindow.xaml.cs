using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using ModernWpf.Controls;

namespace TACSV
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        int number = 0;
        public string entries = "";
        public MainWindow()
        {
            InitializeComponent();
        }

		private void Button_Click(object sender, RoutedEventArgs e)
		{
            Trace.WriteLine(((Button)e.OriginalSource).Content.ToString());
            number++;
		}

		private void ListView_Selected(object sender, RoutedEventArgs e)
		{
            if (e.OriginalSource == null | MainPanel == null)
            {
                return;
            }
            var listView = e.OriginalSource as ModernWpf.Controls.ListViewItem;
            MainPanel.Navigate(new Uri($"{listView.Content}.xaml", UriKind.Relative));
        }

		private void MainPanel_Navigated(object sender, System.Windows.Navigation.NavigationEventArgs e)
		{
            //Removes history bar from top of panel
            MainPanel.NavigationService.RemoveBackEntry();
		}

		private void Window_Closed(object sender, EventArgs e)
		{
            DataManager.CloseDatabase();
            Environment.Exit(Environment.ExitCode);
		}

		private void Window_Loaded(object sender, RoutedEventArgs e)
		{
			Trace.WriteLine("Register Commands(main winow)");
			CommandExecuter.RegisterCommands();
            DataManager.InitialiseDatabase();
		}
	}
}