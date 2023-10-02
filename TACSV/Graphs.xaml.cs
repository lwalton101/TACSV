using ScottPlot;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TACSV
{
    /// <summary>
    /// Interaction logic for Graphs.xaml
    /// </summary>
    public partial class Graphs : Page
    {

        Random r = new Random();
        public Graphs()
        {
            InitializeComponent();
            
        }

		private void Page_Loaded(object sender, RoutedEventArgs e)
		{
			double[] dataX = new double[] { 1, 2, 3, 4, 5 };
			double[] dataY = new double[] { 1, 4, 9, 16, 25 };

			PlotControl.Plot.Add.Scatter(dataX, dataY);
			PlotControl.Refresh();

            Trace.WriteLine("COCK");
		}
	}
}
