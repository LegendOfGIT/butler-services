using Newtonsoft.Json;
using Xunit;

namespace Information.Warehouse.Interactor.Tests
{
  public class GetChannelTests
  {
    [Fact]
    public void InteractorReturnsEmptyResponseByDefault()
    {
      var interactor = new GetChannelInteractor();
      var response = interactor.Execute();

      var expectedResponse = new GetChannelResponse();
      Assert.Equal(
        JsonConvert.SerializeObject(expectedResponse),
        JsonConvert.SerializeObject(response)
      );
    }
  }
}
