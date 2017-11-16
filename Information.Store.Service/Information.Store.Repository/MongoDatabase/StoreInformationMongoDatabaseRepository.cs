using Information.Store.Repository.Entity;
using MongoDB.Bson;
using MongoDB.Driver;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Information.Store.Repository.MongoDatabase
{
  public class StoreInformationMongoDatabaseRepository : IStoreInformationRepository
  {
    private IMongoDatabase Database;
    private IMongoCollection<InformationEntry> informationCollection;

    public StoreInformationMongoDatabaseRepository(IMongoDatabase Database)
    {
      this.Database = Database;
      this.informationCollection = this.Database.GetCollection<InformationEntry>("information");
    }

    public void StoreInformation(InformationEntity information)
    {
      if (!this.HasIdenticalActiveEntryInCollection(information))
      {
        var informationId = information?.Id;
        this.ResetActivityFlagsInInformationEntries(informationId);
        this.InsertNewInformationEntryInCollection(information, informationId);
      }
    }

    private bool HasIdenticalActiveEntryInCollection(InformationEntity information)
    {
      var activeInformationEntry = GetActiveInformationEntryByInformationId(information?.Id);
      if (activeInformationEntry == null)
      {
        return false;
      }

      var activeEntryProperties =
        activeInformationEntry.Properties?
          .OrderBy(property => property.Name)
          .ToDictionary(
            property => property.Name, 
            property => 
              property.Values?
                .Select(value => value.ToString())
                .OrderBy(value => value)
          )
      ;

      var newEntryProperties = 
        information.Properties?
          .OrderBy(property => property.Name)
          .ToDictionary(
            property => property.Name,
            property => 
              property.Values?
                .Select(value => value.ToString())
                .OrderBy(value => value)
          )
      ;

      return JsonConvert.SerializeObject(newEntryProperties) == JsonConvert.SerializeObject(activeEntryProperties);
    }

    private void ResetActivityFlagsInInformationEntries(string informationId)
    {
      var informationItems = this.GetInformationEntriesByInformationId(informationId);
      foreach (var informationItem in informationItems)
      {
        var replaceItem = new InformationEntry
        {
          DiscoveryTimestamp = informationItem.DiscoveryTimestamp,
          Id = informationItem.Id,
          IsActive = false,
          Properties = informationItem.Properties
        };

        this.informationCollection.ReplaceOne(
          item =>
            item.Id == informationId && item.DiscoveryTimestamp == informationItem.DiscoveryTimestamp

          , replaceItem
        );
      }
    }

    private void InsertNewInformationEntryInCollection(InformationEntity information, string informationId)
    {
      this.informationCollection?.InsertOne(new InformationEntry
      {
        Id = informationId,
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

    private InformationEntry GetActiveInformationEntryByInformationId(string informationId)
    {
      return GetInformationEntriesByInformationId(informationId, true).FirstOrDefault();
    }

    private IEnumerable<InformationEntry> GetInformationEntriesByInformationId(string informationId, bool? isActive = null)
    {
      var informationEntries = this.informationCollection?.FindSync(item => item.Id == informationId);
      var resultEntries = (informationEntries ?? new EmptyFindCursor()).ToList();

      if (isActive.HasValue)
      {
        resultEntries = resultEntries.Where(resultEntry => resultEntry.IsActive == isActive.Value).ToList();
      }

      return resultEntries;
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
