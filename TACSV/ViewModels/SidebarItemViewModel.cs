using System.Windows;
using System.Windows.Input;

namespace TACSV.ViewModels
{
	internal class SidebarItemViewModel : ViewModelBase
	{
		private string _name;
		public string Name { get { return _name; } set { _name = value; OnPropertyChanged(nameof(Name)); } }
		
		private RelayCommand _itemSelectedCommand;
		public ICommand ItemSelectedCommand
		{
			get
			{
				if (_itemSelectedCommand == null)
				{
					_itemSelectedCommand = new RelayCommand(ExecuteItemSelectedCommand);
				}
				return _itemSelectedCommand;
			}
		}

		private void ExecuteItemSelectedCommand(object parameter)
		{
			// Handle item selection logic here
			MessageBox.Show($"Item '{Name}' selected!");
		}
	}
}
