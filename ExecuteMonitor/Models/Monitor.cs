﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExecuteMonitor.Models
{
  public class Monitor
  {

    public string MonitorName { get; set; }
    public string ExecutableName { get; set; }
    public string ExecutablePath { get; set; }
    public string ParameterString { get; set; }
    public string AllowedComputer { get; set; }
    public string MailFrom { get; set; }
    public string MailTo { get; set; }
    public string MailCc { get; set; }
    public string MailBcc { get; set; }
    public List<Check> Checks { get; set; } = new List<Check>();

  }
}
