using iBand.BL.Interfaces;
using iBand.Common;
using iBand.DAL;
using iBand.Models;
using iBand.Models.Inputs;
using iBand.Models.Inputs.Subscription;
using iBand.Models.Outputs.Subscription;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using iBand.Models.Outputs;
using System.Security.Cryptography;
using System.Text.RegularExpressions;

namespace iBand.BL.Implementations
{
    public class SubscriptionBL : ISubscriptionBL
    {
        // Fields
        public iBandEntities db = new iBandEntities();

        // Methods
        #region API Methods

        public DTO<SubscriptionServiceDTO> GetServices(Input<SubscriptionServices> obj)
        {
            DTO<SubscriptionServiceDTO> dto = new DTO<SubscriptionServiceDTO>();
            SubscriptionServiceDTO subscribeservice = new SubscriptionServiceDTO();
            dto.objname = "GetServices";
            try
            {
                if ((string.IsNullOrEmpty(obj.input.countryid) || string.IsNullOrEmpty(obj.param.company)) || (string.IsNullOrEmpty(obj.param.deviceid) || string.IsNullOrEmpty(obj.param.username) || string.IsNullOrEmpty(obj.param.password)))

                {
                    dto.status = new Status(800);
                    return dto;
                }
                subscribeservice.services = this.GetServices(Convert.ToInt32(obj.input.countryid));
                dto.response = subscribeservice;
                dto.status = new Status(0);
            }
            catch (Exception exception)
            {
                TraceLog.WriteToLog(exception.Message.ToString() + "Trace = " + exception.StackTrace.ToString(), exception);
                dto.status = new Status(1);
                dto.status.info1 = exception.Message;
                if (exception.Message.ToString().StartsWith("ERROR"))
                {
                    dto.status = new Status(Convert.ToInt32(exception.InnerException.Message));
                }
            }
            dto.response = subscribeservice;
            return dto;
        }

        public DTO<ChannelsForServiceDTO> GetServiceChannels(Input<ChannelsForService> obj)
        {
            DTO<ChannelsForServiceDTO> dto = new DTO<ChannelsForServiceDTO>();
            ChannelsForServiceDTO servicechannel = new ChannelsForServiceDTO();
            dto.objname = "GetServiceChannels";
            try
            {
                if ((string.IsNullOrEmpty(obj.input.serviceid) || string.IsNullOrEmpty(obj.input.countryid) || string.IsNullOrEmpty(obj.param.company)) || (string.IsNullOrEmpty(obj.param.deviceid) || string.IsNullOrEmpty(obj.param.username) || string.IsNullOrEmpty(obj.param.password)))
                {
                    dto.status = new Status(800);
                    return dto;
                }
                servicechannel.channels = this.getservicechannels(Convert.ToInt32(obj.input.serviceid), Convert.ToInt32(obj.input.countryid));
                dto.response = servicechannel;
                dto.status = new Status(0);
            }
            catch (Exception exception)
            {
                TraceLog.WriteToLog(exception.Message.ToString() + "Trace = " + exception.StackTrace.ToString(), exception);
                dto.status = new Status(1);
                dto.status.info1 = exception.Message;
                if (exception.Message.ToString().StartsWith("ERROR"))
                {
                    dto.status = new Status(Convert.ToInt32(exception.InnerException.Message));
                }
            }
            dto.response = servicechannel;
            return dto;
        }

        public DTO<SubscribeUserChannelDTO> SubscribeUserOnChannel(Input<SubscribeUserChannel> obj)
        {
            DTO<SubscribeUserChannelDTO> dto = new DTO<SubscribeUserChannelDTO>();
            SubscribeUserChannelDTO resp = new SubscribeUserChannelDTO();
            dto.objname = "SubscribeUserOnChannel";
            try
            {
                if ((string.IsNullOrEmpty(obj.input.userID) || string.IsNullOrEmpty(obj.input.serviceID) || string.IsNullOrEmpty(obj.input.channelID) || string.IsNullOrEmpty(obj.input.countryID) || string.IsNullOrEmpty(obj.input.operatorID) || string.IsNullOrEmpty(obj.param.company)) || (string.IsNullOrEmpty(obj.param.deviceid) || string.IsNullOrEmpty(obj.param.username) || string.IsNullOrEmpty(obj.param.password)))
                {
                    dto.status = new Status(800);
                    return dto;
                }

                if (!SubscribeUser(Convert.ToInt64(obj.input.userID), Convert.ToInt32(obj.input.serviceID), Convert.ToInt32(obj.input.channelID), Convert.ToInt32(obj.input.countryID), Convert.ToInt32(obj.input.operatorID), false))
                {
                    dto.status = new Status(1070);
                    return dto;
                }
                dto.response = resp;
                dto.status = new Status(0);
            }
            catch (Exception exception)
            {
                TraceLog.WriteToLog(exception.Message.ToString() + "Trace = " + exception.StackTrace.ToString(), exception);
                dto.status = new Status(1);
                dto.status.info1 = exception.Message;
                if (exception.Message.ToString().StartsWith("ERROR"))
                {
                    dto.status = new Status(Convert.ToInt32(exception.InnerException.Message));
                }
            }
            dto.response = resp;
            return dto;
        }

        public DTO<UnSubscribeUserChannelDTO> UnSubscribeUserOnChannel(Input<UnSubscribeUserChannel> obj)
        {
            DTO<UnSubscribeUserChannelDTO> dto = new DTO<UnSubscribeUserChannelDTO>();
            UnSubscribeUserChannelDTO resp = new UnSubscribeUserChannelDTO();
            dto.objname = "UnSubscribeUserOnChannel";
            try
            {
                if ((string.IsNullOrEmpty(obj.input.userID) || string.IsNullOrEmpty(obj.input.serviceID) || string.IsNullOrEmpty(obj.input.channelID) || string.IsNullOrEmpty(obj.input.countryID) || string.IsNullOrEmpty(obj.input.operatorID) || string.IsNullOrEmpty(obj.param.company)) || (string.IsNullOrEmpty(obj.param.deviceid) || string.IsNullOrEmpty(obj.param.username) || string.IsNullOrEmpty(obj.param.password)))
                {
                    dto.status = new Status(800);
                    return dto;
                }

                if (!UnSubscribeUser(Convert.ToInt64(obj.input.userID), Convert.ToInt32(obj.input.serviceID), Convert.ToInt32(obj.input.channelID), Convert.ToInt32(obj.input.countryID), Convert.ToInt32(obj.input.operatorID), false))
                {
                    dto.status = new Status(1100);
                    return dto;
                }
                dto.response = resp;
                dto.status = new Status(0);
            }
            catch (Exception exception)
            {
                TraceLog.WriteToLog(exception.Message.ToString() + "Trace = " + exception.StackTrace.ToString(), exception);
                dto.status = new Status(1);
                dto.status.info1 = exception.Message;
                if (exception.Message.ToString().StartsWith("ERROR"))
                {
                    dto.status = new Status(Convert.ToInt32(exception.InnerException.Message));
                }
            }
            dto.response = resp;
            return dto;

        }

        public DTO<UserSubscribeChannelDTO> GetUserSubscribeOnChannel(Input<UserSubscribeChannel> obj)
        {

            DTO<UserSubscribeChannelDTO> dto = new DTO<UserSubscribeChannelDTO>();
            UserSubscribeChannelDTO usersubscribechannel = new UserSubscribeChannelDTO();
            dto.objname = "GetUserSubscribeOnChannels";


            try
            {
                if ((string.IsNullOrEmpty(obj.input.serviceid) || string.IsNullOrEmpty(obj.input.userid) || string.IsNullOrEmpty(obj.input.countryid) || string.IsNullOrEmpty(obj.param.company)) || (string.IsNullOrEmpty(obj.param.deviceid) || string.IsNullOrEmpty(obj.param.username) || string.IsNullOrEmpty(obj.param.password)))
                {
                    dto.status = new Status(800);
                    return dto;
                }
                usersubscribechannel.channels = this.GetUserSubscribeOnChannels(Convert.ToInt32(obj.input.userid), Convert.ToInt32(obj.input.serviceid), Convert.ToInt32(obj.input.countryid));
                dto.response = usersubscribechannel;
                dto.status = new Status(0);
            }
            catch (Exception exception)
            {
                TraceLog.WriteToLog(exception.Message.ToString() + "Trace = " + exception.StackTrace.ToString(), exception);
                dto.status = new Status(1);
                dto.status.info1 = exception.Message;
                if (exception.Message.ToString().StartsWith("ERROR"))
                {
                    dto.status = new Status(Convert.ToInt32(exception.InnerException.Message));
                }
            }
            dto.response = usersubscribechannel;
            return dto;
        }

        public DTO<UserChannelContentDataDTO> GetUserChannelContentData(Input<ChannelContentData> obj)
        {
            DTO<UserChannelContentDataDTO> dto = new DTO<UserChannelContentDataDTO>();
            UserChannelContentDataDTO userchannelcontentdata = new UserChannelContentDataDTO();
            dto.objname = "GetUserChannelContentData";

            try
            {
                if ((string.IsNullOrEmpty(obj.input.serviceid) || string.IsNullOrEmpty(obj.input.userid) || string.IsNullOrEmpty(obj.input.channelid) || string.IsNullOrEmpty(obj.input.countryid) || string.IsNullOrEmpty(obj.param.company)) || (string.IsNullOrEmpty(obj.param.deviceid) || string.IsNullOrEmpty(obj.param.username) || string.IsNullOrEmpty(obj.param.password)))
                {
                    dto.status = new Status(800);
                    return dto;
                }

                if (!IsUserSubscribed(Convert.ToInt32(obj.input.userid), Convert.ToInt32(obj.input.serviceid), Convert.ToInt32(obj.input.channelid), Convert.ToInt32(obj.input.countryid), false))
                {
                    dto.status = new Status(1100);
                    return dto;
                }


                userchannelcontentdata.contentdata = this.GetUserChannelContentData(Convert.ToInt32(obj.input.userid), Convert.ToInt32(obj.input.serviceid), Convert.ToInt32(obj.input.channelid), Convert.ToInt32(obj.input.countryid));
                dto.response = userchannelcontentdata;
                dto.status = new Status(0);
            }
            catch (Exception exception)
            {
                TraceLog.WriteToLog(exception.Message.ToString() + "Trace = " + exception.StackTrace.ToString(), exception);
                dto.status = new Status(1);
                dto.status.info1 = exception.Message;
                if (exception.Message.ToString().StartsWith("ERROR"))
                {
                    dto.status = new Status(Convert.ToInt32(exception.InnerException.Message));
                }
            }
            dto.response = userchannelcontentdata;
            return dto;
        }

        public DTO<channels> GetUserServiceChannelDetails(Input<UserSubcribeChannelDetails> obj)
        {
            DTO<channels> dto = new DTO<channels>();
            channels usersubscribechannel = new channels();
            dto.objname = "GetUserServiceChannelDetails";


            try
            {
                if ((string.IsNullOrEmpty(obj.input.serviceid) || string.IsNullOrEmpty(obj.input.userid) || string.IsNullOrEmpty(obj.input.channelid) || string.IsNullOrEmpty(obj.param.company)) || (string.IsNullOrEmpty(obj.param.deviceid) || string.IsNullOrEmpty(obj.param.username) || string.IsNullOrEmpty(obj.param.password)))
                {
                    dto.status = new Status(800);
                    return dto;
                }
                usersubscribechannel = GetUserServiceChannelDetail(Convert.ToInt32(obj.input.userid), Convert.ToInt32(obj.input.serviceid), Convert.ToInt32(obj.input.channelid));
                {
                    dto.response = usersubscribechannel;
                    dto.status = new Status(0);
                }
            }
            catch (Exception exception)
            {
                TraceLog.WriteToLog(exception.Message.ToString() + "Trace = " + exception.StackTrace.ToString(), exception);
                dto.status = new Status(1);
                dto.status.info1 = exception.Message;
                if (exception.Message.ToString().StartsWith("ERROR"))
                {
                    dto.status = new Status(Convert.ToInt32(exception.InnerException.Message));
                }
            }
            dto.response = usersubscribechannel;
            return dto;
        }

        public DTO<ServicesByCountryDTO> GetSubs_RecentServiceChannels(Input<ServicesByCountry> obj)
        {
            DTO<ServicesByCountryDTO> dto = new DTO<ServicesByCountryDTO>();
            ServicesByCountryDTO sbc = new ServicesByCountryDTO();
            dto.objname = "GetSubs_RecentServiceChannels";
            try
            {
                if ((string.IsNullOrEmpty(obj.input.CountryID) || string.IsNullOrEmpty(obj.input.UserID) || string.IsNullOrEmpty(obj.param.company)) || (string.IsNullOrEmpty(obj.param.deviceid) || string.IsNullOrEmpty(obj.param.username) || string.IsNullOrEmpty(obj.param.password)))
                {
                    dto.status = new Status(800);
                    return dto;
                }
                sbc.servicechannels = this.getrecentservicechannels(Convert.ToInt32(obj.input.CountryID), Convert.ToInt32(obj.input.OperatorID), Convert.ToInt64(obj.input.UserID));
               
                dto.response = sbc;
                dto.status = new Status(0);
            }
            catch (Exception exception)
            {
                TraceLog.WriteToLog(exception.Message.ToString() + "Trace = " + exception.StackTrace.ToString(), exception);
                dto.status = new Status(1);
                dto.status.info1 = exception.Message;
                if (exception.Message.ToString().StartsWith("ERROR"))
                {
                    dto.status = new Status(Convert.ToInt32(exception.InnerException.Message));
                }
            }
            dto.response = sbc;
            return dto;
        }

        public DTO<ServicesByCountryDTO> GetSubs_ServiceChannel(Input<ServicesByCountry> obj)
        {
            DTO<ServicesByCountryDTO> dto = new DTO<ServicesByCountryDTO>();
            ServicesByCountryDTO sc = new ServicesByCountryDTO();
            dto.objname = "GetSubs_ServiceChannel";
            try
            {
                if ((string.IsNullOrEmpty(obj.input.UserID) || string.IsNullOrEmpty(obj.input.CountryID) || string.IsNullOrEmpty(obj.param.company)) || (string.IsNullOrEmpty(obj.param.deviceid) || string.IsNullOrEmpty(obj.param.username) || string.IsNullOrEmpty(obj.param.password)))
                {
                    dto.status = new Status(800);
                    return dto;
                }
                sc.servicechannels = this.getservicechannel(Convert.ToInt32(obj.input.CountryID), Convert.ToInt64(obj.input.UserID));
                dto.response = sc;
                dto.status = new Status(0);
            }
            catch (Exception exception)
            {
                TraceLog.WriteToLog(exception.Message.ToString() + "Trace = " + exception.StackTrace.ToString(), exception);
                dto.status = new Status(1);
                dto.status.info1 = exception.Message;
                if (exception.Message.ToString().StartsWith("ERROR"))
                {
                    dto.status = new Status(Convert.ToInt32(exception.InnerException.Message));
                }
            }
            dto.response = sc;
            return dto;
        }

        public DTO<Models.Outputs.Subscription.SubsGetCategoriesDTO> GetSubs_Categories(Input<Models.Inputs.Subscription.Country> obj)
        {
            DTO<SubsGetCategoriesDTO> dto = new DTO<SubsGetCategoriesDTO>();
            SubsGetCategoriesDTO sgc = new SubsGetCategoriesDTO();
            dto.objname = "GetSubs_Categories";
            try
            {
                if ((string.IsNullOrEmpty(obj.input.CountryID) || string.IsNullOrEmpty(obj.param.company)) || (string.IsNullOrEmpty(obj.param.deviceid) || string.IsNullOrEmpty(obj.param.username) || string.IsNullOrEmpty(obj.param.password)))
                {
                    dto.status = new Status(800);
                    return dto;
                }
                sgc.categories = this.getsubs_categories(Convert.ToInt32(obj.input.CountryID));
                dto.response = sgc;
                dto.status = new Status(0);
            }
            catch (Exception exception)
            {
                TraceLog.WriteToLog(exception.Message.ToString() + "Trace = " + exception.StackTrace.ToString(), exception);
                dto.status = new Status(1);
                dto.status.info1 = exception.Message;
                if (exception.Message.ToString().StartsWith("ERROR"))
                {
                    dto.status = new Status(Convert.ToInt32(exception.InnerException.Message));
                }
            }
            dto.response = sgc;
            return dto;
        }

        public DTO<Models.Outputs.Subscription.ServicebyCategoryDTO> GetSubs_ServiceByCategory(Input<Models.Inputs.Subscription.ServiceByCategory> obj)
        {
            DTO<ServicebyCategoryDTO> dto = new DTO<ServicebyCategoryDTO>();
            ServicebyCategoryDTO sbc = new ServicebyCategoryDTO();
            dto.objname = "GetSubs_ServiceByCategory";
            try
            {
                if ((string.IsNullOrEmpty(obj.input.countryid) || string.IsNullOrEmpty(obj.input.userid) || string.IsNullOrEmpty(obj.input.categoryid) || string.IsNullOrEmpty(obj.param.company)) || (string.IsNullOrEmpty(obj.param.deviceid) || string.IsNullOrEmpty(obj.param.username) || string.IsNullOrEmpty(obj.param.password)))
                {
                    dto.status = new Status(800);
                    return dto;
                }
                sbc.servicechannels = this.getservicebycategory(Convert.ToInt32(obj.input.countryid), Convert.ToInt32(obj.input.categoryid), Convert.ToInt64(obj.input.userid));
                dto.response = sbc;
                dto.status = new Status(0);
            }
            catch (Exception exception)
            {
                TraceLog.WriteToLog(exception.Message.ToString() + "Trace = " + exception.StackTrace.ToString(), exception);
                dto.status = new Status(1);
                dto.status.info1 = exception.Message;
                if (exception.Message.ToString().StartsWith("ERROR"))
                {
                    dto.status = new Status(Convert.ToInt32(exception.InnerException.Message));
                }
            }
            dto.response = sbc;
            return dto;
        }

        public DTO<Models.Outputs.Subscription.ContentViewDTO> GetUserSubs_AllContentView(Input<Models.Inputs.Subscription.ContentView> obj)
        {
            DTO<ContentViewDTO> dto = new DTO<ContentViewDTO>();
            ContentViewDTO cv = new ContentViewDTO();
            dto.objname = "GetSubs_ContentView";
            try
            {
                if ((string.IsNullOrEmpty(obj.input.countryid) || string.IsNullOrEmpty(obj.input.userid) || string.IsNullOrEmpty(obj.param.company)) || (string.IsNullOrEmpty(obj.param.deviceid) || string.IsNullOrEmpty(obj.param.username) || string.IsNullOrEmpty(obj.param.password)))
                {
                    dto.status = new Status(800);
                    return dto;
                }
                cv.contentview = this.getcontentview(Convert.ToInt32(obj.input.countryid), Convert.ToInt64(obj.input.userid));
                dto.response = cv;
                dto.status = new Status(0);
            }
            catch (Exception exception)
            {
                TraceLog.WriteToLog(exception.Message.ToString() + "Trace = " + exception.StackTrace.ToString(), exception);
                dto.status = new Status(1);
                dto.status.info1 = exception.Message;
                if (exception.Message.ToString().StartsWith("ERROR"))
                {
                    dto.status = new Status(Convert.ToInt32(exception.InnerException.Message));
                }
            }
            dto.response = cv;
            return dto;
        }

        public DTO<Models.Outputs.Subscription.ContentViewDTO> GetUserSubs_ContentViewbyService(Input<Models.Inputs.Subscription.Contentviewbyservice> obj)
        {
            DTO<ContentViewDTO> dto = new DTO<ContentViewDTO>();
            ContentViewDTO cv = new ContentViewDTO();
            dto.objname = "GetSubs_ContentView";
            try
            {
                if ((string.IsNullOrEmpty(obj.input.countryid) || string.IsNullOrEmpty(obj.input.userid) || string.IsNullOrEmpty(obj.input.channelid) || string.IsNullOrEmpty(obj.param.company)) || (string.IsNullOrEmpty(obj.param.deviceid) || string.IsNullOrEmpty(obj.param.username) || string.IsNullOrEmpty(obj.param.password)))
                {
                    dto.status = new Status(800);
                    return dto;
                }
                cv.contentview = this.getcontentviewbyservice(Convert.ToInt64(obj.input.userid), Convert.ToInt32(obj.input.countryid), Convert.ToInt32(obj.input.channelid));
                dto.response = cv;
                dto.status = new Status(0);
            }
            catch (Exception exception)
            {
                TraceLog.WriteToLog(exception.Message.ToString() + "Trace = " + exception.StackTrace.ToString(), exception);
                dto.status = new Status(1);
                dto.status.info1 = exception.Message;
                if (exception.Message.ToString().StartsWith("ERROR"))
                {
                    dto.status = new Status(Convert.ToInt32(exception.InnerException.Message));
                }
            }
            dto.response = cv;
            return dto;
        }

        public DTO<Models.Outputs.Subscription.SubusersubcribechannelDTO> GetSubs_UserSubcribeChannels(Input<Models.Inputs.Subscription.SubsUsersubscribechannel> obj)
        {

            DTO<SubusersubcribechannelDTO> dto = new DTO<SubusersubcribechannelDTO>();
            SubusersubcribechannelDTO subusersubscribechannel = new SubusersubcribechannelDTO();
            dto.objname = "GetSubs_UserSubcribeChannels";


            try
            {
                if ((string.IsNullOrEmpty(obj.input.userid) || string.IsNullOrEmpty(obj.input.countryid) || string.IsNullOrEmpty(obj.param.company)) || (string.IsNullOrEmpty(obj.param.deviceid) || string.IsNullOrEmpty(obj.param.username) || string.IsNullOrEmpty(obj.param.password)))
                {
                    dto.status = new Status(800);
                    return dto;
                }
                subusersubscribechannel.channels = this.GetSubs_UserSubscribeOnChannels(Convert.ToInt64(obj.input.userid), Convert.ToInt32(obj.input.countryid));
                dto.response = subusersubscribechannel;
                dto.status = new Status(0);
            }
            catch (Exception exception)
            {
                TraceLog.WriteToLog(exception.Message.ToString() + "Trace = " + exception.StackTrace.ToString(), exception);
                dto.status = new Status(1);
                dto.status.info1 = exception.Message;
                if (exception.Message.ToString().StartsWith("ERROR"))
                {
                    dto.status = new Status(Convert.ToInt32(exception.InnerException.Message));
                }
            }
            dto.response = subusersubscribechannel;
            return dto;
        }

        public DTO<Models.Outputs.Subscription.InteractiveservicevoteDTO> InteractiveServiceContestantVote(Input<Models.Inputs.Subscription.InteractiveServiceContestantVote> obj)
        {
            DTO<InteractiveservicevoteDTO> dto = new DTO<InteractiveservicevoteDTO>();
            InteractiveservicevoteDTO resp = new InteractiveservicevoteDTO();
            dto.objname = "InteractiveServiceContestantVote";


            try
            {
                if ((string.IsNullOrEmpty(obj.input.operatorid) || string.IsNullOrEmpty(obj.input.userid) || string.IsNullOrEmpty(obj.input.interactiveserviceid) || string.IsNullOrEmpty(obj.input.interactivecontestantid) || string.IsNullOrEmpty(obj.param.company)) || (string.IsNullOrEmpty(obj.param.deviceid) || string.IsNullOrEmpty(obj.param.username) || string.IsNullOrEmpty(obj.param.password)))
                {
                    dto.status = new Status(800);
                    return dto;
                }
                if (!Interactiveservicecontestantvoting(Convert.ToInt64(obj.input.userid), Convert.ToInt32(obj.input.operatorid), Convert.ToInt32(obj.input.interactiveserviceid), Convert.ToInt32(obj.input.interactivecontestantid), false))
                {
                    dto.status = new Status(3080);
                    return dto;
                }
                dto.response = resp;
                dto.status = new Status(0);
            }
            catch (Exception exception)
            {
                TraceLog.WriteToLog(exception.Message.ToString() + "Trace = " + exception.StackTrace.ToString(), exception);
                dto.status = new Status(1);
                dto.status.info1 = exception.Message;
                if (exception.Message.ToString().StartsWith("ERROR"))
                {
                    dto.status = new Status(Convert.ToInt32(exception.InnerException.Message));
                }
            }
            return dto;
        }

        public DTO<Models.Outputs.Subscription.InteractiveserviceusermsgDTO> InteractiveServiceUserMessage(Input<Models.Inputs.Subscription.Interactiveserviceusermsg> obj)
        {
            DTO<InteractiveserviceusermsgDTO> dto = new DTO<InteractiveserviceusermsgDTO>();
            InteractiveserviceusermsgDTO resp = new InteractiveserviceusermsgDTO();
            dto.objname = "InteractiveServiceUserMessage";


            try
            {
                if ((string.IsNullOrEmpty(obj.input.operatorid) || string.IsNullOrEmpty(obj.input.userid) || string.IsNullOrEmpty(obj.input.interactiveserviceid) || string.IsNullOrEmpty(obj.input.usermessage) || string.IsNullOrEmpty(obj.param.company)) || (string.IsNullOrEmpty(obj.param.deviceid) || string.IsNullOrEmpty(obj.param.username) || string.IsNullOrEmpty(obj.param.password)))
                {
                    dto.status = new Status(800);
                    return dto;
                }
                if (!Interactiveserviceusermessage(Convert.ToInt64(obj.input.userid), Convert.ToInt32(obj.input.operatorid), Convert.ToInt32(obj.input.interactiveserviceid), obj.input.usermessage, false))
                {
                    dto.status = new Status(3090);
                    return dto;
                }
                dto.response = resp;
                dto.status = new Status(0);
            }
            catch (Exception exception)
            {
                TraceLog.WriteToLog(exception.Message.ToString() + "Trace = " + exception.StackTrace.ToString(), exception);
                dto.status = new Status(1);
                dto.status.info1 = exception.Message;
                if (exception.Message.ToString().StartsWith("ERROR"))
                {
                    dto.status = new Status(Convert.ToInt32(exception.InnerException.Message));
                }
            }
            return dto;
        }

        
        #endregion

        #region Private Methods

        public List<channels> getservicechannels(int serID, int countryID)
        {
            List<channels> channels = new List<channels>();

            //channels = (from usc in db.Subs_ServiceChannelGroupsForCountry
            //            join scg in db.Subs_ServiceChannelGroups on usc.ServiceChannelGroupID equals scg.ID
            //            join sc in db.Subs_ServiceChannels on scg.ServiceChannelID equals sc.ID
            //            join ser in db.Subs_Services on sc.ServiceID equals ser.ID
            //            where usc.Status == true && scg.Status == true && sc.Status == true && ser.Status == true && ser.ID == serID && usc.CountryID == countryID

            //            select new { sc = sc }
            //            ).OrderBy(x => x.sc.ServiceChannelName).AsEnumerable().Distinct()
            //            .Select(x => new channels
            //            {
            //                channelid = x.sc.ID.ToString(),
            //                channelname = x.sc.ServiceChannelName == null ? "" : x.sc.ServiceChannelName,
            //                channeldesc = x.sc.ServiceChannelDesc == null ? "" : x.sc.ServiceChannelDesc,
            //                ImageURL = x.sc.Info1 == null ? "" : x.sc.Info1,
            //                translations = getTranslations(new List<string> { x.sc.ServiceChannelName, x.sc.ServiceChannelDesc })
            //            }).ToList();

            return channels;

        }

        public List<channels> GetUserSubscribeOnChannels(int userid, int serid, int countryid)
        {
            List<channels> channels = new List<channels>();
            //channels = (from usc in db.Subs_ServiceChannelGroupsForCountry
            //            join scg in db.Subs_ServiceChannelGroups on usc.ServiceChannelGroupID equals scg.ID
            //            join sc in db.Subs_ServiceChannels on scg.ServiceChannelID equals sc.ID
            //            join ser in db.Subs_Services on sc.ServiceID equals ser.ID
            //            join susc in db.Subs_UserServiceChannels on ser.ID equals susc.ServiceID
            //            where usc.Status == true && scg.Status == true && sc.Status == true && ser.Status == true && susc.Status == true
            //            && ser.ID == serid && usc.CountryID == countryid && susc.UserID == userid

            //            select new { sc = sc }
            //            ).OrderBy(x => x.sc.ServiceChannelName).AsEnumerable().Distinct()
            //            .Select(x => new channels
            //            {
            //                channelid = x.sc.ID.ToString(),
            //                channelname = x.sc.ServiceChannelName == null ? "" : x.sc.ServiceChannelName,
            //                channeldesc = x.sc.ServiceChannelDesc == null ? "" : x.sc.ServiceChannelDesc,
            //                ImageURL = x.sc.Info1 == null ? "" : x.sc.Info1,
            //                translations = getTranslations(new List<string> { x.sc.ServiceChannelName, x.sc.ServiceChannelDesc })
            //            }).ToList();

            return channels;
        }

        public channels GetUserServiceChannelDetail(int userid, int serid, int channnelid)
        {
            var rows = db.Subs_UserServiceChannels.Where(x => x.UserID == userid && x.ChannelID == channnelid && x.Status == true).SingleOrDefault();
            var chnl = db.Subs_ServiceChannels.Where(x => x.ServiceID == serid && x.ID == channnelid && x.Status == true).SingleOrDefault();

            channels channels = new channels();
            channels.channelid = chnl.ID.ToString();
            channels.channelname = chnl.ServiceChannelName == null ? "" : chnl.ServiceChannelName;
            channels.channeldesc = chnl.ServiceChannelDesc == null ? "" : chnl.ServiceChannelDesc;
            channels.ImageURL = chnl.Info1 == null ? "" : chnl.Info1;
            channels.translations = getTranslations(new List<string> { channels.channelname, channels.channeldesc });


            //if (rows != null)
            //{
            //    channels.isSubscribed = true.ToString();
            //}
            //else
            //{
            //    channels.isSubscribed = false.ToString();
            //}

            return channels;
        }

        public List<contentdata> GetUserChannelContentData(int userid, int serid, int channelid, int countryid)
        {

            List<contentdata> contentdata = new List<contentdata>();

            contentdata = (from scd in db.Subs_ContentDelivery
                           join scdata in db.Subs_ContentData on scd.ContentDataID equals scdata.ID
                           where scd.Status == true && scdata.Status == true
                           && scd.UserID == userid && scd.ServiceID == serid && scd.ServiceChannelID == channelid && scd.CountryID == countryid
                           select new { cd = scdata }
                           ).OrderBy(x => x.cd.ContentName).AsEnumerable().Distinct()
                           .Select(x => new contentdata
                           {
                               contentdataid = x.cd.ID.ToString(),
                               contentname = x.cd.ContentName == null ? "" : x.cd.ContentName,
                               contentdesc = x.cd.ContentDescription == null ? "" : x.cd.ContentDescription,
                               contenttype = x.cd.ContentType.ToString(),
                               contentData = x.cd.ContentData == null ? "" : x.cd.ContentData,
                               translations = getTranslations(new List<string> { x.cd.ContentName, x.cd.ContentDescription })

                           }).ToList();

            return contentdata;
        }

        public List<Models.Outputs.Subscription.service> GetServices(int countryid)
        {
            List<Models.Outputs.Subscription.service> service = new List<Models.Outputs.Subscription.service>();

            //service = (from usc in db.Subs_ServiceChannelGroupsForCountry
            //           join scg in db.Subs_ServiceChannelGroups on usc.ServiceChannelGroupID equals scg.ID
            //           join sc in db.Subs_ServiceChannels on scg.ServiceChannelID equals sc.ID
            //           join s in db.Subs_Services on sc.ServiceID equals s.ID
            //           where usc.Status == true && sc.Status == true && scg.Status == true && s.Status == true && usc.CountryID == countryid
            //           select new { ser = s }
            //         ).OrderBy(x => x.ser.ServiceName).AsEnumerable().Distinct()
            //         .Select(x => new Models.Outputs.Subscription.service
            //         {
            //             serviceid = x.ser.ID.ToString(),
            //             servicename = x.ser.ServiceName == null ? "" : x.ser.ServiceName,
            //             servicedesc = x.ser.ServiceDescription == null ? "" : x.ser.ServiceDescription,
            //             ImageUrl = x.ser.Info1 == null ? "" : x.ser.Info1,
            //             translations = getTranslations(new List<string> { x.ser.ServiceName, x.ser.ServiceDescription })
            //         }).ToList();

            return service;
        }

        public bool SubscribeUser2(Int64 userid, int serid, int channelid, int countryid, int operatorid, bool checkbilling)
        {

           // var row = db.Subs_Billing.Where(x => x.UserID == userid && x.ServiceID == serid && x.ServiceChannelID == channelid && x.CountryID == countryid && x.OperatorID == operatorid).SingleOrDefault();
            var row = db.Subs_Billing.Where(x => x.UserID == userid && x.ServiceID == serid && x.ServiceChannelID == channelid && x.CountryID == countryid ).SingleOrDefault();
           
            var scgfc = db.Subs_ServiceChannelContentForCountry.Where(x => x.CountryID == countryid && x.ServiceChannelID == channelid && x.Status == true).SingleOrDefault();
            var subscribeuser = db.Subs_UserServiceChannels.Where(x => x.UserID == userid && x.ServiceID == serid && x.ChannelID == channelid && x.CountryID == countryid).SingleOrDefault();

            var contentdelivery = 0;  // ContentDelivery is to add no. of rows in Subs_ContentDelivery table to delivery the contentdata


            int lid = 0;

            var lastid = db.Subs_ContentDelivery.Where(x => x.UserID == userid && x.ServiceID == serid && x.ServiceChannelID == channelid && x.CountryID == countryid && x.Status == true).OrderByDescending(x => x.ContentDataID).FirstOrDefault();
            if (lastid != null)
            {
                lid = Convert.ToInt32(lastid.ContentDataID);
            }

            var datenow = DateTime.Now;

            if (subscribeuser == null)
            {
                // Subs_UserSubscriptions
                Subs_UserSubscriptions us = new Subs_UserSubscriptions();
                us.UserID = userid;
                us.ServiceID = serid;
                us.ChannelID = channelid;
                us.CountryID = countryid;
                us.Status = true;
                us.Action = 1;
                us.ActionDescription = "Subscribe from iBand Application";
                us.ActionStatus = "SUCCESS";
                us.CreatedDate = DateTime.Now;

                db.Subs_UserSubscriptions.Add(us);
                //  End

                //  Subs_UserServiceChannels
                Subs_UserServiceChannels suc = new Subs_UserServiceChannels();
                suc.UserID = userid;
                suc.ServiceID = serid;
                suc.ChannelID = channelid;
                suc.CountryID = countryid;
                suc.Status = true;
                suc.SubscriptionDate = DateTime.Now;
                suc.CreatedDate = DateTime.Now;

                db.Subs_UserServiceChannels.Add(suc);
                //   End

                //   Subs_Billing
                Subs_Billing subbill = new Subs_Billing();
                subbill.UserID = userid;
                subbill.ServiceID = serid;
                subbill.CountryID = countryid;
                subbill.ServiceChannelID = channelid;
                subbill.OperatorID = operatorid;
                subbill.Amount = scgfc.Charge;
                subbill.BillingStatus = "SUCCESS";
                subbill.LastBilledDate = DateTime.Now;
                if (scgfc.ChargeDurationType == 1)
                {
                    subbill.BillingScheduledDate = DateTime.Now.AddDays(Convert.ToInt32(0 * scgfc.ChargeDuration));
                }
                else if (scgfc.ChargeDurationType == 2)
                {
                    subbill.BillingScheduledDate = DateTime.Now.AddDays(Convert.ToInt32(6 * scgfc.ChargeDuration));
                }
                else
                {
                    subbill.BillingScheduledDate = DateTime.Now.AddDays(Convert.ToInt32(29 * scgfc.ChargeDuration));
                }
                subbill.BilledDate = DateTime.Now;
                subbill.LastBillingStatus = "SUCCESS";
                subbill.Status = true;
                subbill.CreatedDate = DateTime.Now;

                db.Subs_Billing.Add(subbill);
                db.SaveChanges();      //Storing the data till here because need BillingScheduledDate in next table i.e. Subs_BillingHistory & in taking data from contentData table

                //    End

                //   Subs_BillingHistory

                Subs_BillingHistory bh = new Subs_BillingHistory();
                bh.UserID = userid;
                bh.ServiceID = serid;
                bh.OperatorID = operatorid;
                bh.CountryID = countryid;
                bh.Amount = scgfc.Charge;
                bh.BillingChannel = "DBO";
                bh.BillingDate = subbill.BillingScheduledDate;
                bh.BilledDate = DateTime.Now;
                bh.BillingStatus = "SUCCESS";
                bh.Status = true;
                bh.CreatedDate = DateTime.Now;

                db.Subs_BillingHistory.Add(bh);
                // End


                //Subs_ContentDelivery

                var scg = (from sccfc in db.Subs_ServiceChannelContentForCountry
                           join sccc in db.Subs_ServiceChannelContentConfig on sccfc.ID equals sccc.ServiceChannelForCountryID
                           join cg in db.Subs_ContentGroups on sccc.ContentGroupID equals cg.ID
                           join cgc in db.Subs_ContentGroupConfig on cg.ID equals cgc.GroupID
                           //join sc in db.Subs_ServiceChannels on sccfc.ServiceChannelID equals sc.ID
                           where sccfc.CountryID == countryid && sccfc.ServiceChannelID == channelid && cg.Status == true && cgc.Status == true
                           && sccfc.Status == true && sccc.Status == true
                           select new { sccc = sccc }
                         ).OrderBy(x => x.sccc.ID).ToList();

                var group = (from sccc in db.Subs_ServiceChannelContentConfig
                             join cg in db.Subs_ContentGroups on sccc.ContentGroupID equals cg.ID
                             join cgc in db.Subs_ContentGroupConfig on cg.ID equals cgc.GroupID
                            // join cd in db.Subs_ContentData on cgc.ContentDataID equals cd.ID
                             where sccc.Status == true && cg.Status == true && cgc.Status == true //&& cd.Status == true
                             select new { g = cgc }
                             ).OrderBy(x => x.g.ID).Distinct().ToList();

                foreach (var c in scg)
                {
                    bool isSubcategoryExist = false;
                    if (c.sccc.SubCategoryID != null)
                    {
                        isSubcategoryExist = true;
                    }

                    bool isCategoryExist = false;
                    if (c.sccc.CategoryID != null)
                    {
                        isCategoryExist = true;
                    }

                    bool isContentTypeExist = false;
                    if (c.sccc.ContentType != null)
                    {
                        isContentTypeExist = true;
                    }

                    bool isContentOwnerExist = false;
                    if (c.sccc.ContentOwnerID != null)
                    {
                        isContentOwnerExist = true;
                    }

                    bool isContentGroupExist = false;
                    foreach (var g1 in group)
                    {
                        if (g1.g.GroupID != null || g1.g.GroupID != 0) //if GroupID exist then take data from Subs_ContentGroupConfig table
                        {
                            isContentGroupExist = true;

                            //checking the data's in the Sub_ContentGroupConfig Table if all subcategory,Category,Contenttype & ContentOwner isExist
                            bool isSubcategorygroupExist = false;
                            if (g1.g.SubCategoryID != null)
                            {
                                isSubcategorygroupExist = true;
                            }

                            bool isCategorygroupExist = false;
                            if (g1.g.CategoryID != null)
                            {
                                isCategorygroupExist = true;
                            }

                            bool isContentTypegroupExist = false;
                            if (g1.g.ContentType != null)
                            {
                                isContentTypegroupExist = true;
                            }

                            bool isContentOwnergroupExist = false;
                            if (g1.g.ContentOwnerID != null)
                            {
                                isContentOwnergroupExist = true;
                            }

                            contentdelivery = getcontentdelivery((int)scgfc.ChargeDurationType, (int)c.sccc.ContentDurationType, (int)c.sccc.ContentQuantity, (int)c.sccc.ContentDuration); //Checking the amouont of data to be delivered


                            var cd1 = db.Subs_ContentData.Where(x => (isSubcategorygroupExist == true ? x.SubCategoryID == c.sccc.SubCategoryID : x.SubCategoryID == null)
                                                   && (isCategorygroupExist == true ? x.CategoryID == c.sccc.CategoryID : x.CategoryID == null)
                                                   && (isContentOwnergroupExist == true ? x.OwnerID == c.sccc.ContentOwnerID : x.OwnerID == null)
                                                   && (isContentTypegroupExist == true ? x.ContentType == c.sccc.ContentType : x.ContentType == null)
                                                   && ((x.ScheduleDate >= datenow && x.ScheduleDate >= subbill.BillingScheduledDate) || x.ScheduleDate == null)
                                                   && x.ID > lid).OrderBy(x => x.ID).Take((int)contentdelivery).ToList();
                            if (cd1 != null)
                            {
                                foreach (var cid in cd1)
                                {
                                    //Subs_ContentDelivery
                                    Subs_ContentDelivery subcd = new Subs_ContentDelivery();
                                    subcd.UserID = userid;
                                    subcd.ServiceID = serid;
                                    subcd.ServiceChannelID = channelid;
                                    subcd.CountryID = countryid;
                                    subcd.ContentDataID = cid.ID;
                                    subcd.ContentScheduledDate = datenow;
                                    subcd.ContentDeliveryStatus = true;
                                    subcd.ContentDeliveryStatusDesc = "SUCCESS";
                                    subcd.ContentChannel = "APP";
                                    subcd.Status = true;
                                    subcd.CreatedDate = DateTime.Now;

                                    db.Subs_ContentDelivery.Add(subcd);
                                    //End
                                    datenow = datenow.AddDays(1);
                                }
                            }
                        }
                    }

                    //Data for when there is NO GroupID Exist fetching the data from the ContentData table.
                    contentdelivery = getcontentdelivery((int)scgfc.ChargeDurationType, (int)c.sccc.ContentDurationType, (int)c.sccc.ContentQuantity, (int)c.sccc.ContentDuration); //Checking the amouont of data to be delivered


                    var cd = db.Subs_ContentData.Where(x => (isSubcategoryExist == true ? x.SubCategoryID == c.sccc.SubCategoryID : x.SubCategoryID == null)
                                                   && (isCategoryExist == true ? x.CategoryID == c.sccc.CategoryID : x.CategoryID == null)
                                                   && (isContentOwnerExist == true ? x.OwnerID == c.sccc.ContentOwnerID : x.OwnerID == null)
                                                   && (isContentTypeExist == true ? x.ContentType == c.sccc.ContentType : x.ContentType == null)
                                                   && ((x.ScheduleDate >= datenow && x.ScheduleDate >= subbill.BillingScheduledDate) || x.ScheduleDate == null)
                                                   && x.ID > lid).OrderBy(x => x.ID).Take((int)contentdelivery).ToList();
                    if (cd != null)
                    {
                        foreach (var cid in cd)
                        {
                            //Subs_ContentDelivery
                            Subs_ContentDelivery subcd = new Subs_ContentDelivery();
                            subcd.UserID = userid;
                            subcd.ServiceID = serid;
                            subcd.ServiceChannelID = channelid;
                            subcd.CountryID = countryid;
                            subcd.ContentDataID = cid.ID;
                            subcd.ContentScheduledDate = datenow;
                            subcd.ContentDeliveryStatus = true;
                            subcd.ContentDeliveryStatusDesc = "SUCCESS";
                            subcd.ContentChannel = "APP";
                            subcd.Status = true;
                            subcd.CreatedDate = DateTime.Now;

                            db.Subs_ContentDelivery.Add(subcd);
                            //End
                            datenow = datenow.AddDays(1);
                        }
                    }
                }

                //End


                db.SaveChanges();

                return true;

            }
            else if (subscribeuser.Status == false)
            {
                //  Subs_UserServiceChannels

                subscribeuser.Status = true;
                subscribeuser.SubscriptionDate = DateTime.Now;
                subscribeuser.UnsubscriptionDate = null;

                //   End

                //    Subs_UserSubscriptions
                Subs_UserSubscriptions us = new Subs_UserSubscriptions();
                us.UserID = userid;
                us.ServiceID = serid;
                us.ChannelID = channelid;
                us.CountryID = countryid;
                us.Status = true;
                us.Action = 1;
                us.ActionDescription = "Subscribe from iBand Application";
                us.ActionStatus = "SUCCESS";
                us.CreatedDate = DateTime.Now;

                db.Subs_UserSubscriptions.Add(us);
                //    End

                //    Subs_Billing
                if (scgfc.ChargeDurationType == 1)
                {
                    row.BillingScheduledDate = DateTime.Now.AddDays(Convert.ToInt32(0 * scgfc.ChargeDuration));
                }
                else if (scgfc.ChargeDurationType == 2)
                {
                    row.BillingScheduledDate = DateTime.Now.AddDays(Convert.ToInt32(6 * scgfc.ChargeDuration));
                }
                else
                {
                    row.BillingScheduledDate = DateTime.Now.AddDays(Convert.ToInt32(29 * scgfc.ChargeDuration));
                }
                row.Status = true;
                row.CreatedDate = DateTime.Now;
                // End

                // Subs_BillingHistory

                Subs_BillingHistory bh = new Subs_BillingHistory();
                bh.UserID = userid;
                bh.ServiceID = serid;
                bh.OperatorID = operatorid;
                bh.CountryID = countryid;
                bh.Amount = scgfc.Charge;
                bh.BillingChannel = "DBO";
                bh.BillingDate = row.BillingScheduledDate;
                bh.BilledDate = DateTime.Now;
                bh.BillingStatus = "SUCCESS";
                bh.Status = true;
                bh.CreatedDate = DateTime.Now;

                db.Subs_BillingHistory.Add(bh);
                //End

                //Subs_ContentDelivery

                var scg = (from sccfc in db.Subs_ServiceChannelContentForCountry
                           join sccc in db.Subs_ServiceChannelContentConfig on sccfc.ID equals sccc.ServiceChannelForCountryID
                           join sc in db.Subs_ServiceChannels on sccfc.ServiceChannelID equals sc.ID
                           where sccfc.CountryID == countryid && sccfc.ServiceChannelID == serid
                           && sccfc.Status == true && sccc.Status == true
                           select new { sccc = sccc }
                         ).OrderBy(x => x.sccc.ID).ToList();

                var group = (from sccc in db.Subs_ServiceChannelContentConfig
                             join cg in db.Subs_ContentGroups on sccc.ContentGroupID equals cg.ID
                             join cgc in db.Subs_ContentGroupConfig on cg.ID equals cgc.GroupID
                             join cd in db.Subs_ContentData on cgc.ContentDataID equals cd.ID
                             where sccc.Status == true && cg.Status == true && cgc.Status == true && cd.Status == true
                             select new { g = cgc }
                             ).OrderBy(x => x.g.ID).ToList();

                foreach (var c in scg)
                {
                    bool isSubcategoryExist = false;
                    if (c.sccc.SubCategoryID != null)
                    {
                        isSubcategoryExist = true;
                    }

                    bool isCategoryExist = false;
                    if (c.sccc.CategoryID != null)
                    {
                        isCategoryExist = true;
                    }

                    bool isContentTypeExist = false;
                    if (c.sccc.ContentType != null)
                    {
                        isContentTypeExist = true;
                    }

                    bool isContentOwnerExist = false;
                    if (c.sccc.ContentOwnerID != null)
                    {
                        isContentOwnerExist = true;
                    }

                    bool isContentGroupExist = false;
                    foreach (var g1 in group)
                    {
                        if (g1.g.GroupID != null || g1.g.GroupID != 0) //if GroupID exist then take data from Subs_ContentGroupConfig table
                        {
                            isContentGroupExist = true;

                            //checking the data's in the Sub_ContentGroupConfig Table if all subcategory,Category,Contenttype & ContentOwner isExist
                            bool isSubcategorygroupExist = false;
                            if (g1.g.SubCategoryID != null)
                            {
                                isSubcategorygroupExist = true;
                            }

                            bool isCategorygroupExist = false;
                            if (g1.g.CategoryID != null)
                            {
                                isCategorygroupExist = true;
                            }

                            bool isContentTypegroupExist = false;
                            if (g1.g.ContentType != null)
                            {
                                isContentTypegroupExist = true;
                            }

                            bool isContentOwnergroupExist = false;
                            if (g1.g.ContentOwnerID != null)
                            {
                                isContentOwnergroupExist = true;
                            }

                            contentdelivery = getcontentdelivery((int)scgfc.ChargeDurationType, (int)c.sccc.ContentDurationType, (int)c.sccc.ContentQuantity, (int)c.sccc.ContentDuration); //Checking the amouont of data to be delivered


                            var cd1 = db.Subs_ContentData.Where(x => (isSubcategorygroupExist == true ? x.SubCategoryID == c.sccc.SubCategoryID : x.SubCategoryID == null)
                                                   && (isCategorygroupExist == true ? x.CategoryID == c.sccc.CategoryID : x.CategoryID == null)
                                                   && (isContentOwnergroupExist == true ? x.OwnerID == c.sccc.ContentOwnerID : x.OwnerID == null)
                                                   && (isContentTypegroupExist == true ? x.ContentType == c.sccc.ContentType : x.ContentType == null)
                                                   && ((x.ScheduleDate >= datenow && x.ScheduleDate >= row.BillingScheduledDate) || x.ScheduleDate == null)
                                                   && x.ID > lid).OrderBy(x => x.ID).Take((int)contentdelivery).ToList();
                            if (cd1 != null)
                            {
                                foreach (var cid in cd1)
                                {
                                    //Subs_ContentDelivery
                                    Subs_ContentDelivery subcd = new Subs_ContentDelivery();
                                    subcd.UserID = userid;
                                    subcd.ServiceID = serid;
                                    subcd.ServiceChannelID = channelid;
                                    subcd.CountryID = countryid;
                                    subcd.ContentDataID = cid.ID;
                                    subcd.ContentScheduledDate = datenow;
                                    subcd.ContentDeliveryStatus = true;
                                    subcd.ContentDeliveryStatusDesc = "SUCCESS";
                                    subcd.ContentChannel = "APP";
                                    subcd.Status = true;
                                    subcd.CreatedDate = DateTime.Now;

                                    db.Subs_ContentDelivery.Add(subcd);
                                    //End
                                    datenow = datenow.AddDays(1);
                                }
                            }
                        }
                    }

                    //Data for when there is NO GroupID Exist fetching the data from the ContentData table.
                    contentdelivery = getcontentdelivery((int)scgfc.ChargeDurationType, (int)c.sccc.ContentDurationType, (int)c.sccc.ContentQuantity, (int)c.sccc.ContentDuration); //Checking the amouont of data to be delivered


                    var cd = db.Subs_ContentData.Where(x => (isSubcategoryExist == true ? x.SubCategoryID == c.sccc.SubCategoryID : x.SubCategoryID == null)
                                                   && (isCategoryExist == true ? x.CategoryID == c.sccc.CategoryID : x.CategoryID == null)
                                                   && (isContentOwnerExist == true ? x.OwnerID == c.sccc.ContentOwnerID : x.OwnerID == null)
                                                   && (isContentTypeExist == true ? x.ContentType == c.sccc.ContentType : x.ContentType == null)
                                                   && ((x.ScheduleDate >= datenow && x.ScheduleDate >= row.BillingScheduledDate) || x.ScheduleDate == null)
                                                   && x.ID > lid).OrderBy(x => x.ID).Take((int)contentdelivery).ToList();
                    if (cd != null)
                    {
                        foreach (var cid in cd)
                        {   //Subs_ContentDelivery
                            Subs_ContentDelivery subcd = new Subs_ContentDelivery();
                            subcd.UserID = userid;
                            subcd.ServiceID = serid;
                            subcd.ServiceChannelID = channelid;
                            subcd.CountryID = countryid;
                            subcd.ContentDataID = cid.ID;
                            subcd.ContentScheduledDate = datenow;
                            subcd.ContentDeliveryStatus = true;
                            subcd.ContentDeliveryStatusDesc = "SUCCESS";
                            subcd.ContentChannel = "APP";
                            subcd.Status = true;
                            subcd.CreatedDate = DateTime.Now;

                            db.Subs_ContentDelivery.Add(subcd);
                            //End
                            datenow = datenow.AddDays(1);
                        }
                    }
                }
                // End

                db.SaveChanges();

                return true;
            }
            else
            {
                return false;
            }

        }

        public bool SubscribeUser(Int64 userid, int serid, int channelid, int countryid, int operatorid, bool checkbilling)
        {

            // var row = db.Subs_Billing.Where(x => x.UserID == userid && x.ServiceID == serid && x.ServiceChannelID == channelid && x.CountryID == countryid && x.OperatorID == operatorid).SingleOrDefault();
            var row = db.Subs_Billing.Where(x => x.UserID == userid && x.ServiceID == serid && x.ServiceChannelID == channelid && x.CountryID == countryid).SingleOrDefault();

            var scgfc = db.Subs_ServiceChannelContentForCountry.Where(x => x.CountryID == countryid && x.ServiceChannelID == channelid && x.Status == true).SingleOrDefault();
            var subscribeuser = db.Subs_UserServiceChannels.Where(x => x.UserID == userid && x.ServiceID == serid && x.ChannelID == channelid && x.CountryID == countryid).SingleOrDefault();

            var contentdelivery = 0;  // ContentDelivery is to add no. of rows in Subs_ContentDelivery table to delivery the contentdata


            int lid = 0;

            var lastid = db.Subs_ContentDelivery.Where(x => x.UserID == userid && x.ServiceID == serid && x.ServiceChannelID == channelid && x.CountryID == countryid && x.Status == true).OrderByDescending(x => x.ContentDataID).FirstOrDefault();
            if (lastid != null)
            {
                lid = Convert.ToInt32(lastid.ContentDataID);
            }

            var datenow = DateTime.Now;

            if (subscribeuser == null)
            {
                // Subs_UserSubscriptions
                Subs_UserSubscriptions us = new Subs_UserSubscriptions();
                us.UserID = userid;
                us.ServiceID = serid;
                us.ChannelID = channelid;
                us.CountryID = countryid;
                us.Status = true;
                us.Action = 1;
                us.ActionDescription = "Subscribe from iBand Application";
                us.ActionStatus = "SUCCESS";
                us.CreatedDate = DateTime.Now;

                db.Subs_UserSubscriptions.Add(us);
                //  End

                //  Subs_UserServiceChannels
                Subs_UserServiceChannels suc = new Subs_UserServiceChannels();
                suc.UserID = userid;
                suc.ServiceID = serid;
                suc.ChannelID = channelid;
                suc.CountryID = countryid;
                suc.Status = true;
                suc.SubscriptionDate = DateTime.Now;
                suc.CreatedDate = DateTime.Now;

                db.Subs_UserServiceChannels.Add(suc);
                //   End

                //   Subs_Billing
                Subs_Billing subbill = new Subs_Billing();
                subbill.UserID = userid;
                subbill.ServiceID = serid;
                subbill.CountryID = countryid;
                subbill.ServiceChannelID = channelid;
                subbill.OperatorID = operatorid;
                subbill.Amount = scgfc.Charge;
                subbill.BillingStatus = "SUCCESS";
                subbill.LastBilledDate = DateTime.Now;
                if (scgfc.ChargeDurationType == 1)
                {
                    subbill.BillingScheduledDate = DateTime.Now.AddDays(Convert.ToInt32(0 * scgfc.ChargeDuration));
                }
                else if (scgfc.ChargeDurationType == 2)
                {
                    subbill.BillingScheduledDate = DateTime.Now.AddDays(Convert.ToInt32(6 * scgfc.ChargeDuration));
                }
                else
                {
                    subbill.BillingScheduledDate = DateTime.Now.AddDays(Convert.ToInt32(29 * scgfc.ChargeDuration));
                }
                subbill.BilledDate = DateTime.Now;
                subbill.LastBillingStatus = "SUCCESS";
                subbill.Status = true;
                subbill.CreatedDate = DateTime.Now;

                db.Subs_Billing.Add(subbill);
                db.SaveChanges();      //Storing the data till here because need BillingScheduledDate in next table i.e. Subs_BillingHistory & in taking data from contentData table

                //    End

                //   Subs_BillingHistory

                Subs_BillingHistory bh = new Subs_BillingHistory();
                bh.UserID = userid;
                bh.ServiceID = serid;
                bh.OperatorID = operatorid;
                bh.CountryID = countryid;
                bh.Amount = scgfc.Charge;
                bh.BillingChannel = "DBO";
                bh.BillingDate = subbill.BillingScheduledDate;
                bh.BilledDate = DateTime.Now;
                bh.BillingStatus = "SUCCESS";
                bh.Status = true;
                bh.CreatedDate = DateTime.Now;

                db.Subs_BillingHistory.Add(bh);
                // End


                //Subs_ContentDelivery

                var scg = (from sccfc in db.Subs_ServiceChannelContentForCountry
                           join sccc in db.Subs_ServiceChannelContentConfig on sccfc.ID equals sccc.ServiceChannelForCountryID
                           join cg in db.Subs_ContentGroups on sccc.ContentGroupID equals cg.ID
                           join cgc in db.Subs_ContentGroupConfig on cg.ID equals cgc.GroupID 
                           //join sc in db.Subs_ServiceChannels on sccfc.ServiceChannelID equals sc.ID
                           where sccfc.CountryID == countryid && sccfc.ServiceChannelID == channelid
                           && cg.Status == true && cgc.Status == true
                           && sccfc.Status == true && sccc.Status == true
                           && sccc.ContentType == cgc.ContentType && sccc.ContentOwnerID == cgc.ContentOwnerID
                           select new { sccc, g = cgc }
                         ).ToList();

              

                foreach (var c in scg)
                {
                    bool isSubcategoryExist = false;
                    if (c.sccc.SubCategoryID != null) isSubcategoryExist = true;                      

                    bool isCategoryExist = false;
                    if (c.sccc.CategoryID != null) isCategoryExist = true; 

                    bool isContentTypeExist = false;
                    if (c.sccc.ContentType != null) isContentTypeExist = true; 

                    bool isContentOwnerExist = false;
                    if (c.sccc.ContentOwnerID != null) isContentOwnerExist = true; 

                    bool isContentGroupExist = false;

                   
                        if (c.g.GroupID != null || c.g.GroupID != 0) //if GroupID exist then take data from Subs_ContentGroupConfig table
                        {
                            isContentGroupExist = true;

                            //checking the data's in the Sub_ContentGroupConfig Table if all subcategory,Category,Contenttype & ContentOwner isExist
                            bool isSubcategorygroupExist = false;
                            if (c.g.SubCategoryID != null) isSubcategorygroupExist = true; 

                            bool isCategorygroupExist = false;
                            if (c.g.CategoryID != null) isCategorygroupExist = true;   

                            bool isContentTypegroupExist = false;
                            if (c.g.ContentType != null) isContentTypegroupExist = true;  

                            bool isContentOwnergroupExist = false;
                            if (c.g.ContentOwnerID != null) isContentOwnergroupExist = true;
                            
                            

                            contentdelivery = getcontentdelivery((int)scgfc.ChargeDurationType, (int)c.sccc.ContentDurationType, (int)c.sccc.ContentQuantity, (int)c.sccc.ContentDuration); //Checking the amouont of data to be delivered


                            var cd1 = db.Subs_ContentData.Where(x => (isSubcategorygroupExist == true ? x.SubCategoryID == c.sccc.SubCategoryID : x.SubCategoryID == null)
                                                   && (isCategorygroupExist == true ? x.CategoryID == c.sccc.CategoryID : x.CategoryID == null)
                                                   && (isContentOwnergroupExist == true ? x.OwnerID == c.sccc.ContentOwnerID : x.OwnerID == null)
                                                   && (isContentTypegroupExist == true ? x.ContentType == c.sccc.ContentType : x.ContentType == null)
                                                   && ((x.ScheduleDate >= datenow && x.ScheduleDate >= subbill.BillingScheduledDate) || x.ScheduleDate == null)
                                                   && x.ID > lid).OrderBy(x => x.ID).Take((int)contentdelivery).ToList();
                            if (cd1 != null)
                            {
                                foreach (var cid in cd1)
                                {
                                    //Subs_ContentDelivery
                                    Subs_ContentDelivery subcd = new Subs_ContentDelivery();
                                    subcd.UserID = userid;
                                    subcd.ServiceID = serid;
                                    subcd.ServiceChannelID = channelid;
                                    subcd.CountryID = countryid;
                                    subcd.ContentDataID = cid.ID;
                                    subcd.ContentScheduledDate = datenow;
                                    subcd.ContentDeliveryStatus = true;
                                    subcd.ContentDeliveryStatusDesc = "SUCCESS";
                                    subcd.ContentChannel = "APP";
                                    subcd.Status = true;
                                    subcd.CreatedDate = DateTime.Now;

                                    db.Subs_ContentDelivery.Add(subcd);
                                    //End
                                    datenow = datenow.AddDays(1);
                                }
                            }
                        }                  
                }

                //End


                db.SaveChanges();

                return true;

            }
            else if (subscribeuser.Status == false)
            {
                //  Subs_UserServiceChannels

                subscribeuser.Status = true;
                subscribeuser.SubscriptionDate = DateTime.Now;
                subscribeuser.UnsubscriptionDate = null;

                //   End

                //    Subs_UserSubscriptions
                Subs_UserSubscriptions us = new Subs_UserSubscriptions();
                us.UserID = userid;
                us.ServiceID = serid;
                us.ChannelID = channelid;
                us.CountryID = countryid;
                us.Status = true;
                us.Action = 1;
                us.ActionDescription = "Subscribe from iBand Application";
                us.ActionStatus = "SUCCESS";
                us.CreatedDate = DateTime.Now;

                db.Subs_UserSubscriptions.Add(us);
                //    End

                //    Subs_Billing
                if (scgfc.ChargeDurationType == 1)
                {
                    row.BillingScheduledDate = DateTime.Now.AddDays(Convert.ToInt32(0 * scgfc.ChargeDuration));
                }
                else if (scgfc.ChargeDurationType == 2)
                {
                    row.BillingScheduledDate = DateTime.Now.AddDays(Convert.ToInt32(6 * scgfc.ChargeDuration));
                }
                else
                {
                    row.BillingScheduledDate = DateTime.Now.AddDays(Convert.ToInt32(29 * scgfc.ChargeDuration));
                }
                row.Status = true;
                row.CreatedDate = DateTime.Now;
                // End

                // Subs_BillingHistory

                Subs_BillingHistory bh = new Subs_BillingHistory();
                bh.UserID = userid;
                bh.ServiceID = serid;
                bh.OperatorID = operatorid;
                bh.CountryID = countryid;
                bh.Amount = scgfc.Charge;
                bh.BillingChannel = "DBO";
                bh.BillingDate = row.BillingScheduledDate;
                bh.BilledDate = DateTime.Now;
                bh.BillingStatus = "SUCCESS";
                bh.Status = true;
                bh.CreatedDate = DateTime.Now;

                db.Subs_BillingHistory.Add(bh);
                //End

               
                //Subs_ContentDelivery

                var scg = (from sccfc in db.Subs_ServiceChannelContentForCountry
                           join sccc in db.Subs_ServiceChannelContentConfig on sccfc.ID equals sccc.ServiceChannelForCountryID
                           join cg in db.Subs_ContentGroups on sccc.ContentGroupID equals cg.ID
                           join cgc in db.Subs_ContentGroupConfig on cg.ID equals cgc.GroupID 
                           where sccfc.CountryID == countryid && sccfc.ServiceChannelID == channelid
                           && cg.Status == true && cgc.Status == true
                           && sccfc.Status == true && sccc.Status == true
                           && sccc.ContentType == cgc.ContentType && sccc.ContentOwnerID == cgc.ContentOwnerID
                           select new { sccc, g = cgc }
                         ).ToList();

              

                foreach (var c in scg)
                {
                    bool isSubcategoryExist = false;
                    if (c.sccc.SubCategoryID != null) isSubcategoryExist = true;                      

                    bool isCategoryExist = false;
                    if (c.sccc.CategoryID != null) isCategoryExist = true; 

                    bool isContentTypeExist = false;
                    if (c.sccc.ContentType != null) isContentTypeExist = true; 

                    bool isContentOwnerExist = false;
                    if (c.sccc.ContentOwnerID != null) isContentOwnerExist = true; 

                    bool isContentGroupExist = false;

                   
                        if (c.g.GroupID != null || c.g.GroupID != 0) //if GroupID exist then take data from Subs_ContentGroupConfig table
                        {
                            isContentGroupExist = true;

                            //checking the data's in the Sub_ContentGroupConfig Table if all subcategory,Category,Contenttype & ContentOwner isExist
                            bool isSubcategorygroupExist = false;
                            if (c.g.SubCategoryID != null) isSubcategorygroupExist = true; 

                            bool isCategorygroupExist = false;
                            if (c.g.CategoryID != null) isCategorygroupExist = true;   

                            bool isContentTypegroupExist = false;
                            if (c.g.ContentType != null) isContentTypegroupExist = true;  

                            bool isContentOwnergroupExist = false;
                            if (c.g.ContentOwnerID != null) isContentOwnergroupExist = true;
                            
                            contentdelivery = getcontentdelivery((int)scgfc.ChargeDurationType, (int)c.sccc.ContentDurationType, (int)c.sccc.ContentQuantity, (int)c.sccc.ContentDuration); //Checking the amouont of data to be delivered


                            var cd1 = db.Subs_ContentData.Where(x => (isSubcategorygroupExist == true ? x.SubCategoryID == c.sccc.SubCategoryID : x.SubCategoryID == null)
                                                   && (isCategorygroupExist == true ? x.CategoryID == c.sccc.CategoryID : x.CategoryID == null)
                                                   && (isContentOwnergroupExist == true ? x.OwnerID == c.sccc.ContentOwnerID : x.OwnerID == null)
                                                   && (isContentTypegroupExist == true ? x.ContentType == c.sccc.ContentType : x.ContentType == null)
                                                   && ((x.ScheduleDate >= datenow && x.ScheduleDate >= row.BillingScheduledDate) || x.ScheduleDate == null)
                                                   && x.ID > lid).OrderBy(x => x.ID).Take((int)contentdelivery).ToList();
                            if (cd1 != null)
                            {
                                foreach (var cid in cd1)
                                {
                                    //Subs_ContentDelivery
                                    Subs_ContentDelivery subcd = new Subs_ContentDelivery();
                                    subcd.UserID = userid;
                                    subcd.ServiceID = serid;
                                    subcd.ServiceChannelID = channelid;
                                    subcd.CountryID = countryid;
                                    subcd.ContentDataID = cid.ID;
                                    subcd.ContentScheduledDate = datenow;
                                    subcd.ContentDeliveryStatus = true;
                                    subcd.ContentDeliveryStatusDesc = "SUCCESS";
                                    subcd.ContentChannel = "APP";
                                    subcd.Status = true;
                                    subcd.CreatedDate = DateTime.Now;

                                    db.Subs_ContentDelivery.Add(subcd);
                                    //End
                                    datenow = datenow.AddDays(1);
                                }
                            }
                        }                    
                }
                // End

                db.SaveChanges();

                return true;
            }
            else
            {
                return false;
            }

        }

        public bool UnSubscribeUser(Int64 userid, int serid, int channelid, int countryid, int operatorid, bool checkuser)
        {

           // var row = db.Subs_Billing.Where(x => x.UserID == userid && x.ServiceID == serid && x.ServiceChannelID == channelid && x.CountryID == countryid && x.OperatorID == operatorid && x.Status == true).SingleOrDefault();
            var row = db.Subs_Billing.Where(x => x.UserID == userid && x.ServiceID == serid && x.ServiceChannelID == channelid && x.CountryID == countryid && x.Status == true).SingleOrDefault();
           
            var subscribeuser = db.Subs_UserServiceChannels.Where(x => x.UserID == userid && x.ServiceID == serid && x.ChannelID == channelid && x.CountryID == countryid && x.Status == true).SingleOrDefault();

            if (subscribeuser != null)
            {
                //Subs_UserServiceChannels
                var usc = db.Subs_UserServiceChannels.Where(x => x.UserID == userid && x.ServiceID == serid && x.ChannelID == channelid && x.CountryID == countryid && x.Status == true).SingleOrDefault();

                usc.Status = false;
                usc.UnsubscriptionDate = DateTime.Now;
                //End

                //Subs_UserSubscriptions
                Subs_UserSubscriptions us = new Subs_UserSubscriptions();
                us.UserID = userid;
                us.ServiceID = serid;
                us.ChannelID = channelid;
                us.CountryID = countryid;
                us.Status = true;
                us.Action = 0;
                us.ActionDescription = "UnSubscribe from iBand Application";
                us.ActionStatus = "SUCCESS";
                us.CreatedDate = DateTime.Now;

                db.Subs_UserSubscriptions.Add(us);
                //End

                // Subs_Billing

                row.Status = false;
                row.CreatedDate = DateTime.Now;
                //End


                db.SaveChanges();

                return true;

            }

            return false;
        }

        private List<Translations> getTranslations(List<string> texts)
        {
            List<Translations> list = new List<Translations>();
            foreach (string str in texts)
            {
                list.AddRange(this.getTranslations(str));
            }
            return list;
        }

        private List<Translations> getTranslations(string sourcetext)
        {
            List<Translations> list = new List<Translations>();
            return (from t in this.db.Translations
                    where t.SourceText.ToLower().Equals(sourcetext) && (t.Status == true)
                    orderby t.LanguageCode
                    select new Translations { languagecode = t.LanguageCode, sourcetext = t.SourceText, translatedtext = t.TranslatedText }).ToList<Translations>();
        }

        private int getcontentdelivery(int chargedurationtype, int contentdurationtype, int contentquantity, int contentduration)
        {
            var contentdelivery = 0;

            if (chargedurationtype == 1 && contentdurationtype == 1)
            {
                contentdelivery = (int)Math.Ceiling(1 / ((double)contentduration * 1)) * contentquantity;
            }
            else if (chargedurationtype == 1 && contentdurationtype == 2)
            {
                contentdelivery = (int)Math.Ceiling(1 / ((double)contentduration * 7)) * contentquantity;
            }
            else if (chargedurationtype == 1 && contentdurationtype == 3)
            {
                contentdelivery = (int)Math.Ceiling(1 / ((double)contentduration * 30)) * contentquantity;
            }
            else if (chargedurationtype == 2 && contentdurationtype == 1)
            {
                contentdelivery = (int)Math.Ceiling(7 / ((double)contentduration * 1)) * contentquantity;
            }
            else if (chargedurationtype == 2 && contentdurationtype == 2)
            {
                contentdelivery = (int)Math.Ceiling(7 / ((double)contentduration * 7)) * contentquantity;
            }
            else if (chargedurationtype == 2 && contentdurationtype == 3)
            {
                contentdelivery = (int)Math.Ceiling(7 / ((double)contentduration * 30)) * contentquantity;
            }
            else if (chargedurationtype == 3 && contentdurationtype == 1)
            {
                contentdelivery = (int)Math.Ceiling(30 / ((double)contentduration * 1)) * contentquantity;
            }
            else if (chargedurationtype == 3 && contentdurationtype == 2)
            {
                contentdelivery = (int)Math.Ceiling(30 / ((double)contentduration * 7)) * contentquantity;
            }
            else if (chargedurationtype == 3 && contentdurationtype == 3)
            {
                contentdelivery = (int)Math.Ceiling(30 / ((double)contentduration * 30)) * contentquantity;
            }
            return contentdelivery;
        }

        private bool IsUserSubscribed(int userid, int serviceid, int channelid, int countryid, bool check)
        {
            var row = db.Subs_UserSubscriptions.Where(x => x.UserID == userid && x.ServiceID == serviceid && x.ChannelID == channelid && x.CountryID == countryid && x.Status == true).SingleOrDefault();
            if (row != null)
            {
                return true;
            }
            return false;
        }

        public List<iBand.Models.Outputs.Subscription.PaymentChannelDTO> getPaymentChannels(int countryid, int serviceid, int operatorid, int servicetype)
        {

            List<iBand.Models.Outputs.Subscription.PaymentChannelDTO> PaymentChannels = new List<iBand.Models.Outputs.Subscription.PaymentChannelDTO>();

            var rows = (from bpc in db.BillingPaymentsConfigurations
                        join bp in db.BillingPayments on bpc.BillingPaymentID equals bp.ID
                        where bpc.CountryID == countryid && (bpc.OperatorID == null || bpc.OperatorID == operatorid) &&
                        (bpc.ServiceType == null || bpc.ServiceType == servicetype) && (bpc.ServiceID == null || bpc.ServiceID == serviceid)
                        select bp).AsEnumerable().Select(x => new Models.Outputs.Subscription.PaymentChannelDTO { PaymentID = x.ID.ToString(), PaymentType = x.PaymentType.Trim(), PaymentName = x.PaymentName, Status = x.Status.ToString()}).ToList();

            //var rows = (from bp in db.BillingPayments
            //            join bpc in db.BillingPaymentsConfigurations on bp.ID equals bpc.BillingPaymentID
            //            where bp.Status == true && ((bpc.Status == true && bpc.CountryID == countryid) ||
            //            (bpc.Status == true && bpc.ServiceID == serviceid) || (bpc.Status == true && bpc.OperatorID == operatorid) || (bpc.Status == true && bpc.ServiceType == servicetype))
            //            select new { bpc = bpc, bp = bp }
            //            ).OrderBy(x => x.bp.PaymentName).ToList().GroupBy(x => x.bp.ID).Select(x => x.First()).ToList();

            //PaymentChannels = rows.Select(x => new Models.Outputs.Subscription.PaymentChannelDTO
            //{
            //    PaymentID = x.bp.ID.ToString(),
            //    PaymentName = x.bp.PaymentName == null ? "" : x.bp.PaymentName,
            //    PaymentType = x.bp.PaymentType == null ? "" : x.bp.PaymentType,
            //}).ToList();

            return rows;
        }

        public List<iBand.Models.Outputs.Subscription.ServiceChannel> getrecentservicechannels(int countryid, int operatorid, Int64 userid)
        {

            //List<iBand.Models.Outputs.Subscription.ServiceChannel> servicechannel = new List<iBand.Models.Outputs.Subscription.ServiceChannel>();

            //var rows = (from sc in db.Subs_ServiceChannels
            //            join us in db.Subs_UserServiceChannels on sc.ID equals us.ChannelID into f
            //            from fc in f.DefaultIfEmpty().Distinct()
            //            join usc in db.Subs_ServiceChannelContentForCountry on sc.ID equals usc.ServiceChannelID
            //            join ser in db.Subs_Services on sc.ServiceID equals ser.ID
            //            where sc.Status == true && ((fc.UserID == userid || fc.UserID != userid) || fc == null)
            //            && usc.Status == true && usc.CountryID == countryid && ser.Status == true
            //            select new { usc = usc, sc = sc, ser = ser, fc = (fc == null ? null : fc) }
            //            ).OrderByDescending(x => x.sc.ID).ToList().GroupBy(x => x.sc.ID).Select(x => x.First()).ToList();
            //servicechannel = rows.Select(x => new Models.Outputs.Subscription.ServiceChannel
            //{
            //    ChannelID = x.sc.ID.ToString(),
            //    ChannelName = x.sc.ServiceChannelName == null ? "" : x.sc.ServiceChannelName,
            //    ChannelDesc = x.sc.ServiceChannelDesc == null ? "" : x.sc.ServiceChannelDesc,
            //    ImageURL = x.sc.Info1 == null ? "" : x.sc.Info1,
            //    ChannelCost = x.usc.Charge.ToString(),
            //    isSubscribed = ((x.fc == null) ? false : ((x.fc.UserID == userid && x.fc.Status == true) ? true : false)).ToString(),
            //    services = new Models.Outputs.Subscription.Service
            //    {
            //        ServiceID = x.ser.ID.ToString(),
            //        ServiceName = x.ser.ServiceName == null ? "" : x.ser.ServiceName,
            //        ServiceDesc = x.ser.ServiceDescription == null ? "" : x.ser.ServiceDescription,
            //        ImageURL = x.ser.Info1 == null ? "" : x.ser.Info1,
            //        translations = getTranslations(new List<string> { x.ser.ServiceName, x.ser.ServiceDescription })
            //    },
            //    translations = getTranslations(new List<string> { x.sc.ServiceChannelName, x.sc.ServiceChannelDesc })
            //}).ToList();

            //return servicechannel;

            List<iBand.Models.Outputs.Subscription.ServiceChannel> servicechannel = new List<iBand.Models.Outputs.Subscription.ServiceChannel>();

            var rows = (from sc in db.Subs_ServiceChannels
                        join usc in db.Subs_ServiceChannelContentForCountry on sc.ID equals usc.ServiceChannelID
                        join ser in db.Subs_Services on sc.ServiceID equals ser.ID
                        where sc.Status == true && usc.Status == true && usc.CountryID == countryid && ser.Status == true
                        select new { usc = usc, sc = sc, ser = ser }
                        ).OrderByDescending(x => x.sc.ID).ToList().GroupBy(x => x.sc.ID).Select(x => x.First()).ToList();
            servicechannel = rows.Select(x => new Models.Outputs.Subscription.ServiceChannel
            {
                ChannelID = x.sc.ID.ToString(),
                ChannelName = x.sc.ServiceChannelName == null ? "" : x.sc.ServiceChannelName,
                ChannelDesc = x.sc.ServiceChannelDesc == null ? "" : x.sc.ServiceChannelDesc,
                ImageURL = x.sc.Info1 == null ? "" : x.sc.Info1,
                ChannelCost = x.usc.Charge.ToString(),
                isSubscribed = ((db.Subs_UserServiceChannels.Where(a => a.UserID == userid && a.ChannelID == x.sc.ID && a.CountryID == countryid && a.Status == true).Any()) ? true : false).ToString(),
                services = new Models.Outputs.Subscription.Service
                {
                    ServiceID = x.ser.ID.ToString(),
                    ServiceName = x.ser.ServiceName == null ? "" : x.ser.ServiceName,
                    ServiceDesc = x.ser.ServiceDescription == null ? "" : x.ser.ServiceDescription,
                    ImageURL = x.ser.Info1 == null ? "" : x.ser.Info1,
                    translations = getTranslations(new List<string> { x.ser.ServiceName, x.ser.ServiceDescription })
                },
                paymentchannel = getPaymentChannels(countryid,x.sc.ID,operatorid,3),
                
                translations = getTranslations(new List<string> { x.sc.ServiceChannelName, x.sc.ServiceChannelDesc })
            }).ToList();

            return servicechannel;


        }

        public List<iBand.Models.Outputs.Subscription.ServiceChannel> getservicechannel(int countryid, Int64 userid)
        {

            List<iBand.Models.Outputs.Subscription.ServiceChannel> servicechannel = new List<iBand.Models.Outputs.Subscription.ServiceChannel>();

            var rows = (from sc in db.Subs_ServiceChannels
                        join usc in db.Subs_ServiceChannelContentForCountry on sc.ID equals usc.ServiceChannelID
                        join ser in db.Subs_Services on sc.ServiceID equals ser.ID
                        where sc.Status == true && usc.Status == true && usc.CountryID == countryid && ser.Status == true
                        select new { usc = usc, sc = sc, ser = ser }
                        ).OrderByDescending(x => x.sc.ServiceChannelName).ToList().GroupBy(x => x.sc.ID).Select(x => x.First()).ToList();
            servicechannel = rows.Select(x => new Models.Outputs.Subscription.ServiceChannel
            {
                ChannelID = x.sc.ID.ToString(),
                ChannelName = x.sc.ServiceChannelName == null ? "" : x.sc.ServiceChannelName,
                ChannelDesc = x.sc.ServiceChannelDesc == null ? "" : x.sc.ServiceChannelDesc,
                ImageURL = x.sc.Info1 == null ? "" : x.sc.Info1,
                ChannelCost = x.usc.Charge.ToString(),
                isSubscribed = ((db.Subs_UserServiceChannels.Where(a => a.UserID == userid && a.ChannelID == x.sc.ID && a.CountryID == countryid && a.Status == true).Any()) ? true : false).ToString(),
                services = new Models.Outputs.Subscription.Service
                {
                    ServiceID = x.ser.ID.ToString(),
                    ServiceName = x.ser.ServiceName == null ? "" : x.ser.ServiceName,
                    ServiceDesc = x.ser.ServiceDescription == null ? "" : x.ser.ServiceDescription,
                    ImageURL = x.ser.Info1 == null ? "" : x.ser.Info1,
                    translations = getTranslations(new List<string> { x.ser.ServiceName, x.ser.ServiceDescription })
                },
                translations = getTranslations(new List<string> { x.sc.ServiceChannelName, x.sc.ServiceChannelDesc })
            }).ToList();

            return servicechannel;
        }

        public List<iBand.Models.Outputs.Subscription.Category> getsubs_categories(int countryid)
        {

            List<iBand.Models.Outputs.Subscription.Category> subs_categories = new List<Models.Outputs.Subscription.Category>();

            subs_categories = (from scc in db.Subs_ServiceChannelContentConfig
                               join c in db.Categories on scc.CategoryID equals c.ID
                               where scc.Status == true && c.Status == true && scc.ServiceChannelForCountryID == countryid

                               select new { c = c }
                             ).OrderBy(x => x.c.CategoryName).AsEnumerable().Distinct()
                             .Select(x => new Models.Outputs.Subscription.Category
                             {
                                 CategoryID = x.c.ID.ToString(),
                                 CategoryName = x.c.CategoryName == null ? "" : x.c.CategoryName,
                                 ImageURL = x.c.Info1 == null ? "" : x.c.Info1,
                                 translations = getTranslations(new List<string> { x.c.CategoryName })
                             }).ToList();

            return subs_categories;
        }

        public List<ServiceChannel> getservicebycategory(int countryid, int categoryid, Int64 userid)
        {
            List<ServiceChannel> servicechannel = new List<ServiceChannel>();

            servicechannel = (from sc in db.Subs_ServiceChannels
                              join usc in db.Subs_ServiceChannelContentForCountry on sc.ID equals usc.ServiceChannelID
                              join ser in db.Subs_Services on sc.ServiceID equals ser.ID
                              join sccc in db.Subs_ServiceChannelContentConfig on usc.ID equals sccc.ServiceChannelForCountryID
                              where sc.Status == true && usc.Status == true && usc.CountryID == countryid && ser.Status == true && sccc.CategoryID == categoryid
                              select new { usc = usc, sc = sc, ser = ser }
                        ).OrderBy(x => x.sc.ServiceChannelName).AsEnumerable().Distinct()
                        .Select(x => new Models.Outputs.Subscription.ServiceChannel
                        {
                            ChannelID = x.sc.ID.ToString(),
                            ChannelName = x.sc.ServiceChannelName == null ? "" : x.sc.ServiceChannelName,
                            ChannelDesc = x.sc.ServiceChannelDesc == null ? "" : x.sc.ServiceChannelDesc,
                            ImageURL = x.sc.Info1 == null ? "" : x.sc.Info1,
                            ChannelCost = x.usc.Charge.ToString(),
                            isSubscribed = ((db.Subs_UserServiceChannels.Where(a => a.UserID == userid && a.ChannelID == x.sc.ID && a.CountryID == countryid && a.Status == true).Any()) ? true : false).ToString(),
                            services = new Models.Outputs.Subscription.Service
                            {
                                ServiceID = x.ser.ID.ToString(),
                                ServiceName = x.ser.ServiceName == null ? "" : x.ser.ServiceName,
                                ServiceDesc = x.ser.ServiceDescription == null ? "" : x.ser.ServiceDescription,
                                ImageURL = x.ser.Info1 == null ? "" : x.ser.Info1,
                                translations = getTranslations(new List<string> { x.ser.ServiceName, x.ser.ServiceDescription })

                            },
                            translations = getTranslations(new List<string> { x.sc.ServiceChannelName, x.sc.ServiceChannelDesc })
                        }).ToList();

            return servicechannel;
        }

        public List<iBand.Models.Outputs.Subscription.ContentView> getcontentview(int countryid, Int64 userid)
        {
            var rows = (from scd in db.Subs_ContentDelivery
                        join cd in db.Subs_ContentData on scd.ContentDataID equals cd.ID
                        where scd.UserID == userid && scd.CountryID == countryid
                        select new { cd = cd }
                     ).OrderByDescending(x => x.cd.ID).ToList()
                     .Select(x => new Models.Outputs.Subscription.ContentView
                     {
                         contentid = x.cd.ID.ToString(),
                         contentname = x.cd.ContentName == null ? "" : x.cd.ContentName,
                         contentdesc = x.cd.ContentDescription == null ? "" : x.cd.ContentDescription,
                         contenttype = x.cd.ContentType.ToString(),
                         contentdata = x.cd.ContentData == null ? "" : x.cd.ContentData,
                         translations = getTranslations(new List<string> { x.cd.ContentName, x.cd.ContentDescription })

                     }).ToList();

            return rows;
        }
        public List<iBand.Models.Outputs.Subscription.ContentView> getcontentviewbyservice(Int64 userid, int countryid, int channelid)
        {
            var rows = (from scd in db.Subs_ContentDelivery
                        join cd in db.Subs_ContentData on scd.ContentDataID equals cd.ID
                        where scd.UserID == userid && scd.CountryID == countryid && scd.ServiceChannelID == channelid
                        select new { cd = cd }
                     ).OrderByDescending(x => x.cd.ID).ToList()
                     .Select(x => new Models.Outputs.Subscription.ContentView
                     {
                         contentid = x.cd.ID.ToString(),
                         contentname = x.cd.ContentName == null ? "" : x.cd.ContentName,
                         contentdesc = x.cd.ContentDescription == null ? "" : x.cd.ContentDescription,
                         contenttype = x.cd.ContentType.ToString(),
                         contentdata = x.cd.ContentData == null ? "" : x.cd.ContentData,
                         translations = getTranslations(new List<string> { x.cd.ContentName, x.cd.ContentDescription })

                     }).ToList();

            return rows;
        }
        public List<channels> GetSubs_UserSubscribeOnChannels(Int64 userid, int countryid)
        {
            List<channels> channels = new List<channels>();

            channels = (from sc in db.Subs_ServiceChannels
                        join us in db.Subs_UserServiceChannels on sc.ID equals us.ChannelID into f
                        from fc in f.DefaultIfEmpty()
                        join usc in db.Subs_ServiceChannelContentForCountry on sc.ID equals usc.ServiceChannelID
                        join ser in db.Subs_Services on sc.ServiceID equals ser.ID
                        where sc.Status == true && fc.UserID == userid
                        && usc.Status == true && usc.CountryID == countryid && ser.Status == true
                        select new { sc = sc }
                        ).OrderBy(x => x.sc.ServiceChannelName).AsEnumerable().Distinct()
                        .Select(x => new channels
                        {
                            channelid = x.sc.ID.ToString(),
                            channelname = x.sc.ServiceChannelName == null ? "" : x.sc.ServiceChannelName,
                            channeldesc = x.sc.ServiceChannelDesc == null ? "" : x.sc.ServiceChannelDesc,
                            ImageURL = x.sc.Info1 == null ? "" : x.sc.Info1,
                            translations = getTranslations(new List<string> { x.sc.ServiceChannelName, x.sc.ServiceChannelDesc })
                        }).ToList();

            return channels;
        }

        public bool Interactiveservicecontestantvoting(long userid, int operatorid, int interactiveserid, int interactivesercontid, bool check)
        {
            InteractiveServiceContestantsVote iscv = new InteractiveServiceContestantsVote();
            iscv.UserID = userid;
            iscv.OperatorID = operatorid;
            iscv.InteractiveServiceID = interactiveserid;
            iscv.InteractiveServiceContestantID = interactivesercontid;
            iscv.Status = true;
            iscv.CreatedDate = DateTime.Now;
            db.InteractiveServiceContestantsVotes.Add(iscv);
            db.SaveChanges();

            return true;
        }

        public bool Interactiveserviceusermessage(long userid, int operatorid, int interactiveserid, string usermessage, bool check)
        {

            DAL.InteractiveServiceUserMessage isum = new DAL.InteractiveServiceUserMessage();
            isum.UserID = userid;
            isum.OperatorID = operatorid;
            isum.InteractiveServiceID = interactiveserid;
            isum.Message = usermessage;

            if (!isEnglish(usermessage) == true)
            {
                isum.Language = "ar";
            }
            else
            {
                isum.Language = "en";
            }
            isum.Status = true;
            isum.CreatedDate = DateTime.Now;
            db.InteractiveServiceUserMessages.Add(isum);
            db.SaveChanges();

            return true;
        }


        public bool isEnglish(string usermessage)
        {
            Regex regex = new Regex(@"[A-Za-z0-9 .,-=+(){}\[\]\\]");
            MatchCollection matches = regex.Matches(usermessage);

            if (matches.Count.Equals(usermessage.Length))
                return true;
            else
                return false;
        }

        //private bool DOB(string mobile, string amount, string ServiceChannelName)
        //{

        //    string username = "";
        //    string password = "";


        //    Models.DOBService.Service1Client serv = new Models.DOBService.Service1Client();

        //    Models.DOBService.SubscribeInfo subs = new Models.DOBService.SubscribeInfo();
        //    subs.amount = Convert.ToDouble(amount);
        //    subs.msisdn = mobile;
        //    subs.hash = ComputeHash(mobile, ServiceChannelName);
        //    subs.password = password;
        //    subs.username = username;
        //    subs.service = ServiceChannelName;
        //    subs.pin = "";



        //    string resp = serv.createSubscription(subs);

        //    if (string.IsNullOrEmpty(resp) || resp.Equals("failed "))
        //    {
        //        return false;
        //    }
        //    return true;
        //}

        //private string ComputeHash(string mobile, string serviceChannelName)
        //{

        //    string username = "isystest";
        //    string password = "isys969";

        //    string dataToComputeHash = username + "username" + password + "password" + mobile + "msisdn"
        //                      + serviceChannelName + "service";

        //    string master_key_Merchant = "rdxO6KJBEcxMH5EnrFnzwqlzXEx2G196";
        //    string master_IV_merchant = "Mru5f0Qvw51Z2v2M";

        //    Aes myAes_Org = Aes.Create();
        //    myAes_Org.Key = System.Text.Encoding.UTF8.GetBytes(master_key_Merchant);
        //    myAes_Org.IV = System.Text.Encoding.UTF8.GetBytes(master_IV_merchant);

        //    string decryptedOriginal = Utilities.Hash.DecryptStringFromBytes_Aes(Convert.FromBase64String("123456abcdef"), myAes_Org.Key, myAes_Org.IV);
        //    HMACSHA256 hmac = new HMACSHA256(System.Text.Encoding.UTF8.GetBytes(decryptedOriginal));

        //    string computedHash = Utilities.Hash.ByteToString(hmac.ComputeHash(System.Text.UTF8Encoding.Default.GetBytes(dataToComputeHash)));

        //    return computedHash;
        //}

        public static string ByteToString(byte[] buff)
        {
            string sbinary = "";

            for (int i = 0; i < buff.Length; i++)
            {
                sbinary += buff[i].ToString("X2"); // hex format
            }
            return (sbinary);
        }

        #endregion
    }
}
