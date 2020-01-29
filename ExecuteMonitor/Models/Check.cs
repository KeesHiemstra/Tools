using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExecuteMonitor.Models
{
  public class Check
  {

    public string CheckFileName { get; set; }
    public TimeSpan MaximumTime { get; set; }
    public DateTime? LastActivity { get; set; }

  }
}
