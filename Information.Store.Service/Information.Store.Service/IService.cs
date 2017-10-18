using System.Collections.Generic;
using System.ServiceModel;
using Newtonsoft.Json;

namespace Information.Store.Service
{
  [ServiceContract]
  public interface IService
  {

    [OperationContract]
    void StoreInformation(string informationToken);
  }
}
