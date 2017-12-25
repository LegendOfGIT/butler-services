using System;
using Information.Satellite.Repository.Interfaces;

namespace Information.Satellite.Usecase.Tests.Stubs.Repository.GetWebContent
{
  public class WebContentRepositoryReturnsSpecificContentStub : IWebContentRepository
  {
    private string webContent;

    public WebContentRepositoryReturnsSpecificContentStub(string webContent)
    {
      this.webContent = webContent;
    }

    public string GetWebContentAsString(Uri uri)
    {
      return this.webContent;
    }
  }
}
