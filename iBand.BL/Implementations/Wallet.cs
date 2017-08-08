using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using iBand.BL.Interfaces;
using iBand.Models.Outputs.WalletOutputs;
using iBand.Models.Inputs.WalletInputs;
using iBand.Models;
using System.Configuration;
using System.Net.Http;
using System.Web.Script.Serialization;
using Newtonsoft.Json;
using iBand.DAL;
using iBand.Models.Outputs.OneInOutputs;
using iBand.Models.Outputs.dobOutputs;
using iBand.Common;

namespace iBand.BL.Implementations
{
    public class Wallet : IWallet
    {
        WalletsEntities db = new WalletsEntities();

        DAL.Wallet wallet = new DAL.Wallet();
        Profile profile = new Profile();
        IOneIn onein = new OneIn();
        OldWallet OldWallet = new OldWallet();

        Models.DTO<RegisterUser> RegResp = new DTO<RegisterUser>();

        Models.DTO<Login> Loginresp = new DTO<Login>();

        Models.DTO<Verify> VerifyOnein = new DTO<Verify>();
        
        Idob dob = new dob();
        Models.DTO<PINDTO> PinResp = new DTO<PINDTO>();
        Models.DTO<CreateDTO> CreateSubResp = new DTO<CreateDTO>();

        #region APIMethods

        public Models.DTO<WalletLoginDTO> Login(Models.Input<WalletLogin> obj)
        {
            Models.DTO<WalletLoginDTO> dto = new Models.DTO<WalletLoginDTO>();
            WalletLoginDTO resp = new WalletLoginDTO();
            dto.objname = "Login";
            try
            {
                if (string.IsNullOrEmpty(obj.input.username) || string.IsNullOrEmpty(obj.input.password))
                {
                    dto.status = new Models.Status(800);
                    return dto;
                }

                if (!isValidOneinLogin(obj))
                {
                    dto.status = Loginresp.status;
                    return dto;
                }

                string uri = ConfigurationManager.AppSettings["walletapi"].ToString();
                string url = uri + "/Login";
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(url);
                if (url != null)
                {
                    try
                    {
                        JavaScriptSerializer jdes = new JavaScriptSerializer();

                        var reqObj = new WalletLogin();
                        reqObj.username = obj.input.username;
                        reqObj.password = obj.input.password;
                        var request = reqObj;

                        Input<WalletLogin> inp = new Input<WalletLogin>();
                        inp.input = request;

                        client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                        client.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/json; charset=utf-8");

                        HttpResponseMessage response = client.PostAsJsonAsync(url, inp).Result;

                        if (response.IsSuccessStatusCode)
                        {
                            if (response != null)
                            {
                                Task<String> ss = response.Content.ReadAsStringAsync();
                                var result = JsonConvert.DeserializeObject<Models.DTO<WalletLoginDTO>>(ss.Result);

                                if (result != null)
                                {
                                    dto.response = result.response;
                                    dto.status = result.status;

                                    return dto;
                                }
                            }
                        }
                    }
                    catch (HttpRequestException e)
                    {
                        Console.WriteLine(e.Message);
                    }
                }
            }
            catch
            {
                dto.status = new Models.Status(1);
            }
            return dto;
        }

        public Models.DTO<VerifyUserDTO> Verify(Models.Input<VerifyUser> obj)
        {
            Models.DTO<VerifyUserDTO> dto = new Models.DTO<VerifyUserDTO>();
            VerifyUserDTO resp = new VerifyUserDTO();
            dto.objname = "Verify";

            try
            {
                if (string.IsNullOrEmpty(obj.input.appuserid) || string.IsNullOrEmpty(obj.input.code) || string.IsNullOrEmpty(obj.input.mobile))
                {
                    dto.status = new Models.Status(800);
                    return dto;
                }

                if (!isValidOneinUser(obj))
                {
                    dto.status = new Models.Status(1040);
                    return dto;
                }
                string uri = ConfigurationManager.AppSettings["walletapi"].ToString();
                string url = uri + "/Verify";
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(url);
                if (url != null)
                {
                    try
                    {
                        JavaScriptSerializer jdes = new JavaScriptSerializer();

                        Profile user = isMobileNumberExist(obj.input.mobile);
                        if (user == null)
                        {
                            dto.status = new Status(2060);
                            return dto;
                        }

                        var reqObj = new VerifyUser();
                        reqObj.appuserid = obj.input.appuserid;
                        reqObj.code = obj.input.code;
                        reqObj.mobile = obj.input.mobile;
                        reqObj.userid = user.UserIdentifier.ToString();
                        reqObj.countrycode = user.CountryCode.ToString();

                        var comin = new CommonInputParams();
                        comin.userid = obj.param.userid;
                        comin.os = obj.param.os;
                        comin.deviceuniqueid = obj.param.deviceuniqueid;
                        comin.appusername = "iBand";
                        comin.password = obj.param.password;
                        comin.username = obj.param.username;
                        comin.apppassword = "iBand";

                        var request = reqObj;

                        Input<VerifyUser> inp = new Input<VerifyUser>();
                        inp.input = request;
                        inp.param = comin;
                        client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                        client.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/json; charset=utf-8");

                        HttpResponseMessage response = client.PostAsJsonAsync(url, inp).Result;

                        if (response.IsSuccessStatusCode)
                        {
                            if (response != null)
                            {
                                Task<String> ss = response.Content.ReadAsStringAsync();
                                var result = JsonConvert.DeserializeObject<Models.DTO<VerifyUserDTO>>(ss.Result);

                                if (result != null)
                                {
                                    // Verifying old wallet
                                    //OldWallet.verifyOld(profile.Mobile, obj.param.deviceuniqueid);

                                    dto.response = result.response;
                                    dto.status = result.status;

                                    return dto;
                                }
                            }
                        }
                    }
                    catch (HttpRequestException e)
                    {

                        Console.WriteLine(e.Message);
                        dto.status.info1 = e.Message;
                    }
                }
            }
            catch (Exception ex)
            {

                dto.status = new Models.Status(1);
                dto.status.info1 = ex.Message;
            }
            return dto;
        }

        public Models.DTO<ModifyUserDTO> ModifyUserDetails(Models.Input<ModifyUser> obj)
        {
            Models.DTO<ModifyUserDTO> dto = new Models.DTO<ModifyUserDTO>();
            ModifyUserDTO resp = new ModifyUserDTO();
            dto.objname = "ModifyUserDetails";

            try
            {
                if (string.IsNullOrEmpty(obj.param.appusername) || string.IsNullOrEmpty(obj.param.apppassword) || string.IsNullOrEmpty(obj.input.username) || string.IsNullOrEmpty(obj.input.password))
                {
                    dto.status = new Models.Status(800);
                    return dto;
                }
                /* OneIn Login Object*/
                Models.Input<WalletLogin> login = new Input<WalletLogin>();
                WalletLogin @object = new WalletLogin();

                CommonInputParams comin = new CommonInputParams();
                comin.deviceuniqueid = obj.param.deviceuniqueid;
                comin.os = obj.param.os;
                comin.username = obj.param.username;
                comin.password = obj.param.password;

                @object.username = obj.param.username;
                @object.password = obj.param.password;
                login.input = @object;
                login.param = comin;
                /* ./OneIn Login Object*/
                if (!(isValidOneinLogin(login)))
                {
                    dto.status = new Models.Status(404);
                    return dto;
                }
                string uri = ConfigurationManager.AppSettings["oneinapi"].ToString();
                string url = uri + "/ModifyUserDetails";
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(url);
                if (url != null)
                {
                    try
                    {
                        JavaScriptSerializer jdes = new JavaScriptSerializer();

                        var reqObj = new ModifyUser();
                        reqObj.username = obj.input.username;
                        reqObj.password = obj.input.password;
                        reqObj.newpassword = obj.input.newpassword;
                        reqObj.firstname = obj.input.firstname;
                        reqObj.mobileNumber = obj.input.mobileNumber;
                        reqObj.emailID = obj.input.emailID;

                        var request = reqObj;

                        Input<ModifyUser> inp = new Input<ModifyUser>();
                        inp.input = request;

                        client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                        client.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/json; charset=utf-8");

                        HttpResponseMessage response = client.PostAsJsonAsync(url, inp).Result;

                        if (response.IsSuccessStatusCode)
                        {
                            if (response != null)
                            {
                                Task<String> ss = response.Content.ReadAsStringAsync();
                                var result = JsonConvert.DeserializeObject<Models.DTO<ModifyUserDTO>>(ss.Result);

                                if (result != null)
                                {
                                    dto.response = result.response;
                                    dto.status = result.status;

                                    return dto;
                                }
                            }
                        }
                    }
                    catch (HttpRequestException e)
                    {
                        Console.WriteLine(e.Message);
                    }
                }
            }
            catch
            {
                dto.status = new Models.Status(1);
            }
            return dto;
        }

        public Models.DTO<ForgotPasswordDTO> ForgotPassword(Models.Input<Models.Inputs.WalletInputs.ForgotPassword> obj)
        {
            Models.DTO<ForgotPasswordDTO> dto = new Models.DTO<ForgotPasswordDTO>();
            ForgotPasswordDTO resp = new ForgotPasswordDTO();
            dto.objname = "ForgotPassword";

            try
            {
                if (string.IsNullOrEmpty(obj.param.appusername) || string.IsNullOrEmpty(obj.param.apppassword) || string.IsNullOrEmpty(obj.input.username))
                {
                    dto.status = new Models.Status(800);
                    return dto;
                }

                string uri = ConfigurationManager.AppSettings["oneinapi"].ToString();
                string url = uri + "/ForgotPassword";
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(url);
                if (url != null)
                {
                    try
                    {
                        JavaScriptSerializer jdes = new JavaScriptSerializer();

                        var reqObj = new Models.Inputs.WalletInputs.ForgotPassword();
                        var comin = new CommonInputParams();
                        reqObj.username = obj.input.username;
                        comin.apppassword = "iBand";
                        comin.appusername = "iBand";
                        comin.deviceuniqueid = obj.param.deviceuniqueid;
                        comin.os = obj.param.os;
                        var request = reqObj;

                        Input<Models.Inputs.WalletInputs.ForgotPassword> inp = new Input<Models.Inputs.WalletInputs.ForgotPassword>();
                        inp.input = request;
                        inp.param = comin;

                        client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                        client.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/json; charset=utf-8");
                        HttpResponseMessage response = client.PostAsJsonAsync(url, inp).Result;

                        if (response.IsSuccessStatusCode)
                        {
                            if (response != null)
                            {
                                Task<String> ss = response.Content.ReadAsStringAsync();
                                var result = JsonConvert.DeserializeObject<Models.DTO<ForgotPasswordDTO>>(ss.Result);

                                if (result != null)
                                {
                                    dto.response = result.response;
                                    dto.status = result.status;

                                    if (dto.status.statuscode.Equals("0"))
                                    {
                                        Profile user = isMobileNumberExist(obj.input.username);
                                        if (user != null)
                                        {
                                            user.Password = result.response.newpass;
                                            db.SaveChanges();
                                        }
                                    }
                                    return dto;
                                }
                            }
                        }
                    }
                    catch (HttpRequestException e)
                    {
                        Console.WriteLine(e.Message);
                    }
                }
            }
            catch
            {
                dto.status = new Models.Status(1);
            }
            return dto;
        }

        public Models.DTO<WalletProfileDTO> Register(Models.Input<WalletProfile> obj)
        {
            Models.DTO<WalletProfileDTO> dto = new Models.DTO<WalletProfileDTO>();
            WalletProfileDTO resp = new WalletProfileDTO();
            dto.objname = "Register";
            try
            {
                if (string.IsNullOrEmpty(obj.input.password) || string.IsNullOrEmpty(obj.input.mobileno))
                {
                    dto.status = new Models.Status(800);
                    return dto;
                }
                // var register = OneinRegistration(obj);
                if (!OneinRegistration(obj))
                {
                    dto.status = RegResp.status;
                    return dto;
                }

                string uri = ConfigurationManager.AppSettings["walletapi"].ToString();
                string url = uri + "/Register";
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(url);
                if (url != null)
                {
                    try
                    {
                        JavaScriptSerializer jdes = new JavaScriptSerializer();
                        var reqObj = new WalletProfile();
                        reqObj.firstname = string.IsNullOrEmpty(obj.input.firstname) ? "" : obj.input.firstname;
                        reqObj.lastname = string.IsNullOrEmpty(obj.input.lastname) ? "" : obj.input.lastname;
                        reqObj.fullname = string.IsNullOrWhiteSpace(obj.input.fullname) ? "" : obj.input.fullname;
                        reqObj.mobileno = string.IsNullOrEmpty(obj.input.mobileno) ? "" : obj.input.mobileno;
                        reqObj.email = string.IsNullOrEmpty(obj.input.email) ? "" : obj.input.email;
                        reqObj.userid = string.IsNullOrEmpty(obj.input.userid) ? "" : obj.input.userid;
                        reqObj.code = string.IsNullOrEmpty(RegResp.response.code) ? "" : RegResp.response.code;
                        reqObj.appuserid = string.IsNullOrEmpty(RegResp.response.appuserid) ? "" : RegResp.response.appuserid;
                        reqObj.password = string.IsNullOrEmpty(obj.input.password) ? "" : obj.input.password;
                        reqObj.countrycode = string.IsNullOrEmpty(obj.input.countrycode) ? "" : obj.input.countrycode;
                        reqObj.dob = string.IsNullOrEmpty(RegResp.response.dob) ? "01/01/1901" : RegResp.response.dob;
                        reqObj.userid = string.IsNullOrEmpty(RegResp.response.userid) ? "" : RegResp.response.userid;
                        reqObj.appuserid = string.IsNullOrEmpty(RegResp.response.appuserid) ? "" : RegResp.response.appuserid;
                        var request = reqObj;

                        Input<WalletProfile> inp = new Input<WalletProfile>();
                        inp.input = request;
                        client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                        client.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/json; charset=utf-8");

                        HttpResponseMessage response = client.PostAsJsonAsync(url, inp).Result;

                        if (response.IsSuccessStatusCode)
                        {
                            if (response != null)
                            {
                                Task<String> ss = response.Content.ReadAsStringAsync();
                                var result = JsonConvert.DeserializeObject<Models.DTO<WalletProfileDTO>>(ss.Result);

                                if (result != null)
                                {
                                    dto.response = result.response;
                                    dto.status = result.status;
                                    return dto;
                                }
                            }
                        }
                    }
                    catch (HttpRequestException e)
                    {
                        Console.WriteLine(e.Message);
                    }
                }
            }
            catch
            {
                dto.status = new Models.Status(1);
            }
            return dto;
        }

        public Models.DTO<CreditTransactionsDTO> CreditTransaction(Models.Input<CreditTransactions> obj)
        {
            Models.DTO<CreditTransactionsDTO> dto = new Models.DTO<CreditTransactionsDTO>();
            CreditTransactionsDTO resp = new CreditTransactionsDTO();
            dto.objname = "CreditTransaction";

            try
            {
                if (string.IsNullOrEmpty(obj.input.userid))
                {
                    dto.status = new Models.Status(800);
                    return dto;
                }
                /* OneIn Login Object*/
                Models.Input<WalletLogin> login = new Input<WalletLogin>();
                WalletLogin @object = new WalletLogin();

                CommonInputParams comin = new CommonInputParams();
                comin.deviceuniqueid = obj.param.deviceuniqueid;
                comin.os = obj.param.os;
                comin.username = obj.param.username;
                comin.password = obj.param.password;

                @object.username = obj.param.username;
                @object.password = obj.param.password;
                login.input = @object;
                login.param = comin;
                /* ./OneIn Login Object*/
                if (!(isValidOneinLogin(login)))
                {
                    dto.status = new Models.Status(404);
                    return dto;
                }
                string uri = ConfigurationManager.AppSettings["walletapi"].ToString();
                string url = uri + "/CreditTransaction";
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(url);
                if (url != null)
                {
                    try
                    {
                        JavaScriptSerializer jdes = new JavaScriptSerializer();
                        var reqObj = new CreditTransactions();
                        reqObj.userid = string.IsNullOrEmpty(obj.input.userid) ? "" : obj.input.userid;
                        reqObj.mobile = string.IsNullOrEmpty(obj.input.mobile) ? "" : obj.input.mobile;
                        reqObj.email = string.IsNullOrEmpty(obj.input.email) ? "" : obj.input.email;
                        reqObj.paymentservice = string.IsNullOrEmpty(obj.input.paymentservice) ? "" : obj.input.paymentservice;
                        reqObj.paymentref = string.IsNullOrEmpty(obj.input.paymentref) ? "" : obj.input.paymentref;
                        reqObj.paymentstatus = string.IsNullOrEmpty(obj.input.paymentstatus) ? "" : obj.input.paymentstatus;
                        reqObj.amount = string.IsNullOrEmpty(obj.input.amount) ? "" : obj.input.amount;
                        reqObj.transactionamount = string.IsNullOrEmpty(obj.input.transactionamount) ? "" : obj.input.transactionamount;
                        reqObj.currency = string.IsNullOrEmpty(obj.input.currency) ? "" : obj.input.currency;
                        reqObj.transCurrency = string.IsNullOrEmpty(obj.input.transCurrency) ? "" : obj.input.transCurrency;

                        var request = reqObj;
                        Input<CreditTransactions> inp = new Input<CreditTransactions>();
                        inp.input = request;

                        client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                        client.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/json; charset=utf-8");

                        HttpResponseMessage response = client.PostAsJsonAsync(url, inp).Result;

                        if (response.IsSuccessStatusCode)
                        {
                            if (response != null)
                            {
                                Task<String> ss = response.Content.ReadAsStringAsync();
                                var result = JsonConvert.DeserializeObject<Models.DTO<CreditTransactionsDTO>>(ss.Result);

                                if (result != null)
                                {
                                    try
                                    {
                                        // Inserting into old wallet Transactions

                                        if (result.status.statuscode.Equals("0"))
                                            OldWallet.CreditTransactionOld(obj);
                                    }
                                    catch { }
                                    finally
                                    {
                                        dto.response = result.response;
                                        dto.status = result.status;
                                    }

                                    return dto;
                                }
                            }
                        }
                    }
                    catch (HttpRequestException e)
                    {
                        dto.status = new Models.Status(1);
                    }
                }
            }
            catch (Exception ex)
            {
                dto.status = new Models.Status(1);
            }
            return dto;
        }

        public Models.DTO<DebitTransactionsDTO> DebitTransaction(Models.Input<DebitTransactions> obj)
        {
            Models.DTO<DebitTransactionsDTO> dto = new Models.DTO<DebitTransactionsDTO>();
            DebitTransactionsDTO resp = new DebitTransactionsDTO();
            dto.objname = "DebitTransaction";
            try
            {
                if (string.IsNullOrEmpty(obj.input.userid) || string.IsNullOrEmpty(obj.input.PaymentID.ToString()))
                {
                    dto.status = new Models.Status(800);
                    return dto;
                }

                /* OneIn Login Object*/
                Models.Input<WalletLogin> login = new Input<WalletLogin>();
                WalletLogin @object = new WalletLogin();

                CommonInputParams comin = new CommonInputParams();
                comin.deviceuniqueid = obj.param.deviceuniqueid;
                comin.os = obj.param.os;
                comin.username = obj.param.username;
                comin.password = obj.param.password;

                @object.username = obj.param.username;
                @object.password = obj.param.password;
                login.input = @object;
                login.param = comin;
                /* ./OneIn Login Object*/
                if (!(isValidOneinLogin(login)))
                {
                    dto.status = new Models.Status(404);
                    return dto;
                }

                if(obj.input.PaymentType.ToLower().Contains("dob"))
                {
                    if(!(dobPin(obj.input.mobile, obj.input.PaymentID)))
                    {
                        dto.status = new Status(1);
                        resp.pin = PinResp.response;
                        resp.Mobile = obj.input.mobile;
                        dto.response = resp;
                        return dto;
                    }
                    else
                    { 
                        dto.status = PinResp.status;
                        resp.pin = PinResp.response;
                        resp.Mobile = obj.input.mobile;
                        dto.response = resp;
                        return dto;
                    }
                }
                else if (obj.input.PaymentType.ToLower().Equals("wallet"))
                {
                    WalletAccount wac = new WalletAccount();
                    wac = WalletAccountDetails(Convert.ToInt64(obj.input.userid));

                    if (wac != null)
                    {
                        if (wac.Balance < 0 || wac.Balance < Convert.ToDouble(obj.input.amount))
                        {
                            dto.status = new Models.Status(1090);
                            return dto;
                        }
                    }
                    else
                    {
                        dto.status = new Models.Status(3040);
                        return dto;
                    }

                    string uri = ConfigurationManager.AppSettings["walletapi"].ToString();
                    string url = uri + "/DebitTransaction";
                    HttpClient client = new HttpClient();
                    client.BaseAddress = new Uri(url);
                    if (url != null)
                    {
                        try
                        {
                            JavaScriptSerializer jdes = new JavaScriptSerializer();
                            var reqObj = new DebitTransactions();
                            reqObj.userid = string.IsNullOrEmpty(obj.input.userid) ? "" : obj.input.userid;
                            reqObj.mobile = string.IsNullOrEmpty(obj.input.mobile) ? "" : obj.input.mobile;
                            reqObj.email = string.IsNullOrEmpty(obj.input.email) ? "" : obj.input.email;
                            reqObj.paymentservice = string.IsNullOrEmpty(obj.input.paymentservice) ? "" : obj.input.paymentservice;
                            reqObj.paymentref = string.IsNullOrEmpty(obj.input.paymentref) ? "" : obj.input.paymentref;
                            reqObj.paymentstatus = string.IsNullOrEmpty(obj.input.paymentstatus) ? "" : obj.input.paymentstatus;
                            reqObj.amount = string.IsNullOrEmpty(obj.input.amount) ? "" : obj.input.amount;
                            reqObj.transactionamount = string.IsNullOrEmpty(obj.input.transactionamount) ? "" : obj.input.transactionamount;
                            reqObj.currency = string.IsNullOrEmpty(obj.input.currency) ? "" : obj.input.currency;
                            reqObj.transCurrency = string.IsNullOrEmpty(obj.input.transCurrency) ? "" : obj.input.transCurrency;

                            var request = reqObj;

                            Input<DebitTransactions> inp = new Input<DebitTransactions>();
                            inp.input = request;

                            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                            client.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/json; charset=utf-8");

                            HttpResponseMessage response = client.PostAsJsonAsync(url, inp).Result;

                            if (response.IsSuccessStatusCode)
                            {
                                if (response != null)
                                {
                                    Task<String> ss = response.Content.ReadAsStringAsync();
                                    var result = JsonConvert.DeserializeObject<Models.DTO<DebitTransactionsDTO>>(ss.Result);

                                    if (result != null)
                                    {
                                        dto.response = result.response;
                                        dto.status = result.status;

                                        //Storing the Information of User in UserWallet Table for BillingWindowsService
                                        //iBandEntities db2 = new iBandEntities();

                                        //var uid=Convert.ToInt64(obj.input.userid);

                                        //var suser = db2.UserWallets.Where(x => x.UserID == uid).SingleOrDefault();

                                        //if (suser == null)
                                        //{
                                        //    UserWallet wa = new UserWallet();
                                        //    wa.UserID = uid;
                                        //    wa.Username = string.IsNullOrEmpty(obj.param.username) ? "" : obj.param.username;
                                        //    wa.Password = string.IsNullOrEmpty(obj.param.password) ? "" : obj.param.password;
                                        //    wa.AppUsername = string.IsNullOrEmpty(obj.param.appusername) ? "" : obj.param.appusername;
                                        //    wa.AppPassword = string.IsNullOrEmpty(obj.param.apppassword) ? "" : obj.param.apppassword;
                                        //    wa.DeviceUniqueID = string.IsNullOrEmpty(obj.param.deviceid) ? "" : obj.param.deviceid;
                                        //    wa.DeviceUniqueID_ID = string.IsNullOrEmpty(obj.param.udid_id) ? "" : obj.param.udid_id;
                                        //    wa.DeviceOS = string.IsNullOrEmpty(obj.param.os) ? "" : obj.param.os;
                                        //    wa.Mobile = string.IsNullOrEmpty(obj.param.username) ? "" : obj.param.username;
                                        //    wa.CountryCode = string.IsNullOrEmpty(obj.param.CountryCode) ? "" : obj.param.CountryCode;
                                        //    wa.Email = string.IsNullOrEmpty(obj.param.email) ? "" : obj.param.email;
                                        //    wa.Currency = string.IsNullOrEmpty(obj.input.currency) ? "" : obj.input.currency;
                                        //    wa.PaymentService = string.IsNullOrEmpty(obj.input.paymentservice) ? "" : obj.input.paymentservice; 
                                        //    wa.Status = true;
                                        //    wa.CreatedDate = DateTime.Now;


                                        //    db2.UserWallets.Add(wa);
                                        //    db2.SaveChanges();
                                        //    //End
                                        //}

                                        //else
                                        //{
                                        //    suser.UserID = uid;
                                        //    suser.Username = string.IsNullOrEmpty(obj.param.username) ? "" : obj.param.username;
                                        //    suser.Password = string.IsNullOrEmpty(obj.param.password) ? "" : obj.param.password;
                                        //    suser.AppUsername = string.IsNullOrEmpty(obj.param.appusername) ? "" : obj.param.appusername;
                                        //    suser.AppPassword = string.IsNullOrEmpty(obj.param.apppassword) ? "" : obj.param.apppassword;
                                        //    suser.DeviceUniqueID = string.IsNullOrEmpty(obj.param.deviceid) ? "" : obj.param.deviceid;
                                        //    suser.DeviceUniqueID_ID = string.IsNullOrEmpty(obj.param.udid_id) ? "" : obj.param.udid_id;
                                        //    suser.DeviceOS = string.IsNullOrEmpty(obj.param.os) ? "" : obj.param.os;
                                        //    suser.Mobile = string.IsNullOrEmpty(obj.param.username) ? "" : obj.param.username;
                                        //    suser.CountryCode = string.IsNullOrEmpty(obj.param.CountryCode) ? "" : obj.param.CountryCode;
                                        //    suser.Email = string.IsNullOrEmpty(obj.param.email) ? "" : obj.param.email;
                                        //    suser.Currency = string.IsNullOrEmpty(obj.input.currency) ? "" : obj.input.currency;
                                        //    suser.PaymentService = string.IsNullOrEmpty(obj.input.paymentservice) ? "" : obj.input.paymentservice;

                                        //    db2.SaveChanges();
                                        //}
                                        //INserting into old wallet Transactions
                                        //var cus = OldWallet.DebitTransactionOld(obj);

                                        return dto;
                                    }
                                }
                            }
                        }
                        catch (HttpRequestException e)
                        {
                            dto.status = new Models.Status(1);
                        }
                    }
                }
                
            }
            catch
            {
                dto.status = new Models.Status(1);
            }
            return dto;
        }

        public Models.DTO<TransactionHistoryDTO> GetTransactions(Models.Input<TransactionHistory> obj)
        {
            Models.DTO<TransactionHistoryDTO> dto = new Models.DTO<TransactionHistoryDTO>();
            TransactionHistoryDTO resp = new TransactionHistoryDTO();
            TransactionDTO onse = new TransactionDTO();
            dto.objname = "GetTransactions";

            try
            {
                if (string.IsNullOrEmpty(obj.input.userid))
                {
                    dto.status = new Models.Status(800);
                    return dto;
                }
                /* OneIn Login Object*/
                Models.Input<WalletLogin> login = new Input<WalletLogin>();
                WalletLogin @object = new WalletLogin();

                CommonInputParams comin = new CommonInputParams();
                comin.deviceuniqueid = obj.param.deviceuniqueid;
                comin.os = obj.param.os;
                comin.username = obj.param.username;
                comin.password = obj.param.password;

                @object.username = obj.param.username;
                @object.password = obj.param.password;
                login.input = @object;
                login.param = comin;
                /* ./OneIn Login Object*/
                if (!(isValidOneinLogin(login)))
                {
                    dto.status = new Models.Status(404);
                    return dto;
                }
                string uri = ConfigurationManager.AppSettings["walletapi"].ToString();
                string url = uri + "/GetTransactions";
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(url);
                if (url != null)
                {
                    try
                    {
                        JavaScriptSerializer jdes = new JavaScriptSerializer();
                        var reqObj = new TransactionHistory();
                        reqObj.userid = string.IsNullOrEmpty(obj.input.userid) ? "" : obj.input.userid;
                        reqObj.mobile = string.IsNullOrEmpty(obj.input.mobile) ? "" : obj.input.mobile;
                        reqObj.email = string.IsNullOrEmpty(obj.input.email) ? "" : obj.input.email;
                        reqObj.count = string.IsNullOrEmpty(obj.input.count) ? "" : obj.input.count;
                        reqObj.lastid = string.IsNullOrEmpty(obj.input.lastid) ? "" : obj.input.lastid;

                        var request = reqObj;

                        Input<TransactionHistory> inp = new Input<TransactionHistory>();
                        inp.input = request;

                        client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                        client.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/json; charset=utf-8");

                        HttpResponseMessage response = client.PostAsJsonAsync(url, inp).Result;

                        if (response.IsSuccessStatusCode)
                        {
                            if (response != null)
                            {
                                Task<String> ss = response.Content.ReadAsStringAsync();
                                var result = JsonConvert.DeserializeObject<Models.DTO<TransactionHistoryDTO>>(ss.Result);

                                if (result != null)
                                {
                                    dto.response = result.response;
                                    dto.status = result.status;

                                    return dto;
                                }
                            }
                        }
                    }
                    catch (HttpRequestException e)
                    {
                        dto.status = new Models.Status(1);
                    }
                }
            }
            catch
            {
                dto.status = new Models.Status(1);
            }
            return dto;
        }

        public Models.DTO<WalletAccountsDTO> GetWalletAccounts(Models.Input<WalletAccounts> obj)
        {
            Models.DTO<WalletAccountsDTO> dto = new Models.DTO<WalletAccountsDTO>();
            WalletAccountsDTO resp = new WalletAccountsDTO();
            WalletDTO onse = new WalletDTO();
            dto.objname = "GetWalletAccounts";

            try
            {
                if (string.IsNullOrEmpty(obj.input.userid))
                {
                    dto.status = new Models.Status(800);
                    return dto;
                }
                /* OneIn Login Object*/
                Models.Input<WalletLogin> login = new Input<WalletLogin>();
                WalletLogin @object = new WalletLogin();

                CommonInputParams comin = new CommonInputParams();
                comin.deviceuniqueid = obj.param.deviceuniqueid;
                comin.os = obj.param.os;
                comin.username = obj.param.username;
                comin.password = obj.param.password;

                @object.username = obj.param.username;
                @object.password = obj.param.password;
                login.input = @object;
                login.param = comin;
                /* ./OneIn Login Object*/
                if (!(isValidOneinLogin(login)))
                {
                    dto.status = new Models.Status(404);
                    return dto;
                }
                string uri = ConfigurationManager.AppSettings["walletapi"].ToString();
                string url = uri + "/GetWalletAccounts";
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(url);
                if (url != null)
                {
                    try
                    {
                        JavaScriptSerializer jdes = new JavaScriptSerializer();
                        var reqObj = new WalletAccounts();
                        reqObj.userid = string.IsNullOrEmpty(obj.input.userid) ? "" : obj.input.userid;
                        reqObj.mobile = string.IsNullOrEmpty(obj.input.mobile) ? "" : obj.input.mobile;

                        var request = reqObj;

                        Input<WalletAccounts> inp = new Input<WalletAccounts>();
                        inp.input = request;

                        client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                        client.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/json; charset=utf-8");

                        HttpResponseMessage response = client.PostAsJsonAsync(url, inp).Result;

                        if (response.IsSuccessStatusCode)
                        {
                            if (response != null)
                            {
                                Task<String> ss = response.Content.ReadAsStringAsync();
                                var result = JsonConvert.DeserializeObject<Models.DTO<WalletAccountsDTO>>(ss.Result);

                                if (result != null)
                                {
                                    dto.response = result.response;
                                    dto.status = result.status;

                                    return dto;
                                }
                            }
                        }
                    }
                    catch (HttpRequestException e)
                    {
                        dto.status = new Models.Status(1);
                    }
                }
            }
            catch
            {
                dto.status = new Models.Status(1);
            }
            return dto;
        }

        //public Models.DTO<QRPayResponse> PayQR(Models.Input<QRPayRequest> obj)
        //{
        //    Models.DTO<QRPayResponse> dto = new Models.DTO<QRPayResponse>();
        //    QRPayResponse resp = new QRPayResponse();
        //    MerchantDTO merchant = new MerchantDTO();

        //    Models.WalletOutputs.DebitTransactionsDTO debit = new Models.WalletOutputs.DebitTransactionsDTO();
        //    dto.objname = "PayQR";
        //    try
        //    {
        //        var reqObj = new DebitTransactions();
        //        Input<GlobalPayit.Models.WalletInputs.DebitTransactions> inp = new Input<Models.WalletInputs.DebitTransactions>();

        //        if (string.IsNullOrEmpty(obj.input.mobileno) || string.IsNullOrEmpty(obj.input.password))
        //        {
        //            dto.status = new Models.Status(800);
        //            return dto;
        //        }

        //        /* OneIn Login Object*/
        //        Models.Input<Models.WalletInputs.WalletLogin> login = new Input<WalletLogin>();
        //        GlobalPayit.Models.WalletInputs.WalletLogin @object = new Models.WalletInputs.WalletLogin();

        //        GlobalPayit.Models.CommonInputParams cominp = new CommonInputParams();
        //        cominp.deviceuniqueid = obj.param.deviceuniqueid;
        //        cominp.os = obj.param.os;
        //        cominp.username = obj.input.mobileno;
        //        cominp.password = obj.input.password;

        //        @object.username = obj.input.mobileno;
        //        @object.password = obj.input.password;
        //        login.input = @object;
        //        login.param = cominp;
        //        /* ./OneIn Login Object*/
        //        if (!(isValidOneinLogin(login)))
        //        {
        //            dto.status = new Models.Status(404);
        //            return dto;
        //        }

        //        Profile user = isMobileNumberExist(obj.input.mobileno);
        //        if (user == null)
        //        {
        //            dto.status = new Status(2060);
        //            return dto;
        //        }

        //        WalletAccount wac = new WalletAccount();
        //        wac = WalletAccountDetails(Convert.ToInt64(user.UserIdentifier));

        //        if (wac != null)
        //        {
        //            if (wac.Balance < 0 || wac.Balance < Convert.ToDouble(obj.input.amount))
        //            {
        //                dto.status = new Models.Status(1090);
        //                return dto;
        //            }
        //        }
        //        else if (wac == null)
        //        {
        //            dto.status = new Models.Status(3040);
        //            return dto;
        //        }
        //        var merdetails = getMerchantDetails(Convert.ToInt32(obj.input.QR.merchantid));
        //        if (merdetails == null)
        //        {
        //            dto.status = new Status(1);
        //            return dto;
        //        }

        //        string uri = ConfigurationManager.AppSettings["walletapi"].ToString();
        //        string url = uri + "/DebitTransaction";
        //        HttpClient client = new HttpClient();
        //        client.BaseAddress = new Uri(url);
        //        if (url != null)
        //        {
        //            try
        //            {
        //                JavaScriptSerializer jdes = new JavaScriptSerializer();
        //                string @refer = Common.Global.GetUniqueKey(12);
        //                reqObj.userid = string.IsNullOrEmpty(user.UserIdentifier.ToString()) ? "" : user.UserIdentifier.ToString();
        //                reqObj.mobile = string.IsNullOrEmpty(obj.input.mobileno) ? "" : obj.input.mobileno;
        //                reqObj.email = string.IsNullOrEmpty(user.Email) ? "" : user.Email;
        //                reqObj.paymentservice = merdetails.Code;
        //                reqObj.paymentref = @refer;
        //                reqObj.paymentstatus = "SUCCESS";
        //                reqObj.amount = string.IsNullOrEmpty(obj.input.amount) ? "" : obj.input.amount;
        //                reqObj.transactionamount = string.IsNullOrEmpty(obj.input.transactionamount) ? obj.input.amount : obj.input.transactionamount;
        //                reqObj.currency = string.IsNullOrEmpty(obj.input.currency) ? "" : obj.input.currency;
        //                reqObj.transCurrency = string.IsNullOrEmpty(obj.input.transCurrency) ? obj.input.currency : obj.input.transCurrency;

        //                Models.CommonInputParams comin = new CommonInputParams();
        //                comin.username = obj.input.mobileno;
        //                comin.password = obj.input.password;
        //                comin.deviceuniqueid = obj.param.deviceuniqueid;
        //                comin.appusername = "iBand";
        //                comin.apppassword = "iBand";
        //                comin.CountryCode = obj.param.CountryCode;
        //                comin.os = obj.param.os;

        //                inp.input = reqObj;
        //                inp.param = comin;
        //                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
        //                client.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/json; charset=utf-8");

        //                HttpResponseMessage response = client.PostAsJsonAsync(url, inp).Result;

        //                if (response.IsSuccessStatusCode)
        //                {
        //                    if (response != null)
        //                    {
        //                        Task<String> ss = response.Content.ReadAsStringAsync();
        //                        var result = JsonConvert.DeserializeObject<Models.DTO<Models.WalletOutputs.DebitTransactionsDTO>>(ss.Result);

        //                        if (result != null)
        //                        {
        //                            if (result.status.statuscode.Equals("0"))
        //                            {
        //                                debit = result.response;
        //                                resp.transactionDetails = debit;
        //                                //Insert into old wallet DebitTransactions
        //                                var oldDebit = OldWallet.DebitTransactionOld(inp);
        //                                if (oldDebit == 0)
        //                                {
        //                                    merchant = OldWallet.MerchantTransaction(obj, @refer, merdetails.Code);
        //                                }
        //                                resp.merchantDetails = merchant;
        //                                dto.response = resp;
        //                                dto.status = result.status;
        //                            }
        //                            else
        //                            {
        //                                dto.status = new Status(1);
        //                            }
        //                            return dto;
        //                        }
        //                    }
        //                }
        //            }
        //            catch (HttpRequestException e)
        //            {
        //                dto.status = new Models.Status(1);
        //            }
        //        }
        //    }
        //    catch
        //    {
        //        dto.status = new Models.Status(1);
        //    }
        //    return dto;
        //}

        public Models.DTO<QROrderResponseDTO> OrderQR(Models.Input<QROrderRequest> obj)
        {
            Models.DTO<QROrderResponseDTO> dto = new Models.DTO<QROrderResponseDTO>();
            QROrderResponseDTO resp = new QROrderResponseDTO();
            dto.objname = "OrderQR";

            DebitTransactionsDTO debit = new DebitTransactionsDTO();
            MerchantDTO merchant = new MerchantDTO();
            try
            {
                var reqObj = new DebitTransactions();
                Input<DebitTransactions> inp = new Input<DebitTransactions>();

                if (string.IsNullOrEmpty(obj.input.mobileno) || string.IsNullOrEmpty(obj.input.password))
                {
                    dto.status = new Models.Status(800);
                    return dto;
                }

                /* OneIn Login Object*/
                Models.Input<WalletLogin> login = new Input<WalletLogin>();
                WalletLogin @object = new WalletLogin();

                CommonInputParams cominp = new CommonInputParams();
                cominp.deviceuniqueid = obj.param.deviceuniqueid;
                cominp.os = obj.param.os;
                cominp.username = obj.input.mobileno;
                cominp.password = obj.input.password;

                @object.username = obj.input.mobileno;
                @object.password = obj.input.password;
                login.input = @object;
                login.param = cominp;
                /* ./OneIn Login Object*/
                if (!(isValidOneinLogin(login)))
                {
                    dto.status = new Models.Status(404);
                    return dto;
                }

                Profile user = isMobileNumberExist(obj.input.mobileno);
                if (user == null)
                {
                    dto.status = new Status(2060);
                    return dto;
                }

                WalletAccount wac = new WalletAccount();
                wac = WalletAccountDetails(Convert.ToInt64(user.UserIdentifier));

                if (wac != null)
                {
                    if (wac.Balance < 0 || wac.Balance < Convert.ToDouble(obj.input.amount))
                    {
                        dto.status = new Models.Status(1090);
                        return dto;
                    }
                }
                else if (wac == null)
                {
                    dto.status = new Models.Status(3040);
                    return dto;
                }
                var merchantdetails = getMerchantDetails(Convert.ToInt32(obj.input.QR.merchantid));
                string uri = ConfigurationManager.AppSettings["walletapi"].ToString();
                string url = uri + "/DebitTransaction";
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(url);
                if (url != null)
                {
                    try
                    {
                        JavaScriptSerializer jdes = new JavaScriptSerializer();
                        string @refer = Common.Global.GetUniqueKey(12);
                        reqObj.userid = string.IsNullOrEmpty(user.UserIdentifier.ToString()) ? "" : user.UserIdentifier.ToString();
                        reqObj.mobile = string.IsNullOrEmpty(obj.input.mobileno) ? "" : obj.input.mobileno;
                        reqObj.email = string.IsNullOrEmpty(user.Email) ? "" : user.Email;
                        reqObj.paymentservice = string.IsNullOrEmpty(obj.input.paymentcode) ? "" : obj.input.paymentcode;
                        reqObj.paymentref = @refer;
                        reqObj.paymentstatus = "SUCCESS";
                        reqObj.amount = string.IsNullOrEmpty(obj.input.amount) ? "" : obj.input.amount;
                        reqObj.transactionamount = string.IsNullOrEmpty(obj.input.transactionamount) ? obj.input.amount : obj.input.transactionamount;
                        reqObj.currency = string.IsNullOrEmpty(obj.input.currency) ? "" : obj.input.currency;
                        reqObj.transCurrency = string.IsNullOrEmpty(obj.input.transCurrency) ? obj.input.currency : obj.input.transCurrency;

                        Models.CommonInputParams comin = new CommonInputParams();
                        comin.username = obj.input.mobileno;
                        comin.password = obj.input.password;
                        comin.deviceuniqueid = obj.param.deviceuniqueid;
                        comin.appusername = "iBand";
                        comin.apppassword = "iBand";
                        comin.CountryCode = obj.param.CountryCode;
                        comin.os = obj.param.os;

                        inp.input = reqObj;
                        inp.param = comin;
                        client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                        client.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/json; charset=utf-8");

                        HttpResponseMessage response = client.PostAsJsonAsync(url, inp).Result;

                        if (response.IsSuccessStatusCode)
                        {
                            if (response != null)
                            {
                                Task<String> ss = response.Content.ReadAsStringAsync();
                                var result = JsonConvert.DeserializeObject<Models.DTO<DebitTransactionsDTO>>(ss.Result);

                                if (result != null)
                                {
                                    if (result.status.statuscode.Equals("0"))
                                    {
                                        debit = result.response;
                                        resp.transactionDetails = debit;
                                        //Insert into old wallet DebitTransactions
                                        var oldDebit = OldWallet.DebitTransactionOld(inp);
                                        if (oldDebit == 0)
                                        {
                                            merchant = OldWallet.MerchantTransaction(obj, @refer, merchantdetails.Code);
                                        }
                                        resp.merchantDetails = merchant;
                                        dto.response = resp;
                                        dto.status = result.status;
                                    }
                                    else
                                    {
                                        dto.status = new Status(1);
                                    }
                                    return dto;
                                }
                            }
                        }
                    }
                    catch (HttpRequestException e)
                    {
                        dto.status = new Models.Status(1);
                    }
                }
            }
            catch (Exception ex)
            {
                dto.status = new Models.Status(1);
            }
            return dto;
        }

        //public Models.DTO<PayItemResponseDTO> PayItem(Models.Input<PayItemRequest> obj)
        //{
        //    Models.DTO<PayItemResponseDTO> dto = new Models.DTO<PayItemResponseDTO>();
        //    PayItemResponseDTO resp = new PayItemResponseDTO();
        //    dto.objname = "PayItem";
        //    DebitTransactionsDTO debit = new DebitTransactionsDTO();
        //    MerchantDTO merchant = new MerchantDTO();
        //    try
        //    {
        //        var reqObj = new DebitTransactions();
        //        Input<DebitTransactions> inp = new Input<DebitTransactions>();

        //        if (string.IsNullOrEmpty(obj.input.mobileno) || string.IsNullOrEmpty(obj.input.password))
        //        {
        //            dto.status = new Models.Status(800);
        //            return dto;
        //        }

        //        /* OneIn Login Object*/
        //        Models.Input<WalletLogin> login = new Input<WalletLogin>();
        //        WalletLogin @object = new WalletLogin();

        //        CommonInputParams cominp = new CommonInputParams();
        //        cominp.deviceuniqueid = obj.param.deviceuniqueid;
        //        cominp.os = obj.param.os;
        //        cominp.username = obj.param.username;
        //        cominp.password = obj.param.password;

        //        @object.username = obj.param.username;
        //        @object.password = obj.param.password;
        //        login.input = @object;
        //        login.param = cominp;
        //        /* ./OneIn Login Object*/
        //        if (!(isValidOneinLogin(login)))
        //        {
        //            dto.status = new Models.Status(404);
        //            return dto;
        //        }

        //        Profile user = isMobileNumberExist(obj.input.mobileno);
        //        if (user == null)
        //        {
        //            dto.status = new Status(2060);
        //            return dto;
        //        }

        //        WalletAccount wac = new WalletAccount();
        //        wac = WalletAccountDetails(Convert.ToInt64(user.UserIdentifier));

        //        if (wac != null)
        //        {
        //            if (wac.Balance < 0 || wac.Balance < Convert.ToDouble(obj.input.amount))
        //            {
        //                dto.status = new Models.Status(1090);
        //                return dto;
        //            }
        //        }
        //        else if (wac == null)
        //        {
        //            dto.status = new Models.Status(3040);
        //            return dto;
        //        }

        //        var merchantdetails = getMerchantDetails(Convert.ToInt32(obj.input.Order.merchantid));
        //        string uri = ConfigurationManager.AppSettings["walletapi"].ToString();
        //        string url = uri + "/DebitTransaction";
        //        HttpClient client = new HttpClient();
        //        client.BaseAddress = new Uri(url);
        //        if (url != null)
        //        {
        //            try
        //            {
        //                JavaScriptSerializer jdes = new JavaScriptSerializer();
        //                string @refer = Common.Global.GetUniqueKey(12);
        //                reqObj.userid = string.IsNullOrEmpty(user.UserIdentifier.ToString()) ? "" : user.UserIdentifier.ToString();
        //                reqObj.mobile = string.IsNullOrEmpty(obj.input.mobileno) ? "" : obj.input.mobileno;
        //                reqObj.email = string.IsNullOrEmpty(user.Email) ? "" : user.Email;
        //                reqObj.paymentservice = string.IsNullOrEmpty(obj.input.paymentcode) ? "" : obj.input.paymentcode;
        //                reqObj.paymentref = @refer;
        //                reqObj.paymentstatus = "SUCCESS";
        //                reqObj.amount = string.IsNullOrEmpty(obj.input.amount) ? "" : obj.input.amount;
        //                reqObj.transactionamount = string.IsNullOrEmpty(obj.input.transactionamount) ? obj.input.amount : obj.input.transactionamount;
        //                reqObj.currency = string.IsNullOrEmpty(obj.input.currency) ? "" : obj.input.currency;
        //                reqObj.transCurrency = string.IsNullOrEmpty(obj.input.transCurrency) ? obj.input.currency : obj.input.transCurrency;

        //                Models.CommonInputParams comin = new CommonInputParams();
        //                comin.username = obj.input.mobileno;
        //                comin.password = obj.input.password;
        //                comin.deviceuniqueid = obj.param.deviceuniqueid;
        //                comin.appusername = "iBand";
        //                comin.apppassword = "iBand";
        //                comin.CountryCode = obj.param.CountryCode;
        //                comin.os = obj.param.os;

        //                inp.input = reqObj;
        //                inp.param = comin;
        //                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
        //                client.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/json; charset=utf-8");

        //                HttpResponseMessage response = client.PostAsJsonAsync(url, inp).Result;

        //                if (response.IsSuccessStatusCode)
        //                {
        //                    if (response != null)
        //                    {
        //                        Task<String> ss = response.Content.ReadAsStringAsync();
        //                        var result = JsonConvert.DeserializeObject<Models.DTO<DebitTransactionsDTO>>(ss.Result);

        //                        if (result != null)
        //                        {
        //                            if (result.status.statuscode.Equals("0"))
        //                            {
        //                                debit = result.response;
        //                                resp.transactionDetails = debit;
        //                                //Insert into old wallet DebitTransactions
        //                                var oldDebit = OldWallet.DebitTransactionOld(inp);
        //                                if (oldDebit == 0)
        //                                {
        //                                    merchant = OldWallet.MerchantTransaction(obj, @refer, merchantdetails.Code, merchantdetails.Name);
        //                                }
        //                                resp.merchantDetails = merchant;
        //                                dto.response = resp;
        //                                dto.status = result.status;
        //                            }
        //                            else
        //                            {
        //                                dto.status = new Status(1);
        //                            }
        //                            return dto;
        //                        }
        //                    }
        //                }
        //            }
        //            catch (HttpRequestException e)
        //            {
        //                dto.status = new Models.Status(1);
        //            }
        //        }
        //    }
        //    catch
        //    {
        //        dto.status = new Models.Status(1);
        //    }
        //    return dto;
        //}

        public Models.DTO<SaveKYCDTO> SaveKYC(Models.Input<SaveKYC> obj)
        {
            Models.DTO<SaveKYCDTO> dto = new Models.DTO<SaveKYCDTO>();
            SaveKYCDTO resp = new SaveKYCDTO();
            dto.objname = "SaveKYC";

            try
            {
                if (string.IsNullOrEmpty(obj.param.username) || string.IsNullOrEmpty(obj.param.password))
                {
                    dto.status = new Models.Status(800);
                    return dto;
                }
                /* OneIn Login Object*/
                Models.Input<WalletLogin> login = new Input<WalletLogin>();
                WalletLogin @object = new WalletLogin();

                CommonInputParams comin = new CommonInputParams();
                comin.deviceuniqueid = obj.param.deviceuniqueid;
                comin.os = obj.param.os;
                comin.username = obj.param.username;
                comin.password = obj.param.password;

                @object.username = obj.param.username;
                @object.password = obj.param.password;
                login.input = @object;
                login.param = comin;
                /* ./OneIn Login Object*/
                if (!(isValidOneinLogin(login)))
                {
                    dto.status = new Models.Status(404);
                    return dto;
                }

                Profile exUser = isMobileNumberExist(obj.param.username);
                if (exUser == null)
                {
                    dto.status = new Status(2060);
                    return dto;
                }

                string uri = ConfigurationManager.AppSettings["walletapi"].ToString();
                string url = uri + "/SaveKYC";
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(url);
                if (url != null)
                {
                    try
                    {
                        JavaScriptSerializer jdes = new JavaScriptSerializer();
                        var reqObj = new SaveKYC();
                        reqObj.userid = string.IsNullOrEmpty(exUser.UserIdentifier.ToString()) ? "" : exUser.UserIdentifier.ToString();
                        reqObj.profileid = string.IsNullOrEmpty(exUser.ID.ToString()) ? "" : exUser.ID.ToString();
                        reqObj.firstname = string.IsNullOrEmpty(obj.input.firstname) ? "" : obj.input.firstname;
                        reqObj.lastname = string.IsNullOrEmpty(obj.input.lastname) ? "" : obj.input.lastname;
                        reqObj.fullname = string.IsNullOrEmpty(obj.input.fullname) ? "" : obj.input.fullname;
                        reqObj.dob = string.IsNullOrEmpty(obj.input.dob) ? "" : obj.input.dob;
                        reqObj.gender = string.IsNullOrEmpty(obj.input.gender) ? "" : obj.input.gender;
                        reqObj.Nationality = string.IsNullOrEmpty(obj.input.Nationality) ? "" : obj.input.Nationality;
                        reqObj.PlaceOfBirth = string.IsNullOrEmpty(obj.input.PlaceOfBirth) ? "" : obj.input.PlaceOfBirth;
                        reqObj.IDProof1No = string.IsNullOrEmpty(obj.input.IDProof1No) ? "" : obj.input.IDProof1No;
                        reqObj.IDProof2No = string.IsNullOrEmpty(obj.input.IDProof2No) ? "" : obj.input.IDProof2No;
                        reqObj.IDProof3No = string.IsNullOrEmpty(obj.input.IDProof3No) ? "" : obj.input.IDProof3No;
                        reqObj.IDProof1Image1 = string.IsNullOrEmpty(obj.input.IDProof1Image1) ? "" : obj.input.IDProof1Image1;
                        reqObj.IDProof1Image2 = string.IsNullOrEmpty(obj.input.IDProof1Image2) ? "" : obj.input.IDProof1Image2;
                        reqObj.IDProof2Image1 = string.IsNullOrEmpty(obj.input.IDProof2Image1) ? "" : obj.input.IDProof2Image1;
                        reqObj.IDProof2Image2 = string.IsNullOrEmpty(obj.input.IDProof2Image2) ? "" : obj.input.IDProof2Image2;
                        reqObj.IDProof3Image1 = string.IsNullOrEmpty(obj.input.IDProof3Image1) ? "" : obj.input.IDProof3Image1;
                        reqObj.IDProof3Image2 = string.IsNullOrEmpty(obj.input.IDProof3Image2) ? "" : obj.input.IDProof3Image2;

                        var request = reqObj;
                        Input<SaveKYC> inp = new Input<SaveKYC>();
                        inp.input = request;

                        client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                        client.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/json; charset=utf-8");

                        HttpResponseMessage response = client.PostAsJsonAsync(url, inp).Result;

                        if (response.IsSuccessStatusCode)
                        {
                            if (response != null)
                            {
                                Task<String> ss = response.Content.ReadAsStringAsync();
                                var result = JsonConvert.DeserializeObject<Models.DTO<SaveKYCDTO>>(ss.Result);

                                if (result != null)
                                {
                                    dto.response = result.response;
                                    dto.status = result.status;
                                    return dto;
                                }
                            }
                        }
                    }
                    catch (HttpRequestException e)
                    {
                        dto.status = new Models.Status(1);
                        return dto;
                    }
                }
            }
            catch (Exception ex)
            {
                dto.status = new Models.Status(1);
            }
            return dto;
        }

        public Models.DTO<GetProfileInfoDTO> GetProfileInfo(Models.Input<GetProfileInfo> obj)
        {
            Models.DTO<GetProfileInfoDTO> dto = new Models.DTO<GetProfileInfoDTO>();
            GetProfileInfoDTO resp = new GetProfileInfoDTO();
            GetProfileInfoDTO onse = new GetProfileInfoDTO();
            dto.objname = "GetProfileInfo";

            try
            {
                if (string.IsNullOrEmpty(obj.param.username) || string.IsNullOrEmpty(obj.param.password))
                {
                    dto.status = new Models.Status(800);
                    return dto;
                }
                Profile exUser = isMobileNumberExist(obj.param.username);
                if (exUser == null)
                {
                    dto.status = new Status(2060);
                    return dto;
                }

                /* OneIn Login Object*/
                Models.Input<WalletLogin> login = new Input<WalletLogin>();
                WalletLogin @object = new WalletLogin();

                CommonInputParams comin = new CommonInputParams();
                comin.deviceuniqueid = obj.param.deviceuniqueid;
                comin.os = obj.param.os;
                comin.username = obj.param.username;
                comin.password = obj.param.password;

                @object.username = obj.param.username;
                @object.password = obj.param.password;
                login.input = @object;
                login.param = comin;
                /* ./OneIn Login Object*/
                if (!(isValidOneinLogin(login)))
                {
                    dto.status = new Models.Status(404);
                    return dto;
                }
                string uri = ConfigurationManager.AppSettings["walletapi"].ToString();
                string url = uri + "/GetProfileInfo";
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(url);
                if (url != null)
                {
                    try
                    {
                        JavaScriptSerializer jdes = new JavaScriptSerializer();
                        var reqObj = new GetProfileInfo();
                        reqObj.userid = string.IsNullOrEmpty(exUser.UserIdentifier.ToString()) ? "" : exUser.UserIdentifier.ToString();
                        reqObj.mobile = string.IsNullOrEmpty(obj.input.mobile) ? "" : obj.input.mobile;

                        var request = reqObj;

                        Input<GetProfileInfo> inp = new Input<GetProfileInfo>();
                        inp.input = request;

                        client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                        client.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/json; charset=utf-8");

                        HttpResponseMessage response = client.PostAsJsonAsync(url, inp).Result;

                        if (response.IsSuccessStatusCode)
                        {
                            if (response != null)
                            {
                                Task<String> ss = response.Content.ReadAsStringAsync();
                                var result = JsonConvert.DeserializeObject<Models.DTO<GetProfileInfoDTO>>(ss.Result);

                                if (result != null)
                                {
                                    dto.response = result.response;
                                    dto.status = result.status;

                                    return dto;
                                }
                            }
                        }
                    }
                    catch (HttpRequestException e)
                    {
                        dto.status = new Models.Status(1);
                    }
                }
            }
            catch
            {
                dto.status = new Models.Status(1);
            }
            return dto;
        }

        #endregion

        #region PRIVATE methods
        private bool isValidOneinLogin(Models.Input<WalletLogin> obj)
        {
            Models.OneinInput<Models.Inputs.OneInInputs.Login> req = new OneinInput<Models.Inputs.OneInInputs.Login>();
            Models.Inputs.OneInInputs.Login log = new Models.Inputs.OneInInputs.Login();
            log.username = obj.input.username;
            log.password = obj.input.password;
            req.input = log;
            Models.OneInCommonInputParams param = new OneInCommonInputParams();
            param.apppassword = "iBand";
            param.appusername = "iBand";
            param.deviceuniqueid = obj.param.deviceuniqueid;
            param.username = obj.param.username;
            param.password = obj.param.password;
            param.os = obj.param.os;

            req.param = param;

            Loginresp = onein.Login(req);

            if (Loginresp.status.statuscode.Equals("0"))
            {
                return true;
            }
            return false;
        }

        private bool isValidOneinUser(Models.Input<VerifyUser> obj)
        {
            Models.OneinInput<Models.Inputs.OneInInputs.Verify> req = new OneinInput<Models.Inputs.OneInInputs.Verify>();
            Models.Inputs.OneInInputs.Verify log = new Models.Inputs.OneInInputs.Verify();
            log.mobile = obj.input.mobile;
            log.code = obj.input.code;
            log.appuserid = obj.input.appuserid;
            req.input = log;
            Models.OneInCommonInputParams param = new OneInCommonInputParams();
            param.apppassword = "iBand";
            param.appusername = "iBand";
            param.deviceuniqueid = obj.param.deviceuniqueid;
            param.username = obj.param.username;
            param.password = obj.param.password;
            param.os = obj.param.os;

            req.param = param;

            VerifyOnein = onein.Verify(req);

            if (VerifyOnein.status.statuscode.Equals("0"))
            {
                return true;
            }
            return false;
        }

        private bool OneinRegistration(Models.Input<WalletProfile> obj)
        {
            Models.OneinInput<Models.Inputs.OneInInputs.RegisterUser> req = new OneinInput<Models.Inputs.OneInInputs.RegisterUser>();
            Models.Inputs.OneInInputs.RegisterUser log = new Models.Inputs.OneInInputs.RegisterUser();
            log.firstname = obj.input.firstname;
            log.lastname = obj.input.lastname;
            log.fullname = obj.input.fullname;
            log.mobile = obj.input.mobileno;
            log.email = obj.input.email;
            log.countrycode = obj.input.countrycode;
            log.password = obj.input.password;
            log.dateofbirth = obj.input.dob;

            req.input = log;

            Models.OneInCommonInputParams param = new OneInCommonInputParams();
            param.apppassword = "iBand";
            param.appusername = "iBand";
            param.deviceuniqueid = obj.param.deviceuniqueid;
            param.username = obj.param.username;
            param.password = obj.param.password;
            param.os = obj.param.os;

            req.param = param;

            RegResp = onein.Register(req);

            if (RegResp.status.statuscode.Equals("0"))
            {
                return true;
            }
            return false;
        }

        private Profile isMobileNumberExist(string mobile)
        {
            var rows = db.Profiles.Where(x => x.Mobile.Equals(mobile)).SingleOrDefault();

            return rows;
        }
        private Profile isEmailExist(string email)
        {
            var rows = db.Profiles.Where(x => x.Email.Equals(email)).SingleOrDefault();

            return rows;
        }
        private Profile isUserExist(string email, string mobile)
        {
            var rows = db.Profiles.Where(x => x.Email.Equals(email) && x.Mobile.Equals(mobile)).SingleOrDefault();
            return rows;
        }
        private bool CheckLogin(string mobile, long userid)
        {
            var rows = db.Profiles.Where(x => x.Mobile.Equals(mobile) && x.UserIdentifier == userid).SingleOrDefault();

            if (rows == null || rows.Status == false)
            {
                return false;
            }
            else
                return true;
        }
        private DAL.WalletType WalletDetails(string walletname)
        {
            var rows = db.WalletTypes.Where(x => x.WalletTypeName.ToLower().Equals(walletname)).SingleOrDefault();
            return rows;
        }
        private Profile UserDetails(long userid)
        {
            var rows = db.Profiles.Where(x => x.UserIdentifier == userid).SingleOrDefault();
            return rows;
        }
        private DAL.WalletAccount WalletAccountDetails(long userid)
        {
            var rows = db.WalletAccounts.Where(x => x.UserIdentifier == userid).SingleOrDefault();
            return rows;
        }
        public WalletAccountsDTO GetWalletAccount(long userid)
        {
            WalletAccountsDTO walac = new WalletAccountsDTO();

            List<WalletDTO> accounts = new List<WalletDTO>();

            try
            {
                var rows = db.WalletAccounts.Where(x => x.UserIdentifier == userid).ToList();

                foreach (var row in rows)
                {
                    WalletDTO tran = new WalletDTO();
                    tran.userid = row.UserIdentifier.ToString();
                    tran.walletID = row.WalletID.ToString();
                    tran.walletTypeID = row.WalletTypeID.ToString();
                    tran.currency = row.Currency;
                    tran.balance = row.Balance.ToString();

                    tran.accountnumber = row.AccountNumber.ToString();

                    accounts.Add(tran);
                }

                walac.walletaccounts = accounts;
                return walac;
            }
            catch
            {
            }
            return walac;
        }
        public Merchant getMerchantDetails(int merchantid)
        {
            PayitMerchantsnWalletsEntities _dbcontext = new PayitMerchantsnWalletsEntities();
            var rows = _dbcontext.Merchants.Where(x => x.ID == merchantid).SingleOrDefault();
            return rows;
        }

        private bool dobPin(string msisdn, string paymentID)
        {
            Models.Input<Models.Inputs.dobInputs.PIN> req = new Input<Models.Inputs.dobInputs.PIN>();
            Models.Inputs.dobInputs.PIN log = new Models.Inputs.dobInputs.PIN();

            log.app = "iBand";
            log.appuser = "InternalService";
            log.template = "subscription";
            log.username = "isystest";
            log.password = "isys969";
            log.msisdn = msisdn;
            log.PaymentID = paymentID;
            log.authkey = "testauthkey";

            req.input = log;
       
            PinResp = dob.Pin(req);

            if (PinResp.status.statuscode.Equals("0"))
            {
                if(!(PinResp.response.result.ToLower().StartsWith("error")))
                {
                    return true;
                }
            }
            return false;
        }

        private bool dobCreateSubscription(string msisdn, string pin)
        {
            Models.Input<Models.Inputs.dobInputs.CreateSubscription> req = new Input<Models.Inputs.dobInputs.CreateSubscription>();
            Models.Inputs.dobInputs.CreateSubscription log = new Models.Inputs.dobInputs.CreateSubscription();
            log.app = "iBand";
            log.appuser = "InternalService";
            log.username = "isystest";
            log.password = "isys969";
            log.service = "test1";
            log.msisdn = msisdn;
            log.pin = pin;
            log.authkey = "testauthkey";
            req.input = log;

            CreateSubResp = dob.CreateSubscription(req);

            if (CreateSubResp.status.statuscode.Equals("0"))
            {
                return true;
            }
            return false;
        }


        #endregion
    }
}
