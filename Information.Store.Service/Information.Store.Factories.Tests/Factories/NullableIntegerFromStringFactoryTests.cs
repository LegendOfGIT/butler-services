namespace Information.Store.Factories.Tests
{
  using Xunit;

  public class NullableIntegerFromStringFactoryTests
  {
    private NullableIntegerFromStringFactory factory = new NullableIntegerFromStringFactory(); 

    [Fact]
    public void FactoryReturnsIntegerValueFromNegativeNumberValue()
    {
      Assert.Equal(-5000, (int?)factory.GetObjectFromString("-5000"));
    }

    [Fact]
    public void FactoryReturnsNullIntegerValueFromNegativeOverflowNumberValue()
    {
      Assert.Null((int?)factory.GetObjectFromString("-999999999999999999"));
    }

    [Fact]
    public void FactoryReturnsIntegerValueFromPositiveNumberValue()
    {
      Assert.Equal(3000, (int?)factory.GetObjectFromString("3000"));
    }

    [Fact]
    public void FactoryReturnsNullIntegerValueFromPositiveOverflowNumberValue()
    {
      Assert.Null((int?)factory.GetObjectFromString("999999999999999999"));
    }

    [Fact]
    public void FactoryReturnsIntegerValueFromZeroNumberValue()
    {
      Assert.Equal(0, (int?)factory.GetObjectFromString("0"));
    }

    [Fact]
    public void FactoryReturnsIntegerValueFromNumberValuesWithWhitespaces()
    {
      Assert.Equal(4, (int?)factory.GetObjectFromString(" 4    "));
      Assert.Equal(-12, (int?)factory.GetObjectFromString("   -12  "));
      Assert.Equal(0, (int?)factory.GetObjectFromString("0   "));
    }

    [Fact]
    public void FactoryReturnsNullIntegerValueFromCharactersValue()
    {
      Assert.Null((int?)factory.GetObjectFromString("abc"));
    }

    [Fact]
    public void FactoryReturnsNullIntegerValueFromMixedNumberAndCharactersValue()
    {
      Assert.Null((int?)factory.GetObjectFromString("A1"));
    }

    [Fact]
    public void FactoryReturnsNullIntegerValueFromEmptyValue()
    {
      Assert.Null((int?)factory.GetObjectFromString(string.Empty));
    }
  }
}
