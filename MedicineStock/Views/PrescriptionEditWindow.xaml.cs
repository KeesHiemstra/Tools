using System.Windows;
using System.Windows.Controls;

namespace MedicationSupply.ViewModels
{
  /// <summary>
  /// Interaction logic for PrescriptionEditWindow.xaml
  /// </summary>
  public partial class PrescriptionEditWindow : Window
  {

    #region [ Fields ]

    readonly PrescriptionEditViewModel PrescriptionEditViewModel;

    #endregion

    #region [ Construction ]

    public PrescriptionEditWindow(PrescriptionEditViewModel prescriptionEditViewModel)
    {

      InitializeComponent();

      PrescriptionEditViewModel = prescriptionEditViewModel;

    }

    #endregion

    private void SaveButton_Click(object sender, RoutedEventArgs e)
    {

      PrescriptionEditViewModel.SaveEdit();

    }

    private void CloseButton_Click(object sender, RoutedEventArgs e)
    {

      PrescriptionEditViewModel.CloseEdit();

    }

    private void TextBox_GotFocus(object sender, RoutedEventArgs e)
    {
      ((TextBox)e.OriginalSource).SelectAll();
    }
  }
}
