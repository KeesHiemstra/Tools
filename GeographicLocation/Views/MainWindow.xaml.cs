using GeographicLocation.Models;
using GeographicLocation.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace GeographicLocation
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

    private void LocationComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {

      if (MainVM is null) { return; }
      MainVM.CoordinateChanged((Location)((ComboBox)sender).SelectedItem, MainVM.CoordinateFormat);

    }

    private void LatitudeFormatedTextBox_MouseRightButtonUp(object sender, MouseButtonEventArgs e)
    {
      Clipboard.SetText(LongitudeFormatedTextBlock.Text);
    }

    private void LongitudeFormatedTextBox_MouseRightButtonUp(object sender, MouseButtonEventArgs e)
    {
      Clipboard.SetText(LongitudeFormatedTextBlock.Text);
    }

    private void DecimalRadioButton_Checked(object sender, RoutedEventArgs e)
    {
      if (MainVM == null) { return; }
      MainVM.CoordinateFormat = MainViewModel.CoordinateFormatType.Decimal;
      MainVM.CoordinateFormatChanged(MainViewModel.CoordinateFormatType.Decimal);
    }

    private void TextRadioButton_Checked(object sender, RoutedEventArgs e)
    {
      if (MainVM == null) { return; }
      MainVM.CoordinateFormat = MainViewModel.CoordinateFormatType.Text;
      MainVM.CoordinateFormatChanged(MainViewModel.CoordinateFormatType.Text);
    }

    private void AddButton_Click(object sender, RoutedEventArgs e)
    {
      MainVM.AddToJson();
    }

    private void SaveButton_Click(object sender, RoutedEventArgs e)
    {
      MainVM.SaveJson();
    }

  }
}
