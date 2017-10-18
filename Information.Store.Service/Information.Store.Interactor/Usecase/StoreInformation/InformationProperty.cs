using System.Collections.Generic;

namespace Information.Store.Interactor
{
  public class InformationProperty
  {
    public string Name { get; set; }
    public IEnumerable<object> Values { get; set; }
  }
}
