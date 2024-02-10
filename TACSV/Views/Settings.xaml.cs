
using System.IO.Ports;
using System.Windows;
using System.Windows.Controls;
using TACSV.ViewModels;

namespace TACSV
{
	/// <summary>
	/// Interaction logic for Settings.xaml
	/// </summary>
	public partial class Settings : UserControl
	{
		public Settings()
		{
			InitializeComponent();
			var viewModel = new SettingsViewModel();
			DataContext = viewModel;
		}

		private void Page_Loaded(object sender, RoutedEventArgs e)
		{
			PopulateCOMBox();
		}

		private void PopulateCOMBox()
		{
			var selectionBox = COMSelectionBox as ComboBox;
			selectionBox.Items.Clear();
			string[] ports = SerialPort.GetPortNames();
			foreach (string port in ports)
			{
				if(!selectionBox.Items.Contains(port))
				{
					selectionBox.Items.Add(port);
				}
				
			}
		}

		private void ReloadButton_Click(object sender, RoutedEventArgs e)
		{
			PopulateCOMBox();
		}

		private void ConnectButton_Click(object sender, RoutedEventArgs e)
		{
			Program.Ground.BaudRate = 9600;
			Program.Ground.ComPort = COMSelectionBox.Text;
			
			Program.Ground.Connect();
		}
	}
}
