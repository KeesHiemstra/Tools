﻿using MaintJournal.ViewModels;

using System.Windows;

namespace MaintJournal.Views
{
	/// <summary>
	/// Interaction logic for OpenedArticlesWindow.xaml
	/// </summary>
	public partial class OpenedArticlesWindow : Window
	{
		readonly OpenedArticlesViewModel ArticlesVM;

		public OpenedArticlesWindow(OpenedArticlesViewModel articlesVM)
		{
			InitializeComponent();

			ArticlesVM = articlesVM;
			DataContext = articlesVM;
		}

		private void ArticleComboBox_Selected(object sender, RoutedEventArgs e)
		{
			ArticlesVM.ArticleSelected(sender, e);
		}
	}
}
