using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GM.Store.Shared.Models
{
    
    public class OrderRequestModel
    {
        public int ApplicationId { get; set; }
        public int? OrderStatus { get; set; }
    }
    public class UserOrderResponseModel
    {
        public List<OrderModel> Order { get; set; }
    }
    public class OrderModel
    {
        public int Id { get; set; }
        public int Amount { get; set; }
        public int AddressId { get; set; }
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
        public List<UserOrderDetailModel> OrderDetails { get; set; }
        public AddressModel ShippingAddress { get; set; }
        public UserOrderDeliveryModel Delivery { get; set; }
        public string Remark { get; set; }
    }

    public class PartialUserOrderRequest
    {
        public int TableId { get;set; }
        public int UserId { get; set; }
        public int OrderItemId { get; set; }
        public  int KOTStatus { get; set; }
        public string? LocalItemId { get; set; }
        public string? LocalOrderId { get; set; }
    }
    public class UpdateCustomerOrderRequest
    {
        public string? CustomerId { get; set; }
        public int TableId { get; set; }
        public int OrderId { get; set; }
        public string? LocalOrderId { get; set; }
    }
    public class OrderRemarkRequest
    {
        public int Id { get; set; }
        [Required]
        public string? Remark { get; set; }
        public int TableId { get; set; }
        public int OrderId { get; set; }
        public string? LocalItemId { get; set; }
        public string? LocalOrderId { get; set; }
    }
    public class OrderComplementary
    {
        public int Id { get; set; }
        [Required]
        public string? Reason { get; set; }
        [Required]
        public int Quantity { get; set; } = 1;
        public int TotalQuantity { get; set; }
        public bool IsComplementary { get; set; }
        public string? LocalOrderId { get; set; }
        public string? LocalItemId { get; set; }
        public string? Name { get; set; }
    }
    public class OrderComplementaryRequest
    {
        public int ItemOrderId { get; set; }
        public string? LocalOrderId { get; set; }
        public string? LocalItemId { get; set; }
    }
}
