﻿namespace TACSV.ViewModels
{
	public class HomeViewModel: ViewModelBase
	{
		private string _connected;
		public string Connected { get { return _connected; } set { _connected = value; OnPropertyChanged(nameof(Connected)); } }

		public HomeViewModel()
		{
			Connected = Program.Ground.IsOpen ? "Connected" : "Not connected :(";
			Program.Ground.OnConnectionOpen += (_, _) => Connected = "Connected!";
			Program.Ground.OnConnectionClosed += (_, _) => Connected = "Not connected :(";
		}
	}
}
