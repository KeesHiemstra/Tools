using MedicationStock.Models;
using MedicationStock.Views;
using System;

namespace MedicationStock.ViewModels
{
  public class MedicineViewModel
  {
    #region [ Fields ]

    readonly MainViewModel MainVM;
    readonly Medicine Medicine;

    MedicineWindow MedicineView;

    #endregion

    #region [ Constructor ]

    public MedicineViewModel(MainViewModel main, MainWindow parent, Medicine medicine)
    {

      MainVM = main;
      bool exists = false;
      if (medicine != null)
      {
        Medicine = medicine;
        exists = true;
      }
      else
      {
        Medicine = new Medicine
        {
          StartDate = DateTime.Now.Date
        };
        exists = false;
      }

      ShowMedicineWindow(parent, exists);

    }

    #endregion

    private void ShowMedicineWindow(MainWindow parent, bool Exists)
    {

      MedicineView = new MedicineWindow(Medicine)
      {
        Left = parent.Left + 20,
        Top = parent.Top + 20
      };
      bool? Result = MedicineView.ShowDialog();

      if (!Exists && Result.Value)
      {
        MainVM.Medicines.Add(Medicine);
      }

    }

  }
}
