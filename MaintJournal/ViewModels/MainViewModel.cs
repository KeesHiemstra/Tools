using CHi.Extensions;
using CHi.Log;

using MaintJournal.Models;

using Newtonsoft.Json;

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.Entity;
using System.Data.Entity.Core.Mapping;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace MaintJournal.ViewModels
{
	public class MainViewModel : INotifyPropertyChanged
	{

		#region [ Fields ]

		private JournalDbContext db;
		public MainWindow View;

		#endregion

		#region [ Properties ]

		public JournalDbContext Db { get => db; set => db = value; }
		public OptionsViewModel Options { get; set; }
		public ObservableCollection<Journal> Journals { get; set; } = new ObservableCollection<Journal>();
		public ObservableCollection<Journal> Filtered { get; set; } = new ObservableCollection<Journal>();

		public int JournalsCount { get => Journals.Count;	}

		#endregion

		#region [ Construction ]

		public MainViewModel(MainWindow mainWindow)
		{
			View = mainWindow;
			Options = new OptionsViewModel();
			OpenOptions();
			GetJournals();
			View.MainDataGrid.ItemsSource = Journals;
			GetJournalEvents();
			View.FilterMessageTextBox.Focus();
		}

		#endregion

		#region [ Public methods ]

		public event PropertyChangedEventHandler PropertyChanged;

		public void GetJournals(bool IsRefresh = false, bool IsManual = false)
		{
			//Use the database
			Db = new JournalDbContext(Options.DbConnection);

			//Prepare the collection
			List<Journal> journals = (from j in Db.Journals
																orderby j.DTStart descending, j.DTCreation descending
																select j).ToList();

			//Get the collection
			Journals = new ObservableCollection<Journal>(journals);
			if (IsRefresh)
			{
				if (IsManual)
				{
					Log.Write($"Manually refreshed Journal table");
				}
				else
				{
					Log.Write($"Refreshed Journal table");
				}
			}
			else
			{
				Log.Write($"Loaded Journal table");
			}
		}

		public void EditRecord(int? logId)
		{
			JournalViewModel journalVM = new JournalViewModel(this);
			journalVM.ShowJournal(logId);
		}

		#endregion

		private void NotifyPropertyChanged(string propertyName = "")
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
			Log.Write("Notification from MainViewModel");
			GetJournals(true);
		}

		private void OpenOptions()
		{
			if (File.Exists(Options.JsonPath))
			{
				using StreamReader stream = File.OpenText(Options.JsonPath);
				string json = stream.ReadToEnd();
				Options = JsonConvert.DeserializeObject<OptionsViewModel>(json);
				Log.Write($"Opened options from '{Options.JsonPath}'");
			}
		}

		#region Backup()

		internal void Backup()
		{
			if (!Directory.Exists(Options.BackupPath.TranslatePath()))
			{
				Log.Write($"Folder '{Options.BackupPath}' doesn't exist");
				MessageBox.Show($"Folder '{Options.BackupPath}' doesn't exist",
					"Backup path",
					MessageBoxButton.OK,
					MessageBoxImage.Warning);
				return;
			}

			string backupDate = DateTime.Now.ToString("yyyy-MM-dd_HHmmss");
			string backupFolder = $"{Options.BackupPath.TranslatePath()}\\{Options.DbName}\\{backupDate}";
			Directory.CreateDirectory(backupFolder);

			string backupFile = $"{backupFolder}\\{backupDate}.bak";

			string sql = $"BACKUP DATABASE [{Options.DbName}] TO DISK = '{backupFile}' WITH NOFORMAT, " +
					$"NOFORMAT, NOINIT, SKIP, NOREWIND, NOUNLOAD, STATS = 10, NAME = " +
					$"N'Journal-Full Database Backup';\n" +
				$"BACKUP DATABASE [{Options.DbName}] TO DISK = '{backupFile}' WITH DIFFERENTIAL, NOFORMAT, " +
					$"NOINIT, SKIP, NOREWIND, NOUNLOAD, STATS = 10, NAME = N'Journal-Diff Database Backup';\n" +
				$"BACKUP LOG [{Options.DbName}] TO DISK = '{backupFile}' WITH NOFORMAT, NOINIT, SKIP, " +
					$"NOREWIND, NOUNLOAD, STATS = 10, NAME = N'Journal-Log Database Backup';\n";

			try
			{
				using JournalDbContext db = new JournalDbContext(Options.DbConnection);
				db.Database.ExecuteSqlCommand(TransactionalBehavior.DoNotEnsureTransaction, sql);
				Log.Write($"Database '{Options.DbConnection}' is backed up");
			}
			catch (Exception ex)
			{
				Log.Write($"Error backup: {ex.Message}");
				MessageBox.Show($"{ex.Message}",
					"Backup exception",
					MessageBoxButton.OK,
					MessageBoxImage.Exclamation);
				return;
			}

			MessageBox.Show($"Backup created successful",
				"Backup",
				MessageBoxButton.OK,
				MessageBoxImage.Information);
		}

		#endregion

		internal void DoubleClickDataGrid(object sender, MouseButtonEventArgs e)
		{
			if (sender == null) { return; }
			foreach (Journal item in ((DataGrid)e.Source).SelectedItems)
			{
				EditRecord(item.LogID);
			}
		}

		internal void ReportOpenedArticles()
		{
			OpenedArticlesViewModel report = new OpenedArticlesViewModel(this);
			report.ShowReport();
		}

		internal void ReportCoffeeUsage()
		{
			CoffeeUsageViewModel report = new CoffeeUsageViewModel(this);
			report.ShowReport();
		}

		internal void Keyboard(object sender, KeyEventArgs e)
		{
			if (sender == null) { return; }
			if (e.Key == Key.Enter)
			{
				ApplyFilter();
			}
		}

		internal void ApplyFilter()
		{
			List<Journal> filtered = new List<Journal>();

			View.MainDataGrid.ItemsSource = null;

			switch (View.FilterEventListBox.SelectedIndex)
			{
				case 0:
					filtered = Journals
						.ToList();
					break;
				case 1:
					filtered = Journals
						.Where(x => string.IsNullOrEmpty(x.Event))
						.ToList();
					break;
				case 2:
					filtered = Journals
						.Where(x => !string.IsNullOrEmpty(x.Event))
						.ToList();
					break;
				default:
					filtered = Journals
						.Where(x => x.Event == View.FilterEventListBox.SelectedItem.ToString())
						.ToList();
					break;
			}

			if (!string.IsNullOrEmpty(View.FilterMessageTextBox.Text))
			{
				filtered = filtered
					.Where(x => x.Message
						.ToLower()
						.Contains(View.FilterMessageTextBox.Text.ToLower()))
					.ToList();
			}

			Filtered = new ObservableCollection<Journal>(filtered);
			View.MainDataGrid.ItemsSource = Filtered;
		}

		internal void ClearFilter()
		{
			View.MainDataGrid.ItemsSource = null;
			View.MainDataGrid.ItemsSource = Journals;
			Filtered = null;
		}

		internal void GotoFilter()
		{
			throw new NotImplementedException();
		}

		internal void CloseWindow()
		{
			Log.Write("Closing Journal");
		}

		internal void UpdateDatabaseConnection()
		{
			View.MainDataGrid.ItemsSource = null;
			GetJournals();
			View.MainDataGrid.ItemsSource = Journals;
			Log.Write($"Connection is changed to {Options.DbName}");
		}

		private void GetJournalEvents()
		{
			View.FilterEventListBox.Items.Clear();

			//Add predefined items
			View.FilterEventListBox.Items.Add(new TextBlock()
			{
				Text = "< all >",
				FontStyle = FontStyles.Italic,
			});
			View.FilterEventListBox.Items.Add(new TextBlock()
			{
				Text = "< empty >",
				FontStyle = FontStyles.Italic,
			});
			View.FilterEventListBox.Items.Add(new TextBlock()
			{
				Text = "< not empty >",
				FontStyle = FontStyles.Italic,
			});

			//Add items from all unique events
			foreach (string item in Journals
				.Where(x => !string.IsNullOrEmpty(x.Event))
				.Select(x => x.Event)
				.Distinct()
				.OrderBy(x => x)
				.ToList())
			{
				View.FilterEventListBox.Items.Add(item);
			}
			View.FilterEventListBox.SelectedIndex = 0;
		}

	}
}
