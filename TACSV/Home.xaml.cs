using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;

namespace TACSV
{
    public partial class HomePage : Page
    {
        public HomePage()
        {
            InitializeComponent();
        }

		private void Page_Loaded(object sender, RoutedEventArgs e)
		{
			ConnectionStatus.Text = Program.Ground.IsOpen ? "Connected!" : "Not connected :(";
			Program.Ground.OnConnectionOpen += (_,_) => ConnectionStatus.Text = "Connected!";
			Program.Ground.OnConnectionClosed += (_, _) => ConnectionStatus.Text = "Not Connected :(";
		}

		private void Page_Unloaded(object sender, RoutedEventArgs e)
		{
			Program.Ground.OnConnectionOpen -= (_, _) => ConnectionStatus.Text = "Connected!";
			Program.Ground.OnConnectionClosed -= (_, _) => ConnectionStatus.Text = "Not Connected :(";
		}
	}
}
