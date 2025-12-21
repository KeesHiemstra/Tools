using Holidays;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Easter
{
  class Program
  {
    static void Main(string[] args)
    {
      for (int year = 2026; year < 2030; year++)
      {
        Console.WriteLine(ChristianHolidays.EasterSunday(year));
      }

      Console.Write("\nPress any key...");
      Console.ReadKey();
    }
  }
}
