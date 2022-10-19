using System;
using System.Collections.Generic;

namespace GM.Store.Shared.Models
{
    
    public class OrderHistoryRequestModel
    {
        public string? SearchKey { get; set; }
        public int skip { get; set; } = 0;
        public int Take { get; set; } = 10;
        public DateTime DateFrom { get; set; }
        public string? StartDate { get; set; }
        public DateTime DateTo { get; set; }
        public string? EndDate { get; set; }
        public string? SortKey { get; set; }
        public string? SortOrder { get; set; }
        public int? StatusValue { get; set; }
        public int? gatewayValue { get; set; }
        public int Sort { get; set; }
        public int PortfolioId { get; set; }
        public int? MinTotal { get; set; }
        public int? MaxTotal { get; set; }
    }
    //#region Old 
    //public class OrderHistoryResponseModel
    //{
    //    public List<OrderHistoryModel> Order { get; set; }
    //}
    //public class OrderHistoryModel
    //{
    //    public int Id { get; set; }
    //    public int Amount { get; set; }
    //    public int AddressId { get; set; }
    //    public string OrdersId { get; set; }
    //    public double? Discount { get; set; }
    //    public int? OrderStatus { get; set; }
    //    public string OrderStatusName { get; set; }
    //    public double? CodCharge { get; set; }
    //    public int? ShippingType { get; set; }
    //    public string TrackingId { get; set; }
    //    public string TrackingUrl { get; set; }
    //    public string DeliveryServiceName { get; set; }
    //    public DateTime? UpdatedDtm { get; set; }
    //    public double? DeliveryCharge { get; set; }
    //    public int? InvoiceStatus { get; set; }
    //    public int? GatewayId { get; set; }
    //    public string PaymentMethod { get; set; }
    //    public string ApplicationName { get; set; }
    //    public int ApplicationId { get; set; }
    //    public string ApplicationRelativeUrl { get; set; }
    //    public bool CancelRequest { get; set; }
    //    public bool RefundRequest { get; set; }
    //    public DateTime? OrderedOn { get; set; }
    //    public DateTime? DeliveredOn { get; set; }
    //    public DateTime? DispatchedOn { get; set; }
    //    public List<UserOrderDetailModel> OrderDetails { get; set; }
    //    public AddressModel ShippingAddress { get; set; }
    //    public UserOrderDeliveryModel Delivery { get; set; }
    //} 
    //#endregion

    public class OrderHistoryResponse
    {
        public int Id { get; set; }
        public string OrderId { get; set; }
        public string OrderUserName { get; set; }
        public double? Amount { get; set; }
        public int? OrderType { get; set; }
        public int? AddressId { get; set; }
        public int? OrderStatus { get; set; }
        public int? ApplicationId { get; set; }
        public int? GatewayId { get; set; }
        public int RefundRequest { get; set; }
        public int CancelRequest { get; set; }
        public string OrderStatusName { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? OrderedOn { get; set; }
        public string OrderedDtm
        {
            get
            {
                if (this.OrderedOn != null && this.OrderedOn != DateTime.MinValue)
                    return this.OrderedOn.GetValueOrDefault().ToString("MMM dd yyyy H:mm:ss");
                return null;
            }
        }
        public string CreatedDtm
        {
            get
            {
                if (this.CreatedDate != null)
                    return this.CreatedDate.GetValueOrDefault().ToString("MMM dd yyyy H:mm:ss");
                return null;
            }
        }
        public DateTime? UpdatedDate { get; set; }
        public string UpdatedDtm
        {
            get
            {
                if (this.UpdatedDate != null)
                    return this.UpdatedDate.GetValueOrDefault().ToString("MMM dd yyyy H:mm:ss");
                return null;
            }
        }
    }
}
