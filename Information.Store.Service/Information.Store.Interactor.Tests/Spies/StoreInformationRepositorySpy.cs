using Information.Store.Repository;
using Information.Store.Repository.Entity;

namespace Information.Store.Interactor.Tests
{
  public class StoreInformationRepositorySpy : IStoreInformationRepository
  {
    public int NumberOfStoreInformationCalls { get; set; }
    public InformationEntity LastRequest { get; set; }

    public void StoreInformation(InformationEntity information)
    {
      this.NumberOfStoreInformationCalls++;
      this.LastRequest = information;
    }
  }
}
