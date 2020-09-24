﻿using System.Windows.Input;

namespace MaintJournal.Commands
{
	public static class MainWindowCommands
	{
		public static readonly RoutedUICommand Exit = new RoutedUICommand
			(
				"E_xit",
				"Exit",
				typeof(MainWindowCommands),
				new InputGestureCollection()
				{
					new KeyGesture(Key.F4, ModifierKeys.Alt)
				}
			);

		public static readonly RoutedUICommand Options = new RoutedUICommand
			(
				"_Options",
				"Options",
				typeof(MainWindowCommands),
				new InputGestureCollection()
				{
					new KeyGesture(Key.O, ModifierKeys.Alt)
				}
			);

		public static readonly RoutedUICommand Backup = new RoutedUICommand
			(
				"_Backup",
				"Backup",
				typeof(MainWindowCommands),
				new InputGestureCollection()
				{
					new KeyGesture(Key.B, ModifierKeys.Alt)
				}
			);

		public static readonly RoutedUICommand RefreshJournals = new RoutedUICommand
			(
				"_Refresh",
				"RefreshJournals",
				typeof(MainWindowCommands),
				new InputGestureCollection()
				{
					new KeyGesture(Key.R, ModifierKeys.Control)
				}
			);

		public static readonly RoutedUICommand NewRecord = new RoutedUICommand
			(
				"_New",
				"NewRecord",
				typeof(MainWindowCommands),
				new InputGestureCollection()
				{
					new KeyGesture(Key.N, ModifierKeys.Control)
				}
			);

		public static readonly RoutedUICommand ReportOpenedArticles = new RoutedUICommand
			(
				"_Opened articles",
				"ReportOpenedArticles",
				typeof(MainWindowCommands),
				new InputGestureCollection() { }
			);

		public static readonly RoutedUICommand ReportCoffeeUsage = new RoutedUICommand
			(
				"_Cofffe usage",
				"ReportCoffeeUsage",
				typeof(MainWindowCommands),
				new InputGestureCollection() { }
			);

	}
}
