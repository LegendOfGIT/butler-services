using System.Collections.Generic;
using System.ServiceModel;

namespace Information.Store.Service
{
  [ServiceContract]
  public interface IService
  {

    [OperationContract]
    void StoreInformation(Dictionary<string, IEnumerable<object>> information);
  }
}
