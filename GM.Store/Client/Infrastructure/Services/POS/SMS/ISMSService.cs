using GM.Store.Shared.Models;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace GM.Store.Client.Infrastructure.Services
{
    public interface ISMSService
    {
        Task<ResponseData<List<SmsTemplateModel>>> GetAllTempate();
        Task<ProductImportResponse> Import(UploadedFile model);
    }
}
