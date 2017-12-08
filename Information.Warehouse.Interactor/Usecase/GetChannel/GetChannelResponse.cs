using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Information.Warehouse.Usecase
{
  [DataContract]
  public class GetChannelResponse
  {
    [DataMember]
    public string ChannelId { get; set; }

    [DataMember]
    public IEnumerable<string> InformationItemIds { get; set; }
  }
}
