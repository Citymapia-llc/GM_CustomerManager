namespace GM.Store.Server.Models
{
    public class Customer
    {
            public int ApplicationId { get; set; }
            public int UserId { get; set; }
            public string FirstName { get; set; }
            public string Email { get; set; }
            public string PhoneNumber { get; set; }
            public string LocalId { get; set; }
    }

    public class AppTeamUser
    {
        public int ApplicationId { get; set; }
        public int UserId { get; set; }
        public string? Role { get; set; }
        public string? FirstName { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string? PasswordHash { get; set; }
        public string? Code { get; set; }
    }
}
