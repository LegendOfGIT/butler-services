using Information.Satellite.Usecase.Interfaces;

namespace Information.Satellite.Usecase.GetInformationItem
{
  public class ParseByCssSelectionCommand : IContentParsingCommand
  {
    public string Attribute { get; set; }
    public string Selector { get; set; }
    public string TargetPropertyId { get; set; }
    public IContentParsingCommand ContentParsingCommand { get; set; }
  }
}
