using CHi.Extensions;
using ExecuteMonitor.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Mail;
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
      foreach (Monitor monitor in Monitors)
      {
        ProcessMonitor(monitor);
      }

    }

    private static void ProcessMonitor(Monitor monitor)
    {

      Log.Write($"Working on '{monitor.MonitorName}', {monitor.Checks.Count} checks available");
      foreach (Check check in monitor.Checks)
      {
        ProcessCheck(check, monitor);
      }

    }

    private static void ProcessCheck(Check check, Monitor monitor)
    {

      Log.Write($"Working on '{check.CheckFileName}'");
      if (!File.Exists(check.CheckFileName.TranslatePath()))
      {
        Log.Write($"'{check.CheckFileName}' doesn't exist");
        return;
      }
      
      FileInfo fileInfo = new FileInfo(check.CheckFileName.TranslatePath());
      check.LastActivity = fileInfo.LastWriteTime;

      TimeSpan timeSpan = DateTime.Now - fileInfo.LastWriteTime;
      if (timeSpan.TotalMinutes > check.MaximumTime.TotalMinutes)
      {
        Log.Write($"'{check.CheckFileName}' is older then {check.MaximumTime.ToString()}");
        WriteMail(check, monitor);
        return;
      }

      Log.Write($"No action needed for '{check.CheckFileName}' ({(int)timeSpan.TotalMinutes} minutes)");

    }

    private static void WriteMail(Check check, Monitor monitor)
    {

      MailMessage mail = new MailMessage();
      mail.From = new MailAddress("chi@xs4all.nl", "Joost");
      mail.Subject = $"Warning - {monitor.MonitorName} found a old {check.CheckFileName}";
      mail.IsBodyHtml = true;
      GetMailAddresses(mail.To, monitor.MailTo);
      GetMailAddresses(mail.CC, monitor.MailCc);
      GetMailAddresses(mail.Bcc, monitor.MailBcc);
      mail.Body = $"<p>'{monitor.MonitorName}' with '{check.CheckFileName}' is out of date.</p>";

      try
      {
        SmtpClient smtpClient = new SmtpClient("smtp.xs4all.nl");
        smtpClient.Send(mail);
        mail.Dispose();
        Log.Write($"'{monitor.MonitorName}' mail has been send");
      }
      catch (Exception ex)
      {
        Log.Write($"'{monitor.MonitorName}' mail caused an exception: {ex.Message}");
      }

    }

    private static void GetMailAddresses(MailAddressCollection addressCollection, string addresses)
    {

      if (string.IsNullOrEmpty(addresses)) { return; }

      string[] addressList = addresses.Split(new char[] { ',', ';' }, StringSplitOptions.RemoveEmptyEntries);
      foreach (string address in addressList)
      {
        addressCollection.Add(address);
      }

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
