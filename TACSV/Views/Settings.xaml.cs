
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
	}
}
