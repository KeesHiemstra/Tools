using MedicationSupply.Extensions;
using MedicationSupply.Models;
using Newtonsoft.Json;
using System.Collections.ObjectModel;
using System.IO;

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

      LoadMedicineStock();

    }

    #endregion

    #region Load & save

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

  }
}
