using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using iBand.BL.Interfaces;
using iBand.Common;
using iBand.DAL;
using iBand.Models;
using iBand.Models.Inputs.OneInInputs;
using System.Web.Script.Serialization;
using System.Net;
using Newtonsoft.Json;
using System.Net.Http;
using System.Configuration;


namespace iBand.BL.Implementations
{
    public class OneIn : IOneIn
    {
        //OneInEntities db = new OneInEntities();

        //DAL.App app = new DAL.App();
        //DAL.User user = new User();
        //DAL.Device device = new Device();
        //DAL.Profile profile = new Profile();

        public DTO<Models.Outputs.OneInOutputs.RegisterUser> Register(Models.OneinInput<Models.Inputs.OneInInputs.RegisterUser> obj)
        {
            Models.DTO<Models.Outputs.OneInOutputs.RegisterUser> dto = new Models.DTO<Models.Outputs.OneInOutputs.RegisterUser>();
            Models.Outputs.OneInOutputs.RegisterUser resp = new Models.Outputs.OneInOutputs.RegisterUser();
            dto.objname = "Register";
            try
            {
                if (string.IsNullOrEmpty(obj.param.appusername) || string.IsNullOrEmpty(obj.input.countrycode) || string.IsNullOrEmpty(obj.param.apppassword) || string.IsNullOrEmpty(obj.input.mobile) || string.IsNullOrEmpty(obj.input.password))
                {
                    dto.status = new Models.Status(800);
                    return dto;
                }

                string uri = ConfigurationManager.AppSettings["oneinapi"].ToString();
                string url = uri + "/Register";
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(url);
                if (url != null)
                {
                    try
                    {
                        JavaScriptSerializer jdes = new JavaScriptSerializer();
                        var reqObj = new RegisterUser();
                        var cominp = new OneInCommonInputParams();
                        cominp.apppassword = "iBand";
                        cominp.appusername = "iBand";
                        cominp.deviceuniqueid = obj.param.deviceuniqueid;
                        cominp.username = obj.param.username;
                        cominp.password = obj.param.password;
                        cominp.os = obj.param.os;

                        reqObj.firstname = obj.input.firstname;
                        reqObj.lastname = obj.input.lastname;
                        reqObj.fullname = obj.input.fullname;
                        reqObj.email = obj.input.email;
                        reqObj.mobile = obj.input.mobile;
                        reqObj.password = obj.input.password;
                        reqObj.countrycode = obj.input.countrycode;
                        reqObj.dateofbirth = obj.input.dateofbirth;

                        var request = reqObj;

                        OneinInput<Models.Inputs.OneInInputs.RegisterUser> inp = new OneinInput<Models.Inputs.OneInInputs.RegisterUser>();
                        inp.input = request;
                        inp.param = cominp;

                        client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                        client.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/json; charset=utf-8");

                        HttpResponseMessage response = client.PostAsJsonAsync(url, inp).Result;

                        if (response.IsSuccessStatusCode)
                        {
                            if (response != null)
                            {
                                Task<String> ss = response.Content.ReadAsStringAsync();
                                var result = JsonConvert.DeserializeObject<Models.DTO<Models.Outputs.OneInOutputs.RegisterUser>>(ss.Result);

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
            catch (Exception ex)
            {
                dto.status = new Models.Status(1);
            }
            return dto;
        }

        public DTO<Models.Outputs.OneInOutputs.Login> Login(Models.OneinInput<Models.Inputs.OneInInputs.Login> obj)
        {
            Models.DTO<Models.Outputs.OneInOutputs.Login> dto = new Models.DTO<Models.Outputs.OneInOutputs.Login>();
            Models.Outputs.OneInOutputs.Login resp = new Models.Outputs.OneInOutputs.Login();
            dto.objname = "Login";
            try
            {
                if (string.IsNullOrEmpty(obj.param.appusername) || string.IsNullOrEmpty(obj.param.apppassword) || string.IsNullOrEmpty(obj.input.username) || string.IsNullOrEmpty(obj.input.password))
                {
                    dto.status = new Models.Status(800);
                    return dto;
                }
                string uri = ConfigurationManager.AppSettings["oneinapi"].ToString();
                string url = uri + "/Login";
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(url);
                if (url != null)
                {
                    try
                    {
                        JavaScriptSerializer jdes = new JavaScriptSerializer();
                        var reqObj = new Login();
                        var cominp = new OneInCommonInputParams();
                        cominp.apppassword = "iBand";
                        cominp.appusername = "iBand";
                        cominp.deviceuniqueid = obj.param.deviceuniqueid;
                        cominp.username = obj.param.username;
                        cominp.password = obj.param.password;
                        cominp.os = obj.param.os;

                        reqObj.username = obj.input.username;
                        reqObj.password = obj.input.password;

                        var request = reqObj;

                        OneinInput<Models.Inputs.OneInInputs.Login> inp = new OneinInput<Models.Inputs.OneInInputs.Login>();
                        inp.input = request;
                        inp.param = cominp;


                        client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                        client.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/json; charset=utf-8");

                        HttpResponseMessage response = client.PostAsJsonAsync(url, inp).Result;

                        if (response.IsSuccessStatusCode)
                        {
                            if (response != null)
                            {
                                Task<String> ss = response.Content.ReadAsStringAsync();
                                var result = JsonConvert.DeserializeObject<Models.DTO<Models.Outputs.OneInOutputs.Login>>(ss.Result);

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

            catch (Exception ex)
            {
                dto.status = new Models.Status(1);
            }
            return dto;
        }

        public Models.DTO<Models.Outputs.OneInOutputs.Verify> Verify(Models.OneinInput<Models.Inputs.OneInInputs.Verify> obj)
        {
            Models.DTO<Models.Outputs.OneInOutputs.Verify> dto = new Models.DTO<Models.Outputs.OneInOutputs.Verify>();
            Models.Outputs.OneInOutputs.Verify resp = new Models.Outputs.OneInOutputs.Verify();
            dto.objname = "Verify";
            try
            {
                if (string.IsNullOrEmpty(obj.param.appusername) || string.IsNullOrEmpty(obj.param.apppassword) || string.IsNullOrEmpty(obj.input.mobile) || string.IsNullOrEmpty(obj.input.appuserid))
                {
                    dto.status = new Models.Status(800);
                    return dto;
                }

                string uri = ConfigurationManager.AppSettings["oneinapi"].ToString();
                string url = uri + "/Verify";
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(url);
                if (url != null)
                {
                    try
                    {
                        JavaScriptSerializer jdes = new JavaScriptSerializer();
                        var reqObj = new Verify();
                        var cominp = new OneInCommonInputParams();
                        cominp.apppassword = "iBand";
                        cominp.appusername = "iBand";
                        cominp.deviceuniqueid = obj.param.deviceuniqueid;
                        cominp.username = obj.param.username;
                        cominp.password = obj.param.password;
                        cominp.os = obj.param.os;

                        reqObj.code = obj.input.code;
                        reqObj.appuserid = obj.input.appuserid;
                        reqObj.mobile = obj.input.mobile;

                        var request = reqObj;

                        OneinInput<Verify> inp = new OneinInput<Verify>();
                        inp.input = request;
                        inp.param = cominp;

                        client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                        client.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/json; charset=utf-8");

                        HttpResponseMessage response = client.PostAsJsonAsync(url, inp).Result;

                        if (response.IsSuccessStatusCode)
                        {
                            if (response != null)
                            {
                                Task<String> ss = response.Content.ReadAsStringAsync();
                                var result = JsonConvert.DeserializeObject<Models.DTO<Models.Outputs.OneInOutputs.Verify>>(ss.Result);

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

        public Models.DTO<Models.Outputs.OneInOutputs.ForgotPassword> ForgotPassword(Models.OneinInput<Models.Inputs.OneInInputs.ForgotPassword> obj)
        {
            Models.DTO<Models.Outputs.OneInOutputs.ForgotPassword> dto = new Models.DTO<Models.Outputs.OneInOutputs.ForgotPassword>();
            Models.Outputs.OneInOutputs.ForgotPassword resp = new Models.Outputs.OneInOutputs.ForgotPassword();
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
                        var reqObj = new Models.Inputs.OneInInputs.ForgotPassword();
                        var cominp = new OneInCommonInputParams();
                        cominp.apppassword = "iBand";
                        cominp.appusername = "iBand";
                        cominp.deviceuniqueid = obj.param.deviceuniqueid;
                        cominp.username = obj.param.username;
                        cominp.password = obj.param.password;
                        cominp.os = obj.param.os;

                        reqObj.username = obj.input.username;

                        var request = reqObj;

                        OneinInput<Models.Inputs.OneInInputs.ForgotPassword> inp = new OneinInput<Models.Inputs.OneInInputs.ForgotPassword>();
                        inp.input = request;
                        inp.param = cominp;
                        client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                        client.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/json; charset=utf-8");

                        HttpResponseMessage response = client.PostAsJsonAsync(url, inp).Result;

                        if (response.IsSuccessStatusCode)
                        {
                            if (response != null)
                            {
                                Task<String> ss = response.Content.ReadAsStringAsync();
                                var result = JsonConvert.DeserializeObject<Models.DTO<Models.Outputs.OneInOutputs.ForgotPassword>>(ss.Result);

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
            catch (Exception ex)
            {
                dto.status = new Models.Status(1);
            }
            return dto;
        }

        public Models.DTO<Models.Outputs.OneInOutputs.ModifyUserDetails> ModifyUserDetails(Models.OneinInput<Models.Inputs.OneInInputs.ModifyUserDetails> obj)
        {
            Models.DTO<Models.Outputs.OneInOutputs.ModifyUserDetails> dto = new Models.DTO<Models.Outputs.OneInOutputs.ModifyUserDetails>();
            Models.Outputs.OneInOutputs.ModifyUserDetails resp = new Models.Outputs.OneInOutputs.ModifyUserDetails();
            dto.objname = "ModifyUserDetails";
            try
            {
                if (string.IsNullOrEmpty(obj.param.appusername) || string.IsNullOrEmpty(obj.param.apppassword) || string.IsNullOrEmpty(obj.input.username) || string.IsNullOrEmpty(obj.input.password))
                {
                    dto.status = new Models.Status(800);
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
                        var reqObj = new ModifyUserDetails();
                        var cominp = new OneInCommonInputParams();
                        cominp.apppassword = "iBand";
                        cominp.appusername = "iBand";
                        cominp.deviceuniqueid = obj.param.deviceuniqueid;
                        cominp.username = obj.param.username;
                        cominp.password = obj.param.password;
                        cominp.os = obj.param.os;

                        reqObj.username = obj.input.username;
                        reqObj.password = obj.input.password;
                        reqObj.newpassword = obj.input.newpassword;
                        reqObj.mobileNumber = obj.input.mobileNumber;
                        reqObj.emailID = obj.input.emailID;
                        reqObj.firstname = obj.input.firstname;
                        reqObj.lastname = obj.input.lastname;

                        var request = reqObj;
                        OneinInput<Models.Inputs.OneInInputs.ModifyUserDetails> inp = new OneinInput<Models.Inputs.OneInInputs.ModifyUserDetails>();
                        inp.input = request;
                        inp.param = cominp;
                        client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                        client.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/json; charset=utf-8");
                        HttpResponseMessage response = client.PostAsJsonAsync(url, inp).Result;
                        if (response.IsSuccessStatusCode)
                        {
                            if (response != null)
                            {
                                Task<String> ss = response.Content.ReadAsStringAsync();
                                var result = JsonConvert.DeserializeObject<Models.DTO<Models.Outputs.OneInOutputs.ModifyUserDetails>>(ss.Result);

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
            catch (Exception ex)
            {
                dto.status = new Models.Status(1);
            }
            return dto;
        }

        public Models.DTO<Models.Outputs.OneInOutputs.GetAddressDTO> GetAddress(Models.OneinInput<Models.Inputs.OneInInputs.GetAddress> obj)
        {
            Models.DTO<Models.Outputs.OneInOutputs.GetAddressDTO> dto = new Models.DTO<Models.Outputs.OneInOutputs.GetAddressDTO>();
            Models.Outputs.OneInOutputs.GetAddressDTO resp = new Models.Outputs.OneInOutputs.GetAddressDTO();
            dto.objname = "GetAddress";
            try
            {
                if (string.IsNullOrEmpty(obj.param.appusername) || string.IsNullOrEmpty(obj.param.apppassword) || string.IsNullOrEmpty(obj.input.mobile) || string.IsNullOrEmpty(obj.input.password))
                {
                    dto.status = new Models.Status(800);
                    return dto;
                }

                string uri = ConfigurationManager.AppSettings["oneinapi"].ToString();
                string url = uri + "/GetAddress";
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(url);
                if (url != null)
                {
                    try
                    {
                        JavaScriptSerializer jdes = new JavaScriptSerializer();
                        var reqObj = new GetAddress();
                        var cominp = new OneInCommonInputParams();
                        cominp.apppassword = "iBand";
                        cominp.appusername = "iBand";
                        cominp.deviceuniqueid = obj.param.deviceuniqueid;
                        cominp.username = obj.param.username;
                        cominp.password = obj.param.password;
                        cominp.os = obj.param.os;

                        reqObj.userid = obj.input.userid;
                        reqObj.mobile = obj.input.mobile;
                        reqObj.password = obj.input.password;

                        var request = reqObj;
                        OneinInput<Models.Inputs.OneInInputs.GetAddress> inp = new OneinInput<Models.Inputs.OneInInputs.GetAddress>();
                        inp.input = reqObj;
                        inp.param = cominp;
                        client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                        client.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/json; charset=utf-8");
                        HttpResponseMessage response = client.PostAsJsonAsync(url, inp).Result;
                        if (response.IsSuccessStatusCode)
                        {
                            if (response != null)
                            {
                                Task<String> ss = response.Content.ReadAsStringAsync();
                                var result = JsonConvert.DeserializeObject<Models.DTO<Models.Outputs.OneInOutputs.GetAddressDTO>>(ss.Result);

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
            catch (Exception ex)
            {
                dto.status = new Models.Status(1);
            }
            return dto;
        }

        public Models.DTO<Models.Outputs.OneInOutputs.SaveAddressDTO> SaveAddress(Models.OneinInput<Models.Inputs.OneInInputs.SaveAddress> obj)
        {
            Models.DTO<Models.Outputs.OneInOutputs.SaveAddressDTO> dto = new Models.DTO<Models.Outputs.OneInOutputs.SaveAddressDTO>();
            Models.Outputs.OneInOutputs.SaveAddressDTO resp = new Models.Outputs.OneInOutputs.SaveAddressDTO();
            dto.objname = "SaveAddress";
            try
            {
                if (string.IsNullOrEmpty(obj.param.appusername) || string.IsNullOrEmpty(obj.param.apppassword) || string.IsNullOrEmpty(obj.param.username) || string.IsNullOrEmpty(obj.param.password))
                {
                    dto.status = new Models.Status(800);
                    return dto;
                }

                string uri = ConfigurationManager.AppSettings["oneinapi"].ToString();
                string url = uri + "/SaveAddress";
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(url);
                if (url != null)
                {
                    try
                    {
                        JavaScriptSerializer jdes = new JavaScriptSerializer();
                        var reqObj = new Models.Inputs.OneInInputs.SaveAddress();
                        var reqAddr = new Models.Inputs.OneInInputs.Address();
                        var cominp = new OneInCommonInputParams();
                        cominp.apppassword = "iBand";
                        cominp.appusername = "iBand";
                        cominp.deviceuniqueid = obj.param.deviceuniqueid;
                        cominp.username = obj.param.username;
                        cominp.password = obj.param.password;
                        cominp.os = obj.param.os;

                        reqObj.userid = obj.input.userid;
                        reqObj.mobile = obj.input.mobile;

                        reqAddr.alias = obj.input.address.alias;
                        reqAddr.addressfield1 = obj.input.address.addressfield1;
                        reqAddr.addressfield2 = obj.input.address.addressfield2;
                        reqAddr.country = obj.input.address.country;
                        reqAddr.pincode = obj.input.address.pincode;
                        reqObj.address = reqAddr;

                        OneinInput<Models.Inputs.OneInInputs.SaveAddress> inp = new OneinInput<Models.Inputs.OneInInputs.SaveAddress>();
                        inp.input = reqObj;
                        inp.param = cominp;
                        client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                        client.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/json; charset=utf-8");
                        HttpResponseMessage response = client.PostAsJsonAsync(url, inp).Result;
                        if (response.IsSuccessStatusCode)
                        {
                            if (response != null)
                            {
                                Task<String> ss = response.Content.ReadAsStringAsync();
                                var result = JsonConvert.DeserializeObject<Models.DTO<Models.Outputs.OneInOutputs.SaveAddressDTO>>(ss.Result);

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
            catch (Exception ex)
            {
                dto.status = new Models.Status(1);
            }
            return dto;
        }

        //public string VerifyEmail(string custid)
        //{

        //    try
        //    {
        //        long appuserdeviceid = Convert.ToInt64(custid);
        //        AppUserDevice dev = db.AppUserDevices.Where(x => x.ID == appuserdeviceid).SingleOrDefault();
        //        if (dev == null)
        //        {
        //            return "Cannot verify your email.";
        //        }

        //        dev.isEmailVerified = true;


        //        User user = db.Users.Where(x => x.ID == dev.UserID).SingleOrDefault();
        //        user.isEmailVerified = true;

        //        db.SaveChanges();

        //        return "Email verification is successful. Thank you";
        //    }
        //    catch (Exception ex)
        //    {
        //        return "Error verifying your email. Please try again";
        //    }

        //}

    }
}
