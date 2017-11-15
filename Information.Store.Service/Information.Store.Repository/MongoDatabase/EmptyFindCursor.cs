using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Information.Store.Repository.MongoDatabase
{
  public class EmptyFindCursor : IAsyncCursor<InformationEntry>
  {
    public IEnumerable<InformationEntry> Current => throw new System.NotImplementedException();

    public void Dispose()
    {
    }

    public bool MoveNext(CancellationToken cancellationToken = default(CancellationToken))
    {
      return false;
    }

    public Task<bool> MoveNextAsync(CancellationToken cancellationToken = default(CancellationToken))
    {
      throw new System.NotImplementedException();
    }
  }
}
