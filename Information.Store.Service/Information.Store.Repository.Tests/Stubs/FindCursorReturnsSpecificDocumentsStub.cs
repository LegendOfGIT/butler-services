using MongoDB.Driver;
using Information.Store.Repository.MongoDatabase;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System;
using System.Linq;

namespace Information.Store.Repository.Tests.Stubs
{
  public class FindCursorReturnsSpecificDocumentsStub : IAsyncCursor<InformationEntry>
  {
    private int cursorIndex = -1;
    private IEnumerable<InformationEntry> informationEntries;

    public FindCursorReturnsSpecificDocumentsStub(IEnumerable<InformationEntry> informationEntries)
    {
      this.informationEntries = informationEntries;
    }

    public IEnumerable<InformationEntry> Current => new List<InformationEntry>(this.informationEntries.Skip(this.cursorIndex));

    public void Dispose()
    {
    }

    public bool MoveNext(CancellationToken cancellationToken = default(CancellationToken))
    {
      this.cursorIndex++;
      return this.informationEntries.Count() > this.cursorIndex + 1;
    }

    public Task<bool> MoveNextAsync(CancellationToken cancellationToken = default(CancellationToken))
    {
      throw new NotImplementedException();
    }
  }
}
