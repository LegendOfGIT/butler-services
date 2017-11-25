using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;

namespace Information.Store.Repository.MongoDatabase
{
  public class InformationEntry
  {
    [BsonId]
    public ObjectId _id { get; set; }

    [BsonElement]
    public string InformationId { get; set; }

    [BsonElement]
    public IEnumerable<InformationPropertyEntry> Properties { get; set; }

    [BsonElement]
    public DateTime DiscoveryTimestamp { get; set; }

    [BsonElement]
    public bool IsActive { get; set; }
  }
}
