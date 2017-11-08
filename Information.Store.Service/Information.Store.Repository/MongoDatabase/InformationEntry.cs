using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;

namespace Information.Store.Repository.MongoDatabase
{
  public struct InformationEntry
  {
    [BsonElement]
    public string Id { get; set; }

    [BsonElement]
    public IEnumerable<InformationPropertyEntry> Properties { get; set; }

    [BsonElement]
    public int Version { get; set; }

    [BsonElement]
    public bool IsActive { get; set; }
  }
}
