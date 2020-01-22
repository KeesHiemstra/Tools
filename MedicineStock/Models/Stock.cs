using Newtonsoft.Json;
using System;
using System.ComponentModel;

namespace MedicationStock.Models
{
  public class Stock : INotifyPropertyChanged
  {

    #region [ Fields ]

    private DateTime date;
    private string info;
    private int count;
    private bool counting;

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
    public int Count 
    { 
      get => count;
      set
      {
        if (count != value)
        {
          count = value;
          NotifyPropertyChanged("Count");
        }
      }
    }
    public bool Counting 
    { 
      get => counting;
      set
      {
        if (counting != value)
        {
          counting = value;
          NotifyPropertyChanged("Counting");
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
