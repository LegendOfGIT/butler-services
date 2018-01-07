using Information.Satellite.Usecase.GetInformationItem;
using Information.Satellite.Usecase.Tests.Spies.Repository.GetWebContent;
using Information.Satellite.Usecase.Tests.Stubs.Repository.GetWebContent;
using System;
using System.Linq;
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
    public void TestInteractorReturnsReleaseDateFromWithinHtmlUsingCssParsingCommands()
    {
      var interactor = new GetInformationItemInteractor(new WebContentRepositoryReturnsSpecificContentStub("Fallout4Goty.stub"));
      var response = interactor.Execute(new GetInformationItemInteractorRequest
      {
        ContentParsingCommands = new[] {
          new ParseCommand
          {
            Selector = ".release_date .date",
            TargetPropertyId = "ReleaseDate"
          }
        }
      });

      Assert.Equal(
        new[] { "13. Okt. 2009" },
        response.Properties["ReleaseDate"]
      );
    }

    [Fact]
    public void TestInteractorReturnsImagesFromWithWithinHtmlUsingCssParsingCommands()
    {
      var interactor = new GetInformationItemInteractor(new WebContentRepositoryReturnsSpecificContentStub("Fallout4Goty.stub"));
      var response = interactor.Execute(new GetInformationItemInteractorRequest
      {
        ContentParsingCommands = new[] {
          new ParseCommand
          {
            Selector = ".highlight_screenshot_link",
            Attribute = "href",
            TargetPropertyId = "Images"
          }
        }
      });

      Assert.Equal(
        new[]{
          "http://cdn.edgecast.steamstatic.com/steam/apps/22370/ss_87fb57e11912a692f29614f88411d0db2cba405a.1920x1080.jpg?t=1449019604",
          "http://cdn.edgecast.steamstatic.com/steam/apps/22370/ss_d770133d0a0c1b9b73fa31f20f29fe6617b73cfb.1920x1080.jpg?t=1449019604",
          "http://cdn.edgecast.steamstatic.com/steam/apps/22370/ss_2405f21839a5cec01918a5f6fe7cae0b80422107.1920x1080.jpg?t=1449019604",
          "http://cdn.edgecast.steamstatic.com/steam/apps/22370/ss_37398576af0f80af58744b56a24b8526833f2efb.1920x1080.jpg?t=1449019604",
          "http://cdn.edgecast.steamstatic.com/steam/apps/22370/ss_b37fe0d332ed8877e06a2b18efc95b1dab2a54af.1920x1080.jpg?t=1449019604",
          "http://cdn.edgecast.steamstatic.com/steam/apps/22370/ss_be899cfc4b2716f77d0a07737d519895ae0919f2.1920x1080.jpg?t=1449019604",
          "http://cdn.edgecast.steamstatic.com/steam/apps/22370/ss_bc45b3514bd9d10450e0f56cf6ca110d487ab9f1.1920x1080.jpg?t=1449019604",
          "http://cdn.edgecast.steamstatic.com/steam/apps/22370/ss_4ea123bae2967a081baf183f99af485a8fe54fd3.1920x1080.jpg?t=1449019604",
          "http://cdn.edgecast.steamstatic.com/steam/apps/22370/ss_da809b52f47085205a8eb1f116e22c73bee18c81.1920x1080.jpg?t=1449019604",
          "http://cdn.edgecast.steamstatic.com/steam/apps/22370/ss_e5eca12ac4678e10d7c0ee86ba6cb423e413ef58.1920x1080.jpg?t=1449019604",
          "http://cdn.edgecast.steamstatic.com/steam/apps/22370/ss_a6caf368d14ba1d5a2ad548ae419a05a7cced29d.1920x1080.jpg?t=1449019604",
          "http://cdn.edgecast.steamstatic.com/steam/apps/22370/ss_f4923635e94cd2c09799f797b5f89ac3dda2eb15.1920x1080.jpg?t=1449019604",
          "http://cdn.edgecast.steamstatic.com/steam/apps/22370/ss_ac1a49312f09178c78c3b4464cfc743bc4c380b1.1920x1080.jpg?t=1449019604",
          "http://cdn.edgecast.steamstatic.com/steam/apps/22370/ss_56f0c65d34f78a4b2b468fa71773ce297c18ba7a.1920x1080.jpg?t=1449019604",
          "http://cdn.edgecast.steamstatic.com/steam/apps/22370/ss_69399123132dcb4fe3691b4dd6b84077bf4da656.1920x1080.jpg?t=1449019604",
          "http://cdn.edgecast.steamstatic.com/steam/apps/22370/ss_18517b1865f2ea003836002dbdd4f88001a40913.1920x1080.jpg?t=1449019604",
          "http://cdn.edgecast.steamstatic.com/steam/apps/22370/ss_30d31f35608dcf8ad940eef06b06b72668a58c63.1920x1080.jpg?t=1449019604",
          "http://cdn.edgecast.steamstatic.com/steam/apps/22370/ss_417951e16be2418b9ea8294f9957d40723c20d83.1920x1080.jpg?t=1449019604",
          "http://cdn.edgecast.steamstatic.com/steam/apps/22370/ss_9ebd8b126b6b0e7f7de5ba46cd52e52079e0247f.1920x1080.jpg?t=1449019604",
          "http://cdn.edgecast.steamstatic.com/steam/apps/22370/ss_515508f4c22bbc5988765988b20ef3f9b042709f.1920x1080.jpg?t=1449019604",
          "http://cdn.edgecast.steamstatic.com/steam/apps/22370/ss_908adced631b238e775ea49caf086578efba22f5.1920x1080.jpg?t=1449019604",
          "http://cdn.edgecast.steamstatic.com/steam/apps/22370/ss_dd703a2d5c329d19a9caaca425c4e3d5d3ba1f8c.1920x1080.jpg?t=1449019604"
        },
        response.Properties["Images"].ToArray()
      );
    }

    [Fact]
    public void TestInteractorReturnsPriceInformationFromWithinHtmlUsingCssParsingCommands()
    {
      var interactor = new GetInformationItemInteractor(new WebContentRepositoryReturnsSpecificContentStub("Fallout4Goty.stub"));
      var response = interactor.Execute(new GetInformationItemInteractorRequest
      {
        ContentParsingCommands = new[] {
          new ParseCommand { Selector = "meta[itemProp=price]", TargetPropertyId = "SalePrices", Attribute="content" },
          new ParseCommand { Selector = "meta[itemProp=priceCurrency]", TargetPropertyId = "PriceCurrencys", Attribute="content" }

        }
      });

      Assert.Equal(new[] { "9,99" }, response.Properties["SalePrices"]);
      Assert.Equal(new[] { "EUR" }, response.Properties["PriceCurrencys"]);
    }

    [Fact]
    public void TestInteractorReturnsPublisherFromWithinHtmlUsingMixedParsingCommands()
    {
      var interactor = new GetInformationItemInteractor(new WebContentRepositoryReturnsSpecificContentStub("Fallout4Goty.stub"));
      var response = interactor.Execute(new GetInformationItemInteractorRequest
      {
        ContentParsingCommands = new[] {
          new ParseCommand {
            Selector = ".block_content_inner",
            ContentParsingCommand = new ParseCommand
            {
              Selector = @"<b>Publisher:<\/b>.*?<a.*?>(.*?)<\/a>",
              TargetIndex = 1,
              Type = ParseCommandType.RegEx
            },
            TargetPropertyId = "Publishers"
          },
        }
      });

      Assert.Equal(new[] { "Bethesda Softworks" }, response.Properties["Publishers"]);
    }
  }
}
