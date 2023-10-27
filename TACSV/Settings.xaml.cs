using System;
using System.Collections.Generic;
using System.IO.Ports;
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
	/// Interaction logic for Settings.xaml
	/// </summary>
	public partial class Settings : Page
	{
		public Settings()
		{
			InitializeComponent();
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
