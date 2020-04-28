using System;

namespace CHi.Extensions
{
	/*
	 * Version 1.0.0.2 (2020-02-22)
	 * - Added: decimal ToCelsius(this double)
	 * - Added: decimal ToKmPerHour(this double)
	 * Version 1.0.0.1 (2020-02-01)
	 * - Added: DateTime ConvertUnixTimeToDate(this int)
	 */

	public static class WeatherExtensions
	{
		#region Time
		/// <summary>
		/// Convert Unix time stamp (number of seconds since epoch to date/time.
		/// </summary>
		/// <param name="unixTimeStamp"></param>
		/// <returns></returns>
		public static DateTime ConvertUnixTimeToDate(this int unixTime)
		{
			// Unix time stamp is seconds past epoch
			DateTime dateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
			return dateTime.AddSeconds(unixTime).ToLocalTime();
		}
		#endregion

		#region Temperature
		/// <summary>
		/// Calculate temperature Kalvin to Celsius in 1 decimal.
		/// </summary>
		/// <param name="Kelvin"></param>
		/// <returns>Celsius</returns>
		public static decimal ToCelsius(this double Kelvin)
		{
			return (decimal)Math.Floor((Kelvin - 273.15) * 10) / 10;
		}
		#endregion

		#region Wind
		/// <summary>
		/// Calculate the wind speed m/s to km/h.
		/// </summary>
		/// <param name="speed"></param>
		/// <returns></returns>
		public static decimal ToKmPerHour(this double speed)
		{
			return (decimal)(speed * 3.6);
		}
		#endregion
	}
}
