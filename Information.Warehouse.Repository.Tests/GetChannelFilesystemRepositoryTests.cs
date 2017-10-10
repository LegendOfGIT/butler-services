using Information.Warehouse.Entity;
using Newtonsoft.Json;
using Xunit;

namespace Information.Warehouse.Repository.Tests
{
  public class GetChannelFilesystemRepositoryTests
  {

    [Fact]
    public void RepositoryReturnsChannelInformation()
    {
      var repository = new GetChannelFilesystemRepository();
      var channel = repository.GetChannel();
      Assert.Equal(
        JsonConvert.SerializeObject(new Channel()),
        JsonConvert.SerializeObject(channel));
      }
  }
}
