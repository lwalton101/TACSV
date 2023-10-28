using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TACSV
{
    class TACSVConsole
    {
        private static List<String> entries = new List<String>();
        public static event Action<String> OnNewMessage;


        public static void Log(string message)
        {
			Trace.WriteLine("Logging TACSVConsole");
			entries.Add(message);
            OnNewMessage?.Invoke(message);
        }

        public static void Command(string message)
        {
            Log("> " + message);
            CommandExecuter.Execute(message);
            Log("");
        }

        public static string GetEntriesString()
        {
            if(entries.Count == 0)
            {
                return "> ";
            }
            return string.Join("\n", entries) + "\n> ";
        }
    }
}
