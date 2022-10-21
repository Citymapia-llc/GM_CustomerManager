using GM.Store.Shared.Models;
using System.Threading.Tasks;

namespace GM.Store.Client.Infrastructure.Services
{
    public interface IUserService
    {
        Task<ResponseListData<UserResponseModel>> GetAllAsync(UserRequestModel model);
    }
}
