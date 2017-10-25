using Information.Store.Repository.Entity;

namespace Information.Store.Repository
{
    public interface IStoreInformationRepository
    {
      void StoreInformation(InformationEntity information);
    }
}
