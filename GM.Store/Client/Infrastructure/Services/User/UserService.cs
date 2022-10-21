using GM.Store.Shared.Models;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace GM.Store.Client.Infrastructure.Services
{
    public class UserService : IUserService
    {
        private readonly IDataManager _dataManager;
        public UserService(IDataManager dataManager)
        {
            _dataManager = dataManager;
        }
        public async Task<ResponseListData<UserResponseModel>> GetAllAsync(UserRequestModel model)
        {
            var response = await _dataManager.PostAsync<ResponseListData<UserResponseModel>>(model, "api/user/all");
            return response;
        }
    }
}
