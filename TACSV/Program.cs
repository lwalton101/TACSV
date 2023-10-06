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
		static string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "TACSVConfig.json");
		public static IApplicationOptions options = new ConfigurationBuilder<IApplicationOptions>()
			.UseJsonFile(path)
			.Build();
	}
}
