using Information.Store.Repository.Entity;
using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;

namespace Information.Store.Repository.MongoDatabase
{
  public struct InformationEntry
  {
    [BsonElement]
    public IEnumerable<InformationPropertyEntry> Properties { get; set; }
  }
}
