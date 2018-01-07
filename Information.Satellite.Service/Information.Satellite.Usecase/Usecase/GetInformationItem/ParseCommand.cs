namespace Information.Satellite.Usecase.GetInformationItem
{
  public class ParseCommand
  {
    public string Attribute { get; set; }
    public string Selector { get; set; }
    public int? TargetIndex { get; set; }
    public string TargetPropertyId { get; set; }
    public ParseCommandType Type { get; set; }
    public ParseCommand ContentParsingCommand { get; set; }
  }
}
