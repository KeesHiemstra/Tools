using System;
using System.ComponentModel;

namespace CopyLogonBackgroundPicture
{
  public class Slide : INotifyPropertyChanged
	{

    #region [ Fields ]

    private string fileName;
    private int slideWidth;
    private DateTime dateTime;
    private bool canOpenSlide = true;

    #endregion

    #region [ Properties ]

    public string FileName
    {
      get => fileName;
      set
      {
        if (fileName != value)
        {
          fileName = value;
          NotifyPropertyChanged("FileName");
        }
      }
    }

    public int SlideWidth
    {
      get => slideWidth;
      set
      {
        if (slideWidth != value)
        {
          slideWidth = value;
          NotifyPropertyChanged("SlideSize");
        }
      }
    }

    public DateTime FileDate
    {
      get => dateTime;
      set
      {
        if (dateTime != value)
        {
          dateTime = value;
          NotifyPropertyChanged("FileDate");
        }
      }
    }

    public bool CanOpenSlide
    {
      get => canOpenSlide;
      set
      {
        if (canOpenSlide != value)
        {
          canOpenSlide = value;
          NotifyPropertyChanged("CanOpenSlide");
        }
      }
    }


    #endregion

    #region [ Events ]

    public event PropertyChangedEventHandler PropertyChanged;
    private void NotifyPropertyChanged(string propertyName = "")
    {
      if (PropertyChanged != null)
      {
        PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
      }
    }

    #endregion

    #region [ Construction ]


    #endregion

    #region [ Public methods ]


    #endregion

  }
}
