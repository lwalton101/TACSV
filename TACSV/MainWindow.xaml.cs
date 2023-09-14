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

		private void CloseButton_Click(object sender, RoutedEventArgs e)
		{
            Close();
		}

		private void MaximiseButton_Click(object sender, RoutedEventArgs e)
		{
			MaxHeight = SystemParameters.MaximizedPrimaryScreenHeight;
			MaxWidth = SystemParameters.MaximizedPrimaryScreenWidth;
			if (this.WindowState == WindowState.Maximized)
				this.WindowState = WindowState.Normal;
			else
				this.WindowState = WindowState.Maximized;
		}

		private void MinimiseButton_Click(object sender, RoutedEventArgs e)
		{
			this.WindowState = WindowState.Minimized;
		}

		private void TitleBar_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
		{
			if(e.ChangedButton == System.Windows.Input.MouseButton.Left)
			{
				this.DragMove();
			}
		}
	}
}