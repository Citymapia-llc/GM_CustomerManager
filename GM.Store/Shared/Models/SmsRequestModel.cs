using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GM.Store.Shared.Models
{
    public class SmsRequestModel
    {
        public string? SLNO { get; set; }
        public string? LocalId { get; set; }
        public string? UserId { get; set; }
        public int LiveId { get; set; }
        public string? RecieptNo { get; set; }
        public string? RecieptDate { get; set; }
        public string? DeliveryDate { get; set; }
        public string? CustomerName { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Brand { get; set; }
        public string? Model { get; set; }
        public string? Complaint { get; set; }
        public string Message { get; set; }
    }
    public class SmsTemplateModel
    {
        public int Id { get; set; }
        public string? Template { get; set; }
        public string? Title { get; set; }
    }
}
