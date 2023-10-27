using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Config.Net;

namespace TACSV
{
	public static class Program
	{
		public static TACSVGround Ground = new();
		static readonly string ConfigPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "TACSV/TACSVConfig.json");

		public static readonly IApplicationOptions Options = new ConfigurationBuilder<IApplicationOptions>()
			.UseJsonFile(ConfigPath)
			.Build();
	}
}
