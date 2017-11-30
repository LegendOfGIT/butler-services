using System.Collections.Generic;

namespace Information.Warehouse.Entity
{
    public class Channel
    {
      public string Id { get; set; }

      public IEnumerable<string> InformationItemIds { get; set; }
    }
}
