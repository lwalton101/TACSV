using System;
using System.IO;
using Config.Net;

namespace TACSV
{
	public static class Program
	{
		public static TACSVGround Ground = new();
		private static string AppDataPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "TACSV");
		private static readonly string ConfigPath = Path.Combine(AppDataPath, "TACSVConfig.json");
		
		public static readonly IApplicationOptions Options = new ConfigurationBuilder<IApplicationOptions>()
			.UseJsonFile(ConfigPath)
			.Build();
	}
}
