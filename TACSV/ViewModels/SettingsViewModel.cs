using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TACSV.ViewModels
{
	public class SettingsViewModel : ViewModelBase
	{
		private int _comPort;
		public int ComPort { get { return _comPort; } set { _comPort = value; OnPropertyChanged(nameof(ComPort)); } }
		
		private string[] _comPorts;
		public string[] ComPorts { get { return _comPorts; } set { _comPorts = value; OnPropertyChanged(nameof(ComPorts)); } }
		
		public RelayCommand RefreshCommand { get; set; }
		public RelayCommand ConnectCommand { get; set; }

		public SettingsViewModel()
		{
			RefreshCommand = new RelayCommand(RefreshSettings);
			ConnectCommand = new RelayCommand(ConnectToGround);
			RefreshSettings(null);
		}

		private void RefreshSettings(object obj)
		{
			PopulateComBox();
			Debug.WriteLine("Reloading");
		}

		private void ConnectToGround(object obj)
		{
			Program.Ground.ComPort = ComPorts[ComPort];
			Program.Ground.BaudRate = 9600;
			Program.Ground.Connect();
		}
		
		private void PopulateComBox()
		{
			ComPorts = SerialPort.GetPortNames();
		}
	}
}
