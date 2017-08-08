using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iBand.BL.Interfaces
{
    public interface IOneIn
    {
        Models.DTO<Models.Outputs.OneInOutputs.Login> Login(Models.OneinInput<Models.Inputs.OneInInputs.Login> obj);

        Models.DTO<Models.Outputs.OneInOutputs.RegisterUser> Register(Models.OneinInput<Models.Inputs.OneInInputs.RegisterUser> obj);

        Models.DTO<Models.Outputs.OneInOutputs.Verify> Verify(Models.OneinInput<Models.Inputs.OneInInputs.Verify> obj);

        Models.DTO<Models.Outputs.OneInOutputs.ForgotPassword> ForgotPassword(Models.OneinInput<Models.Inputs.OneInInputs.ForgotPassword> obj);

        Models.DTO<Models.Outputs.OneInOutputs.ModifyUserDetails> ModifyUserDetails(Models.OneinInput<Models.Inputs.OneInInputs.ModifyUserDetails> obj);

        Models.DTO<Models.Outputs.OneInOutputs.GetAddressDTO> GetAddress(Models.OneinInput<Models.Inputs.OneInInputs.GetAddress> obj);

        Models.DTO<Models.Outputs.OneInOutputs.SaveAddressDTO> SaveAddress(Models.OneinInput<Models.Inputs.OneInInputs.SaveAddress> obj);

       // string VerifyEmail(string custid);
        //string sendEmail(string custid, string emailid);

        //string sendSMSTest(string msisdn, string message);
    }
}
