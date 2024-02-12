using System;

namespace TACSV
{
	public class Command
	{
		public string name;
		public string usage;
		public string description;
		public Action<string> execute;
		public Command(string name, string usage, string description, Action<string> executeMethod) {
			this.name = name;
			this.usage = usage;
			this.description = description;
			this.execute = executeMethod;
		}
	}
}
