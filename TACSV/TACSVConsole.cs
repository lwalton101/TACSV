using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace TACSV
{
    class TACSVConsole
    {
        public static ObservableCollection<String> Entries = new ObservableCollection<string>();


        public static void Log(object message)
        {
			Trace.WriteLine("Logging to TACSVConsole");
			Entries.Add($"[{DateTime.Now}] {message}");
        }

        public static string GetEntriesString()
        {
            return string.Join("\n", Entries);
        }
    }
}
