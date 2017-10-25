using System.Linq;
using Information.Store.Repository;
using Information.Store.Repository.Entity;

namespace Information.Store.Interactor
{
  public class StoreInformationInteractor
  {
    private IStoreInformationRepository repository;

    public StoreInformationInteractor(IStoreInformationRepository repository)
    {
      this.repository = repository;
    }

    public void Execute(StoreInformationRequest request)
    {
      this.repository.StoreInformation(new InformationEntity
      {
        Id = request.Id,
        Properties = request.Properties?.Select(requestProperty =>
        {
          return new InformationPropertyEntity
          {
            Name = requestProperty.Name,
            Values = requestProperty.Values
          };
        })
      });
    }
  }
}
