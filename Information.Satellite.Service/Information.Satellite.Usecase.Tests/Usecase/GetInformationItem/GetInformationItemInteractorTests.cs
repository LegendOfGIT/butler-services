using Information.Satellite.Usecase.GetInformationItem;
using Information.Satellite.Usecase.Tests.Spies.Repository.GetWebContent;
using Information.Satellite.Usecase.Tests.Stubs.Repository.GetWebContent;
using System;
using System.Collections.Generic;
using Xunit;

namespace Information.Satellite.Usecase.GetInformationItems
{
  public class GetInformationItemInteractorTests
  {
    private WebContentRepositorySpy webContentRepositorySpy;

    public GetInformationItemInteractorTests()
    {
      this.webContentRepositorySpy = new WebContentRepositorySpy();
    }

    [Fact]
    public void TestCallsRepositoryToGetContentFromInformationSource()
    {
      var interactor = new GetInformationItemInteractor(this.webContentRepositorySpy);
      interactor.Execute(new GetInformationItemInteractorRequest());

      Assert.Equal(1, this.webContentRepositorySpy.GetWebContentAsStringCalls);
    }

    [Fact]
    public void TestInteractorReturnsContentFromInformationSource()
    {
      var repository = new WebContentRepositoryReturnsSpecificContentStub("emptyHtml.stub");
      var interactor = new GetInformationItemInteractor(repository);
      var response = interactor.Execute(new GetInformationItemInteractorRequest());

      Assert.Equal("<html></html>", response.WebContent);
    }

    [Fact]
    public void TestInteractorUsesUriFromRequestToGetWebContent()
    {
      var interactor = new GetInformationItemInteractor(this.webContentRepositorySpy);
      interactor.Execute(new GetInformationItemInteractorRequest
      {
        Uri = new Uri("http://store.steampowered.com/app/22370/Fallout_3_Game_of_the_Year_Edition/")
      });

      Assert.Equal(
        "http://store.steampowered.com/app/22370/Fallout_3_Game_of_the_Year_Edition/",
        this.webContentRepositorySpy.LastRequestedUri.ToString()
      );
    }

    [Fact]
    public void TestInteractorReturnsContentFromWithinHtmlTag()
    {
      var interactor = new GetInformationItemInteractor(new WebContentRepositoryReturnsSpecificContentStub("Fallout4Goty.stub"));
      var response = interactor.Execute(new GetInformationItemInteractorRequest());

      Assert.Equal(
        "Fallout 3: Game of the Year Edition",
        response.Title
      );
    }

    [Fact]
    public void TestInteractorReturnsReleaseDateFromWithinHtmlTagUsingTagParsingCommands()
    {
      var interactor = new GetInformationItemInteractor(new WebContentRepositoryReturnsSpecificContentStub("Fallout4Goty.stub"));
      var response = interactor.Execute(new GetInformationItemInteractorRequest
      {
        ContentParsingCommands = new[]
        {
          new ParseByTagCommand
          {
            TagName = "div",
            Attributes = new Dictionary<string, string>{ { "class", "release_date" } },
            ContentParsingCommand = new ParseByTagCommand {
              TagName = "div",
              Attributes = new Dictionary<string, string>{ { "class", "date" }}
            }
          }
        }
      });

      Assert.Equal(
        "13. Okt. 2009",
        response.ReleaseDate
      );
    }
  }
}
