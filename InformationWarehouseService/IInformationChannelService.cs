using Information.Warehouse.Interactor;
using System.ServiceModel;
using System.ServiceModel.Web;

namespace InformationWarehouse
{
  [ServiceContract]
  public interface IInformationChannelService
  {
    [OperationContract]
    [WebGet(UriTemplate = "/GetChannel/{channelId}", ResponseFormat = WebMessageFormat.Json)]
    GetChannelResponse GetChannel(string channelId);
  }
}
