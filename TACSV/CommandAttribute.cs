using System;

namespace TACSV
{
	internal class CommandAttribute : Attribute
	{
		public string name;
		public string usage;
		public string description;

		public CommandAttribute(string name, string usage, string description)
		{
			this.name = name;
			this.usage = usage;
			this.description = description;
		}
	}
}
