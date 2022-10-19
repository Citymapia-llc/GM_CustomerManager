namespace GM.Store.Server.Models
{
    public class User
    {
        public string? LocalId { get; set; }
        public int UserId { get; set; }
        public string? UserName { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public bool IsToSync { get; set; }
    }
}
