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
    public Location SelectedLocation { get; set; } = new Location();

    #region [ Construction ]

    public MainViewModel(MainWindow mainView)
    {

      this.MainView = mainView;

      if (!File.Exists(jsonFile))
      {
        CreateInitialJson();
      }
      else
      {
        using (StreamReader stream = File.OpenText(jsonFile))
        {
          string json = stream.ReadToEnd();
          Locations = JsonConvert.DeserializeObject<List<Location>>(json);
        }
      }

      MainView.LocationComboBox.ItemsSource = Locations;


    }

    #endregion

    public void CoordinateChanged(Location location)
    {

      SelectedLocation = location;
      MainView.LatitudeTextBox.Text = $"{SelectedLocation.Coordinate.Latitude}";
      MainView.LatitudeFormatedTextBox.Text = $"({VectorDoubleToString(SelectedLocation.Coordinate.Latitude, CoordinateType.Latitude)})";
      MainView.LongitudeTextBox.Text = $"{SelectedLocation.Coordinate.Longitude}";
      MainView.LongitudeFormatedTextBox.Text = $"({VectorDoubleToString(SelectedLocation.Coordinate.Longitude, CoordinateType.Longitude)})";

    }

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

    enum CoordinateType { Latitude, Longitude };

    /// <summary>
    /// Vector can be latitude number or longitude number.
    /// latitude must be between -90 to 90 (vertical). Longitude must between -180 to 180.
    /// </summary>
    /// <returns></returns>
    private string VectorDoubleToString(double vector, CoordinateType coordinateType)
    {

      string result = string.Empty;
      bool positive;


      if (coordinateType == CoordinateType.Latitude && (vector < -90 || vector > 90))
      {
        return string.Empty;
      }
      else if (coordinateType == CoordinateType.Longitude && (vector < -180 || vector > 180))
      {
        return string.Empty;
      }

      //Sign
      positive = vector >= 0;
      vector = Math.Abs(vector);
      //Degrees
      result = $"{(int)vector}° ";
      vector %= 1.0;
      //Minutes
      vector *= 60;
      result = $"{result}{(int)(vector)}\' ";
      vector %= 1.0;
      //Seconds
      vector *= 60;
      result = $"{result}{vector.ToString("#.#####")}\" ";

      if (coordinateType == CoordinateType.Latitude)
      {
        result = $"{result}{(positive ? "N" : "S")}";
      }
      else
      {
        result = $"{result}{(positive ? "E" : "W")}";
      }

      return result;

    }
  }
}
