using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

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
            if(ConsoleInput.Text == "")
            {
                e.Handled = false;
                return;
            }
            if(e.Key == Key.Enter)
            {
                Trace.WriteLine("Enter has been pressed");
                TACSVConsole.Command(ConsoleInput.Text);
                ConsoleInput.Text = "";
                
                ConsoleScrollViewer.ScrollToBottom();
            }
		}

		private void Page_Loaded(object sender, RoutedEventArgs e)
		{
            Trace.WriteLine("Loaded console page");
			TACSVConsole.OnNewMessage += TACSVConsole_OnNewMessage;
			RefreshConsole();
		}

		private void TACSVConsole_OnNewMessage(string message)
		{
            Trace.WriteLine("OnNewMessage Fired");
            RefreshConsole();
			ConsoleScrollViewer.ScrollToBottom();
		}

        private void RefreshConsole() => ConsoleTextBlock.Text = "TACSV Console\nType 'help' for more info\n\n" + TACSVConsole.GetEntriesString();
	}
}
