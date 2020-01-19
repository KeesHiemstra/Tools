using System.Windows;
using System.Windows.Controls;

namespace MedicationSupply.ViewModels
{
  /// <summary>
  /// Interaction logic for StockEditWindow.xaml
  /// </summary>
  public partial class StockEditWindow : Window
  {

    #region [ Fields ]

    readonly StockEditViewModel StockEditViewModel;

    #endregion

    #region [ Construction ]

    public StockEditWindow(StockEditViewModel stockEditViewModel)
    {

      InitializeComponent();

      StockEditViewModel = stockEditViewModel;

    }

    #endregion

    private void SaveButton_Click(object sender, RoutedEventArgs e)
    {

      StockEditViewModel.SaveEdit();

    }

    private void CloseButton_Click(object sender, RoutedEventArgs e)
    {

      StockEditViewModel.CloseEdit();

    }

    private void TextBox_GotFocus(object sender, RoutedEventArgs e)
    {
      ((TextBox)e.OriginalSource).SelectAll();
    }
  }
}
