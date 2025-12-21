using System;
using System.Text.RegularExpressions;

namespace CHi.Extensions
{
  /* History
   * Version 1.0.0.2 (2020-01-27)
   * - Added: string SavePath(this string)
   * Version 1.0.0.1 (2018)
   * - Added: string TranslatePath(this string)
   */

  public static class PathExtensions
  {
    #region TranslatePath
    /// <summary>
    /// Translate the PathName with %OneDrive% to the full path.
    /// %OneDrive% is a environment variable.
    /// </summary>
    /// <param name="PathName"></param>
    /// <returns></returns>
    // 2018-??-??, Kees Hiemstra
    public static string TranslatePath(this string PathName)
    {
      
      string result = PathName;

      Regex regex = new Regex("%OneDrive%", RegexOptions.IgnoreCase);
      if (regex.IsMatch(result))
      {
        result = regex.Replace(result, Environment.GetEnvironmentVariable("OneDrive"));
      }

      return result;

    }
    #endregion

    #region SavePath
    /// <summary>
    /// Replace the part of PathName to %OneDrive% if it exists.
    /// %OneDrive% is a environment variable.
    /// </summary>
    /// <param name="PathName"></param>
    /// <returns></returns>
    // 2020-01-27, Kees Hiemstra
    public static string SavePath(this string PathName)
    {

      string result = PathName;

      if (string.IsNullOrEmpty(Environment.GetEnvironmentVariable("OneDrive")))
      {
        return result;
      }

      string OneDrive = Environment.GetEnvironmentVariable("OneDrive").Replace("\\", "\\\\");
      Regex regex = new Regex(OneDrive, RegexOptions.IgnoreCase);
      if (regex.IsMatch(result))
      {
        result = regex.Replace(result, "%OneDrive%");
      }
 
      return result;
    }
    #endregion
  }

}
