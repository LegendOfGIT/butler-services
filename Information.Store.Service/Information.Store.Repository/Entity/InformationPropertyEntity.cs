using System.Collections.Generic;

namespace Information.Store.Repository.Entity
{
  public class InformationPropertyEntity
  {
    public string Name { get; set; }
    public IEnumerable<object> Values { get; set; }
  }
}
