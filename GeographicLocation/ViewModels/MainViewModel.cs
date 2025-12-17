using CHi.Extensions;

using GeographicLocation.Models;

using Newtonsoft.Json;

using System;
using System.Collections.Generic;
using System.IO;

namespace GeographicLocation.ViewModels
{
  public class MainViewModel
  {

    public enum CoordinateFormatType { Decimal, Text };
    private enum CoordinateType { Latitude, Longitude };

    private MainWindow MainView;
    private string jsonFile = "%OneDrive%\\Data\\GeographicLocation.json".TranslatePath();

    public CoordinateFormatType CoordinateFormat { get; set; } = CoordinateFormatType.Decimal;
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

    public void CoordinateChanged(Location location, CoordinateFormatType coordinateFormat)
    {

      SelectedLocation = location;

      MainView.LatitudeTextBlock.Text = $"{SelectedLocation.Coordinate.Latitude}";
      MainView.LatitudeFormatedTextBlock.Text = $"({VectorDoubleToString(SelectedLocation.Coordinate.Latitude, CoordinateType.Latitude)})";

      MainView.LongitudeTextBlock.Text = $"{SelectedLocation.Coordinate.Longitude}";
      MainView.LongitudeFormatedTextBlock.Text = $"({VectorDoubleToString(SelectedLocation.Coordinate.Longitude, CoordinateType.Longitude)})";

      switch (coordinateFormat)
      {
        case CoordinateFormatType.Decimal:
          MainView.LatitudeTextBox.Text = $"{SelectedLocation.Coordinate.Latitude}";
          MainView.LongitudeTextBox.Text = $"{SelectedLocation.Coordinate.Longitude}";
          break;
        case CoordinateFormatType.Text:
          MainView.LatitudeTextBox.Text = $"{VectorDoubleToString(SelectedLocation.Coordinate.Latitude, CoordinateType.Latitude)}";
          MainView.LongitudeTextBox.Text = $"{VectorDoubleToString(SelectedLocation.Coordinate.Longitude, CoordinateType.Longitude)}";
          break;
        default:
          break;
      }

      MainView.NameTextBox.Text = SelectedLocation.Name;

    }

    public void CoordinateFormatChanged(CoordinateFormatType coordinateFormat)
    {
      switch (coordinateFormat)
      {
        case CoordinateFormatType.Decimal:
          MainView.LatitudeTextBox.Text = $"{SelectedLocation.Coordinate.Latitude}";
          MainView.LongitudeTextBox.Text = $"{SelectedLocation.Coordinate.Longitude}";
          break;
        case CoordinateFormatType.Text:
          MainView.LatitudeTextBox.Text = $"{VectorDoubleToString(SelectedLocation.Coordinate.Latitude, CoordinateType.Latitude)}";
          MainView.LongitudeTextBox.Text = $"{VectorDoubleToString(SelectedLocation.Coordinate.Longitude, CoordinateType.Longitude)}";
          break;
        default:
          break;
      }
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

    private bool Save(bool Exists)
    {

      double lat, lon;
      if (!double.TryParse(MainView.LatitudeTextBox.Text, out lat)) { return false; }
      if (!double.TryParse(MainView.LongitudeTextBox.Text, out lon)) { return false; }

      if (!Exists)
      {
        SelectedLocation = new Location();
      }

      SelectedLocation.Name = MainView.NameTextBox.Text;
      SelectedLocation.Coordinate = new Coordinate(latitude: lat, longitute: lon);

      if (!Exists)
      {
        Locations.Add(SelectedLocation);
      }

      return true;

    }

    public void SaveJson()
    {
      Save(true);
      SaveJson(jsonFile);
    }

    public void AddToJson()
    {
      Save(false);
    }

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
      result = $"{result}{vector.ToString("0.0000")}\" ";

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
