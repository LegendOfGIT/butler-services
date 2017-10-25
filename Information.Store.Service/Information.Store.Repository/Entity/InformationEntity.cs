using System.Collections.Generic;

namespace Information.Store.Repository.Entity
{
  public class InformationEntity
  {
    public string Id { get; set; }
    public IEnumerable<InformationPropertyEntity> Properties { get; set; }
  }
}
