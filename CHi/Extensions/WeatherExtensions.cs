using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CHi.Extensions
{
	/*
	 * Version 1.0.0.1 (2020-02-01)
	 * - Added DateTime ConvertUnixTimeToDate(int)
	 */
	 
	public static class WeatherExtensions
	{
		#region Time
		/// <summary>
		/// Convert Unix time stamp (number of seconds since epoch to date/time).
		/// </summary>
		/// <param name="unixTimeStamp"></param>
		/// <returns></returns>
		public static DateTime ConvertUnixTimeToDate(int unixTime)
		{
			// Unix time stamp is seconds past epoch
			DateTime dateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
			return dateTime.AddSeconds(unixTime).ToLocalTime();
		}
		#endregion

	}
}
