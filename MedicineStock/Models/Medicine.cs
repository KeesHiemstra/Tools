using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;

namespace MedicationStock.Models
{
  public class Medicine : INotifyPropertyChanged
  {

    #region [ Fields ]

    private readonly DateTime CalculateCountDate = DateTime.Now.Date;
    private int id;
    private string name;
    private bool recurring;
    private string info;
    private DateTime startDate;
    private DateTime? finishDate;
    private decimal dailyUsage = 1;

    #endregion

    #region [ Properties ]

    public int Id
    {
      get => id;
      set
      {
        if (id != value)
        {
          id = value;
          NotifyPropertyChanged("Id");
        }
      }
    }
    public string Name
    {
      get => name;
      set
      {
        if (name != value)
        {
          name = value;
          NotifyPropertyChanged("Name");
        }
      }
    }
    public bool Recurring
    {
      get => recurring;
      set
      {
        if (recurring != value)
        {
          recurring = value;
          NotifyPropertyChanged("Recurring");
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
    public DateTime StartDate
    {
      get => startDate.Date;
      set
      {
        if (startDate != value)
        {
          startDate = value.Date;
          NotifyPropertyChanged("StartDate");
        }
      }
    }
    public DateTime? FinishDate
    {
      get => finishDate;
      set
      {
        if (finishDate != value)
        {
          if (value is null)
          {
            finishDate = value;
          }
          else
          {
            finishDate = value.Value.Date;
          }
          NotifyPropertyChanged();
        }
      }
    }
    [JsonIgnore]
    public bool IsChanged { get; private set; }

    public ObservableCollection<Prescription> Prescriptions { get; set; } = 
      new ObservableCollection<Prescription>();
    public ObservableCollection<Stock> Stocks { get; set; } = 
      new ObservableCollection<Stock>();

    #endregion

    #region [ Methods ]

    #region INotifyPropertyChanged
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


    public void Saved()
    {
      IsChanged = false;
    }

    [JsonIgnore]
    public int LastPrescripeStrength
    {
      get
      {
        if (Prescriptions.Count == 0)
        {
          return -1;
        }
        return Prescriptions
        .First()
        .Strength;
      }
    }
    [JsonIgnore]
    public decimal LastPrescripeDay
    {
      get
      {
        if (Prescriptions.Count == 0)
        {
          return -1;
        }
        return Prescriptions
        .First()
        .Day;
      }
    }
    [JsonIgnore]
    public decimal LastPrescriptionUnit
    {
      get
      {
        if (Prescriptions.Count == 0)
        {
          return -1;
        }
        return Prescriptions
          .First()
          .Unit;
      }
    }

    [JsonIgnore]
    public int PredictedStock
    {
      get { return CalculateStock(); }
    }

    [JsonIgnore]
    public DateTime PredictedEndStock
    {
      get
      {
        return CalculateCountDate.AddDays((int)((decimal)PredictedStock / dailyUsage));
        //Calculate the end based on the dailyUsage
      }
    }

    #endregion

    private int CalculateStock()
    {
      int result = 0;
      DateTime countDate = DateTime.Now.Date;
      dailyUsage = 1; //(re)Calculate the dailyUsage

      if (Stocks.Count == 0)
      {
        return -1;
      }

      //Calculate total stock
      List<Stock> stock = Stocks
        .OrderByDescending(x => x.Date)
        .ToList();

      DateTime CountStock = stock
        .Last()
        .Date;

      foreach (Stock item in stock)
      {
        result += item.Count;
        if (item.Counting)
        {
          countDate = item.Date;
          break;
        }
      }

      List<Prescription> prescriptions = Prescriptions
        .OrderBy(x => x.Date)
        .Where(x => (x.Date <= CalculateCountDate))
        .ToList();

      //Find the previous prescription date of the count date
      DateTime previousDate = new DateTime(1, 1, 1);
      decimal subTotalUsage = 0;
      foreach (Prescription item in prescriptions.Where(x => x.Date <= countDate))
      {
        previousDate = item.Date;
        dailyUsage = item.Unit / item.Day;
      }

      if (previousDate < countDate)
      {
        previousDate = countDate;
      }

      foreach (Prescription item in prescriptions.Where(x => (x.Date <= CalculateCountDate) && 
                                                             (x.Date >= previousDate) && 
                                                             (x.Date >= countDate)))
      {
        if (countDate > previousDate)
        {
          subTotalUsage = (countDate - previousDate).Days * dailyUsage;
        }
        else
        {
          subTotalUsage += (item.Date - previousDate).Days * dailyUsage;
        }
        previousDate = item.Date;
        dailyUsage = item.Unit / item.Day;
      }

      result -= (int)subTotalUsage;

      //Calculate remaining stock
      result -= (int)((CalculateCountDate - previousDate).Days * dailyUsage);

      return result;
    }

  }
}
