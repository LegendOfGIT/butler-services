using System.Collections.Generic;

namespace Information.Warehouse.Entity
{
  public class ChannelItem
  {
    public string Id { get; set; }

    public Dictionary<string, IEnumerable<object>> Properties;
  }
}
