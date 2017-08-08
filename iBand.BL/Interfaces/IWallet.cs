using iBand.Models;
using iBand.Models.Inputs.OneInInputs;
using iBand.Models.Inputs.WalletInputs;
using iBand.Models.Outputs.WalletOutputs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iBand.BL.Interfaces
{
    public interface IWallet
    {

        DTO<WalletLoginDTO> Login(Input<WalletLogin> obj);
        DTO<WalletProfileDTO> Register(Input<WalletProfile> obj);
        DTO<VerifyUserDTO> Verify(Input<VerifyUser> obj);
        DTO<ModifyUserDTO> ModifyUserDetails(Input<ModifyUser> obj);
        DTO<CreditTransactionsDTO> CreditTransaction(Input<CreditTransactions> obj);
        DTO<DebitTransactionsDTO> DebitTransaction(Input<DebitTransactions> obj);
        DTO<TransactionHistoryDTO> GetTransactions(Input<TransactionHistory> obj);
        DTO<WalletAccountsDTO> GetWalletAccounts(Input<WalletAccounts> obj);
        DTO<ForgotPasswordDTO> ForgotPassword(Input<Models.Inputs.WalletInputs.ForgotPassword> obj);
        DTO<SaveKYCDTO> SaveKYC(Input<SaveKYC> obj);
        DTO<GetProfileInfoDTO> GetProfileInfo(Input<GetProfileInfo> obj);
        //  DTO<QRPayResponse> PayQR(Input<QRPayRequest> obj);
        DTO<QROrderResponseDTO> OrderQR(Input<QROrderRequest> obj);
       // DTO<PayItemResponseDTO> PayItem(Input<PayItemRequest> obj);
    }
}
