using MedicationStock.Models;
using System.Windows;

namespace MedicationStock.Views
{
  /// <summary>
  /// Interaction logic for MedicineWindow.xaml
  /// </summary>
  public partial class MedicineWindow : Window
  {
    public MedicineWindow(Medicine medicine)
    {

      InitializeComponent();

      DataContext = medicine;

      if (medicine.Id == 0)
      {
        IdTextBox.Focus();
      }
      else
      {
        NameTextBox.Focus();
      }
    }

    private void ButtonSave_Click(object sender, RoutedEventArgs e)
    {
      DialogResult = true;
    }

    private void Button_Cancel_Click(object sender, RoutedEventArgs e)
    {
      DialogResult = false;
    }
  }
}
