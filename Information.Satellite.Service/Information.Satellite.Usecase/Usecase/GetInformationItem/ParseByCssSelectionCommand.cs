using Information.Satellite.Usecase.Interfaces;

namespace Information.Satellite.Usecase.GetInformationItem
{
  public class ParseByCssSelectionCommand : IContentParsingCommand
  {
    public string Selector { get; set; }
    public IContentParsingCommand ContentParsingCommand { get; set; }
  }
}
