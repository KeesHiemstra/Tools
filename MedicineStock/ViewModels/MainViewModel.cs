using MedicationSupply.Extensions;
using MedicationSupply.Models;
using MedicationSupply.Views;
using Newtonsoft.Json;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows;

namespace MedicationSupply.ViewModels
{
  public class MainViewModel
  {

    #region [ Fields ]

    private MainWindow MainView;// { get; set; }
    private string JsonFile { get; set; } = "%OneDrive%\\Data\\MedicineStock.json";

    #endregion

    #region [ Properties ]

    public ObservableCollection<Medicine> Medicines { get; set; } = new ObservableCollection<Medicine>();

    #endregion

    #region [ Construction ]

    public MainViewModel(MainWindow mainWindow)
    {

      MainView = mainWindow;

      MainView.Title = 
        $"Medicine stock ({System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString()})";

      LoadMedicineStock();

    }

    #endregion

    public void Closing()
    {
      if (IsJsonChanged())
      {
        if (!File.Exists("NoAutoSave.trg"))
        {
          SaveMedicineStock();
        }
        else
        {
          MessageBox.Show("Not saved...");
        }
      }
    }

    #region Load & save

    public bool IsJsonChanged()
    {
      bool result = false;
      foreach (var item in Medicines)
      {
        result = result || item.IsChanged;

        foreach (var deep in item.Prescriptions)
        {
          result = result || deep.IsChanged;
        }

        foreach (var deep in item.Stocks)
        {
          result = result || deep.IsChanged;
        }
      }

      return result;
    }

    private void JsonIsSaved()
    {
      foreach (var item in Medicines)
      {
        item.Saved();
      }
    }

    private void LoadMedicineStock()
    {
      JsonFile = JsonFile.TranslatePath();
      if (File.Exists(JsonFile))
      {
        using (StreamReader stream = File.OpenText(JsonFile))
        {
          string json = stream.ReadToEnd();
          Medicines = JsonConvert.DeserializeObject<ObservableCollection<Medicine>>(json);
        }
      }

    }

    public void SaveMedicineStock()
    {
      //Create Json backup
      string BackupJsonFile = $"{JsonFile}.bak";
      if (File.Exists(BackupJsonFile))
      {
        File.Delete(BackupJsonFile);
      }
      if (File.Exists(JsonFile))
      {
        File.Copy(JsonFile, BackupJsonFile);
      }

      string json = JsonConvert.SerializeObject(Medicines, Formatting.Indented);
      using (StreamWriter stream = new StreamWriter(JsonFile))
      {
        stream.Write(json);
      }

      JsonIsSaved();
    }
    
    #endregion

    public void MedicineAdd()
    {

      _ = new MedicineViewModel(this, MainView, null);

    }

    public void MedicineEdit(Medicine medicine)
    {

      _ = new MedicineViewModel(this, MainView, medicine);

    }

    public void PrescriptionList(Medicine medicine)
    {

      _ = new PrescriptionListViewModel(MainView, medicine);
      MainView.DataContext = null;
      MainView.DataContext = this;

    }

    public void StockList(Medicine medicine)
    {

      _ = new StockListViewModel(MainView, medicine);
      MainView.DataContext = null;
      MainView.DataContext = this;

    }

    /// <summary>
    /// Copy the selected medicine name.
    /// </summary>
    /// <param name="medicine"></param>
    public void CopyName(Medicine medicine)
    {

      Clipboard.SetText(medicine.Name);

    }

    public void ShowHistory()
    {
      _ = new HistoryWindow()
      {
        Left = MainView.Left + 20,
        Top = MainView.Top + 20
      }.ShowDialog();
    }

  }
}
