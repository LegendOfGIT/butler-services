using System.Collections.Generic;
using System.Linq;
using Information.Store.Interactor;
using Information.Store.Shared.Entity;

namespace Information.Store.Factories
{
  public class StoreInformationRequestFromServiceRequestFactory
  {
    private IEnumerable<IStringToObject> ObjectFactories;

    public StoreInformationRequestFromServiceRequestFactory(IEnumerable<IStringToObject> ObjectFactories)
    {
      this.ObjectFactories = ObjectFactories;
    }

    public StoreInformationRequest CreateRequest(InformationRequest request)
    {
      return new StoreInformationRequest
      {
         Id = request?.Id ?? string.Empty,
         Properties = GetPropertiesFromPropertiesNode(request)
      };
    }

    private IEnumerable<InformationProperty> GetPropertiesFromPropertiesNode(InformationRequest request)
    {
      var properties = Enumerable.Empty<InformationProperty>();
      request.Properties = request.Properties ?? Enumerable.Empty<InformationPropertyRequest>();

      foreach (var propertyNode in request.Properties)
      {
        var propertyName = propertyNode.Name;
        var property = properties.FirstOrDefault(p => p.Name == propertyName);
        if (property == null)
        {
          property = new InformationProperty { Name = propertyName };
          properties = properties.Concat(new[] { property });
        }

        property.Values = property.Values ?? Enumerable.Empty<object>();
        foreach (var propertyNodeValue in propertyNode.Values)
        {
          var propertyObject = GetPropertyObjectFromValue(propertyNodeValue);
          if (!property.Values.Contains(propertyObject))
          {
            property.Values = property.Values.Concat(new[] { propertyObject });
          }
        }
      }

      return properties;
    }

    private object GetPropertyObjectFromValue(string value)
    {
      value = value.Trim();

      foreach (var factory in this.ObjectFactories)
      {
        var response = factory.GetObjectFromString(value);
        if(response != null)
        {
          return response;
        }
      }     

      return value;
    }
  }
}
