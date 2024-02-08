﻿using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using ModernWpf.Controls;
using TACSV.ViewModels;
using ListView = ModernWpf.Controls.ListView;

namespace TACSV
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        int number = 0;
        public string entries = "";
        public MainWindow()
        {
            InitializeComponent();

            var viewModel = new MainWindowViewModel();
            DataContext = viewModel;
        }

		private void Window_Closed(object sender, EventArgs e)
		{
            DataManager.CloseDatabase();
            Environment.Exit(Environment.ExitCode);
		}

		private void Window_Loaded(object sender, RoutedEventArgs e)
		{
			Trace.WriteLine("Register Commands(main winow)");
			CommandExecuter.RegisterCommands();
            DataManager.InitialiseDatabase();
            Sidebar.SelectedIndex = 0;
		}
	}
}