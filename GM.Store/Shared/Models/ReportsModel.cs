using System;

namespace GM.Store.Shared.Models
{
    public class ReportsModel
    {
        public int Id { get; set; }
        public int ApplicationId { get; set; }
        public int Status { get; set; }
        public string Title { get; set; }
        public DateTime CreatedDtm { get; set; }
        public int CreatedBy { get; set; }
        public DateTime UpdatedDtm { get; set; }
        public int UpdatedBy { get; set; }
    }
    public class ReportRequest
    {
        public int Id { get; set; }
        public int ApplicationId { get; set; }
        public double TimeZoneOffset { get; set; }
        public DateTime DateFrom { get;  set; }
        public DateTime DateTo { get; set; }
    }
    public class OrderReportModel
    {
        public int Id { get; set; }
        public int? AddressId { get; set; }
        public string OrdersId { get; set; }
        public double? Discount { get; set; }
        public int? OrderStatus { get; set; }
        public string OrderStatusName { get; set; }
        public double? CodCharge { get; set; }
        public int? ShippingType { get; set; }
        public string TrackingId { get; set; }
        public string TrackingUrl { get; set; }
        public string DeliveryServiceName { get; set; }
        public DateTime? UpdatedDtm { get; set; }
        public double? DeliveryCharge { get; set; }
        public int? InvoiceStatus { get; set; }
        public int? GatewayId { get; set; }
        public string PaymentMethod { get; set; }
        public string ApplicationName { get; set; }
        public int ApplicationId { get; set; }
        public string ApplicationRelativeUrl { get; set; }
        public bool CancelRequest { get; set; }
        public bool RefundRequest { get; set; }
        public DateTime? OrderedOn { get; set; }
        public DateTime? DeliveredOn { get; set; }
        public DateTime? DispatchedOn { get; set; }
        public string Remark { get; set; }
    }
    public class DayEndReportModel
    {
        public int ApplicationId { get; set; }
        public double? CashSale { get; set; }
        public double? CreditSale { get; set; }
        public double? CardSale { get; set; }
        public double? TotalSale { get; set; }
    }
    public class ItemSalesReportModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double? TotalPrice { get; set; }
        public int? ApplicationId { get; set; }
        public int? TotalQuantity { get; set; }
    }
}
