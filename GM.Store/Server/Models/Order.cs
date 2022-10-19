using GM.Store.Server.Helper;

namespace GM.Store.Server.Models
{
    public class Order
    {
        [System.Text.Json.Serialization.JsonConverter(typeof(LongToStringJsonConverter))]
        public string Id { get; set; }
        public int Amount { get; set; }
        public int AddressId { get; set; }
        public string? OrdersId { get; set; }
        public double? Discount { get; set; }
        public double? DiscountAmount { get; set; }
        public int? OrderStatus { get; set; }
        public string? OrderStatusName { get; set; }
        public double? CodCharge { get; set; }
        public int? ShippingType { get; set; }
        public string? TrackingId { get; set; }
        public string? TrackingUrl { get; set; }
        public string? DeliveryServiceName { get; set; }
        public DateTime? UpdatedDtm { get; set; }
        public double? DeliveryCharge { get; set; }
        public int? InvoiceStatus { get; set; }
        public int? GatewayId { get; set; }
        public int? PaymentType { get; set; }
        public string? PaymentMethod { get; set; }
        public string? ApplicationName { get; set; }
        public string? Remark { get; set; }
        public int ApplicationId { get; set; }
        public string? ApplicationRelativeUrl { get; set; }
        public bool CancelRequest { get; set; }
        public bool RefundRequest { get; set; }
        public DateTime? OrderedOn { get; set; }
        public DateTime? DeliveredOn { get; set; }
        public DateTime? DispatchedOn { get; set; }
        public List<OrderDetailModel> OrderDetails { get; set; }
        public OrderAddressModel ShippingAddress { get; set; }
        public OrderDeliveryModel Delivery { get; set; }
        public string? LocalOrderId { get; set; }
        public int TableId { get; set; }
        public string? CustomerId { get; set; }
        public int LiveId { get; set; }
    }
    public class OrderDetailModel
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string ProductCaption { get; set; }
        public string RelativePageUrl { get; set; }
        public string ImageUrl { get; set; }
        public double? UnitPrice { get; set; }
        public double? TotalPrice { get; set; }
        public int? Quantity { get; set; }
        public double? DeliveryCharge { get; set; }
        public string Remark { get; set; }
        public int OrderStatus { get; set; }
        public bool IsComplementary { get; set; }
        public int KOTNumber { get; set; }
        public int TableId { get; set; }
        public string TableNumber { get; set; }
        public string SectionName { get; set; }
        public string ComplementaryReason { get; set; }
    }
    public class OrderDeliveryModel
    {
        public int Id { get; set; }
        public int DeliveryPeriod { get; set; }
        public int ExpectedDay { get; set; }
        public DateTime? Date { get; set; }
        public string Time { get; set; }
    }
    public class RemarkRequest
    {
        public int Id { get; set; }
        public string? Remark { get; set; }
        public int TableId { get; set; }
        public int OrderId { get; set; }
        public string? LocalItemId { get; set; }
        public string? LocalOrderId { get; set; }
    }
}
