namespace Information.Satellite.Usecase.Interfaces
{
  public interface IContentParsingCommand {
    string TargetPropertyId { get; set; }
    IContentParsingCommand ContentParsingCommand { get; set; }
  }
}
