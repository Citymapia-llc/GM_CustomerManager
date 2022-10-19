using System;
using System.Collections.Generic;

namespace GM.Store.Shared.Models
{
    public class UserModel
    {
    }
    public class UserRequestModel
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int? Gender { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public DateTime? DateOfBirth { get; set; }

        public string ImageFile { get; set; }
    }
    public class UserResponseModel
    {
        public int UserId { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string FullName { get; set; }

        public string Country_code { get; set; }

        public string Password { get; set; }

        public string LoginType { get; set; }

        public string ImageUrl { get; set; }

        public string Address { get; set; }

        public int SubscriptionCount { get; set; }

        public int? Gender { get; set; }

        public string GenderString { get; set; }

        public DateTime? DateOfBirth { get; set; }

        public string UserName { get; set; }
    }

    #region POS Customer
    public class CustomerFilterModel
    {
        public string Keyword { get; set; }
    }
    public class CustomerRequestModel
    {
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public string Name { get; set; }
    }
    public class CustomerResponseModel
    {
        public int ApplicationId { get; set; }
        public int UserId { get; set; }
        public int TableId { get; set; }
        public int OrderId { get; set; }
        public string LocalOrderId { get; set; }
        public string? LocalId { get; set; }
        public string FirstName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
    }
    #endregion

    public class AppUserModel
    {
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public DateTime StartedDtm { get; set; }
        public DateTime CompletedDtm { get; set; }
    }
}
