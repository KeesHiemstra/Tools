using MaintJournal.Views;

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace MaintJournal.ViewModels
{
	public class FallenRainViewModel
	{

		#region [ Fields ]

		private readonly MainViewModel VM;
		private FallenRainWindow View;

		#endregion

		#region [ Properties ]

		public List<FallenRain> FallenRains { get; set; } = new List<FallenRain>();

		#endregion

		#region [ Construction ]

		public FallenRainViewModel(MainViewModel mainVM)
		{
			VM = mainVM;
		}

		#endregion

		#region [ Public methods ]

		public void ShowReport()
		{
			FallenRainWindow view = new FallenRainWindow(this)
			{
				Left = VM.View.Left + 100,
				Top = VM.View.Top + 20,
			};
			View = view;
			View.DataContext = this;

			CollectFallenRain();
			View.Show();
		}

		#endregion

		private void CollectFallenRain()
		{
			string[] names = new string[] { "Total",
				"January",
				"February",
				"March",
				"April",
				"May",
				"June",
				"July",
				"August",
				"September",
				"October",
				"November",
				"December"
				};

			View.FallenRainDataGrid.ItemsSource = FallenRains;

			foreach (string item in names)
			{
				FallenRains.Add(new FallenRain() { Name = item });
			}

			TotalYearFallenRain();
		}

		private void TotalYearFallenRain()
		{
			var years = VM.Journals
				.Where(x => x.Event == "Regen")
				.Where(x => x.DTStart >= new DateTime(2018, 01, 01))
				.GroupBy(
					x => x.DTStart.Value.Year,
					x => decimal.Parse(x.Message),
					(Year, TotaalRain) => new
					{
						Key = Year,
						Rain = TotaalRain.Sum(x => x)
					}
					)
				.OrderByDescending(x => x.Key);

			foreach (var year in years)
			{
				DataGridTextColumn column = new DataGridTextColumn()
				{
					Header = $"{year.Key}",
					FontWeight = FontWeights.Bold,
				};

				View.FallenRainDataGrid.Columns.Add(column);
			}
		}

		private void TotalMonthFallenRain()
		{
			var months = VM.Journals
				.Where(x => x.Event == "Regen")
				.Where(x => x.DTStart >= new DateTime(2018, 01, 01))
				.GroupBy(
					x => (Year: x.DTStart.Value.Year, Month: x.DTStart.Value.Month),
					x => decimal.Parse(x.Message),
					(Date, rain) => new
					{
						Key = Date,
						MonthTotals = rain.Sum(x => x)
					}
					)
				.OrderBy(x => x.Key.Month)
				.ThenByDescending(x => x.Key.Year);
		}

	}

	public class FallenRain
	{
		public string Name { get; set; }
	}
}
