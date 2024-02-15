using System.Collections.Generic;

namespace TACSV
{
	public class ApplicationOptions
	{
		public string InfluxDBToken { get; set; }
		public string InfluxUrl { get; set; }
		public string Bucket { get; set; }
		public string Organisation { get; set; }
		public Dictionary<string,string> DataPrefixes { get; set; }
	}
}
