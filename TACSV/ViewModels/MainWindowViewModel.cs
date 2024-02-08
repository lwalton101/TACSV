using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace TACSV.ViewModels
{
	internal class MainWindowViewModel : ViewModelBase
	{
		private ViewModelBase _currentViewModel;
		public ViewModelBase CurrentViewModel
		{
			get { return _currentViewModel; }
			set {
				if (_currentViewModel != value)
				{
					_currentViewModel = value;
					OnPropertyChanged(nameof(CurrentViewModel));
				}
			}
		}	 

		private ObservableCollection<SidebarItemViewModel> _sidebarModels;
		public ObservableCollection<SidebarItemViewModel> SidebarModels
		{
			get
			{
				_sidebarModels ??= new ObservableCollection<SidebarItemViewModel>
					{
						new SidebarItemViewModel { Name = "Home" },
						new SidebarItemViewModel { Name = "Settings" }
					};
				return _sidebarModels;
			}
		}

		private SidebarItemViewModel _selectedItem;
		public SidebarItemViewModel SelectedItem
		{
			get
			{
				return _selectedItem;
			}
			set
			{
				if(_selectedItem  != value) 
				{
					_selectedItem = value;
					OnPropertyChanged(nameof(SelectedItem));
					HandleSelectionChange();
				}
			}
		}

		private void HandleSelectionChange()
		{
			if(SelectedItem.Name == "Home")
			{
				CurrentViewModel = new HomeViewModel();
			}
			else
			{
				CurrentViewModel = new SettingsViewModel();
			}
		}
	}
}
