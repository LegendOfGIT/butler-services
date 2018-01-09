namespace Information.Store.Factories.Tests
{
  using System;
  using Xunit;

  public class NullableDateTimeFromStringFactoryTests
  {
    private NullableDateTimeFromStringFactory factory = new NullableDateTimeFromStringFactory(); 

    [Fact]
    public void FactoryReturnsDateTimeValueFromUSDateTimeFormat_yyyy_mm_dd_Value()
    {
      Assert.Equal((DateTime?)(new DateTime(2017, 4, 3)), (DateTime?)factory.GetObjectFromString("2017-04-03"));
    }

    [Fact]
    public void FactoryReturnsDateTimeValueFromUSDateTimeFormat_yyyy_mm_dd_ValueWithSlashDivider()
    {
      Assert.Equal((DateTime?)(new DateTime(2017, 4, 3)), (DateTime?)factory.GetObjectFromString("2017/04/03"));
    }

    [Fact]
    public void FactoryReturnsDateTimeValueFromUSDateTimeFormat_yyyy_mm_dd_ValueWithWhitespaces()
    {
      Assert.Equal((DateTime?)(new DateTime(2014, 2, 16)), (DateTime?)factory.GetObjectFromString("2014 -02-  16 "));
    }

    [Fact]
    public void FactoryReturnsDateTimeValueFromGermanDateTimeFormat_dd_MM_yyyy_Value()
    {
      Assert.Equal((DateTime?)(new DateTime(2019, 6, 12)), (DateTime?)factory.GetObjectFromString("12.06.2019"));
    }

    [Fact]
    public void FactoryReturnsDateTimeValueFromGermanDateTimeFormat_dd_MM_yyyy_ValueWithSlashDivider()
    {
      Assert.Equal((DateTime?)(new DateTime(2019, 6, 12)), (DateTime?)factory.GetObjectFromString("12/06/2019"));
    }

    [Fact]
    public void FactoryReturnsDateTimeValueFromGermanDateTimeFormat_dd_MM_yyyy_ValueWithWhitespaces()
    {
      Assert.Equal((DateTime?)(new DateTime(2012, 3, 1)), (DateTime?)factory.GetObjectFromString("    01. 0 3.   2  012  "));
    }

    [Fact]
    public void FactoryReturnsDateTimeValueFromSpanishDateWithFullMonthNameAndWhitespaces()
    {
      Assert.Equal((DateTime?)(new DateTime(2014, 6, 4)), (DateTime?)factory.GetObjectFromString("    04. Junio   2  014  "));
      Assert.Equal((DateTime?)(new DateTime(2014, 6, 4)), (DateTime?)factory.GetObjectFromString("    04. Junio.   2  014 "));
    }

    [Fact]
    public void FactoryReturnsDateTimeValueFromHungarianDateWithMonthNameAbbrevationAndWhitespaces()
    {
      Assert.Equal((DateTime?)(new DateTime(2009, 3, 11)), (DateTime?)factory.GetObjectFromString("    11. Márc   20 09  "));
      Assert.Equal((DateTime?)(new DateTime(2009, 3, 11)), (DateTime?)factory.GetObjectFromString("    11. Márc.   20 09 "));
    }
  }
}
