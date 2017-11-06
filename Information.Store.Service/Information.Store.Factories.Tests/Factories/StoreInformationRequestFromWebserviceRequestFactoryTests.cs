namespace Information.Store.Factories.Tests
{
  using Information.Store.Shared.Entity;
  using Newtonsoft.Json;
  using System;
  using System.Linq;
  using Xunit;

  public class StoreInformationRequestFromWebserviceRequestFactoryTests
  {
    private StringToObjectFactoryReturnsIntegerSpy stringToObjectFactorySpy;
    private StoreInformationRequestFromWebserviceRequestFactory factory;

    public StoreInformationRequestFromWebserviceRequestFactoryTests()
    {
      this.stringToObjectFactorySpy = new StringToObjectFactoryReturnsIntegerSpy();
      this.factory = new StoreInformationRequestFromWebserviceRequestFactory(new[]
      {
        this.stringToObjectFactorySpy
      });
    }

    [Fact]
    public void FactoryAppliesIdFromWebserviceRequestToRequest()
    {
      var id = Guid.NewGuid().ToString();
      var request = factory.CreateRequest(new InformationRequest { Id = id });
      Assert.Equal(id, request.Id);
    }

    [Fact]
    public void FactoryAppliesEmptyIdWhenWebserviceRequestDoesNotContainAnIdToRequest()
    {
      var id = Guid.NewGuid().ToString();
      var request = factory.CreateRequest(new InformationRequest { });
      Assert.Equal(string.Empty, request.Id);
    }

    [Fact]
    public void FactoryAppliesStringPropertyFromWebserviceRequestToRequest()
    {
      var firstName = StringToObjectFactoryReturnsIntegerSpy.NotRecognizedValue;
      var request = factory.CreateRequest(new InformationRequest
      {
        Properties = new[]
        {
          new InformationPropertyRequest
          {
            Name = "firstName",
            Values = new[]{ firstName }
          }
        }
      });

      Assert.Equal(
        JsonConvert.SerializeObject(new[] { firstName }),
        JsonConvert.SerializeObject(
          request.Properties.FirstOrDefault(prop => prop.Name == "firstName").Values
        )
      );
    }

    [Fact]
    public void FactoryAppliesDistinctIntegerPropertyWithMultipleValuesFromWebserviceRequestToRequest()
    {
      var request = factory.CreateRequest(new InformationRequest
      {
        Properties = new[]
        {
          new InformationPropertyRequest
          {
            Name = "sizes",
            Values = new []{ "1", "1" }
          }
        }
      });

      Assert.Equal(2, this.stringToObjectFactorySpy.GetObjectFromStringCalls);
      Assert.Equal(
        JsonConvert.SerializeObject(new[] { 1 }),
        JsonConvert.SerializeObject(
          request.Properties.FirstOrDefault(prop => prop.Name == "sizes").Values
        )
      );
    }
  }
}
