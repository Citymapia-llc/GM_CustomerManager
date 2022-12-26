using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GM.Store.Shared.Models
{
    public class ReceiptModel
    {
        public int Id { get; set; }
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
    }
    public class ReceiptRequestModel
    {
        public string? Keyword { get; set; }
        public int PageSize { get; set; }
        public int CurrentPage { get; set; }
    }
    public class SmsBalanceModel
    {
        public float BalanceAmount { get; set; }
        public string? CurrenceCode { get; set; }
    }
    public class ConfirmedReciept
    {
        public string? SLNO { get; set; }
        public string? LocalId { get; set; }
        public string? ReceiptId { get; set; }
        public string? UserId { get; set; }
        public int LiveId { get; set; }
        public string? RecieptNo { get; set; }
        public DateTime CreatedOn { get; set; }
        public string? RecieptDate { get; set; }
        public string? DeliveryDate { get; set; }
        public string? CustomerName { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Brand { get; set; }
        public string? Model { get; set; }
        public string? Complaint { get; set; }
        public string Message { get; set; }
    }
}
