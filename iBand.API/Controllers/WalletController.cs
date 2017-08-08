using iBand.BL.Implementations;
using iBand.BL.Interfaces;
using iBand.Models;
using iBand.Models.Outputs.WalletOutputs;
using iBand.Models.Inputs.WalletInputs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace iBand.API.Controllers
{
    public class WalletController : ApiController
    {
        IWallet wallet = new Wallet();

        [HttpPost]
        public DTO<iBand.Models.Outputs.WalletOutputs.WalletLoginDTO> Login(Input<iBand.Models.Inputs.WalletInputs.WalletLogin> obj)
        {
            return wallet.Login(obj);
        }

        [HttpPost]
        public DTO<VerifyUserDTO> Verify(Input<VerifyUser> obj)
        {
            return wallet.Verify(obj);
        }

        [HttpPost]
        public DTO<iBand.Models.Outputs.WalletOutputs.ModifyUserDTO> ModifyUserDetails(Input<iBand.Models.Inputs.WalletInputs.ModifyUser> obj)
        {
            return wallet.ModifyUserDetails(obj);
        }

        [HttpPost]
        public DTO<iBand.Models.Outputs.WalletOutputs.WalletProfileDTO> Register(Input<iBand.Models.Inputs.WalletInputs.WalletProfile> obj)
        {
            return wallet.Register(obj);
        }

        [HttpPost]
        public DTO<ForgotPasswordDTO> ForgotPassword(Input<ForgotPassword> obj)
        {
            return wallet.ForgotPassword(obj);
        }

        [HttpPost]
        public DTO<CreditTransactionsDTO> CreditTransaction(Input<CreditTransactions> obj)
        {
            return wallet.CreditTransaction(obj);
        }

        [HttpPost]
        public DTO<DebitTransactionsDTO> DebitTransaction(Input<DebitTransactions> obj)
        {
            return wallet.DebitTransaction(obj);
        }

        [HttpPost]
        public DTO<TransactionHistoryDTO> GetTransactions(Input<TransactionHistory> obj)
        {
            return wallet.GetTransactions(obj);
        }

        [HttpPost]
        public DTO<WalletAccountsDTO> GetWalletAccounts(Input<WalletAccounts> obj)
        {
            return wallet.GetWalletAccounts(obj);
        }

    }
}