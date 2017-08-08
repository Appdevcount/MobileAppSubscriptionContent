using iBand.Models;
using iBand.Models.Inputs.dobInputs;
using iBand.Models.Outputs.dobOutputs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iBand.BL.Interfaces
{
    public interface Idob
    {
        DTO<PINDTO> Pin(Models.Input<PIN> obj);

        DTO<CreateDTO> CreateSubscription(Models.Input<CreateSubscription> obj);

        DTO<DeleteSubscriptionDTO> DeleteSubscription(Models.Input<DeleteSubscription> obj);
    }
}
