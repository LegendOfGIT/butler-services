using System.Collections.Generic;

namespace Information.Satellite.Usecase.GetInformationItem
{
  public class GetInformationItemInteractorResponse
  {
    public Dictionary<string, IEnumerable<string>> Properties { get; set; }
    public string WebContent { get; set; }
  }
}
