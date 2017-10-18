using Newtonsoft.Json;

namespace Information.Store.Service
{
  public class Service : IService
  {
    public void StoreInformation(string informationToken)
    {
      var dom = JsonConvert.DeserializeXNode(informationToken);
    }
  }
}
