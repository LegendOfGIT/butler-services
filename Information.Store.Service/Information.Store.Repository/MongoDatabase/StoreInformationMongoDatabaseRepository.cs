﻿using Information.Store.Repository.Entity;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
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
          Version = 1
        });
      }
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
