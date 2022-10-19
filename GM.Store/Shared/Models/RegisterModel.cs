namespace GM.Store.Shared.Models
{
    public class RegisterModel
    {
        public string Email { get; set; }

        public string Phone { get; set; }
        //[Required]
        public string Name { get; set; }

        public string Country_code { get; set; }

        public string Password { get; set; }

        public string ConfirmPassword { get; set; }

        public string LoginType { get; set; }

        public bool TermsandConditions { get; set; }
    }
}
