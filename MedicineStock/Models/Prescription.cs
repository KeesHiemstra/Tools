using Newtonsoft.Json;
using System;
using System.ComponentModel;

namespace MedicationStock.Models
{
  public class Prescription : INotifyPropertyChanged
  {

    #region [ Fields ]

    private DateTime date;
    private string info;
    private int strength;
    private decimal day;
    private decimal unit;

    #endregion

    #region [ Properties ]

    public DateTime Date
    {
      get => date;
      set
      {
        if (date != value)
        {
          date = value.Date;
          NotifyPropertyChanged("Date");
        }
      }
    }
    public string Info
    {
      get => info;
      set
      {
        if (info != value)
        {
          info = value;
          NotifyPropertyChanged("Info");
        }
      }
    }
    public int Strength
    {
      get => strength;
      set
      {
        if (strength != value)
        {
          strength = value;
          NotifyPropertyChanged("Strength");
        }
      }
    }
    public decimal Day
    {
      get => day;
      set
      {
        if (day != value)
        {
          day = value;
          NotifyPropertyChanged("Dosage");
        }
      }
    }
    public decimal Unit
    {
      get => unit;
      set
      {
        if (unit != value)
        {
          unit = value;
          NotifyPropertyChanged("Unit");
        }
      }
    }
    [JsonIgnore]
    public bool IsChanged { get; private set; }

    #endregion

    #region [ Methods ]

    public event PropertyChangedEventHandler PropertyChanged;
    private void NotifyPropertyChanged(string propertyName = "")
    {
      if (PropertyChanged != null)
      {
        PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        IsChanged = true;
      }
    }

    #endregion

  }
}
