using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace TACSV
{
	public class CommandExecuter
	{
		private static Dictionary<CommandAttribute, MethodInfo> commands = new();

		private static void RegisterCommand(CommandAttribute command, MethodInfo method) {
			if (commands.Count(x => x.Key.name == command.name) == 0)
			{
				MessageBox.Show($"Command with name {command.name} already exists!");
				return;
			}
			commands.Add(command, method);
		}

		public static void Execute(string commandString)
		{
			var commandParts = commandString.Split(" ");
			if (commandParts.Length <= 0)
			{
				TACSVConsole.Log($"Unknown command: {commandString}");
				return;
			}

			foreach(var pair in commands)
			{
				if(pair.Key.name == commandParts[0])
				{
					pair.Value.Invoke(null,new object[] {commandString});
					return;
				}
			}

			TACSVConsole.Log($"Unkown Command: {commandString}");
		}

		public static void RegisterCommands()
		{
			Trace.WriteLine("Registering Commands");
			var listOfMethods = typeof(CommandExecuter)
				.GetMethods();
			var listOfCommandMethods = typeof(CommandExecuter)
				.GetMethods()
				.Where(m => m.GetCustomAttributes(typeof(CommandAttribute), false).Length == 1)
				.ToArray();

            foreach (var methodInfo in listOfCommandMethods)
            {
				Trace.WriteLine("command register");
				var cmdAttribute = methodInfo.GetCustomAttribute<CommandAttribute>(false);
				if(cmdAttribute == null)
				{
					continue;
				}
				commands.Add(cmdAttribute, methodInfo);
            }
        }

		[Command("help", "help", "Displays this help screen")]
		public static void HelpCommand(string message)
		{
			TACSVConsole.Log("Get some help bitch");
		}

		[Command("ping", "ping", "Pings the satelite to confirm radio is working")]
		public static void PingCommand(string message)
		{
			TACSVConsole.Log("Pong");
		}
	}
}
