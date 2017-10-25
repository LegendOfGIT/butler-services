using Newtonsoft.Json;
using System;
using Xunit;

namespace Information.Store.Interactor.Tests
{
  public class StoreInformationInteractorTests
  {
    [Fact]
    public void InteractorCallsRepositoryToStoreInformation()
    {
      var request = new StoreInformationRequest { };
      var repositorySpy = new StoreInformationRepositorySpy();
      var interactor = new StoreInformationInteractor(repositorySpy);
      interactor.Execute(request);

      Assert.Equal(1, repositorySpy.NumberOfStoreInformationCalls);
    }

    [Fact]
    public void InteractorCallsRepositoryWithBasicInformationFromRequest()
    {
      var id = Guid.NewGuid().ToString();
      var request = new StoreInformationRequest { Id = id };
      var repositorySpy = new StoreInformationRepositorySpy();
      var interactor = new StoreInformationInteractor(repositorySpy);
      interactor.Execute(request);

      Assert.Equal(id, repositorySpy.LastRequest.Id);
    }

    [Fact]
    public void InteractorCallsRepositoryWithInformationPropertiesFromRequest()
    {
      var id = Guid.NewGuid().ToString();
      var request = new StoreInformationRequest
      {
        Properties = new[]{
          new InformationProperty{ Name = "title", Values = new[]{ "Sportschuh" } },
          new InformationProperty{ Name = "price", Values = new object[]{ 12.45 } }
        }
      };
      var repositorySpy = new StoreInformationRepositorySpy();
      var interactor = new StoreInformationInteractor(repositorySpy);
      interactor.Execute(request);

      Assert.Equal(
        JsonConvert.SerializeObject(request.Properties),
        JsonConvert.SerializeObject(repositorySpy.LastRequest.Properties)
      );
    }
  }
}
