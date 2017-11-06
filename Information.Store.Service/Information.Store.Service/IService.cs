using Information.Store.Shared.Entity;
using System.ServiceModel;
using System.ServiceModel.Web;

namespace Information.Store.Service
{
  [ServiceContract]
  public interface IService
  {
    [OperationContract]
    [WebInvoke(UriTemplate = "/StoreInformation", Method = "POST", RequestFormat = WebMessageFormat.Json)]
    void StoreInformation(InformationRequest request);
  }
}
