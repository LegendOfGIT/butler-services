using Information.Warehouse.Entity;
using Information.Warehouse.Usecase.Tests.Repository.Spies;
using Information.Warehouse.Repository;
using Newtonsoft.Json;
using Xunit;
using System.Collections.Generic;

namespace Information.Warehouse.Usecase.Tests
{
  public class GetChannelItemTests
  {
    private GetChannelItemRepositorySpy repositorySpy;
    private GetChannelItemInteractor interactor;

    public GetChannelItemTests()
    {
      repositorySpy = new GetChannelItemRepositorySpy();
      SetUpSpecified(repositorySpy);
    }

    private void SetUpSpecified(IGetChannelItemRepository GetChannelItemRepository)
    {
      interactor = new GetChannelItemInteractor(GetChannelItemRepository);
    }

    [Fact]
    public void InteractorReturnsEmptyResponseByDefault()
    {
      var response = interactor.Execute(string.Empty);

      var expectedResponse = new GetChannelItemResponse();
      Assert.Equal(
        JsonConvert.SerializeObject(expectedResponse),
        JsonConvert.SerializeObject(response)
      );
    }

    [Fact]
    public void InteractorCallsRepositoryWithGivenParameters()
    {
      var response = interactor.Execute("AAA-BBB-CCC-DDD");

      Assert.Equal(1, repositorySpy.GetChannelItemCalls);
      Assert.Equal(
        "AAA-BBB-CCC-DDD", 
        repositorySpy.LastUsedParameters[GetChannelItemRepositorySpy.ParameterIdChannelItemId]
      );
    }

    [Fact]
    public void InteractorReturnsTitlePropertyFromRepository()
    {
      var channelItem = new ChannelItem
      {
        Properties = new Dictionary<string, IEnumerable<object>>
        {
          { "titles", new[]{ "Lord of the rings: Shadow of mordor" } }
        }
      };
      SetUpSpecified(new GetChannelItemRepositoryReturnsSpecificResponseStub(channelItem));

      var response = interactor.Execute("AAA-BBB-CCC-DDD");
      Assert.Equal("Lord of the rings: Shadow of mordor", response.Title);
    }

    [Fact]
    public void InteractorReturnsMainImagePropertyFromRepository()
    {
      var channelItem = new ChannelItem
      {
        Properties = new Dictionary<string, IEnumerable<object>>
        {
          {
            "images",
            new[]{
              "http://de.web.img2.acsta.net/r_1920_1080/pictures/16/11/16/11/48/237316.jpg",
              "http://de.web.img2.acsta.net/r_1920_1080/pictures/16/11/16/11/55/237316.jpg"
            }
          }
        }
      };
      SetUpSpecified(new GetChannelItemRepositoryReturnsSpecificResponseStub(channelItem));

      var response = interactor.Execute("AAA-BBB-CCC-DDD");
      Assert.Equal("http://de.web.img2.acsta.net/r_1920_1080/pictures/16/11/16/11/48/237316.jpg", response.MainImageUrl);
    }
  }
}
