using Information.Warehouse.Repository;

namespace Information.Warehouse.Interactor
{
  public class GetChannelInteractor
  {
    private IGetChannelRepository GetChannelRepository;

    public GetChannelInteractor(IGetChannelRepository GetChannelRepository)
    {
      this.GetChannelRepository = GetChannelRepository;
    }

    public GetChannelResponse Execute()
    {
      var response = this.GetChannelRepository.GetChannel();
      return new GetChannelResponse
      {
        ChannelId = response.Id,
        InformationItemIds = response.InformationItemIds
      };
    }
  }
}
