using MaintJournal.ViewModels;

using System.Windows;

namespace MaintJournal.Views
{
	/// <summary>
	/// Interaction logic for CoffeeUsageWindow.xaml
	/// </summary>
	public partial class CoffeeUsageWindow : Window
	{
		public CoffeeUsageWindow(CoffeeUsageViewModel coffeeUsageViewModel)
		{
			InitializeComponent();
		}
	}
}
