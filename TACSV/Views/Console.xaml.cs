using System.Windows.Controls;
using TACSV.ViewModels;

namespace TACSV
{
    /// <summary>
    /// Interaction logic for Console.xaml
    /// </summary>
    public partial class Console : UserControl
    {
        public Console()
        {
            InitializeComponent();
            var viewModel = new ConsoleViewModel();
            DataContext = viewModel;
        }
    }
}
