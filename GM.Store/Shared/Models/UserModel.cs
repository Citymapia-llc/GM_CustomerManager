using System;
using System.Collections.Generic;

namespace GM.Store.Shared.Models
{
   
   
    public class UserResponseModel
    {
        public string? LocalId { get; set; }
        public int UserId { get; set; }
        public string? UserName { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public bool IsToSync { get; set; }
    }

    public class UserRequestModel
    {
        public string? Keyword { get; set; }
        public int PageSize { get; set; }
        public int CurrentPage { get; set; }
    }
}
