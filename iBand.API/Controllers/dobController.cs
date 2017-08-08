using iBand.BL.Implementations;
using iBand.BL.Interfaces;
using iBand.Models;
using iBand.Models.Inputs.dobInputs;
using iBand.Models.Outputs.dobOutputs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace iBand.API.Controllers
{
    public class dobController : ApiController
    {
        Idob _dob = new dob();

        [HttpPost]
        public DTO<PINDTO> Pin(Models.Input<PIN> obj)
        {
            return _dob.Pin(obj);
        }

        [HttpPost]
        public DTO<CreateDTO> CreateSubscription(Models.Input<CreateSubscription> obj)
        {
            return _dob.CreateSubscription(obj);
        }

        [HttpPost]
        public DTO<DeleteSubscriptionDTO> DeleteSubscription(Models.Input<DeleteSubscription> obj)
        {
            return _dob.DeleteSubscription(obj);
        }

    }
}
