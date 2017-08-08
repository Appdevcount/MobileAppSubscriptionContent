using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using iBand.API;
using iBand.API.Controllers;
using iBand.Models;
using iBand.BL.Implementations;
using iBand.BL.Interfaces;


namespace iBand.API.Tests.Controllers
{
    [TestClass]
    public class JSONControllerTests
    {
        [TestMethod]
        public void GetRecentRBTsTest()
        {
            JSONController jsoncontroller = new JSONController();
            Input<Models.Inputs.RecentRBTs> rbts = new Input<Models.Inputs.RecentRBTs>();
            Models.Inputs.RecentRBTs rbt = new Models.Inputs.RecentRBTs();
            rbt.lastid = "0";
            rbt.operatorid = "1";
            rbt.count = "20";
            rbts.input = rbt;

            Models.CommonInputParams param = new CommonInputParams();
            param.company = "iSYS";
            param.deviceid = "123123123";
            param.password = "iSYS";
            param.username = "iSYS";

            rbts.param = param;
            var result = jsoncontroller.GetRecentRBTs(rbts);



            Assert.IsNotNull(result);
            Assert.AreEqual("0", result.status.statuscode);
            Assert.AreNotEqual(result.response.rbts.Count, 0);
        }

        [TestMethod]
        public void GetCategories()
        {
            JSONController jsoncontroller = new JSONController();
            Input<Models.Inputs.RBTCategories> rbts = new Input<Models.Inputs.RBTCategories>();
            Models.Inputs.RBTCategories rbt = new Models.Inputs.RBTCategories();
            rbt.operatorid = "1";
            rbts.input = rbt;

            Models.CommonInputParams param = new CommonInputParams();
            param.company = "iSYS";
            param.deviceid = "123123123";
            param.password = "iSYS";
            param.username = "iSYS";

            rbts.param = param;
            var result = jsoncontroller.GetCategoriesRBT(rbts);



            Assert.IsNotNull(result);
            Assert.AreEqual("0", result.status.statuscode);
            Assert.AreNotEqual(result.response.categories.Count, 0);

        }

        [TestMethod]
        public void GetAlbumsForCategory()
        {
            JSONController jsoncontroller = new JSONController();
            Input<Models.Inputs.AlbumsForCategory> rbts = new Input<Models.Inputs.AlbumsForCategory>();
            Models.Inputs.AlbumsForCategory rbt = new Models.Inputs.AlbumsForCategory();
            rbt.operatorid = "1";
            rbt.categoryid = "1";
            rbts.input = rbt;

            Models.CommonInputParams param = new CommonInputParams();
            param.company = "iSYS";
            param.deviceid = "123123123";
            param.password = "iSYS";
            param.username = "iSYS";

            rbts.param = param;
            var result = jsoncontroller.GetAlbumsForCategory(rbts);



            Assert.IsNotNull(result);
            Assert.AreEqual("0", result.status.statuscode);
            Assert.AreNotEqual(result.response.albums.Count, 0);

        }

        [TestMethod]
        public void GetRBTsForAlbum()
        {
            JSONController jsoncontroller = new JSONController();
            Input<Models.Inputs.RBTsForAlbums> rbts = new Input<Models.Inputs.RBTsForAlbums>();
            Models.Inputs.RBTsForAlbums rbt = new Models.Inputs.RBTsForAlbums();
            rbt.operatorid = "1";
            rbt.albumid = "4";
            //rbt.artistid = "1";
            rbts.input = rbt;

            Models.CommonInputParams param = new CommonInputParams();
            param.company = "iSYS";
            param.deviceid = "123123123";
            param.password = "iSYS";
            param.username = "iSYS";

            rbts.param = param;
            var result = jsoncontroller.GetRBTsForAlbums(rbts);



            Assert.IsNotNull(result);
            Assert.AreEqual("0", result.status.statuscode);
            Assert.AreNotEqual(result.response.RBTs.Count, 0);

        }

        [TestMethod]
        public void GetArtists()
        {
            JSONController jsoncontroller = new JSONController();
            Input<Models.Inputs.RBTArtists> rbts = new Input<Models.Inputs.RBTArtists>();
            Models.Inputs.RBTArtists rbt = new Models.Inputs.RBTArtists();
            rbt.operatorid = "1";

            rbts.input = rbt;

            Models.CommonInputParams param = new CommonInputParams();
            param.company = "iSYS";
            param.deviceid = "123123123";
            param.password = "iSYS";
            param.username = "iSYS";

            rbts.param = param;
            var result = jsoncontroller.GetArtists(rbts);



            Assert.IsNotNull(result);
            Assert.AreEqual("0", result.status.statuscode);
            Assert.AreNotEqual(result.response.artists.Count, 0);

        }

        [TestMethod]
        public void GetAlbumsForArtists()
        {
            JSONController jsoncontroller = new JSONController();
            Input<Models.Inputs.RBTAlbumsForArtists> rbts = new Input<Models.Inputs.RBTAlbumsForArtists>();
            Models.Inputs.RBTAlbumsForArtists rbt = new Models.Inputs.RBTAlbumsForArtists();
            rbt.operatorid = "1";
            rbt.artistid = "1";
            rbts.input = rbt;

            Models.CommonInputParams param = new CommonInputParams();
            param.company = "iSYS";
            param.deviceid = "123123123";
            param.password = "iSYS";
            param.username = "iSYS";

            rbts.param = param;
            var result = jsoncontroller.GetAlbumsForArtist(rbts);



            Assert.IsNotNull(result);
            Assert.AreEqual("0", result.status.statuscode);
            Assert.AreNotEqual(result.response.albums.Count, 0);

        }

        [TestMethod]
        public void GetRBTsForAlbumArtists()
        {
            JSONController jsoncontroller = new JSONController();
            Input<Models.Inputs.RBTsForAlbums> rbts = new Input<Models.Inputs.RBTsForAlbums>();
            Models.Inputs.RBTsForAlbums rbt = new Models.Inputs.RBTsForAlbums();
            rbt.operatorid = "1";
            rbt.albumid = "4";
            rbt.artistid = "1";
            rbts.input = rbt;

            Models.CommonInputParams param = new CommonInputParams();
            param.company = "iSYS";
            param.deviceid = "123123123";
            param.password = "iSYS";
            param.username = "iSYS";

            rbts.param = param;
            var result = jsoncontroller.GetRBTsForAlbums(rbts);



            Assert.IsNotNull(result);
            Assert.AreEqual("0", result.status.statuscode);
            Assert.AreNotEqual(result.response.RBTs.Count, 0);

        }


        [TestMethod]
        public void GetServices(int catID, int count)
        {

            ServicesBL bl = new ServicesBL();

            var rows = bl.GetServices(1, catID, 50, 1, 0);

            Assert.AreEqual(count, rows.Count);

            //   var rows1 = bl.GetServices(1, 1, 1, Convert.ToInt32(rows[rows.Count -1].serviceid));

            // Assert.AreNotEqual(null, rows1);

        }

        [TestMethod]
        public void GetServiceCategores()
        {

            ServicesBL bl = new ServicesBL();

            var rows = bl.GetServiceCategories(1);

            Assert.AreNotEqual(null, rows);

            this.GetServices(Convert.ToInt32(rows[0].categoryid), Convert.ToInt32(rows[0].albumcount));



        }

        [TestMethod]
        public void SetFavourite()
        {

            ServicesBL bl = new ServicesBL();
            bl.setFavourite(1, 1, "rbt");

            bl.setFavourite(1, 1, "service");


        }
        [TestMethod]
        public void GetFavourite()
        {
            ServicesBL bl = new ServicesBL();
            var rows = bl.GetFavouritesOfUser(1, 1);
            Assert.AreNotEqual(null, rows);

        }

        [TestMethod]
        public void GetServices()
        {
            SubscriptionBL bl = new SubscriptionBL();
            var rows = bl.GetServices(1);
            Assert.AreNotEqual(null, rows);

        }
        [TestMethod]
        public void GetServiceChannels()
        {
            SubscriptionBL bl = new SubscriptionBL();
            var rows = bl.getservicechannels(1, 1);
            Assert.AreNotEqual(null, rows);

        }
        [TestMethod]
        public void SubscribeUserOnChannel()
        {
            SubscriptionBL bl = new SubscriptionBL();
            SubscriptionController subscriptioncontroller = new SubscriptionController();

            Input<Models.Inputs.Subscription.SubscribeUserChannel> sucs = new Input<Models.Inputs.Subscription.SubscribeUserChannel>();
            Models.Inputs.Subscription.SubscribeUserChannel suc = new Models.Inputs.Subscription.SubscribeUserChannel();
            suc.userID = "2";
            suc.serviceID = "1";
            suc.operatorID = "1";
            suc.channelID = "1";
            suc.countryID = "1";
            sucs.input = suc;


            Models.CommonInputParams param = new CommonInputParams();
            param.company = "iSYS";
            param.deviceid = "123123123";
            param.password = "iSYS";
            param.username = "iSYS";

            sucs.param = param;
            var result = subscriptioncontroller.SubscribeUserOnChannel(sucs);



            Assert.IsNotNull(result);
            Assert.AreEqual("0", result.status.statuscode);

        }
        [TestMethod]
        public void UnSubscribeUserOnChannel()
        {
            SubscriptionBL bl = new SubscriptionBL();
            SubscriptionController subscriptioncontroller = new SubscriptionController();

            Input<Models.Inputs.Subscription.UnSubscribeUserChannel> usucs = new Input<Models.Inputs.Subscription.UnSubscribeUserChannel>();
            Models.Inputs.Subscription.UnSubscribeUserChannel usuc = new Models.Inputs.Subscription.UnSubscribeUserChannel();
            usuc.userID = "1";
            usuc.serviceID = "1";
            usuc.operatorID = "1";
            usuc.channelID = "1";
            usuc.countryID = "1";
            usucs.input = usuc;

            Models.CommonInputParams param = new CommonInputParams();
            param.company = "iSYS";
            param.deviceid = "123123123";
            param.password = "iSYS";
            param.username = "iSYS";

            usucs.param = param;

            var result = subscriptioncontroller.UnSubscribeUserOnChannel(usucs);
            Assert.IsNotNull(result);
            Assert.AreEqual("0", result.status.statuscode);
        }

        [TestMethod]
        public void GetUserSubscribeOnChannel()
        {
            SubscriptionBL bl = new SubscriptionBL();
            var rows = bl.GetUserSubscribeOnChannels(1, 1, 1);
            Assert.AreNotEqual(null, rows);

        }
        [TestMethod]
        public void GetUserChannelContentData()
        {
            SubscriptionBL bl = new SubscriptionBL();
           // var rows = bl.GetUserChannelContentData(1, 1, 1, 1);
           // Assert.AreNotEqual(null, rows);

        }

        [TestMethod]
        public void GetUserServiceChannelDetails()
        {
            SubscriptionBL bl = new SubscriptionBL();
            var rows = bl.GetUserServiceChannelDetail(1, 1, 1);
            Assert.AreNotEqual(null, rows);

        }
        [TestMethod]
        public void GetSubs_RecentServiceChannels()
        {
            SubscriptionBL bl = new SubscriptionBL();
            Int64 val = 5623445;
          //  var rows = bl.getrecentservicechannels(1, val);
           // Assert.AreNotEqual(null, rows);

        }
        [TestMethod]
        public void GetSubs_ServiceChannel()
        {
            SubscriptionBL bl = new SubscriptionBL();
            Int64 val = 5623445;
            var rows = bl.getservicechannel(1, val);
            Assert.AreNotEqual(null, rows);

        }
        [TestMethod]
        public void GetSubs_Categories()
        {
            SubscriptionBL bl = new SubscriptionBL();
            var rows = bl.getsubs_categories(1);
            Assert.AreNotEqual(null, rows);

        }
        [TestMethod]
        public void GetSubs_ServiceByCategory()
        {
            SubscriptionBL bl = new SubscriptionBL();
            Int64 val = 5623445;
            var rows = bl.getservicebycategory(1, 1, val);
            Assert.AreNotEqual(null, rows);

        }
        [TestMethod]
        public void GetUserSubs_AllContentView()
        {
            SubscriptionBL bl = new SubscriptionBL();
            Int64 val = 5623445;
            var rows = bl.getcontentview(1, val);
            Assert.AreNotEqual(null, rows);

        }
        [TestMethod]
        public void GetUserSubs_ContentViewbyService()
        {
            SubscriptionBL bl = new SubscriptionBL();
            Int64 val = 5623445;
            var rows = bl.getcontentviewbyservice(val, 1, 1);
            Assert.AreNotEqual(null, rows);

        }
        [TestMethod]
        public void GetSubs_UserSubcribeChannels()
        {
            SubscriptionBL bl = new SubscriptionBL();
            Int64 val = 5623445;
            var rows = bl.GetSubs_UserSubscribeOnChannels(val, 1);
            Assert.AreNotEqual(null, rows);

        }
        [TestMethod]
        public void InteractiveServiceContestantVote()
        {
            SubscriptionBL bl = new SubscriptionBL();
            SubscriptionController subscriptioncontroller = new SubscriptionController();

            Input<Models.Inputs.Subscription.InteractiveServiceContestantVote> usucs = new Input<Models.Inputs.Subscription.InteractiveServiceContestantVote>();
            Models.Inputs.Subscription.InteractiveServiceContestantVote usuc = new Models.Inputs.Subscription.InteractiveServiceContestantVote();
            usuc.userid = "56454561";
            usuc.interactiveserviceid = "1";
            usuc.interactivecontestantid = "1";
            usuc.operatorid = "1";
           
            usucs.input = usuc;

            Models.CommonInputParams param = new CommonInputParams();
            param.company = "iSYS";
            param.deviceid = "123123123";
            param.password = "iSYS";
            param.username = "iSYS";

            usucs.param = param;

            var result = subscriptioncontroller.InteractiveServiceContestantVote(usucs);
            Assert.IsNotNull(result);
            Assert.AreEqual("0", result.status.statuscode);
        }

        [TestMethod]
        public void InteractiveServiceUserMessage()
        {
            SubscriptionBL bl = new SubscriptionBL();
            SubscriptionController subscriptioncontroller = new SubscriptionController();

            Input<Models.Inputs.Subscription.Interactiveserviceusermsg> usucs = new Input<Models.Inputs.Subscription.Interactiveserviceusermsg>();
            Models.Inputs.Subscription.Interactiveserviceusermsg usuc = new Models.Inputs.Subscription.Interactiveserviceusermsg();
            usuc.userid = "56454561";
            usuc.interactiveserviceid = "1";
            usuc.usermessage = "hello";
            usuc.operatorid = "1";

            usucs.input = usuc;

            Models.CommonInputParams param = new CommonInputParams();
            param.company = "iSYS";
            param.deviceid = "123123123";
            param.password = "iSYS";
            param.username = "iSYS";

            usucs.param = param;

            var result = subscriptioncontroller.InteractiveServiceUserMessage(usucs);
            Assert.IsNotNull(result);
            Assert.AreEqual("0", result.status.statuscode);
        }

    }
}
