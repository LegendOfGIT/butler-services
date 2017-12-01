using System;
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
      value = value ?? string.Empty;
      return value.Replace(" ", string.Empty);
    }
  }
}
