using MedicationStock.Models;
using MedicationStock.Views;
using System;

namespace MedicationStock.ViewModels
{
  public class PrescriptionEditViewModel
  {

    #region [ Fields ]

    readonly PrescriptionListViewModel PrescriptionListViewModel;
    readonly PrescriptionListWindow PrescriptionListView;
    PrescriptionEditWindow PrescriptionEditView;

    readonly bool PrescriptionExists;

    #endregion

    #region [ Properties ]

    Prescription Prescription { get; set; }

    #endregion

    #region [ Construction ]

    public PrescriptionEditViewModel(PrescriptionListViewModel prescriptionListViewModel,
      PrescriptionListWindow prescriptionListView, 
      Prescription prescription)
    {

      PrescriptionListViewModel = prescriptionListViewModel;
      PrescriptionListView = prescriptionListView;

      if (prescription != null)
      {
        PrescriptionExists = true;
        Prescription = prescription;
      }
      else
      {
        //Add prescription
        PrescriptionExists = false;
        Prescription = new Prescription()
        {
          Date = DateTime.Now.Date
        };
      }

      ShowPrescriptionEditView();

    }

    #endregion

    private void ShowPrescriptionEditView()
    {

      PrescriptionEditView = new PrescriptionEditWindow(this)
      {
        Left = PrescriptionListView.Left + 20,
        Top = PrescriptionListView.Top + 20,

        DataContext = Prescription
      };
      PrescriptionEditView.InfoTextBox.Focus();
      bool? result = PrescriptionEditView.ShowDialog();
      if (result.Value && !PrescriptionExists)
      {
        PrescriptionListViewModel.Medicine.Prescriptions.Add(Prescription);
      }

    }

    internal void SaveEdit()
    {

      PrescriptionEditView.DialogResult = true;

    }

    internal void CloseEdit()
    {

      PrescriptionEditView.DialogResult = false;

    }

  }
}
