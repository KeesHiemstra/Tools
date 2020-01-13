using CHi.Extensions;
using GeographicLocation.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeographicLocation.ViewModels
{
  public class MainViewModel
  {

    private MainWindow MainView;
    private string jsonFile = "%OneDrive%\\Data\\GeographicLocation.json".TranslatePath();

    public List<Location> Locations { get; set; } = new List<Location>();

    #region [ Construction ]

    public MainViewModel(MainWindow mainView)
    {

      this.MainView = mainView;

      if (!File.Exists(jsonFile))
      {
        CreateInitialJson();
      }

    }

    #endregion

    private void CreateInitialJson()
    {

      Location location = new Location()
      {
        Name = "Amsterdam",
        Coordinate = new Coordinate(latitude: 52.373248, longitute: 4.892510)
      };
      Locations.Add(location);

      SaveJson(jsonFile);

    }

    private void SaveJson(string jsonFile)
    {

      string json = JsonConvert.SerializeObject(Locations, Formatting.Indented);
      using (StreamWriter stream = new StreamWriter(jsonFile))
      {
        stream.Write(json);
      }


    }


  }
}
