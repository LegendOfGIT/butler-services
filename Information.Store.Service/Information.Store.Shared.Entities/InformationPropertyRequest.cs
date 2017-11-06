using System.Runtime.Serialization;

namespace Information.Store.Shared.Entity
{
  [DataContract]
  public class InformationPropertyRequest
  {
    [DataMember]
    public string Name { get; set; }

    [DataMember]
    public string[] Values { get; set; }
  }
}
