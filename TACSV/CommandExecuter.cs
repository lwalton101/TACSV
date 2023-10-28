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

		private static KeyValuePair<CommandAttribute, MethodInfo>? GetPairByName(string name)
		{
			foreach(var pair in commands)
			{
				if (pair.Key.name == name)
				{
					return pair;
				}
			}
			return null;
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
			TACSVConsole.Log($"List of commands:");
			foreach (var pair in commands)
			{
				TACSVConsole.Log($"{pair.Key.name} : {pair.Key.description}");
			}
		}

		[Command("cmdInfo", "cmdInfo <cmdName>", "Displays more info about a specific command")]
		public static void CmdInfoCommand(string message)
		{
			var messageParts = message.Split(" ");
			var cmdUsage = GetPairByName(messageParts[0]);

			if((cmdUsage) == null)
			{
				return;
			}

			if(messageParts.Length < 2) 
			{
				TACSVConsole.Log($"Wrong usage. {cmdUsage}");
				return;
			}

			var cmdInfoPairNull = GetPairByName(messageParts[1]);
			if(cmdInfoPairNull == null)
			{
				TACSVConsole.Log($"Cannot find command {messageParts[1]}");
				return;
			}
			var cmdInfoPair = (KeyValuePair<CommandAttribute, MethodInfo>)cmdInfoPairNull;
			TACSVConsole.Log($"Command {cmdInfoPair.Key.name}:");
			TACSVConsole.Log($"Usage: {cmdInfoPair.Key.usage}");
			TACSVConsole.Log($"Description: {cmdInfoPair.Key.description}");
			TACSVConsole.Log($"Method Name: {cmdInfoPair.Value.Name}");
		}

		[Command("ping", "ping", "Pings the satelite to confirm radio is working")]
		public static void PingCommand(string message)
		{
			TACSVConsole.Log("Pong");
		}
	}
}
