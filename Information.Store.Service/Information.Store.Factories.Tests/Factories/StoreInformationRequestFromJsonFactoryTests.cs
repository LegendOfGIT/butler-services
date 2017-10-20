namespace Information.Store.Factories.Tests
{
  using Newtonsoft.Json;
  using System;
  using System.Linq;
  using Xunit;

  public class StoreInformationRequestFromJsonFactoryTests
  {
    private StringToObjectFactoryReturnsIntegerSpy stringToObjectFactorySpy;
    private StoreInformationRequestFromJsonFactory factory;

    public StoreInformationRequestFromJsonFactoryTests()
    {
      this.stringToObjectFactorySpy = new StringToObjectFactoryReturnsIntegerSpy();
      this.factory = new StoreInformationRequestFromJsonFactory(new[]
      {
        this.stringToObjectFactorySpy
      });
    }

    [Fact]
    public void FactoryAppliesIdFromJsonToRequest()
    {
      var id = Guid.NewGuid().ToString();
      var request = factory.CreateRequest($"{{\"id\": \"{id}\"}}");
      Assert.Equal(id, request.Id);
    }

    [Fact]
    public void FactoryAppliesEmptyIdWhenJsonDoesNotContainAnIdToRequest()
    {
      var id = Guid.NewGuid().ToString();
      var request = factory.CreateRequest($"{{\"infoid\": \"{id}\"}}");
      Assert.Equal(string.Empty, request.Id);
    }

    [Fact]
    public void FactoryAppliesStringPropertyFromJsonToRequest()
    {
      var firstName = StringToObjectFactoryReturnsIntegerSpy.NotRecognizedValue;
      var request = factory.CreateRequest(
        $"{{ \"information\": {{ \"properties\": [ {{\"firstName\": [\"{firstName}\"]}} ] }} }}"
      );
      Assert.Equal(
        JsonConvert.SerializeObject(new[] { firstName }),
        JsonConvert.SerializeObject(
          request.Properties.FirstOrDefault(prop => prop.Name == "firstName").Values
        )
      );
    }

    [Fact]
    public void FactoryAppliesBooleanPropertyWithMultipleValuesFromJsonToRequest()
    {
      var request = factory.CreateRequest(
        $"{{ \"information\": {{ \"properties\": [ {{\"size\": [1, \"1\"]}} ] }} }}"
      );

      Assert.Equal(2, this.stringToObjectFactorySpy.GetObjectFromStringCalls);
      Assert.Equal(
        JsonConvert.SerializeObject(new[] { 1, 1 }),
        JsonConvert.SerializeObject(
          request.Properties.FirstOrDefault(prop => prop.Name == "size").Values
        )
      );
    }
  }
}
