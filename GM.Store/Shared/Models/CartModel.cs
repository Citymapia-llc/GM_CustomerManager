using System;
using System.Collections.Generic;

namespace GM.Store.Shared.Models
{
    public class SyncCartModel
    {
        public List<CartModel> Data { get; set; }
    }
    public class CartModel : IDisposable
    {
        public  string? Id { get; set; }
        public  string? LocalId { get; set; }
        public int LiveId { get; set; }
        public int TableId { get; set; }
        public string? CustomerId { get; set; }
        public int SectionId { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime LastUpdatedOn { get; set; }
        public bool IsOrdered { get; set; }
        public bool ItemToRemove { get; set; }
        public NavCartModel OrderSummary { get; set; }
        public List<ViewCartModel> Items { get; set; }
        public CustomerResponseModel Customer { get; set; }
        public string? ExternalOrderId { get; set; }
        public DateTime OrderedOn { get; set; }

        public void Dispose()
        {
            this.OrderSummary = null;
            this.Items = null;
        }
    }
    public class NavCartModel
    {
        public int? Status { get; set; }
        public int? OrderId { get; set; }
        public int? Count { get; set; }
        public double? TotalPrice { get; set; }
        public double? DeliveryCharge { get; set; }
        public double? TotalPayableAmount { get; set; }
        public string? OrderNumber { get; set; }
        public float? Discount { get; set; }
        public float? DiscountAmount { get; set; }
        public string? Remark { get; set; }
        public int? PaymentType { get; set; }
    }
    public class ViewCartModel
    {
        public int Id { get; set; }
        public  string? LocalId { get; set; }
        public  string? LocalOrderId { get; set; }
        public int OrderId { get; set; }
        public int ParentId { get; set; }
        public int ApplicationId { get; set; }
        public double? UnitPrice { get; set; }
        public double? TotalPrice { get; set; }
        public int? Quantity { get; set; }
        public double? DeliveryCharge { get; set; }
        public int OrderStatus { get; set; }
        public string? Remark { get; set; }
        public int? TotalProductQuantity { get; set; }
        public string? ParentName { get; set; }
        public string? ParentCaption { get; set; }
        public string? Description { get; set; }
        public string? ParentRelativeUrl { get; set; }
        public string? ImageUrl { get; set; }
        private int? StockId { get; set; }
        private int? AvailableStock { get; set; }
        private int? Stock { get; set; }
        public bool? COD { get; set; }
        public int? KOTNumber { get; set; }
        public int? KOTStatus { get; set; }
        public int TableId { get; set; }
        public string? TableName { get; set; }
        public DateTime KOTDtm { get; set; }
        public DateTime CompletedDtm { get; set; }
        public DateTime StartedDtm { get; set; }
        public bool IsComplementary { get; set; }
        public string? ComplementaryReason { get; set; }
        public int ComplementaryQuantity { get; set; }
        public string? SectionName { get; set; }
        public AppUserModel Chef { get; set; }
        public  string? TableNumber { get; set; }
        public DateTime OrderedOn { get; set; }
    }
    public class TableOrderModel
    {
        public string? CustomerId { get; set; }
        public int TableId { get; set; }
        public int ApplicationId { get; set; }
        public int OrderId { get; set; }
        public  string? LocalOrderId { get; set; }
        public  string? ExternalOrderId { get; set; }
    }
    public class PartialCartModel
    {
        public CustomerResponseModel Customer { get; set; }
        public NavCartModel OrderSummary { get; set; }
        public List<ViewCartModel> Items { get; set; }
    }
    public class CartDetailRequest
    {
        public int OrderId { get; set; }
        public string? LocalOrderId { get; set; } }
   
    public class AddToCartRequestModel
    {
        public int OrderId { get; set; }
        public string? LocalOrderId { get; set; }
        public int ParentId { get; set; }
        public int Quantity { get; set; } = 1;
        public int StockQuantity { get; set; }
        public int UserId { get; set; }
        public int TableId { get; set; }
        public int SectionId { get; set; }
        public string? TableName { get; set; }
        public string? SectionName { get; set; }
        public  string? CustomerId { get; set; }
        public bool IsQuantityUpdate { get; set; }
        public bool  infinityStock { get; set; }
        public string? ExternalOrderId { get; set; }
        public string? LocalItemId { get; set; }
    }
    public class ViewCartRequestModel
    {
        public int UserId { get; set; }
        public int orderId { get; set; }
    }
    public class DeleteItemRequestModel
    {
        public int TableId { get; set; }
        public string? CustomerId { get; set; }
        public int ParentId { get; set; }
        public string? ActiveOrderId { get; set; }
        public string? LocalItemId { get; set; }
        public int Id { get; set; }
    }
    public class ComplementaryitemRequestModel
    {
        public int OrderItemId { get; set; }
        public  string? Reason { get; set; }
        public int Quantity { get; set; }
        public string? LocalOrderId { get; set; }
        public string? LocalItemId { get; set; }
    }
    public class CancelItemRequestModel
    {
        public string? LocalOrderId { get; set; }
        public string? LocalItemId { get; set; }
        public int OrderItemId { get; set; }
        public string? Remark { get; set; }
    }
    public class ItemUpdateRequestModel
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int TableId { get; set; }
        public string? LocalItemId { get; set; }
        public string? ActiveOrderId { get; set; }
        public string? CustomerId { get; set; }
        public int ParentId { get; set; }
        public int Quantity { get; set; }
    }
    public class CreatePartialOrderRequest
    {
        public int TableId { get; set; }
        public int OrderId { get; set; }
        public string? LocalOrderId { get; set; }
    }
    public class OrderPlacedRequestModel
    {
        public int orderId { get; set; }
        public int LocationId { get; set; }
    }

    public class PlaceOrderResponseModel
    {
        public int OrderId { get; set; }
        public List<AddressModel> ShippingAddress { get; set; }
    }

    public class ConfirmOrderRequestModel
    {
        public int OrderId { get; set; }
        public int LocationId { get; set; }
        public string? PCode { get; set; }
        public string? ParentId { get; set; }
        public string? PaymentType { get; set; }
        public string? Remark { get; set; }
    }
    public class CompleteOrderRequestModel
    {
        public int OrderId { get; set; }
        public int LocationId { get; set; }
        public string? PCode { get; set; }
        public string? ParentId { get; set; }
        public string? PaymentType { get; set; }
        public string? Remark { get; set; }
        public string? CustomerId { get; set; }
        public int PaymentMethod { get; set; }
        public int TableId { get; set; }
        public string? LocalOrderId { get; set; }

        public float Discount { get; set; }
        public float DiscountAmount { get; set; }
    }
    public class RazorPayRequestModel
    {
        public string? OrderNumber { get; set; }
        public  string? PaymentId { get; set; }
        public  string? PaymentRequestId { get; set; }
    }
    public class PaymentRequestModel
    {
        public int TableId { get; set; }
        public string? LocalOrderId { get; set; }
        public string? OrderNumber { get; set; }
        public string? PaymentType { get; set; }
        public bool IsOrderCompleted { get; set; }
    }

  
    public class OrderPlacedResponseModel
    {
        public int OrderId { get; set; }
        public int LocationId { get; set; }
        public string? PCode { get; set; }
        public string? ParentId { get; set; }
        public string? Remark { get; set; }
        public List<ViewCartModel> OrderDetails { get; set; }
    }
    public class ConfirmOrderResponseModel
    {
        public string? id { get; set; }
        public string? LocalOrderId { get; set; }
        public string? phone { get; set; }
        public string? email { get; set; }
        public string? buyer_name { get; set; }
        public string? amount { get; set; }
        public string? OrderNumber { get; set; }
        public string? status { get; set; }
        public bool send_sms { get; set; }
        public bool send_email { get; set; }
        public string? redirect_url { get; set; }
        public string? webhook { get; set; }
        public bool allow_repeated_payments { get; set; }
        public string? Razorpay_apikey { get; set; }
        public string? Razorpay_authtoken { get; set; }
        public bool IsTickets { get; set; }
        public string? Order_id { get; set; }
        public int? ApplicationId { get; set; }
        public string? Billing_address { get; set; }
        public string? Billing_zip { get; set; }
        public string? Billing_state { get; set; }
        public string? Billing_country { get; set; }
        public string? Billing_city { get; set; }
        public string? error_message { get; set; }
        public bool IsHomeEntry { get; set; }
        public int? UserId { get; set; }
        public string? RazorpayKey { get; set; }
        public string? Currency { get; set; }
        public string? payment_request_id { get; set; }
        public int RazorpayAmount { get; set; }
        public string? Cancel_url { get; set; }
        public string? PaymentType { get; set; }
        public string? Logo { get; set; }
        public int TableId { get; set; }
    }

    public class OrderSuccessModel
    {
        public int Id { get; set; }
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
        public List<UserOrderDetailModel> OrderDetails { get; set; }
        public AddressModel ShippingAddress { get; set; }
        public UserOrderDeliveryModel Delivery { get; set; }
        public string? LocalOrderId { get; set; }
        public int TableId { get; set; }
        public string? CustomerId { get; set; }
    }
    public class UserOrderDetailModel
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public  string? ProductName { get; set; }
        public  string? ProductCaption { get; set; }
        public  string? RelativePageUrl { get; set; }
        public  string? ImageUrl { get; set; }
        public double? UnitPrice { get; set; }
        public double? TotalPrice { get; set; }
        public int? Quantity { get; set; }
        public double? DeliveryCharge { get; set; }
        public  string? Remark { get; set; }
        public int OrderStatus { get; set; }
        public bool IsComplementary { get; set; }
        public int KOTNumber { get; set; }
        public int TableId { get; set; }
        public  string? TableNumber { get; set; }
        public  string? SectionName { get; set; }
        public  string? ComplementaryReason { get; set; }
    }
    public class UserOrderDeliveryModel
    {
        public int Id { get; set; }
        public int DeliveryPeriod { get; set; }
        public int ExpectedDay { get; set; }
        public DateTime? Date { get; set; }
        public  string? Time { get; set; }
    }
    public class RestaurantSection
    {
        public int Id { get; set; }
        public  string? Title { get; set; }
        public int SectionType { get; set; }
        public List<TableModel> Tables { get; set; }
    }
}
