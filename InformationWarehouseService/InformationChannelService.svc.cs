namespace InformationWarehouse
{
    public class InformationChannelService : IInformationChannelService
    {
        public Channel GetChannel(string channelId)
        {
            return new Channel {
                title = channelId     
            };
        }
    }
}
