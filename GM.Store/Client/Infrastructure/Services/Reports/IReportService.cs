using GM.Store.Client.Data;
using GM.Store.Shared.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GM.Store.Client.Infrastructure.Services
{
    public interface IReportService
    {
        Task<TModel> GetAllAsync<TModel>() where TModel : class;
        Task<ResponseData<dynamic>> GenerateReportDataAsync(ReportRequest model);
        Task<ResponseData<DayEndReportModel>> GenerateDayEndReportAsync(ReportRequest model);
        Task<ResponseData<List<ItemSalesReportModel>>> GenerateItemSalesReportAsync(ReportRequest model);

        Task<ResponseData<List<ItemSalesReportModel>>> GenerateComplementaryItemReportAsync(ReportRequest model);
        Task<ResponseData<List<ItemSalesReportModel>>> GenerateVoidItemReportAsync(ReportRequest model);
    }
}
