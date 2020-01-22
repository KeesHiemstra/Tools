using System.Windows.Input;

namespace MedicationStock.Commands
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

    #endregion

    #region Command [ Help ]

    public static readonly RoutedUICommand History = new RoutedUICommand
    (
      "_History",
      "History",
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
