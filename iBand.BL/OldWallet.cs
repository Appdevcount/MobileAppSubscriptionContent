using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using iBand.Models.Inputs.WalletInputs;
using iBand.Models.Outputs.WalletOutputs;
using iBand.DAL;

namespace iBand.BL
{
    public class OldWallet
    {
        PayitMerchantsnWalletsEntities db = new PayitMerchantsnWalletsEntities();

        public static void verifyOld(String mobile, String deviceUDID)
        {
            PayitMerchantsnWalletsEntities db = new PayitMerchantsnWalletsEntities();
            var Customer = (from a in db.Customers
                            join b in db.CustomerDevices on a.ID equals b.CustomerID
                            where a.MobileNo == mobile && b.DeviceUDID == deviceUDID
                            select new { a, b }).FirstOrDefault();
            if (Customer != null)
            {
                Customer.a.MobileActivated = true;
                Customer.b.Status = true;
                Customer.a.Status = true;
                db.SaveChanges();
            }
        }
        public static void RegisterOld(Models.Input<WalletProfile> obj)
        {
            PayitMerchantsnWalletsEntities db = new PayitMerchantsnWalletsEntities();
            var isWalletExist = (from a in db.Customers
                                 join b in db.CustomerDevices on a.ID equals b.CustomerID
                                 where a.MobileNo == obj.input.mobileno && b.DeviceUDID == obj.param.deviceuniqueid
                                 select a).FirstOrDefault();
            if (isWalletExist == null)
            {
                Customer cus = new Customer();

                cus.CountryCode = string.IsNullOrEmpty(obj.input.countrycode) ? "" : obj.input.countrycode;
                cus.CustomerCode = string.IsNullOrEmpty(obj.input.userid) ? "" : obj.input.userid;
                cus.Password = string.IsNullOrEmpty(obj.input.password) ? "" : obj.input.password;
                cus.MobileNo = string.IsNullOrEmpty(obj.input.mobileno) ? "" : obj.input.mobileno;
                cus.MobileActivated = false;
                cus.OTP = 0;
                cus.Email = string.IsNullOrEmpty(obj.input.email) ? "" : obj.input.email;
                cus.EmailActivated = false;
                //cus.AppIdentifier="";
                cus.TranDate = DateTime.Now;
                cus.Status = false;
                db.Customers.Add(cus);
                db.SaveChanges();


                var CID = cus.ID;

                CustomerDevice CusD = new CustomerDevice();

                CusD.CustomerID = CID;
                CusD.DeviceUDID = obj.param.deviceuniqueid;
                CusD.Status = false;
                CusD.CreatedDae = DateTime.Now;
                db.CustomerDevices.Add(CusD);
                db.SaveChanges();

                var DeviceID = CusD.ID;

                CustomerWallet CusW = new CustomerWallet();
                CusW.CustomerID = CID;
                CusW.Balance = 0;
                CusW.CreatedDate = DateTime.Now;
                CusW.Status = true;

                db.CustomerWallets.Add(CusW);
                db.SaveChanges();

                CustomerOTP CusO = new CustomerOTP();
                CusO.CusomerID = CID;
                CusO.DeviceID = DeviceID;
                CusO.WalletServiceID = 1;
                CusO.OTP = "";
                CusO.CreatedDate = DateTime.Now;
                CusO.Status = true;
                CusO.isUsed = false;
                db.CustomerOTPs.Add(CusO);
                db.SaveChanges();

            }
        }
        public static void CreditTransactionOld(Models.Input<CreditTransactions> obj)
        {

            PayitMerchantsnWalletsEntities db = new PayitMerchantsnWalletsEntities();

            var CustDeatils = (from a in db.Customers
                               join b in db.CustomerDevices on a.ID equals b.CustomerID
                               join c in db.CustomerWallets on a.ID equals c.CustomerID
                               where a.MobileNo == obj.input.mobile && b.DeviceUDID == obj.param.deviceuniqueid
                               select c.ID).FirstOrDefault();
            if (CustDeatils != null)
            {
                CustomerWalletTransaction CusWL = new CustomerWalletTransaction();

                CusWL.CustomerWalletID = CustDeatils;
                CusWL.Amount = string.IsNullOrEmpty(obj.input.amount) ? 0 : Convert.ToDouble(obj.input.amount);
                CusWL.ThirdPartyID = 1;
                CusWL.Reference = obj.input.paymentref;
                CusWL.TransactionType = "CREDIT";
                CusWL.TransactionDate = DateTime.Now;
                CusWL.Status = true;

                db.CustomerWalletTransactions.Add(CusWL);
                db.SaveChanges();
                var walletTranID = CusWL.ID;
                CustomerWalletTransactionsCredit CusWC = new CustomerWalletTransactionsCredit();

                CusWC.WalletTransactionID = walletTranID;
                CusWC.CreditChannel = "OneInWallet";
                CusWC.CreditChannelReference = obj.input.paymentref;
                CusWC.Status = true;
                CusWC.TransactionDate = DateTime.Now;
                CusWC.Description = "CAPTURED";

                db.CustomerWalletTransactionsCredits.Add(CusWC);
                db.SaveChanges();

            }


        }
        CustomerWalletTransaction CusWL = new CustomerWalletTransaction();

        public int DebitTransactionOld(Models.Input<DebitTransactions> obj)
        {
            PayitMerchantsnWalletsEntities db = new PayitMerchantsnWalletsEntities();

            CustomerWalletTransactionsDebit CusWC = new CustomerWalletTransactionsDebit();

            int status = 1;
            var CustDeatils = (from a in db.Customers
                               join c in db.CustomerWallets on a.ID equals c.CustomerID
                               where a.MobileNo == obj.input.mobile
                               select c.ID).FirstOrDefault();
            try
            {
                if (CustDeatils != 0)
                {
                    CusWL.CustomerWalletID = CustDeatils;
                    CusWL.Amount = string.IsNullOrEmpty(obj.input.amount) ? 0 : Convert.ToDouble(obj.input.amount);
                    CusWL.ThirdPartyID = 1;
                    CusWL.Reference = obj.input.paymentref;
                    CusWL.TransactionType = "DEBIT";
                    CusWL.TransactionDate = DateTime.Now;
                    CusWL.Status = true;

                    db.CustomerWalletTransactions.Add(CusWL);
                    db.SaveChanges();
                    var walletTranID = CusWL.ID;

                    CusWC.WalletTransactionID = walletTranID;
                    CusWC.DebitServiceName = "OneInWallet";
                    CusWC.DebitReference = obj.input.paymentref;
                    CusWC.Status = true;
                    CusWC.TransactionDate = DateTime.Now;
                    CusWC.Description = "PAID";

                    db.CustomerWalletTransactionsDebits.Add(CusWC);
                    db.SaveChanges();
                    status = 0;
                }
            }
            catch
            {
                status = 1;
            }
            return status;
        }

        //public MerchantDTO MerchantTransaction(Models.Input<QRPayRequest> obj, string refer, string code)
        //{
        //    PayitMerchantsnWalletsEntities db = new PayitMerchantsnWalletsEntities();
        //    Merchant merchant = new Merchant();
        //    MerchantDTO dto = new MerchantDTO();
        //    MerchantOrder order = new MerchantOrder();
        //    MerchantTransaction merchantTran = new MerchantTransaction();
        //    CustomerWalletTransaction CusWL = new CustomerWalletTransaction();
        //    var mDetails = (from a in db.Merchants
        //                    where a.Name == obj.input.QR.name && a.Code == code
        //                    select a).FirstOrDefault();
        //    try
        //    {
        //        if (mDetails != null)
        //        {
        //            merchantTran.Amount = string.IsNullOrEmpty(obj.input.amount) ? 0 : Convert.ToDouble(obj.input.amount);
        //            merchantTran.MerchantID = mDetails.ID;
        //            merchantTran.Reference = refer;
        //            merchantTran.Status = true;
        //            merchantTran.TransactionDate = DateTime.Now;
        //            db.MerchantTransactions.Add(merchantTran);
        //            db.SaveChanges();

        //            order.Amount = merchantTran.Amount;
        //            order.MerchantUserID = Convert.ToInt32(obj.input.QR.merchantuserid);
        //            order.Description = "QRPAYMENT";
        //            order.isPaid = true;
        //            //order.MerchantUserID = 
        //            order.MobileNo = mDetails.PhoneNo;
        //            order.PaymentReference = merchantTran.Reference;
        //            order.ProcessDate = DateTime.Now;
        //            order.Reference = merchantTran.Reference;
        //            order.Status = true;
        //            order.StatusDescription = "WalletPay";
        //            order.TranDate = DateTime.Now;
        //            db.MerchantOrders.Add(order);
        //            db.SaveChanges();

        //            merchant.Amount = mDetails.Amount + merchantTran.Amount;
        //            db.SaveChanges();
        //            dto.Amount = mDetails.Amount.ToString();
        //            dto.Code = mDetails.Code.ToString();
        //            dto.CountryCode = mDetails.CountryCode;
        //            dto.Name = mDetails.Name;
        //        }
        //    }
        //    catch
        //    {
        //        dto.Name = "";
        //        dto.Amount = "";
        //        dto.Code = "";
        //        dto.CountryCode = "";
        //    }
        //    return dto;
        //}

        public MerchantDTO MerchantTransaction(Models.Input<QROrderRequest> obj, string refer, string code)
        {
            PayitMerchantsnWalletsEntities db = new PayitMerchantsnWalletsEntities();
            Merchant merchant = new Merchant();
            MerchantDTO dto = new MerchantDTO();
            MerchantOrder order = new MerchantOrder();
            //QROrderItem qrord = new QROrderItem();
            QROrderRequest qrord = new QROrderRequest();
            MerchantOrderItem ordItem = new MerchantOrderItem();
            MerchantTransaction merchantTran = new MerchantTransaction();
            CustomerWalletTransaction CusWL = new CustomerWalletTransaction();
            var mDetails = (from a in db.Merchants
                            where a.Name == obj.input.QR.name && a.Code == code
                            select a).FirstOrDefault();
            try
            {
                if (mDetails != null)
                {
                    merchantTran.Amount = string.IsNullOrEmpty(obj.input.amount) ? 0 : Convert.ToDouble(obj.input.amount);
                    merchantTran.MerchantID = mDetails.ID;
                    merchantTran.Reference = refer;
                    merchantTran.Status = true;
                    merchantTran.TransactionDate = DateTime.Now;
                    db.MerchantTransactions.Add(merchantTran);
                    db.SaveChanges();

                    order.Amount = merchantTran.Amount;
                    order.Description = "QRPAYMENT";
                    order.isPaid = true;
                    order.MerchantUserID = Convert.ToInt32(obj.input.QR.merchantuserid);
                    order.MobileNo = mDetails.PhoneNo;
                    order.PaymentReference = merchantTran.Reference;
                    order.ProcessDate = DateTime.Now;
                    order.Reference = merchantTran.Reference;
                    order.Status = true;
                    order.StatusDescription = "WalletPay";
                    order.TranDate = DateTime.Now;
                    db.MerchantOrders.Add(order);
                    db.SaveChanges();

                    foreach (var row in obj.input.QR.orderitems)
                    {
                        double total = Convert.ToInt32(row.price) * Convert.ToInt32(row.quantity);

                        ordItem.MerchantOrderID = order.ID;
                        ordItem.MerchantItemID = Convert.ToInt32(row.itemid);
                        ordItem.Quantity = Convert.ToInt32(row.quantity);
                        ordItem.UnitPrice = Convert.ToDouble(row.price);
                        ordItem.ItemAmount = total;
                        ordItem.CreatedDate = DateTime.Now;
                        ordItem.Status = true;
                        db.MerchantOrderItems.Add(ordItem);
                        db.SaveChanges();
                    }
                    merchant.Amount = mDetails.Amount + merchantTran.Amount;
                    db.SaveChanges();
                    dto.Amount = mDetails.Amount.ToString();
                    dto.Code = mDetails.Code.ToString();
                    dto.CountryCode = mDetails.CountryCode;
                    dto.Name = mDetails.Name;
                }
            }
            catch
            {
                dto.Name = "";
                dto.Amount = "";
                dto.Code = "";
                dto.CountryCode = "";
            }
            return dto;
        }

        //public MerchantDTO MerchantTransaction(Models.Input<PayItemRequest> obj, string refer, string code, string name)
        //{
        //    PayitMerchantsnWalletsEntities db = new PayitMerchantsnWalletsEntities();
        //    Merchant merchant = new Merchant();
        //    MerchantDTO dto = new MerchantDTO();
        //    MerchantOrder order = new MerchantOrder();
        //    PayItemRequest qrord = new PayItemRequest();
        //    MerchantOrderItem ordItem = new MerchantOrderItem();
        //    MerchantTransaction merchantTran = new MerchantTransaction();
        //    CustomerWalletTransaction CusWL = new CustomerWalletTransaction();
        //    var mDetails = (from a in db.Merchants
        //                    where a.Name == name && a.Code == code
        //                    select a).FirstOrDefault();
        //    try
        //    {
        //        if (mDetails != null)
        //        {
        //            merchantTran.Amount = string.IsNullOrEmpty(obj.input.amount) ? 0 : Convert.ToDouble(obj.input.amount);
        //            merchantTran.MerchantID = mDetails.ID;
        //            merchantTran.Reference = refer;
        //            merchantTran.Status = true;
        //            merchantTran.TransactionDate = DateTime.Now;
        //            db.MerchantTransactions.Add(merchantTran);
        //            db.SaveChanges();
        //            int meruserid;
        //            var mer_user = merchantuser(Convert.ToInt32(obj.input.Order.merchantid));
        //            if (mer_user == null)
        //            {
        //                meruserid = 0;
        //            }
        //            else
        //            {
        //                meruserid = mer_user.ID;
        //            }

        //            order.Amount = merchantTran.Amount;
        //            order.Description = "QRPAYMENT";
        //            order.isPaid = true;
        //            order.MerchantUserID = meruserid;
        //            order.MobileNo = mDetails.PhoneNo;
        //            order.PaymentReference = merchantTran.Reference;
        //            order.ProcessDate = DateTime.Now;
        //            order.Reference = merchantTran.Reference;
        //            order.Status = true;
        //            order.AddressID = string.IsNullOrEmpty(obj.input.Order.addressid) ? 0 : Convert.ToInt32(obj.input.Order.addressid);
        //            order.StatusDescription = "WalletPay";
        //            order.TranDate = DateTime.Now;
        //            db.MerchantOrders.Add(order);
        //            db.SaveChanges();

        //            double total = Convert.ToInt32(obj.input.Order.price) * Convert.ToInt32(obj.input.Order.quantity);

        //            ordItem.MerchantOrderID = order.ID;
        //            ordItem.MerchantItemID = Convert.ToInt32(obj.input.Order.itemid);
        //            ordItem.Quantity = Convert.ToInt32(obj.input.Order.quantity);
        //            ordItem.UnitPrice = Convert.ToDouble(obj.input.Order.price);
        //            ordItem.ItemAmount = total;
        //            ordItem.CreatedDate = DateTime.Now;
        //            ordItem.Status = true;
        //            db.MerchantOrderItems.Add(ordItem);
        //            db.SaveChanges();

        //            mDetails.Amount = mDetails.Amount + merchantTran.Amount;
        //            db.SaveChanges();
        //            dto.Amount = mDetails.Amount.ToString();
        //            dto.Code = mDetails.Code.ToString();
        //            dto.CountryCode = mDetails.CountryCode;
        //            dto.Name = mDetails.Name;
        //        }
        //    }
        //    catch
        //    {
        //        dto.Name = "";
        //        dto.Amount = "";
        //        dto.Code = "";
        //        dto.CountryCode = "";
        //    }
        //    return dto;
        //}

        private MerchantUser merchantuser(int mer_id)
        {
            var rows = db.MerchantUsers.Where(x => x.MyPaymentMerchantID == mer_id).SingleOrDefault();
            return rows;
        }
    }
}
