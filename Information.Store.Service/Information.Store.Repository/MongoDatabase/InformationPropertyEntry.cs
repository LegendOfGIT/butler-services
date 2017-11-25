using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;

namespace Information.Store.Repository.MongoDatabase
{
  public class InformationPropertyEntry
  {
    [BsonElement]
    public string Name { get; set; }

    [BsonElement]
    public IEnumerable<BsonValue> Values { get; set; }
  }
}
