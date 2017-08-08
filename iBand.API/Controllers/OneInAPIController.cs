using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using iBand.BL.Interfaces;
using iBand.BL.Implementations;
using System.Web.Http;

namespace iBand.API.Controllers
{
    public class OneInAPIController : ApiController
    {
        IOneIn _OneIn = new OneIn();

        [HttpPost]
        public Models.DTO<Models.Outputs.OneInOutputs.RegisterUser> Register(Models.OneinInput<Models.Inputs.OneInInputs.RegisterUser> obj)
        {
            return _OneIn.Register(obj);
        }


        [HttpPost]
        public Models.DTO<Models.Outputs.OneInOutputs.Login> Login(Models.OneinInput<Models.Inputs.OneInInputs.Login> obj)
        {
            return _OneIn.Login(obj);
        }



        [HttpPost]
        public Models.DTO<Models.Outputs.OneInOutputs.Verify> Verify(Models.OneinInput<Models.Inputs.OneInInputs.Verify> obj)
        {
            return _OneIn.Verify(obj);
        }


        [HttpPost]
        public Models.DTO<Models.Outputs.OneInOutputs.ForgotPassword> ForgotPassword(Models.OneinInput<Models.Inputs.OneInInputs.ForgotPassword> obj)
        {
            return _OneIn.ForgotPassword(obj);
        }

        [HttpPost]
        public Models.DTO<Models.Outputs.OneInOutputs.ModifyUserDetails> ModifyUserDetails(Models.OneinInput<Models.Inputs.OneInInputs.ModifyUserDetails> obj)
        {
            return _OneIn.ModifyUserDetails(obj);
        }



        //[HttpGet]
        //public string VerifyEmail(string custid)
        //{
        //    return _OneIn.VerifyEmail(custid);
        //}

        //[HttpGet]
        //public string sendEmail(string custid, string emailid)
        //{
        //    return _OneIn.sendEmail(custid, emailid);
        //}

        //[HttpGet]
        //public string sendSMS(string msisdn, string message)
        //{
        //    return _OneIn.sendSMSTest(msisdn, message);
        //}
    }
}