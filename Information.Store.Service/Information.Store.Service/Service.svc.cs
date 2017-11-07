using Information.Store.Factories;
using Information.Store.Interactor;
using Information.Store.Repository.MongoDatabase;
using Information.Store.Shared.Entity;
using MongoDB.Driver;

namespace Information.Store.Service
{
  public class Service : IService
  {
    public void StoreInformation(InformationRequest request)
    {
      var storeInformationRequest = new StoreInformationRequestFromServiceRequestFactory(
        new IStringToObject[]{
          new NullableBooleanFromStringFactory(),
          new NullableIntegerFromStringFactory()
        }
      ).CreateRequest(request);

      var mongoClient = new MongoClient();
      var repository = new StoreInformationMongoDatabaseRepository(mongoClient.GetDatabase("information-store"));
      var interactor = new StoreInformationInteractor(repository);

      interactor.Execute(storeInformationRequest);
    }
  }
}
