using System;
using Information.Satellite.Repository.Interfaces;

namespace Information.Satellite.Usecase.Tests.Spies.Repository.GetWebContent
{
  public class WebContentRepositorySpy : IWebContentRepository
  {
    public int GetWebContentAsStringCalls { get; set; }
    public Uri LastRequestedUri { get; set; }

    public string GetWebContentAsString(Uri uri)
    {
      this.GetWebContentAsStringCalls++;
      this.LastRequestedUri = uri;
      return null;
    }
  }
}
