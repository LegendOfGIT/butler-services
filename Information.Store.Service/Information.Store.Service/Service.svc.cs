using Information.Store.Factories;
using Information.Store.Shared.Entity;

namespace Information.Store.Service
{
  public class Service : IService
  {
    public void StoreInformation(InformationRequest request)
    {
      var storeInformationRequest = new StoreInformationRequestFromWebserviceRequestFactory(
        new IStringToObject[]{
          new NullableBooleanFromStringFactory(),
          new NullableIntegerFromStringFactory()
        }
      ).CreateRequest(request); 
    }
  }
}
