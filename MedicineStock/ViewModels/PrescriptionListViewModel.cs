using MedicationStock.Models;
using MedicationStock.Views;
using System.Collections.ObjectModel;
using System.Linq;

namespace MedicationStock.ViewModels
{
  public class PrescriptionListViewModel
  {

    #region [ Fields ]

    readonly MainWindow MainView;
    PrescriptionListWindow PrescriptionListView;

    #endregion

    #region [ Properties ]

    public Medicine Medicine { get; set; }

    #endregion

    #region [ Construction ]

    public PrescriptionListViewModel(MainWindow mainView, Medicine medicine)
    {

      MainView = mainView; //Location sub window
      Medicine = medicine; //Medicine selected data

      ShowPrescriptionList();

    }

    #endregion

    internal void ShowPrescriptionList()
    {

      UpdatePrescriptionList();

      PrescriptionListView = new PrescriptionListWindow(this)
      {
        Left = MainView.Left + 20,
        Top = MainView.Top + 20,

        DataContext = Medicine
      };

      PrescriptionListView.ShowDialog();

    }

    private void UpdatePrescriptionList()
    {

      var lst = (from s in Medicine.Prescriptions
                 orderby s.Date descending
                 select s).ToList();
      Medicine.Prescriptions = new ObservableCollection<Prescription>(lst);

    }

    internal void AddList()
    {

      _ = new PrescriptionEditViewModel(this, PrescriptionListView, null);
      UpdatePrescriptionList();
      PrescriptionListView.DataContext = null;
      PrescriptionListView.DataContext = Medicine;


    }

    internal void CloseList()
    {

      PrescriptionListView.DialogResult = true;

    }

    internal void PrescriptionEdit(Prescription prescription)
    {

      _ = new PrescriptionEditViewModel(this, PrescriptionListView, prescription);
      PrescriptionListView.DataContext = null;
      PrescriptionListView.DataContext = Medicine;

    }

  }
}
