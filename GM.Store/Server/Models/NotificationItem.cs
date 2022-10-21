namespace GM.Store.Server.Models
{
    public class NotificationItem
    {
        public int ProjectId { get; set; }
        public int CurrentUserId { get; set; }
        public int? LeadId { get; set; }
        public int[] UserId { get; set; }
        public int Applicationid { get; set; }
        public string UserName { get; set; }

        public int? ProductId { get; set; }
        public string PhoneNumber { get; set; }
        public string Name { get; set; }
        public string ContactNumber { get; set; }
        public string ContactEmail { get; set; }
        public string Amount { get; set; }
        public string Template { get; set; }

        public string Message { get; set; }

        public string OrderId { get; set; }
        public string OtpCode { get; set; }
        public string Date { get; set; }
        public string TrackingUrl { get; set; }
        public string TrackingId { get; set; }
        public string ProductName { get; set; }
        public string CustomerPhone { get; set; }
    }
}
