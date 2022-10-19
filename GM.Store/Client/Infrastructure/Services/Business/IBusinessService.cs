using GM.Store.Shared.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GM.Store.Client.Infrastructure.Services
{
    public interface IBusinessService
    {
        Task<TModel> GetAllAsync<TModel>()
           where TModel : class; 
        Task<TModel> GetCustomStyleAsync<TModel>()
           where TModel : class;
        Task<TModel> GetListAsync<TModel>()
           where TModel : class;
        BusinessModel StoreDetails { get; set; }
        TimeSpan? UserTimeSpan { get; set; }
        string CurrentPortal { get; set; }
        bool IsListEnabled { get; set; }
        Task<ResponseData<List<BusinessTimingModel>>> BusinessTimingAsyc(BusinessTimingRequest model);
    }
}
