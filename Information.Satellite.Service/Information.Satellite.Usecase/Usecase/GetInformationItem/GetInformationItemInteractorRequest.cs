using System;
using System.Collections.Generic;

namespace Information.Satellite.Usecase.GetInformationItem
{
  public class GetInformationItemInteractorRequest
  {
    public Uri Uri { get; set; }
    public IEnumerable<ParseCommand> ContentParsingCommands { get; set; }
  }
}
