using MedicationStock.Models;
using MedicationStock.Views;
using System;

namespace MedicationStock.ViewModels
{
  public class StockEditViewModel
  {

    #region [ Fields ]

    readonly StockListViewModel StockListViewModel;
    readonly StockListWindow StockListView;
    StockEditWindow StockEditView;

    readonly bool StockExists;

    #endregion

    #region [ Properties ]

    Stock Stock { get; set; }

    #endregion

    #region [ Construction ]

    public StockEditViewModel(StockListViewModel stockListViewModel, 
      StockListWindow stockListView, 
      Stock stock)
    {

      StockListViewModel = stockListViewModel;
      StockListView = stockListView;

      if (stock != null)
      {
        StockExists = true;
        Stock = stock;
      }
      else
      {
        //Add stock
        StockExists = false;
        Stock = new Stock()
        {
          Date = DateTime.Now.Date
        };
      }

      ShowStockEditView();

    }

    #endregion

    private void ShowStockEditView()
    {

      StockEditView = new StockEditWindow(this)
      {
        Left = StockListView.Left + 20,
        Top = StockListView.Top + 20,

        DataContext = Stock
      };
      StockEditView.InfoTextBox.Focus();
      bool? result = StockEditView.ShowDialog();
      if (result.Value && !StockExists)
      {
        StockListViewModel.Medicine.Stocks.Add(Stock);
      }

    }

    internal void SaveEdit()
    {

      StockEditView.DialogResult = true;
      

    }

    internal void CloseEdit()
    {

      StockEditView.DialogResult = false;

    }
  }
}
