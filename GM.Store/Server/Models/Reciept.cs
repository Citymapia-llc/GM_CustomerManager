namespace GM.Store.Server.Models
{
    public class Reciept
    {
        public string? LocalId { get; set; }
        public string? SLNO { get; set; }
        public int LiveId { get; set; }
        public string? RecieptNo { get; set; }
        public string? RecieptDate { get; set; }
        public string? DeliveryDate { get; set; }
        public string? CustomerName { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Brand { get; set; }
        public string? Model { get; set; }
        public string? Complaint { get; set; }
        public string? UserId { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime LastUpdatedOn { get;  set; }
        public bool IsToSync { get;  set; }
    }
    
}
