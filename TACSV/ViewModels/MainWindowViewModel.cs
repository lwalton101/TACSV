using System.Collections.ObjectModel;

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
						new SidebarItemViewModel { Name = "Settings" },
						new SidebarItemViewModel { Name = "Console"}
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
			switch (SelectedItem.Name)
			{
				case "Home":
					CurrentViewModel = new HomeViewModel();
					break;
				case "Settings":
					CurrentViewModel = new SettingsViewModel();
					break;
				case "Console":
					CurrentViewModel = new ConsoleViewModel();
					break;
				default:
					break;
			}
		}
	}
}
