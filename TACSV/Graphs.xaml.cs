﻿using System;
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
		}

		private void AutoScaleButton_Click(object sender, RoutedEventArgs e)
		{
			PlotControl.Plot.AutoScale();
			PlotControl.Refresh();
		}

		private void RefreshButton_Click(object sender, RoutedEventArgs e)
		{
			PlotControl.Refresh();
		}

		private void RefreshTimeTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
		{
			try
			{
				Program.options.GraphAutoRefreshTime = int.Parse(((TextBox)e.Source).Text + e.Text);
			}
			catch
			{
				e.Handled = true;
			}
			
		}

		private void RefreshTimeTextBox_Pasting(object sender, DataObjectPastingEventArgs e)
		{
			Regex regex = new Regex("[0-9]");
			if (e.DataObject.GetDataPresent(typeof(String)))
			{
				String text = (String)e.DataObject.GetData(typeof(String));
				if (!regex.IsMatch(text))
				{
					e.CancelCommand();
				}
				else
				{
					try
					{
						Program.options.GraphAutoRefreshTime = int.Parse(((TextBox)e.Source).Text + text);
					}
					catch
					{
						e.CancelCommand();
						e.Handled = true;
					}
				}
			}
			else
			{
				e.CancelCommand();
			}
		}
	}
}
