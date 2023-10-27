using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;
using static System.Net.Mime.MediaTypeNames;

namespace TACSV
{
    /// <summary>
    /// Interaction logic for Graphs.xaml
    /// </summary>
    public partial class Graphs : Page
    {

        Random r = new Random();
		List<double> dataX = new List<double>();
		List<double> dataY = new List<double>();

		DispatcherTimer autoRefreshGraphTimer = new DispatcherTimer();

        public Graphs()
        {
            InitializeComponent();
			dataX.Add(0);
			dataY.Add(0);
		}

		private void Page_Loaded(object sender, RoutedEventArgs e)
		{
			PlotControl.Plot.Add.Scatter(dataX, dataY);
			PlotControl.Plot.AutoScale();
			PlotControl.Refresh();

			RefreshTimeTextBox.Text = Program.Options.GraphAutoRefreshTime.ToString();

			autoRefreshGraphTimer.Interval = TimeSpan.FromSeconds(Program.Options.GraphAutoRefreshTime);
			autoRefreshGraphTimer.Tick += (sender, e) => RefreshGraph();

			//AutoRefreshCheckBox.IsChecked = Program.options.GraphAutoRefreshEnabled;
			RefreshGraph();
			ControlAutoRefreshTimer();
		}

		private void RefreshGraph()
		{
			//Poll data
			Trace.WriteLine("Updating graph");
			PlotControl.Refresh();

			
		}

		private void AutoScaleButton_Click(object sender, RoutedEventArgs e)
		{
			PlotControl.Plot.AutoScale();
			RefreshGraph();
		}

		private void RefreshButton_Click(object sender, RoutedEventArgs e)
		{
			RefreshGraph();
		}

		private void RefreshTimeTextBox_TextChanged(object sender, TextChangedEventArgs e)
		{
			var textBox = (TextBox)e.Source;
			string text = textBox.Text;
			try
			{
				Program.Options.GraphAutoRefreshTime = int.Parse(text);
				autoRefreshGraphTimer.Interval = TimeSpan.FromSeconds(Program.Options.GraphAutoRefreshTime);
			}
			catch
			{
				foreach(var change in e.Changes)
				{
					textBox.Text = text.Substring(0, textBox.Text.Length - (change.AddedLength));
				}
			}
		}

		private void AutoRefreshCheckBox_Click(object sender, RoutedEventArgs e)
		{
			CheckBox checkBox = (CheckBox)e.Source;
			Program.Options.GraphAutoRefreshEnabled = (bool)checkBox.IsChecked;
			ControlAutoRefreshTimer();
		}

		private void ControlAutoRefreshTimer()
		{
			if (Program.Options.GraphAutoRefreshEnabled)
			{
				autoRefreshGraphTimer.Interval = TimeSpan.FromSeconds(Program.Options.GraphAutoRefreshTime);
				autoRefreshGraphTimer.Start();
			}
			else
			{
				autoRefreshGraphTimer.Stop();
			}
		}
	}
}
