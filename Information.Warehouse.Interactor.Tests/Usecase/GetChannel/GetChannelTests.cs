using Information.Warehouse.Interactor.Tests.Repository.Spies;
using Information.Warehouse.Repository;
using Newtonsoft.Json;
using Xunit;

namespace Information.Warehouse.Interactor.Tests
{
  public class GetChannelTests
  {
    private GetChannelRepositorySpy repositorySpy;
    private GetChannelInteractor interactor;

    public GetChannelTests()
    {
      repositorySpy = new GetChannelRepositorySpy();
      SetUpSpecified(repositorySpy);
    }

    private void SetUpSpecified(IGetChannelRepository GetChannelRepository)
    {
      interactor = new GetChannelInteractor(GetChannelRepository);
    }

    [Fact]
    public void InteractorReturnsEmptyResponseByDefault()
    {
      var response = interactor.Execute();

      var expectedResponse = new GetChannelResponse();
      Assert.Equal(
        JsonConvert.SerializeObject(expectedResponse),
        JsonConvert.SerializeObject(response)
      );
    }

    [Fact]
    public void InteractorCallsRepositoryToGetChannelInformation()
    {
      interactor.Execute();
      Assert.Equal(1, repositorySpy.GetChannelCalls);
    }

    [Fact]
    public void InteractorReturnsChannelInformationFromRepository()
    {
      SetUpSpecified(new GetChannelRepositoryReturnsChannelInformationStub());
      var response = interactor.Execute();

      var expectedResponse = new GetChannelResponse
      {
        ChannelId = GetChannelRepositoryReturnsChannelInformationStub.ChannelId
      };
      Assert.Equal(
        JsonConvert.SerializeObject(expectedResponse),
        JsonConvert.SerializeObject(response)
      );
    }
  }
}
