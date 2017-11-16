using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Linq;
using System.Collections.Generic;

namespace Information.Store.Repository.MongoDatabase
{
  public class InformationEntry
  {
    [BsonElement]
    public string Id { get; set; }

    [BsonElement]
    public IEnumerable<InformationPropertyEntry> Properties { get; set; }

    [BsonElement]
    public DateTime DiscoveryTimestamp { get; set; }

    [BsonElement]
    public bool IsActive { get; set; }
  }
}
