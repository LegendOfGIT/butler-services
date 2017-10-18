using System.Collections.Generic;

namespace Information.Store.Interactor
{
  public struct StoreInformationRequest
  {
    public string Id { get; set; }
    public IEnumerable<InformationProperty> Properties { get; set; }
  }
}
