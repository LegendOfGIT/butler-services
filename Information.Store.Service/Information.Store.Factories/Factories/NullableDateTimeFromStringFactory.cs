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
        .ToLower()
        .Trim();

      value = GetValueWithReplacedMonthNames(value);

      return value.Replace(" ", string.Empty);
    }

    private string GetValueWithReplacedMonthNames(string value)
    {
      var cultureInfos = CultureInfo.GetCultures(CultureTypes.NeutralCultures);
      foreach(var cultureInfo in cultureInfos)
      {
        var monthSuffixes = new[] { " ", ". " };
        var dateTimeFormat = DateTimeFormatInfo.GetInstance(cultureInfo);
        for(int monthIndex = 1; monthIndex < dateTimeFormat.MonthNames.Length; monthIndex++)
        {
          for(int i = 1; i <= 2; i++){
            var monthName = (
              i == 1
              ? dateTimeFormat.MonthNames[monthIndex - 1]
              : dateTimeFormat.AbbreviatedMonthNames[monthIndex - 1].Replace(".", string.Empty)
            ).ToLower();

            if(monthName.Any(c => !char.IsDigit(c))) { 
              foreach (var monthSuffix in monthSuffixes)
              {
                var replacer = $"{monthName}{monthSuffix}";
                if (value.Contains(replacer))
                {
                  return value.Replace(replacer, $"{monthIndex.ToString("00")}.");
                }
              }
            }
          }
        }
      }

      return value;
    }
  }
}
