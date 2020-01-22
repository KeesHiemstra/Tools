using MedicationStock.Models;
using MedicationStock.ViewModels;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace MedicationStock.Views
{
  /// <summary>
  /// Interaction logic for StockListWindow.xaml
  /// </summary>
  public partial class StockListWindow : Window
  {

    #region [ Fields ]

    StockListViewModel StockListViewModel;

    #endregion

    public StockListWindow(StockListViewModel stockListViewModel)
    {

      InitializeComponent();

      StockListViewModel = stockListViewModel;

    }

    private void AddButton_Click(object sender, RoutedEventArgs e)
    {

      StockListViewModel.AddList();

    }

    private void CloseButton_Click(object sender, RoutedEventArgs e)
    {

      StockListViewModel.CloseList();

    }

    private void StockDataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
    {

      StockListViewModel.StockEdit((Stock)((DataGrid)sender).CurrentItem);

    }

  }
}
