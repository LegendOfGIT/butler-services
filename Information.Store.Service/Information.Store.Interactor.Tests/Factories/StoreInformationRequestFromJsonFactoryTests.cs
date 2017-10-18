namespace Information.Store.Interactor.Tests
{
  using System;
  using System.Linq;
  using Newtonsoft.Json;
  using Xunit;

  public class StoreInformationRequestFromJsonFactoryTests
  {
    [Fact]
    public void FactoryAppliesIdFromJsonToRequest()
    {
      var id = Guid.NewGuid().ToString();
      var request = StoreInformationRequestFromJsonFactory.CreateRequest($"{{\"id\": \"{id}\"}}");
      Assert.Equal(id, request.Id);
    }

    [Fact]
    public void FactoryAppliesEmptyIdWhenJsonDoesNotContainAnIdToRequest()
    {
      var id = Guid.NewGuid().ToString();
      var request = StoreInformationRequestFromJsonFactory.CreateRequest($"{{\"infoid\": \"{id}\"}}");
      Assert.Equal(string.Empty, request.Id);
    }

    [Fact]
    public void FactoryAppliesStringPropertyFromJsonToRequest()
    {
      var firstName = "Max";
      var request = StoreInformationRequestFromJsonFactory.CreateRequest(
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
      var request = StoreInformationRequestFromJsonFactory.CreateRequest(
        $"{{ \"information\": {{ \"properties\": [ {{\"isOnSale\": [true, false]}} ] }} }}"
      );
      Assert.Equal(
        JsonConvert.SerializeObject(new[] { true, false }),
        JsonConvert.SerializeObject(
          request.Properties.FirstOrDefault(prop => prop.Name == "isOnSale").Values
        )
      );
    }
  }
}
