﻿using CHi.Log;
using CHi.Extensions;

using MaintJournal.ViewModels;

using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Input;

namespace MaintJournal
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		private readonly MainViewModel VM;
		
		public MainWindow()
		{
			InitializeComponent();
			Log.Write("Started Journal");
			string version = Assembly.GetExecutingAssembly().GetName().Version.ToString();
			Title = $"Journal ({version})";

			VM = new MainViewModel(this);
			DataContext = VM;
		}

		#region Exit command
		private void ExitCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
		{
			e.CanExecute = true;
		}

		private void ExitCommand_Execute(object sender, ExecutedRoutedEventArgs e)
		{
			Application.Current.Shutdown();
		}
		#endregion

		#region Options command
		private void OptionsCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
		{
			e.CanExecute = true;
		}

		private void OptionsCommand_Execute(object sender, ExecutedRoutedEventArgs e)
		{
			VM.Options.ShowOptions(this);
		}
		#endregion

		#region Backup command
		private void BackupCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
		{
			e.CanExecute = !string.IsNullOrEmpty(VM.Options.BackupPath);
		}

		private void BackupCommand_Execute(object sender, ExecutedRoutedEventArgs e)
		{
			VM.Backup();
		}
		#endregion

		#region Restore command
		private void RestoreCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
		{
			e.CanExecute = File.Exists(VM.Options.RestoreFile.TranslatePath()) &&
				Assembly.GetEntryAssembly().Location.ToLower() != 
					@"%OneDrive\Bin\MaintJournal.exe%".ToLower() &&
					VM.Options.DbName.ToLower() != "joost";
		}

		private void RestoreCommand_Execute(object sender, ExecutedRoutedEventArgs e)
		{
			VM.Restore();
		}
		#endregion

		#region RefreshJournals

		private void RefreshJournals_CanExecute(object sender, CanExecuteRoutedEventArgs e)
		{
			e.CanExecute = VM.Journals
				.Count() > 0;
		}

		private void RefreshJournals_Execute(object sender, ExecutedRoutedEventArgs e)
		{
			FilterMessageTextBox.Focus();
			VM.GetJournals(true, true);
		}

		#endregion

		#region NewRecord

		private void NewRecordCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
		{
			e.CanExecute = true;
		}

		private void NewRecordCommand_Executed(object sender, ExecutedRoutedEventArgs e)
		{
			VM.EditRecord(null);
		}

		#endregion

		#region ReportOpenedArticles

		private void ReportOpenedArticlesCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
		{
			e.CanExecute = VM.Journals
				.Count(x => x.Event == "Aangebroken") > 0;
		}

		private void ReportOpenedArticlesCommand_Executed(object sender, ExecutedRoutedEventArgs e)
		{
			VM.ReportOpenedArticles();
		}

		#endregion

		#region ReportCoffeeUsage

		private void ReportCoffeeUsageCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
		{
			e.CanExecute = (VM.Journals
				.Count(x => x.Event == "Aangebroken") > 0) &&
				(VM.Journals
					.Count(x => x.Event == "Kop koffie") > 0);
		}

		private void ReportCoffeeUsageCommand_Executed(object sender, ExecutedRoutedEventArgs e)
		{
			VM.ReportCoffeeUsage();
		}

		#endregion

		private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			VM.CloseWindow();
		}

		internal void UpdateDatabaseConnection()
		{
			VM.UpdateDatabaseConnection();
		}

		private void ApplyButton_Click(object sender, RoutedEventArgs e)
		{
			VM.ApplyFilter();
		}

		private void ClearButton_Click(object sender, RoutedEventArgs e)
		{
			VM.ClearFilter();
		}

		private void GotoButton_Click(object sender, RoutedEventArgs e)
		{
			VM.GotoFilter();
		}

		private void PreviousButton_Click(object sender, RoutedEventArgs e)
		{
			VM.GotoPreviousFilter();
		}

		private void NextButton_Click(object sender, RoutedEventArgs e)
		{
			VM.GotoNextFilter();
		}

		private void FilterBorden_KeyUp(object sender, KeyEventArgs e)
		{
			VM.FilterKeyboard(sender, e);
		}

		private void MainDataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
		{
			VM.DoubleClickDataGrid(sender, e);
		}

		private void FilterMessageTextBox_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
		{
			FilterVisibility(!string.IsNullOrWhiteSpace(FilterMessageTextBox.Text) ||
				FilterEventComboBox.SelectedIndex > 0);
		}

		private void FilterEventComboBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
		{
			if (VM == null) { return; }
			FilterVisibility(!string.IsNullOrWhiteSpace(FilterMessageTextBox.Text) ||
				FilterEventComboBox.SelectedIndex > 0);
		}

		private void FilterVisibility(bool canGoto)
		{
			if (canGoto != VM.CanGoto)
			{
				VM.CanGoto = canGoto;
				if (canGoto)
				{
					GotoBorder.Visibility = Visibility.Visible;
				}
				else
				{
					GotoBorder.Visibility = Visibility.Hidden;
					PreviousButton.Visibility = Visibility.Collapsed;
					NextButton.Visibility = Visibility.Collapsed;
				}
			}
		}

	}
}
