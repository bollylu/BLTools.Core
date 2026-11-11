namespace BLTools.Core;

/// <summary>
/// DateTime extensions
/// </summary>
public static class DateTimeExtension {
  extension(DateTime datetime) {

    /// <summary>
    /// Returns a DateTime as a string formatted as "yyyy-MM-dd"
    /// </summary>
    /// <param name="datetime">The source Datetime</param>
    /// <returns>A DateTime as a string formatted as "yyyy-MM-dd"</returns>
    public string ToYMD() {
      return datetime.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);
    }



    /// <summary>
    /// Returns a DateTime as a string formatted as "dd/MM/yyyy"
    /// </summary>
    /// <param name="datetime">The source Datetime</param>
    /// <returns>A DateTime as a string formatted as "dd/MM/yyyy"</returns>
    public string ToDMY() {
      return datetime.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
    }



    /// <summary>
    /// Returns a DateTime as a string formatted as "yyyy-MM-dd HH:mm:ss"
    /// </summary>
    /// <param name="datetime">The source Datetime</param>
    /// <returns>A DateTime as a string formatted as "yyyy-MM-dd HH:mm:ss"</returns>
    public string ToYMDHMS() {
      return datetime.ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture);
    }



    /// <summary>
    /// Returns a DateTime as a string formatted as "dd/MM/yyyy HH:mm:ss"
    /// </summary>
    /// <param name="datetime">The source Datetime</param>
    /// <returns>A DateTime as a string formatted as "dd/MM/yyyy HH:mm:ss"</returns>
    public string ToDMYHMS() {
      return datetime.ToString("dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture);
    }



    /// <summary>
    /// Returns a DateTime as a string formatted as "HH:mm:ss"
    /// </summary>
    /// <param name="datetime">The source Datetime</param>
    /// <returns>A DateTime as a string formatted as "HH:mm:ss"</returns>
    public string ToHMS() {
      return datetime.ToString("HH:mm:ss", CultureInfo.InvariantCulture);
    }



    /// <summary>
    /// Returns a UTC DateTime converted into local DateTime
    /// </summary>
    /// <param name="datetime">The source Datetime</param>
    /// <returns>A UTC DateTime converted into local DateTime</returns>
    public DateTime FromUTC() {
      return datetime.ToLocalTime();
    }



    /// <summary>
    /// Returns a DateTime as a formatted string or a specific value for empty dates
    /// </summary>
    /// <param name="datetime">The source DateTime</param>
    /// <param name="valueForEmptyDate">The string value to return when DateTime is DateTime.MinValue (default = "-")</param>
    /// <returns>The DateTime converted or the default value</returns>
    public string EmptyDateAs(string valueForEmptyDate = "-") {
      if (datetime.Date == DateTime.MinValue.Date) {
        return valueForEmptyDate;
      }
      return datetime.DateOrDateTime();
    }



    /// <summary>
    /// Returns a DateTime as a formatted string or a "-" for empty dates
    /// </summary>
    /// <param name="datetime">The source DateTime</param>
    /// <returns>The DateTime converted or "-"</returns>
    public string EmptyDateAsDash() {
      return datetime.EmptyDateAs("-");
    }



    /// <summary>
    /// Returns a DateTime as a formatted string or blank ("") for empty dates
    /// </summary>
    /// <param name="datetime">The source DateTime</param>
    /// <returns>The DateTime converted or blank ("")</returns>
    public string EmptyDateAsBlank() {
      return datetime.EmptyDateAs("");
    }



    /// <summary>
    /// Returns a DateTime as a formatted string with date only or date and time depending if time is 00:00:00 or not
    /// </summary>
    /// <param name="datetime">The source DateTime</param>
    /// <returns>The formatted string</returns>
    public string DateOrDateTime() {
      if (datetime.TimeOfDay == DateTime.MinValue.TimeOfDay) {
        return datetime.ToYMD();
      } else {
        return datetime.ToYMDHMS();
      }
    }



    /// <summary>
    /// Returns The time part of the date time
    /// </summary>
    /// <param name="datetime">The source DateTime</param>
    /// <returns>A DateTime containing only time</returns>
    public DateTime Time() {
      return default(DateTime).Add(datetime.TimeOfDay);
    }



    /// <summary>
    /// Returns next day (with correct rolling of the month/year)
    /// </summary>
    /// <param name="datetime">The source DateTime</param>
    /// <returns>A DateTime containing only time</returns>
    public DateTime NextDay() {
      return datetime.AddDays(1);
    }



    /// <summary>
    /// Returns previous day (with correct rolling of the month/year)
    /// </summary>
    /// <param name="datetime">The source DateTime</param>
    /// <returns>A DateTime containing only time</returns>
    public DateTime PreviousDay() {
      return datetime.AddDays(-1);
    }
  }


}
