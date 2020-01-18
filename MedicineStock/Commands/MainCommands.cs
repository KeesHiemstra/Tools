using System.Windows.Input;

namespace MedicationSupply.Commands
{
  public static class MainCommands
  {

    #region Command [ File ]

    public static readonly RoutedUICommand Save = new RoutedUICommand
    (
      "_Save",
      "Save",
      typeof(MainCommands),
      new InputGestureCollection()
      {
        new KeyGesture(Key.S, ModifierKeys.Control)
      }
    );

    public static readonly RoutedUICommand Exit = new RoutedUICommand
    (
      "E_xit",
      "Exit",
      typeof(MainCommands),
      new InputGestureCollection()
      {
        new KeyGesture(Key.F4, ModifierKeys.Alt)
      }
    );


    #endregion

    #region Command [ Add ]

    public static readonly RoutedUICommand AddMedicine = new RoutedUICommand
    (
      "_Medicine",
      "AddMedicine",
      typeof(MainCommands),
      new InputGestureCollection() { }
    );

    public static readonly RoutedUICommand AddPresciption = new RoutedUICommand
    (
      "_Presciption",
      "AddPrescription",
      typeof(MainCommands),
      new InputGestureCollection() { }
    );

    public static readonly RoutedUICommand AddStock = new RoutedUICommand
    (
      "_Stock",
      "AddStock",
      typeof(MainCommands),
      new InputGestureCollection() { }
    );

    public static readonly RoutedUICommand AddStockCounting = new RoutedUICommand
    (
      "_Counting",
      "AddStockCounting",
      typeof(MainCommands),
      new InputGestureCollection() { }
    );

    public static readonly RoutedUICommand AddStockOrder = new RoutedUICommand
    (
      "_Order",
      "AddStockOrder",
      typeof(MainCommands),
      new InputGestureCollection() { }
    );

    #endregion

    #region Context [ Copy name ]

    public static readonly RoutedUICommand CopyName = new RoutedUICommand
    (
      "Copy _name",
      "CopyName",
      typeof(MainCommands),
      new InputGestureCollection() { }
    );

    #endregion

  }
}
