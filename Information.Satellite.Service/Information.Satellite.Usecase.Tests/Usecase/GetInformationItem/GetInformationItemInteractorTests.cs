using Newtonsoft.Json;
using Xunit;

namespace Information.Satellite.Usecase.GetInformationItems
{
  public class GetInformationItemInteractorTests
  {
    [Fact]
    public void TestInteractorReturnsEmptyInformationItem()
    {
      var interactor = new GetInformationItemInteractor();
      var response = interactor.Execute();
      Assert.Equal(JsonConvert.SerializeObject(new GetInformationItemInteractorResponse()), JsonConvert.SerializeObject(response));
    }
  }
}
