namespace Information.Store.Factories.Tests
{
  using Xunit;

  public class NullableBooleanFromStringFactoryTests
  {
    private NullableBooleanFromStringFactory factory = new NullableBooleanFromStringFactory(); 

    [Fact]
    public void FactoryReturnsBooleanValueFromFalseValue()
    {
      Assert.False((bool?)factory.GetObjectFromString("False"));
    }

    [Fact]
    public void FactoryReturnsBooleanValueFromMixedWrittenFalseValue()
    {
      Assert.False((bool?)factory.GetObjectFromString("FAlsE"));
    }

    [Fact]
    public void FactoryReturnsBooleanValueFromFalseValueWithWhitespaces()
    {
      Assert.False((bool?)factory.GetObjectFromString(" False  "));
    }

    [Fact]
    public void FactoryReturnsBooleanValueFromTrueValue()
    {
      Assert.True((bool?)factory.GetObjectFromString("True"));
    }

    [Fact]
    public void FactoryReturnsBooleanValueFromMixedWrittenTrueValue()
    {
      Assert.True((bool?)factory.GetObjectFromString("tRUe"));
    }

    [Fact]
    public void FactoryReturnsBooleanValueFromTrueValueWithWhitespaces()
    {
      Assert.True((bool?)factory.GetObjectFromString(" true  "));
    }

    [Fact]
    public void FactoryReturnsNullBooleanWhenValueIsEmpty()
    {
      Assert.Null((bool?)factory.GetObjectFromString(string.Empty));
    }

    [Fact]
    public void FactoryReturnsNullBooleanWhenValueIsNull()
    {
      Assert.Null((bool?)factory.GetObjectFromString(null));
    }

    [Fact]
    public void FactoryReturnsNullBooleanWhenStringContainsNoBooleanConstellation()
    {
      Assert.Null((bool?)factory.GetObjectFromString("Its not a boolean"));
    }
  }
}
