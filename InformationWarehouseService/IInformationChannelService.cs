using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;

namespace InformationWarehouse
{
    [ServiceContract]
    public interface IInformationChannelService
    {
        [OperationContract]
        [WebGet(UriTemplate = "/GetChannel/{channelId}", ResponseFormat = WebMessageFormat.Json)]
        Channel GetChannel(string channelId);
    }

    [DataContract]
    public class Channel
    {
        [DataMember]
        public string title { get; set; }
    }
}
