using CHi.Extensions;

using Newtonsoft.Json;

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Trinet.Core;

namespace SunUpAndDown
{
	internal class Program
	{
		static void Main(string[] args)
		{
			int year = 2026;
			DateTime dateTime = new DateTime(year, 1, 1);
			DateTime SunriseTime;
			DateTime SunsetTime;

			//Read the stored Open Weather location json
			GeographicLocation location;
			string jsonPath = "%OneDrive%\\Etc\\DemonOpenWeather.json".TranslatePath();
			using (StreamReader stream = File.OpenText(jsonPath))
			{
				string json = stream.ReadToEnd();
				location = JsonConvert.DeserializeObject<GeographicLocation>(json);
			}
			
			while (dateTime.Year == year)
			{
				DaylightHours daylight = DaylightHours.Calculate(dateTime.Date.AddHours(2), location);
				SunriseTime = daylight.SunriseUtc.Value.LocalDateTime.ToLocalTime();
				SunsetTime = daylight.SunsetUtc.Value.LocalDateTime.ToLocalTime();

				if (SunriseTime.DayOfYear	== 60)
				{
					if (SunriseTime.Day != 29)
					{
						Console.WriteLine();
          }
				}
				Console.WriteLine($"{SunsetTime:dd-MMM}\t{SunriseTime:HH:mm}\t{SunsetTime:HH:mm}");

				dateTime = dateTime.AddDays(1);
			}

			//Console.ReadKey();
		}
	}
}
