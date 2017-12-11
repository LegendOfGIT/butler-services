using System.Runtime.Serialization;

namespace Information.Warehouse.Usecase
{
  [DataContract]
  public class GetChannelItemResponse
  {
    [DataMember]
    public string ChannelItemId { get; set; }

    [DataMember]
    public string MainImageUrl { get; set; }

    [DataMember]
    public string Title;
  }
}
