using System;
using System.Linq;
using System.Globalization;

namespace Information.Store.Factories
{
  public class NullableDateTimeFromStringFactory : IStringToObject
  {
    public object GetObjectFromString(string value) => (
      DateTime.TryParseExact(
        GetPreparedValueForDateTimeFactoring(value), 
        new[] { "yyyy/MM/dd", "yyyy-MM-dd" }, 
        CultureInfo.GetCultureInfo("en-US"), 
        DateTimeStyles.None, 
        out DateTime dateTimeUSValue
      ) ? (DateTime?)dateTimeUSValue

      : DateTime.TryParseExact(
        GetPreparedValueForDateTimeFactoring(value),
        new[] { "dd/MM/yyyy", "dd.MM.yyyy" },
        CultureInfo.GetCultureInfo("de-DE"), 
        DateTimeStyles.None, 
        out DateTime dateTimeDEValue
      ) ? (DateTime?)dateTimeDEValue

      : default(DateTime?)
    );

    private string GetPreparedValueForDateTimeFactoring(string value)
    {
      value = (value ?? string.Empty)
        
        .ToLower();

      value = GetValueWithReplacedMonthNames(value);

      return value.Replace(" ", string.Empty);
    }

    private string GetValueWithReplacedMonthNames(string value)
    {
      var cultureInfos = CultureInfo.GetCultures(CultureTypes.AllCultures);
      foreach(var cultureInfo in cultureInfos)
      {
        var dateTimeFormat = DateTimeFormatInfo.GetInstance(cultureInfo);
        for(int i = 1; i < dateTimeFormat.MonthNames.Length; i++)
        {
          var monthName = dateTimeFormat.MonthNames[i - 1].ToLower();
          if (monthName.StartsWith("0")) { 
          value = value.Replace(monthName + " ", i.ToString("00") + ".");
          value = value.Replace(monthName + ".", i.ToString("00") + ".");
          }
        }
      }

      return value;
    }
  }
}
