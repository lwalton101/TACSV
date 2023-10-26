using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TACSV
{
	public interface IApplicationOptions
	{
		int GraphAutoRefreshTime { get; set; }
		bool GraphAutoRefreshEnabled { get; set; }
	}
}
