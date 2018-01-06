using Information.Satellite.Usecase.Interfaces;
using System;
using System.Collections.Generic;

namespace Information.Satellite.Usecase.GetInformationItem
{
  public class GetInformationItemInteractorRequest
  {
    public Uri Uri { get; set; }
    public IEnumerable<IContentParsingCommand> ContentParsingCommands { get; set; }
  }
}
