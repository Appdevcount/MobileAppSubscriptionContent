using iBand.BL.Implementations;
using iBand.BL.Interfaces;
using iBand.Models;
using iBand.Models.Outputs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace iBand.API.Controllers
{
    public class SubscriptionController : ApiController
    {
        // Fields
        private ISubscriptionBL services = new SubscriptionBL();

        // Methods
        [HttpPost]
        public DTO<Models.Outputs.Subscription.SubscriptionServiceDTO> GetServices(Input<Models.Inputs.Subscription.SubscriptionServices> obj)
        {
            return this.services.GetServices(obj);
        }

        [HttpPost]
        public DTO<Models.Outputs.Subscription.ChannelsForServiceDTO> GetServicesChannels(Input<Models.Inputs.Subscription.ChannelsForService> obj)
        {
            return this.services.GetServiceChannels(obj);
        }
        [HttpPost]
        public DTO<Models.Outputs.Subscription.SubscribeUserChannelDTO> SubscribeUserOnChannel(Input<Models.Inputs.Subscription.SubscribeUserChannel> obj)
        {
            return this.services.SubscribeUserOnChannel(obj);

        }
        [HttpPost]
        public DTO<Models.Outputs.Subscription.UnSubscribeUserChannelDTO> UnSubscribeUserOnChannel(Input<Models.Inputs.Subscription.UnSubscribeUserChannel> obj)
        {
            return this.services.UnSubscribeUserOnChannel(obj);
        }
        [HttpPost]
        public DTO<Models.Outputs.Subscription.UserSubscribeChannelDTO> GetUserSubscribeChannels(Input<Models.Inputs.Subscription.UserSubscribeChannel> obj)
        {
            return this.services.GetUserSubscribeOnChannel(obj);
        }
        [HttpPost]
        public DTO<Models.Outputs.Subscription.UserChannelContentDataDTO> GetUserChannelContentData(Input<Models.Inputs.Subscription.ChannelContentData> obj)
        {
            return this.services.GetUserChannelContentData(obj);
        }
        [HttpPost]
        public DTO<Models.Outputs.Subscription.channels> GetUserServiceChannelDetails(Input<Models.Inputs.Subscription.UserSubcribeChannelDetails> obj)
        {
            return this.services.GetUserServiceChannelDetails(obj);
        }
        [HttpPost]
        public DTO<Models.Outputs.Subscription.ServicesByCountryDTO> GetSubs_RecentServiceChannels(Input<Models.Inputs.Subscription.ServicesByCountry> obj)
        {
            return this.services.GetSubs_RecentServiceChannels(obj);
        }
        [HttpPost]
        public DTO<Models.Outputs.Subscription.ServicesByCountryDTO> GetSubs_ServiceChannel(Input<Models.Inputs.Subscription.ServicesByCountry> obj)
        {
            return this.services.GetSubs_ServiceChannel(obj);
        }
        [HttpPost]
        public DTO<Models.Outputs.Subscription.SubsGetCategoriesDTO> GetSubs_Categories(Input<Models.Inputs.Subscription.Country> obj)
        {
            return this.services.GetSubs_Categories(obj);
        }
        [HttpPost]
        public DTO<Models.Outputs.Subscription.ServicebyCategoryDTO> GetSubs_ServiceByCategory(Input<Models.Inputs.Subscription.ServiceByCategory> obj)
        {
            return this.services.GetSubs_ServiceByCategory(obj);
        }
        [HttpPost]
        public DTO<Models.Outputs.Subscription.ContentViewDTO> GetUserSubs_AllContentView(Input<Models.Inputs.Subscription.ContentView> obj)
        {
            return this.services.GetUserSubs_AllContentView(obj);
        }
        [HttpPost]
        public DTO<Models.Outputs.Subscription.ContentViewDTO> GetUserSubs_ContentViewbyService(Input<Models.Inputs.Subscription.Contentviewbyservice> obj)
        {
            return this.services.GetUserSubs_ContentViewbyService(obj);
        }
        [HttpPost]
        public DTO<Models.Outputs.Subscription.SubusersubcribechannelDTO> GetSubs_UserSubcribeChannels(Input<Models.Inputs.Subscription.SubsUsersubscribechannel> obj)
        {
            return this.services.GetSubs_UserSubcribeChannels(obj);

        }
        [HttpPost]
        public DTO<Models.Outputs.Subscription.InteractiveservicevoteDTO> InteractiveServiceContestantVote(Input<Models.Inputs.Subscription.InteractiveServiceContestantVote> obj)
        {
            return this.services.InteractiveServiceContestantVote(obj);
        }
        [HttpPost]
        public DTO<Models.Outputs.Subscription.InteractiveserviceusermsgDTO> InteractiveServiceUserMessage(Input<Models.Inputs.Subscription.Interactiveserviceusermsg> obj)
        {
            return this.services.InteractiveServiceUserMessage(obj);
        }
    }


}
