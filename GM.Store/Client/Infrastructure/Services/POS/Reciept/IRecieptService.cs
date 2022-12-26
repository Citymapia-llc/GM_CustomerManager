using GM.Store.Shared.Models;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace GM.Store.Client.Infrastructure.Services
{
    public interface IRecieptService
    {
        Task<ResponseListData<ReceiptModel>> GetAll(ReceiptRequestModel model);
        Task<ResponseListData<ConfirmedReciept>> GetLog(ReceiptRequestModel model);
        Task<ProductImportResponse> Import(UploadedFile model);
        Task<ResponseSmsData<SmsBalanceModel>> CheckSmsBalanace();
    }
}
