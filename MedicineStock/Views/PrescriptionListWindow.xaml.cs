using MedicationStock.Models;
using MedicationStock.ViewModels;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace MedicationStock.Views
{
  /// <summary>
  /// Interaction logic for PrescriptionListWindow.xaml
  /// </summary>
  public partial class PrescriptionListWindow : Window
  {

    #region [ Fields ]

    PrescriptionListViewModel PrescriptionListViewModel;

    #endregion

    public PrescriptionListWindow(PrescriptionListViewModel prescriptionListViewModel)
    {

      InitializeComponent();

      PrescriptionListViewModel = prescriptionListViewModel;

    }

    private void AddButton_Click(object sender, RoutedEventArgs e)
    {

      PrescriptionListViewModel.AddList();

    }

    private void CloseButton_Click(object sender, RoutedEventArgs e)
    {

      PrescriptionListViewModel.CloseList();

    }

    private void PrescriptionDataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
    {

      PrescriptionListViewModel.PrescriptionEdit((Prescription)((DataGrid)sender).CurrentItem);

    }

  }
}
