using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace LastSunday {
	internal class Program {
		static void Main(string[] args) {
			DateTime CheckDate = new DateTime(2026, 1, 1);
			bool Extra = true;
			DateTime Limit = new DateTime(2027, 1, 1);

			while (CheckDate < Limit) {
				if (!Extra) {
					Console.WriteLine(CalcLastSunday(CheckDate));
				} else {
					Console.WriteLine(CalcLastSundayExtra(CheckDate));
				}

				CheckDate = CheckDate.AddMonths(1);
			}

			Console.Write("\nPress a key...");
			Console.ReadKey();
		}

		static DateTime CalcLastSunday(DateTime CheckDate) {
			DateTime LastMonthDay = CalcLastMonthDay(CheckDate);
			int DayNo = (int) LastMonthDay.DayOfWeek;

			if (DayNo != 7) {
				LastMonthDay = LastMonthDay.AddDays(-DayNo);
			}

			return LastMonthDay;
		}

		static DateTime CalcLastSundayExtra(DateTime checkDate) {
			DateTime LastMonthDay = CalcLastMonthDay(checkDate);
			int DayNo = (int)LastMonthDay.DayOfWeek;

			if (DayNo != 7) {
				LastMonthDay = LastMonthDay.AddDays(-DayNo);

				if (DayNo >= 4) {
					LastMonthDay = LastMonthDay.AddDays(7);
				}
			}

			return LastMonthDay;
		}

		static DateTime CalcLastMonthDay(DateTime CheckDate) {
			return CheckDate.AddMonths(1).AddDays(-1);
		}
	}
}
