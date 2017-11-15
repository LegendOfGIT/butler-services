using Information.Store.Repository.Entity;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Information.Store.Repository.MongoDatabase
{
  public class StoreInformationMongoDatabaseRepository : IStoreInformationRepository
  {
    private IMongoDatabase Database;

    public StoreInformationMongoDatabaseRepository(IMongoDatabase Database)
    {
      this.Database = Database;
    }

    public void StoreInformation(InformationEntity information)
    {
      var informationCollection = this.Database.GetCollection<InformationEntry>("information");
      
      if (informationCollection != null)
      {
        var informationItems = this.GetInformationEntriesByInformationId(informationCollection, information.Id);
        foreach(var informationItem in informationItems)
        {
          var replaceItem = new InformationEntry {
            DiscoveryTimestamp = informationItem.DiscoveryTimestamp,
            Id = informationItem.Id,
            IsActive = false,
            Properties = informationItem.Properties
          };

          informationCollection.ReplaceOne(item => item.Id == information.Id && item.DiscoveryTimestamp == informationItem.DiscoveryTimestamp, replaceItem);
        }

        informationCollection.InsertOne(new InformationEntry
        {
          Id = information.Id,
          IsActive = true,
          Properties = information?.Properties?.OrderBy(property => property.Name).Select(property =>
          {
            return new InformationPropertyEntry
            {
              Name = property.Name,
              Values = property.Values?.Select(value => GetBsonValueFromObject(value))
            };
          }),
          DiscoveryTimestamp = DateTime.Now
        });
      }
    }

    private IEnumerable<InformationEntry> GetInformationEntriesByInformationId(IMongoCollection<InformationEntry> informationCollection, string informationId)
    {
      var informationEntries = informationCollection.FindSync(item => item.Id == informationId);
      informationEntries = informationEntries ?? new EmptyFindCursor();
      return informationEntries.ToList();
    }

    private BsonValue GetBsonValueFromObject(object value)
    {
      if (value is int)
        return new BsonInt32((int)value);
      if (value is decimal)
        return new BsonDecimal128((decimal)value);
      if (value is double)
        return new BsonDouble((double)value);
      if (value is DateTime)
        return new BsonDateTime((DateTime)value);
      if (value is bool)
        return new BsonBoolean((bool)value);

      return new BsonString((string)value);
    }
  }
}
