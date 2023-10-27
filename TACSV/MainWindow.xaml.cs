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
            MainPanel.NavigationService.RemoveBackEntry();
		}

		private void Window_Closed(object sender, EventArgs e)
		{
            Environment.Exit(Environment.ExitCode);
		}
	}
}