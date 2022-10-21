using GM.Store.Server.Database;
using GM.Store.Server.Helper;
using GM.Store.Server.Models;
using GM.Store.Shared.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GM.Store.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _log;
        private readonly IDataAccessManager _dataAccessManager;
        public UserController(ILogger<UserController> log, IDataAccessManager dataAccessManager)
        {
            _log = log;
            _dataAccessManager = dataAccessManager;
        }
        [Route("all")]
        [HttpPost]
        public async Task<IActionResult> GetAllUsers(UserRequestModel model)
        {
            try
            {

                var userResponse = await _dataAccessManager.GetAllUserAsync(model);

                return Ok(userResponse);

            }
            catch (Exception ex)
            {
                _log.LogError(ex, ex.Message);
            }
            return BadRequest(new ResponseListData<User> { Status = StatusCodes.Status500InternalServerError, Message = "Error" });
        }
    }
}
