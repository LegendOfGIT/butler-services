using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Information.Store.Shared.Entity
{
  [DataContract]
  public class InformationRequest
  {
    [DataMember]
    public string Id { get; set; }

    [DataMember]
    public IEnumerable<InformationPropertyRequest> Properties { get; set; }
  }
}
