using MedicationStock.Models;
using MedicationStock.Views;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;

namespace MedicationStock.ViewModels
{
  public class StockListViewModel : INotifyPropertyChanged
  {

    #region [ Fields ]

    readonly MainWindow MainView;
    StockListWindow StockListView;

    #endregion

    #region [ Properties ]

    public Medicine Medicine { get; set; }

    #endregion

    #region [ Construction ]

    public StockListViewModel(MainWindow mainView, Medicine medicine)
    {

      MainView = mainView;
      Medicine = medicine;

      ShowStockListView();

    }

    #endregion

    #region [ Method ]

    public event PropertyChangedEventHandler PropertyChanged
    {
      add
      {
        ((INotifyPropertyChanged)Medicine).PropertyChanged += value;
      }

      remove
      {
        ((INotifyPropertyChanged)Medicine).PropertyChanged -= value;
      }
    }

    #endregion

    internal void ShowStockListView()
    {

      UpdateStockList();

      StockListView = new StockListWindow(this)
      {
        Left = MainView.Left + 20,
        Top = MainView.Top + 20,

        DataContext = Medicine
      };

      StockListView.ShowDialog();

    }

    private void UpdateStockList()
    {

      var lst = (from s in Medicine.Stocks
                 orderby s.Date descending
                 select s).ToList();
      Medicine.Stocks = new ObservableCollection<Stock>(lst);

    }

    internal void AddList()
    {

      _ = new StockEditViewModel(this, StockListView, null);
      UpdateStockList();
      StockListView.DataContext = null;
      StockListView.DataContext = Medicine;

    }

    internal void CloseList()
    {

      StockListView.DialogResult = true;

    }

    internal void StockEdit(Stock stock)
    {

      _ = new StockEditViewModel(this, StockListView, stock);
      StockListView.DataContext = null;
      StockListView.DataContext = Medicine;

    }

  }
}
