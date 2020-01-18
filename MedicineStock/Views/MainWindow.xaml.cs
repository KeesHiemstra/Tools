using MedicationSupply.Models;
using MedicationSupply.ViewModels;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace MedicationSupply
{
  /// <summary>
  /// Interaction logic for MainWindow.xaml
  /// </summary>
  public partial class MainWindow : Window
  {
    public MainViewModel MainVM { get; set; }

    public MainWindow()
    {

      InitializeComponent();

      MainVM = new MainViewModel(this);

      DataContext = MainVM;

    }

    private void MedicinesDataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
    {
      Medicine medicine = (Medicine)((DataGrid)sender).CurrentItem;
      if (medicine is null)
      {
        return;
      }

      string column = (string)((DataGrid)sender).CurrentColumn.Header;
      switch (column)
      {
        case "Name":
        case "Recurring":
          {
            MainVM.MedicineEdit(medicine);
            break;
          }
        case "Strength":
        case "Day":
        case "Unit":
          {
            MainVM.PrescriptionList(medicine);
            break;
          }
        case "Stock":
        case "End stock":
          {
            MainVM.StockList(medicine);
            break;
          }
        default:
          {
            MainVM.MedicineEdit(medicine);
            break;
          }
      }

    }

    #region [ Commands ]

    #region File

    #region Save

    private void SaveCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
    {
      e.CanExecute = (MainVM.Medicines.Count > 0) && MainVM.IsJsonChanged();
    }

    private void SaveCommand_Execute(object sender, ExecutedRoutedEventArgs e)
    {
      MainVM.SaveMedicineStock();
    }

    #endregion

    #region Exit
    private void ExitCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
    {
      e.CanExecute = true;
    }

    private void ExitCommand_Execute(object sender, ExecutedRoutedEventArgs e)
    {
      Application.Current.Shutdown();
    }

    #endregion

    #endregion

    #region Add

    #region Medicine
    private void MedicineCommandMedicineAdd_CanExecute(object sender, CanExecuteRoutedEventArgs e)
    {
      e.CanExecute = true;
    }

    private void MedicineCommandMedicineAdd_Execute(object sender, ExecutedRoutedEventArgs e)
    {
      MainVM.MedicineAdd();
    }

    #endregion

    #region Prescription
    private void MedicineCommandPresciptionAdd_CanExecute(object sender, CanExecuteRoutedEventArgs e)
    {
      e.CanExecute = MedicinesDataGrid.SelectedCells.Count > 0;
    }

    private void MedicineCommandPresciptionAdd_Execute(object sender, ExecutedRoutedEventArgs e)
    {

    }

    #endregion

    #region Stock
    private void MedicineCommandStockAdd_CanExecute(object sender, CanExecuteRoutedEventArgs e)
    {
      e.CanExecute = MedicinesDataGrid.SelectedCells.Count > 0;
    }

    private void MedicineCommandStockCountingAdd_CanExecute(object sender, CanExecuteRoutedEventArgs e)
    {
      e.CanExecute = MedicinesDataGrid.SelectedCells.Count > 0;
    }

    private void MedicineCommandStockCountingAdd_Execute(object sender, ExecutedRoutedEventArgs e)
    {

    }

    private void MedicineCommandStockOrderAdd_CanExecute(object sender, CanExecuteRoutedEventArgs e)
    {
      e.CanExecute = MedicinesDataGrid.SelectedCells.Count > 0;
    }

    private void MedicineCommandStockOrderAdd_Execute(object sender, ExecutedRoutedEventArgs e)
    {

    }

    #endregion

    #endregion

    #region Context Copy name

    private void CopyNameCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
    {
      e.CanExecute = MedicinesDataGrid.SelectedIndex >= 0;
    }

    private void CopyNameCommand_Execute(object sender, ExecutedRoutedEventArgs e)
    {
      MainVM.CopyName((Medicine)((DataGrid)e.Source).CurrentItem);
    }

    #endregion

    #endregion

    private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
    {
      MainVM.Closing();
    }

  }
}
