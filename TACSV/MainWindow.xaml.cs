using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;

namespace TACSV
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        int number = 0;
        public MainWindow()
        {
            InitializeComponent();
        }

		private void Button_Click(object sender, RoutedEventArgs e)
		{
            Trace.WriteLine(((Button)e.OriginalSource).Content.ToString());
            number++;
		}
	}
}