using System;
using Information.Satellite.Repository.Interfaces;
using System.IO;

namespace Information.Satellite.Usecase.Tests.Stubs.Repository.GetWebContent
{
  public class WebContentRepositoryReturnsSpecificContentStub : IWebContentRepository
  {
    private string webContent;

    public WebContentRepositoryReturnsSpecificContentStub(string webContentStubFile)
    {
      this.webContent = File.ReadAllText($@"../../Stubs/Repository/WebContent/{webContentStubFile}");
    }

    public string GetWebContentAsString(Uri uri)
    {
      return this.webContent;
    }
  }
}
