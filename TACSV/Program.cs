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
		static string configPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "TACSV/TACSVConfig.json");

		public static IApplicationOptions options = new ConfigurationBuilder<IApplicationOptions>()
			.UseJsonFile(configPath)
			.Build();
	}
}
