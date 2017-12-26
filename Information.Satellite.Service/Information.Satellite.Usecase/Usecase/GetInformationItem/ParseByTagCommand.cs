using Information.Satellite.Usecase.Interfaces;
using System.Collections.Generic;

namespace Information.Satellite.Usecase.GetInformationItem
{
  public class ParseByTagCommand : IContentParsingCommand
  {
    public Dictionary<string, string> Attributes { get; set; }
    public IContentParsingCommand ContentParsingCommand { get; set; }
    public string TagName { get; set; }
  }
}
