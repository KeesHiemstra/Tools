using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeographicLocation.Models
{
  public struct Coordinate
  {

    //Dutch: breedte
    public double Latitude;
    //Dutch: lengte
    public double Longitude;

    public Coordinate(double latitude, double longitute)
    {
      Latitude = latitude;
      Longitude = longitute;
    }

  }
}
