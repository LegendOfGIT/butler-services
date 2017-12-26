using Information.Satellite.Usecase.Interfaces;
using System;

namespace Information.Satellite.Usecase.GetInformationItem
{
  public class GetInformationItemInteractorRequest
  {
    public Uri Uri { get; set; }
    public IContentParsingCommand[] ContentParsingCommands { get; set; }
  }
}
