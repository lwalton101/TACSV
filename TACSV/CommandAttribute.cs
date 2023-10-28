using ScottPlot.Statistics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

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
