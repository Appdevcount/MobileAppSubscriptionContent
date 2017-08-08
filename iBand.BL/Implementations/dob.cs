using iBand.BL.Interfaces;
using iBand.Common;
using iBand.DAL;
using iBand.Models;
using iBand.Models.Inputs.dobInputs;
using iBand.Models.Outputs.dobOutputs;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace iBand.BL.Implementations
{
    public class dob : Idob
    {
        public DTO<PINDTO> Pin(Models.Input<PIN> obj)
        {
            Models.DTO<PINDTO> dto = new Models.DTO<PINDTO>();
            PINDTO resp = new PINDTO();
            dto.objname = "Pin";
            try
            {
                /* Check required parameters */
                if (string.IsNullOrEmpty(obj.input.username) || string.IsNullOrEmpty(obj.input.password) || string.IsNullOrEmpty(obj.input.authkey) || string.IsNullOrEmpty(obj.input.msisdn))
                {
                    dto.status = new Models.Status(800);
                    return dto;
                }

                if(!(string.IsNullOrEmpty(obj.input.PaymentID)))
                {
                    obj.input.service = getService(Convert.ToInt32(obj.input.PaymentID));
                }

                Authentication ar  = new Authentication();
                string hash = ar.UserAuth(obj.input.msisdn, obj.input.service);

                string uri = ConfigurationManager.AppSettings["oneglobalDOB"].ToString();
                string url = uri + "/Pin";
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(url);
                if (url != null)
                {
                    try
                    {
                        JavaScriptSerializer jdes = new JavaScriptSerializer();
                        var reqObj = new PIN();

                        reqObj.username = obj.input.username;
                        reqObj.password = obj.input.password;
                        reqObj.authkey = obj.input.authkey;
                        reqObj.service = obj.input.service;
                        reqObj.hash = hash;
                        reqObj.msisdn = obj.input.msisdn;
                        reqObj.language = obj.input.language;
                        reqObj.template = obj.input.template;
                        reqObj.appuser = "InternalService";

                        Input<PIN> inp = new Input<PIN>();
                        inp.input = reqObj;

                        client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                        client.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/json; charset=utf-8");

                        HttpResponseMessage response = client.PostAsJsonAsync(url, inp).Result;

                        if (response.IsSuccessStatusCode)
                        {
                            if (response != null)
                            {
                                Task<String> ss = response.Content.ReadAsStringAsync();
                                var result = JsonConvert.DeserializeObject<Models.DTO<PINDTO>>(ss.Result);

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

        public DTO<CreateDTO> CreateSubscription(Models.Input<CreateSubscription> obj)
        {
            Models.DTO<CreateDTO> dto = new Models.DTO<CreateDTO>();
            CreateDTO resp = new CreateDTO();
            dto.objname = "CreateSubscription";
            try
            {
                /* Check required parameters */
                if (string.IsNullOrEmpty(obj.input.msisdn))
                {
                    dto.status = new Models.Status(800);
                    return dto;
                }

                if (!(string.IsNullOrEmpty(obj.input.PaymentID)))
                {
                    obj.input.service = getService(Convert.ToInt32(obj.input.PaymentID));
                    if(string.IsNullOrEmpty(obj.input.service))
                    {
                        dto.status = new Status(800);
                        return dto;
                    }
                    obj.input.app = "InternalService";
                    obj.input.appuser = "iBand";
                    obj.input.username = "isystest";
                    obj.input.password = "isys969";
                    obj.input.authkey = "testauthkey";
                    obj.input.trial = "0";
                }

                Authentication ar = new Authentication();
                string hash = ar.UserAuth(obj.input.msisdn, obj.input.service);

                string uri = ConfigurationManager.AppSettings["oneglobalDOB"].ToString();
                string url = uri + "/CreateSubscription";
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(url);
                if (url != null)
                {
                    try
                    {
                        JavaScriptSerializer jdes = new JavaScriptSerializer();
                        var reqObj = new CreateSubscription();

                        reqObj.username = obj.input.username;
                        reqObj.password = obj.input.password;
                        reqObj.authkey = obj.input.authkey;
                        reqObj.service = obj.input.service;
                        reqObj.hash = hash;
                        reqObj.msisdn = obj.input.msisdn;
                        reqObj.pin = obj.input.pin;
                        reqObj.trial = obj.input.trial;
                        reqObj.appuser = "InternalService";

                        Input<CreateSubscription> inp = new Input<CreateSubscription>();
                        inp.input = reqObj;

                        client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                        client.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/json; charset=utf-8");

                        HttpResponseMessage response = client.PostAsJsonAsync(url, inp).Result;

                        if (response.IsSuccessStatusCode)
                        {
                            if (response != null)
                            {
                                Task<String> ss = response.Content.ReadAsStringAsync();
                                var result = JsonConvert.DeserializeObject<Models.DTO<CreateDTO>>(ss.Result);

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

        public DTO<DeleteSubscriptionDTO> DeleteSubscription(Models.Input<DeleteSubscription> obj)
        {
            Models.DTO<DeleteSubscriptionDTO> dto = new Models.DTO<DeleteSubscriptionDTO>();
            DeleteSubscriptionDTO resp = new DeleteSubscriptionDTO();
            dto.objname = "DeleteSubscription";
            try
            {
                /* Check required parameters */
                if (string.IsNullOrEmpty(obj.input.username) || string.IsNullOrEmpty(obj.input.password) || string.IsNullOrEmpty(obj.input.authkey) || string.IsNullOrEmpty(obj.input.hash) || string.IsNullOrEmpty(obj.input.msisdn))
                {
                    dto.status = new Models.Status(800);
                    return dto;
                }
                Authentication ar = new Authentication();
                string hash = ar.UserAuth(obj.input.msisdn, obj.input.service);

                string uri = ConfigurationManager.AppSettings["oneglobalDOB"].ToString();
                string url = uri + "/DeleteSubscription";
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(url);
                if (url != null)
                {
                    try
                    {
                        JavaScriptSerializer jdes = new JavaScriptSerializer();
                        var reqObj = new DeleteSubscription();

                        reqObj.username = obj.input.username;
                        reqObj.password = obj.input.password;
                        reqObj.authkey = obj.input.authkey;
                        reqObj.service = obj.input.service;
                        reqObj.hash = hash;
                        reqObj.msisdn = obj.input.msisdn;
                        reqObj.appuser = "InternalService";

                        Input<DeleteSubscription> inp = new Input<DeleteSubscription>();
                        inp.input = reqObj;

                        client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                        client.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/json; charset=utf-8");

                        HttpResponseMessage response = client.PostAsJsonAsync(url, inp).Result;

                        if (response.IsSuccessStatusCode)
                        {
                            if (response != null)
                            {
                                Task<String> ss = response.Content.ReadAsStringAsync();
                                var result = JsonConvert.DeserializeObject<Models.DTO<DeleteSubscriptionDTO>>(ss.Result);

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

    #region Private Methods

        private string getService(int paymentid)
        {
            iBandEntities dc = new iBandEntities();
            string service_name;
            var rows = dc.BillingPaymentsConfigurations.Where(x => x.ID == paymentid && x.Status == true).SingleOrDefault();

            if(rows !=null)
            {
                service_name = rows.ServiceName;
            }
            else
            {
                service_name = "";
            }

            return service_name;
        }

    #endregion

    }
}

   