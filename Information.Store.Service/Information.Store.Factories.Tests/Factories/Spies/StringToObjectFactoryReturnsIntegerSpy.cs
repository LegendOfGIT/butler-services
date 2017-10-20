namespace Information.Store.Factories.Tests
{
  public class StringToObjectFactoryReturnsIntegerSpy : IStringToObject
  {
    public const string NotRecognizedValue = "This is no integer";
    public int GetObjectFromStringCalls { get; set; }

    public object GetObjectFromString(string value)
    {
      this.GetObjectFromStringCalls++;
      return value == NotRecognizedValue ? (int?)null : 1;
    }
  }
}
