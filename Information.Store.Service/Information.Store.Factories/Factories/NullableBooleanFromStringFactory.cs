namespace Information.Store.Factories
{
  public class NullableBooleanFromStringFactory : IStringToObject
  {
    public object GetObjectFromString(string value) => (
      bool.TryParse((value ?? string.Empty), out bool boolValue)
        ? (bool?)boolValue
        : default(bool?)
    );
  }
}
