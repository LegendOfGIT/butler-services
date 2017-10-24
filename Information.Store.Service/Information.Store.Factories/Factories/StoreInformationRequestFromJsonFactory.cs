using System.Xml.XPath;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Xml.Linq;
using System.Linq;
using Information.Store.Interactor;

namespace Information.Store.Factories
{
  public class StoreInformationRequestFromJsonFactory
  {
    private IEnumerable<IStringToObject> ObjectFactories;

    public StoreInformationRequestFromJsonFactory(IEnumerable<IStringToObject> ObjectFactories)
    {
      this.ObjectFactories = ObjectFactories;
    }

    public StoreInformationRequest CreateRequest(string jsonString)
    {
      var node = JsonConvert.DeserializeXNode(jsonString);

      return new StoreInformationRequest
      {
         Id = node.XPathSelectElement("//id")?.Value ?? string.Empty,
         Properties = GetPropertiesFromPropertiesNode(node)
      };
    }

    private IEnumerable<InformationProperty> GetPropertiesFromPropertiesNode(XNode node)
    {
      var properties = Enumerable.Empty<InformationProperty>();

      var propertyNodes = node.XPathSelectElements("//properties/*");
      foreach (var propertyNode in propertyNodes)
      {
        var propertyName = propertyNode.Name.LocalName;
        var property = properties.FirstOrDefault(p => p.Name == propertyName);
        if (property == null)
        {
          property = new InformationProperty { Name = propertyName };
          properties = properties.Concat(new[] { property });
        }

        property.Values = property.Values ?? Enumerable.Empty<object>();
        var propertyObject = GetPropertyObjectFromValue(propertyNode.Value);
        if(!property.Values.Contains(propertyObject))
        { 
          property.Values = property.Values.Concat(new[] { propertyObject });
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
