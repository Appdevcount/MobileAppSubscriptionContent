using iBand.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iBand.BL.Interfaces
{
    public interface ISubscriptionBL
    {
        DTO<Models.Outputs.Subscription.SubscriptionServiceDTO> GetServices(Input<Models.Inputs.Subscription.SubscriptionServices> obj);
        DTO<Models.Outputs.Subscription.ChannelsForServiceDTO> GetServiceChannels(Input<Models.Inputs.Subscription.ChannelsForService> obj);
        DTO<Models.Outputs.Subscription.channels> GetUserServiceChannelDetails(Input<Models.Inputs.Subscription.UserSubcribeChannelDetails> Obj);
        DTO<Models.Outputs.Subscription.SubscribeUserChannelDTO> SubscribeUserOnChannel(Input<Models.Inputs.Subscription.SubscribeUserChannel> obj);
        DTO<Models.Outputs.Subscription.UnSubscribeUserChannelDTO> UnSubscribeUserOnChannel(Input<Models.Inputs.Subscription.UnSubscribeUserChannel> obj);
        DTO<Models.Outputs.Subscription.UserSubscribeChannelDTO> GetUserSubscribeOnChannel(Input<Models.Inputs.Subscription.UserSubscribeChannel> Obj);
        DTO<Models.Outputs.Subscription.UserChannelContentDataDTO> GetUserChannelContentData(Input<Models.Inputs.Subscription.ChannelContentData> Obj);
        DTO<Models.Outputs.Subscription.ServicesByCountryDTO> GetSubs_RecentServiceChannels(Input<Models.Inputs.Subscription.ServicesByCountry> Obj);
        DTO<Models.Outputs.Subscription.ServicesByCountryDTO> GetSubs_ServiceChannel(Input<Models.Inputs.Subscription.ServicesByCountry> Obj);
        DTO<Models.Outputs.Subscription.SubsGetCategoriesDTO> GetSubs_Categories(Input<Models.Inputs.Subscription.Country> Obj);
        DTO<Models.Outputs.Subscription.ServicebyCategoryDTO> GetSubs_ServiceByCategory(Input<Models.Inputs.Subscription.ServiceByCategory> Obj);
        DTO<Models.Outputs.Subscription.ContentViewDTO> GetUserSubs_AllContentView(Input<Models.Inputs.Subscription.ContentView> Obj);
        DTO<Models.Outputs.Subscription.ContentViewDTO> GetUserSubs_ContentViewbyService(Input<Models.Inputs.Subscription.Contentviewbyservice> Obj);
        DTO<Models.Outputs.Subscription.SubusersubcribechannelDTO> GetSubs_UserSubcribeChannels(Input<Models.Inputs.Subscription.SubsUsersubscribechannel> Obj);
        DTO<Models.Outputs.Subscription.InteractiveservicevoteDTO> InteractiveServiceContestantVote(Input<Models.Inputs.Subscription.InteractiveServiceContestantVote> Obj);
        DTO<Models.Outputs.Subscription.InteractiveserviceusermsgDTO> InteractiveServiceUserMessage(Input<Models.Inputs.Subscription.Interactiveserviceusermsg> Obj);
    }
}
