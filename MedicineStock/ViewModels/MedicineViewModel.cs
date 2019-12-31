using MedicationSupply.Models;
using MedicationSupply.Views;
using System;

namespace MedicationSupply.ViewModels
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
      if (medicine != null)
      {
        Medicine = medicine;
      }
      else
      {
        Medicine = new Medicine
        {
          StartDate = DateTime.Now.Date
        };
      }

      ShowMedicineWindow(parent, true);

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
