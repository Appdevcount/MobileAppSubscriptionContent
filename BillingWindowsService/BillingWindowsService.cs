using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using iBand.BL;
using iBand.DAL;
using iBand.Models.Outputs.WalletOutputs;
using iBand.Models.Inputs.WalletInputs;
using iBand.Models;

namespace BillingWindowsService
{
    public class BillingWindowsService
    {
        public iBandEntities db = new iBandEntities();

        public void GetUserBillingForToday()
        {
            try
            {
                List<Subs_Billing> rows = db.Subs_Billing.Where(x => x.Status == true && x.BillingScheduledDate == DateTime.Now && 
                (x.BillingStatus.Equals(STATUSDESC.SUCCESS) || x.BillingStatus.Equals(STATUSDESC.FAILEDDUETOINSUFFICIENTFUNDS))).ToList();

                LogClass.writeLog("Rows Count = " + rows.Count.ToString());

                foreach (Subs_Billing row in rows)
                {
                    row.BillingStatus = STATUSDESC.PROCESSING;
                }


                db.SaveChanges();
                BillAndDeliver(rows);
        
            }
            catch (Exception ex)
            {
                LogClass.writeLog("GetUserBillingForToday : " + ex.Message.ToString());
            }
        }

        public void BillAndDeliver(List<Subs_Billing> rows)
        {
            foreach (var row in rows)
            {
                DebitTransaction(row);

            }
        }

        public void DebitTransaction(Subs_Billing sb)
        {
            iBand.BL.Implementations.Wallet wallet = new iBand.BL.Implementations.Wallet();
            UserWallet uw = new UserWallet();

            iBand.Models.Input<DebitTransactions> input = new iBand.Models.Input<DebitTransactions>();
            CommonInputParams com = new CommonInputParams();
            DebitTransactions dt = new DebitTransactions();

            dt.userid = uw.UserID.ToString();
            dt.email = uw.Email;
            dt.mobile = uw.Mobile;
            dt.amount = sb.Amount.ToString();
            dt.currency = uw.Currency;
            dt.paymentservice = uw.PaymentService;

            com.apppassword = uw.AppPassword;
            com.appusername = uw.AppUsername;
            com.CountryCode = uw.CountryCode;
            com.deviceid = uw.DeviceUniqueID;
            com.deviceuniqueid = uw.DeviceUniqueID_ID;
            com.email = uw.Email;
            com.mobile = uw.Mobile;
            com.os = uw.DeviceOS;
            com.password = uw.Password;
            com.udid = uw.DeviceUniqueID;
            com.udid_id = uw.DeviceUniqueID_ID;
            com.userid = uw.UserID.ToString();
            com.username = uw.Username;

            input.input = dt;
            input.param = com;


            var resp = wallet.DebitTransaction(input);

            if (resp.status.Equals(0))
            {
                DeliverContentData(sb);
            }
            else if (resp.status.Equals(1090))
            {
                sb.BillingScheduledDate = DateTime.Now.AddDays(1);
                sb.BillingStatus = STATUSDESC.FAILEDDUETOINSUFFICIENTFUNDS;
                db.SaveChanges();
            }

        }
        public void DeliverContentData(Subs_Billing sb)
        {

            var datenow = DateTime.Now;
            var contentdelivery = 0;  // ContentDelivery is to add no. of rows in Subs_ContentDelivery table to delivery the contentdata
            int lid = 0;



            var scgfc = db.Subs_ServiceChannelContentForCountry.Where(x => x.CountryID == sb.CountryID && x.ServiceChannelID == sb.ServiceChannelID && x.Status == true).SingleOrDefault();
            var lastid = db.Subs_ContentDelivery.Where(x => x.UserID == sb.UserID && x.ServiceID == sb.ServiceID && x.ServiceChannelID == sb.ServiceChannelID && x.CountryID == sb.CountryID && x.Status == true).OrderByDescending(x => x.ContentDataID).FirstOrDefault();
            if (lastid != null)
            {
                lid = Convert.ToInt32(lastid.ContentDataID);
            }


            // Subs_UserSubscriptions
            Subs_UserSubscriptions us = new Subs_UserSubscriptions();
            us.UserID = sb.UserID;
            us.ServiceID = sb.ServiceID;
            us.ChannelID = sb.ServiceChannelID;
            us.CountryID = sb.CountryID;
            us.Status = true;
            us.Action = 1;
            us.ActionDescription = "Subscribe from iBand Application";
            us.ActionStatus = "SUCCESS";
            us.CreatedDate = DateTime.Now;

            db.Subs_UserSubscriptions.Add(us);
            //  End

            //  Subs_UserServiceChannels
            Subs_UserServiceChannels suc = new Subs_UserServiceChannels();
            suc.UserID = sb.UserID;
            suc.ServiceID = sb.ServiceID;
            suc.ChannelID = sb.ServiceChannelID;
            suc.CountryID = sb.CountryID;
            suc.Status = true;
            suc.SubscriptionDate = DateTime.Now;
            suc.CreatedDate = DateTime.Now;

            db.Subs_UserServiceChannels.Add(suc);
            //   End

            //   Subs_Billing
            Subs_Billing subbill = new Subs_Billing();
            subbill.UserID = sb.UserID; ;
            subbill.ServiceID = sb.ServiceID;
            subbill.CountryID = sb.CountryID;
            subbill.ServiceChannelID = sb.ServiceChannelID;
            subbill.OperatorID = sb.OperatorID;
            subbill.Amount = sb.Amount;
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
            bh.UserID = sb.UserID;
            bh.ServiceID = sb.ServiceID;
            bh.OperatorID = sb.OperatorID;
            bh.CountryID = sb.CountryID;
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
                       join sc in db.Subs_ServiceChannels on sccfc.ServiceChannelID equals sc.ID
                       where sccfc.CountryID == sb.CountryID && sccfc.ServiceChannelID == sb.ServiceChannelID
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
                                               && ((x.ScheduleDate >= datenow && x.ScheduleDate >= subbill.BillingScheduledDate) || x.ScheduleDate == null)
                                               && x.ID > lid).OrderBy(x => x.ID).Take((int)contentdelivery).ToList();
                        if (cd1 != null)
                        {
                            foreach (var cid in cd1)
                            {
                                //Subs_ContentDelivery
                                Subs_ContentDelivery subcd = new Subs_ContentDelivery();
                                subcd.UserID = sb.UserID;
                                subcd.ServiceID = sb.ServiceID;
                                subcd.ServiceChannelID = sb.ServiceChannelID;
                                subcd.CountryID = sb.CountryID;
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
                        subcd.UserID = sb.UserID;
                        subcd.ServiceID = sb.ServiceID;
                        subcd.ServiceChannelID = sb.ServiceChannelID;
                        subcd.CountryID = sb.CountryID;
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

        public static class STATUSDESC
        {

            public static string PENDING = "PENDING";
            public static string FAILED = "FAILED";
            public static string SUCCESS = "SUCCESS";
            public static string PROCESSING = "PROCESSING";
            public static string REPROCESSING = "REPROCESSING";
            public static string FAILEDDUETOINSUFFICIENTFUNDS = "FAILED DUE TO INSUFFICIENT FUNDS";


        }

    }
}
