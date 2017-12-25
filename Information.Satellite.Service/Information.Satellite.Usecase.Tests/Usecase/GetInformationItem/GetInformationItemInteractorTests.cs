using Information.Satellite.Usecase.Tests.Spies.Repository.GetWebContent;
using Information.Satellite.Usecase.Tests.Stubs.Repository.GetWebContent;
using Newtonsoft.Json;
using System;
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
      var repository = new WebContentRepositoryReturnsSpecificContentStub("<html></html>");
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
      var interactor = new GetInformationItemInteractor(new WebContentRepositoryReturnsSpecificContentStub(
        "<div class=\"breadcrumbs\">"
          + "<div class=\"blockbg\">"
		        + "<a href=\"http://store.steampowered.com/search/?term=&snr=1_5_9__205\">Alle Spiele</a>"
						+ "&gt;"
            + "<a href=\"http://store.steampowered.com/genre/RPG/?snr=1_5_9__205\">RPG</a>"
            + "&gt;"
            + "<a href=\"http://store.steampowered.com/app/22370/?snr=1_5_9__205\">"
              + "<span itemprop=\"name\">Fallout 3: Game of the Year Edition</span>"
            + "</a>"
				  + "</div>"
        + "</div>"
      ));
      var response = interactor.Execute(new GetInformationItemInteractorRequest());

      Assert.Equal(
        "Fallout 3: Game of the Year Edition",
        response.Title
      );
    }
  }
}
