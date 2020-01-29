using ExecuteMonitor.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExecuteMonitor
{
  class Program
  {

    public static Logging Log = new Logging("ExecuteMonitor.log");

    public static string MonitorsFile { get; set; } = "Monitors.json";
    public static List<Monitor> Monitors { get; set; } = new List<Monitor>();

    static int Main(string[] args)
    {

      #region [= Initialize =]
      if (!File.Exists(MonitorsFile))
      {
        Log.Write($"'{MonitorsFile}' doesn't exist");
        return 1;
      }
      if (!LoadMonitorsJson())
      {
        Log.Write($"'{MonitorsFile}' isn't loaded");
        return 2;
      }

      Log.Write($"'{MonitorsFile}' is loaded");
      #endregion

      ProcessMonitors();

#if DEBUG
      Console.Write("\nPress any key...");
      Console.ReadKey();
#endif

      return 0;

    }

    private static void ProcessMonitors()
    {

      Log.Write($"{Monitors.Count} monitors available");
      foreach (Monitor item in Monitors)
      {
        ProcessMonitor(item);
      }

    }

    private static void ProcessMonitor(Monitor monitor)
    {

      Log.Write($"Working on {monitor.MonitorName}");


    }

    private static bool LoadMonitorsJson()
    {

      bool result = true;
      if (File.Exists(MonitorsFile))
      {
        try
        {
          using (StreamReader stream = File.OpenText(MonitorsFile))
          {
            string json = stream.ReadToEnd();
            Monitors = JsonConvert.DeserializeObject<List<Monitor>>(json);
          }

        }
        catch (Exception ex)
        {
          Log.Write($"Loading json error: {ex.Message}");
          result = false;
        }      
      }
      return result;

    }

    private static void SaveMonitorsJson()
    {

      Monitor monitor = new Monitor()
      {
        ExecutableName = "%OneDrive%\\Bin\\WeatherDemon.exe"
      };
      monitor.Checks.Add(new Check()
      {
        CheckFileName = "%OneDrive%\\Data\\DailyWeather\\DayWeather.json",
        MaximumTime = TimeSpan.FromMinutes(40)
      });
      Monitors.Add(monitor);

      string json = JsonConvert.SerializeObject(Monitors, Formatting.Indented);
      using (StreamWriter stream = new StreamWriter(MonitorsFile))
      {
        stream.Write(json);
      }

    }
  }
}
