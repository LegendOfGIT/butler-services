namespace Information.Store.Factories
{
  public class NullableIntegerFromStringFactory : IStringToObject
  {
    public object GetObjectFromString(string value) => (
      int.TryParse((value ?? string.Empty), out int intValue)
        ? (int?)intValue
        : default(int?)
    );
  }
}
