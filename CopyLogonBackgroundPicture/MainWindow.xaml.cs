using CHi.Extensions;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace CopyLogonBackgroundPicture
{
  /// <summary>
  /// Interaction logic for MainWindow.xaml
  /// </summary>
  public partial class MainWindow : Window
  {

    public ObservableCollection<Slide> SlideStrip { get; set; } = new ObservableCollection<Slide>();
    public int CurrentNumber { get; private set; }

    public MainWindow()
    {
      Left = 10;
      Top = 50;
      InitializeComponent();
      CollectDetails();
      var slideStrip = (from a in SlideStrip
                        orderby a.FileDate descending
                        select a).ToList();
      SlideStrip = new ObservableCollection<Slide>(slideStrip);
      FillSlideStrip(SlideStrip);
      ShowImage(CurrentNumber);
    }

    private void CollectDetails()
    {
      string folder = $"C:\\Users\\{Environment.UserName}" +
        $"\\AppData\\Local\\Packages\\" +
        $"Microsoft.Windows.ContentDeliveryManager_cw5n1h2txyewy" +
        $"\\LocalState\\Assets";

      IEnumerable<string> fileNames = null;
      try
      {
        fileNames = Directory.EnumerateFiles(folder);
      }
      catch (Exception ex)
      {
        MessageBox.Show(ex.Message,
          "Error opening folder",
          MessageBoxButton.OK,
          MessageBoxImage.Error);
        Environment.Exit(1);
      }

      if (fileNames == null) 
      {
				MessageBox.Show("No files to select.",
					"Error select file",
					MessageBoxButton.OK,
					MessageBoxImage.Error);
				Environment.Exit(1);
			}

			foreach (string fileName in fileNames)
      {
        SlideStrip.Add(new Slide() { FileName = fileName });
      }

      CollectFileNameDetails();
    }

    private void CollectFileNameDetails()
    {
      foreach (Slide slide in SlideStrip)
      {
        FileInfo fileInfo = new FileInfo(slide.FileName);
        slide.FileDate = fileInfo.LastWriteTime;
      }
    }

    private void FillSlideStrip(ObservableCollection<Slide> slideStrip)
    {
      for (int i = 0; i < SlideStrip.Count; i++)
      {
        SlideStripStackPanel.Children.Add(CreateSlide(slideStrip[i], i));
      }
    }

    private Button CreateSlide(Slide slide, int number)
    {
      Uri uri = new Uri(slide.FileName, UriKind.Absolute);

      BitmapImage bitmapImage = null;
      try
      {
        bitmapImage = new BitmapImage(uri);
        slide.SlideWidth = bitmapImage.PixelWidth;
      }
      catch
      {
        slide.CanOpenSlide = false;
      }

      TextBlock textBlock = new TextBlock()
      {
        Text = $"{slide.FileDate.ToString("yyyy-MM-dd")}\n{slide.SlideWidth}",
        FontSize = 10,
        HorizontalAlignment = HorizontalAlignment.Center,
      };

      StackPanel stackPanel = new StackPanel()
      {
        Orientation = Orientation.Vertical,
      };
      stackPanel.Children.Add(new Image()
      {
        Source = bitmapImage,
        Stretch = Stretch.UniformToFill,
        Height = 64,
        Margin = new Thickness(0, 0, 2, 0),
      });
      stackPanel.Children.Add(textBlock);

      Button button = new Button()
      {
        Background = Brushes.White,
        BorderBrush = Brushes.Black,
        BorderThickness = new Thickness(0.5),
        Content = stackPanel,
        Name = $"ImageButton{number}",
      };
      button.Click += Border_Click;

      return button;
    }

    private void Border_Click(object sender, RoutedEventArgs e)
    {
      CurrentNumber = int.Parse(((Button)sender).Name.Split('n')[1]);
      ShowImage(CurrentNumber);
    }

    private void ShowImage(int number)
    {
      Uri uri = new Uri(SlideStrip[number].FileName, UriKind.Absolute);

      BitmapImage bitmapImage = null;
      try
      {
        bitmapImage = new BitmapImage(uri);
      }
      catch { }

      MainBorder.Child = new Image() { Source = bitmapImage };
    }

    private void CopyButton_Click(object sender, RoutedEventArgs e)
    {
      FileInfo fileInfo = new FileInfo(SlideStrip[CurrentNumber].FileName);

      SaveFileDialog saveFileDialog = new SaveFileDialog
      {
        DefaultExt = ".jpg",
        Filter = "Jpeg files|*.jpg|Png files|*.png",
        FileName = fileInfo.Name,
        Title = @"Copy the selected image to %OneDrive%\Pictures\Saved pictures".TranslatePath(),
        InitialDirectory = @"%OneDrive%\Pictures\Saved pictures".TranslatePath(),
        ValidateNames = true
      };

      if (saveFileDialog.ShowDialog() == true)
      {
        File.Copy(SlideStrip[CurrentNumber].FileName, saveFileDialog.FileName);
      }
    }

  }
}
