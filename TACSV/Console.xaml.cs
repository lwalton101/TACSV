using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TACSV
{
    /// <summary>
    /// Interaction logic for Console.xaml
    /// </summary>
    public partial class Console : Page
    {
        public Console()
        {
            InitializeComponent();
        }

		private void ConsoleInput_KeyDown(object sender, KeyEventArgs e)
		{
            if(e.Key == Key.Enter)
            {
                Trace.WriteLine("Enter has been pressed");
                TACSVConsole.Log(ConsoleInput.Text);
                ConsoleScrollViewer.ScrollToBottom();
            }
		}

		private void Page_Loaded(object sender, RoutedEventArgs e)
		{
            Trace.WriteLine("Loaded console page");
			TACSVConsole.OnNewMessage += TACSVConsole_OnNewMessage;
            ConsoleTextBlock.Text = "TACSV Console\n\n" + TACSVConsole.GetEntriesString();
		}

		private void TACSVConsole_OnNewMessage(string message)
		{
            Trace.WriteLine("OnNewMessage Fired");
            ConsoleTextBlock.Text = "TACSV Console\n\n" + TACSVConsole.GetEntriesString();
		}
	}
}
