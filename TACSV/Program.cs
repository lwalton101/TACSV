using System;

namespace TACSV
{
	public static class Program
	{
		public static TACSVGround Ground = new();
		public static DateTime? LastInitTime = null;
		public static float LastMillis = 0;
	}
}
