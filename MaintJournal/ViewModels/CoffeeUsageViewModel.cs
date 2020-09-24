using MaintJournal.Models;
using MaintJournal.Views;

using System;
using System.Collections.Generic;
using System.Linq;

namespace MaintJournal.ViewModels
{
	public class CoffeeUsageViewModel
	{

		#region [ Fields ]

		private readonly MainViewModel VM;
		private CoffeeUsageWindow View;

		#endregion

		#region [ Properties ]

		public List<CoffeeUsage> Report { get; private set; } = new List<CoffeeUsage>();

		#endregion

		#region [ Construction ]

		public CoffeeUsageViewModel(MainViewModel mainVM)
		{
			VM = mainVM;
		}

		#endregion

		#region [ Public methods ]

		public void ShowReport()
		{
			CoffeeUsageWindow view = new CoffeeUsageWindow(this)
			{
				Left = VM.View.Left + 100,
				Top = VM.View.Top + 20,
			};
			View = view;

			CollectReport();
			View.Show();
		}

		private void CollectReport()
		{
			View.ReportDataGrid.ItemsSource = null;

			List<Journal> articles = VM.Journals
				.Where(x => x.Event == "Aangebroken" && x.Message == "Pot oploskoffie")
				.OrderByDescending(x => x.DTStart)
				.Select(x => x)
				.ToList();

			Report = new List<CoffeeUsage>();

			if ((DateTime.Now - articles.First().DTStart.Value).TotalDays > 1)
			{
				Report.Add(new CoffeeUsage
				{
					Opened = null,
					Days = (int)(DateTime.Now - articles.First().DTStart.Value).TotalDays
				});
			}

			for (int i = 0; i < articles.Count - 1; i++)
			{
				Report.Add(new CoffeeUsage
				{
					Opened = articles[i].DTStart,
					Days = (int)(articles[i].DTStart.Value - articles[i + 1].DTStart.Value).TotalDays,
					LastOpened = articles[i + 1].DTStart.Value,
				});
			}

			foreach (CoffeeUsage coffee in Report)
			{
				var query = VM.Journals
					.Where(x => x.DTStart <= coffee.Opened && x.DTStart > coffee.LastOpened && x.Event == "Kop koffie");

				coffee.Cups = query.Sum(x => int.Parse(x.Message));
				coffee.ActualDays = VM.Journals
					.Where(x => x.DTStart <= coffee.Opened && x.DTStart > coffee.LastOpened && x.Event == "Kop koffie")
					.GroupBy(date => date.DTStart.Value.Date).Count();
				coffee.CupsPerDay = (decimal)coffee.Cups / coffee.ActualDays;
				coffee.CupsMin = query
					.GroupBy(date => date.DTStart.Value.Date).Min(x => x.Sum(x => int.Parse(x.Message)));
				coffee.CupsMax = query
					.GroupBy(date => date.DTStart.Value.Date).Max(x => x.Sum(x => int.Parse(x.Message)));
			}

			View.ReportDataGrid.ItemsSource = Report;
		}

		#endregion

	}
}
