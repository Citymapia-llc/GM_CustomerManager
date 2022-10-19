using GM.Store.Server.Config;
using System.Text.RegularExpressions;

namespace GM.Store.Server.Helper
{
    public interface ISmsHelper 
    {
        Task<int> SendAsync(List<NotificationItem> notify, string SmsSenderId = null, string notificationKey = null, int applicationId = 0, string SmsRegistrationName = "CITYMAPIA");
    }
        public class SmsHelper: ISmsHelper
    {

        private readonly ILogger<SmsHelper> _log;
        private readonly HttpClient _httpClient;
        private readonly ServiceConfiguration _serviceConfig;
        public SmsHelper(ILogger<SmsHelper> log, HttpClient httpClient, ServiceConfiguration serviceConfig)
        {
            _log = log;
            _httpClient = httpClient;   
            _serviceConfig = serviceConfig;
        }
        public async Task<int> SendAsync(List<NotificationItem> notify, string SmsSenderId = null, string notificationKey = null, int applicationId = 0, string SmsRegistrationName = "CITYMAPIA")
        {
            try
            {
                #region AppSettings keys
                string SmsKey = _serviceConfig.Sms.Key;
                string SmsSecret = _serviceConfig.Sms.Secret;
                string SmsApiUrl = _serviceConfig.Sms.ApiUrl;
                #endregion
                SmsRegistrationName = "CITYMAPIA";
                string SenderID = _serviceConfig.Sms.SenderID;
                foreach (var item in notify)
                {
                    var smsTemplate = item.Message;
                    if (!string.IsNullOrEmpty(smsTemplate))
                        smsTemplate = FormatTemplate(smsTemplate, item);
                    var smsMessage = $@"{smsTemplate}{SmsRegistrationName}";
                    var smsService = new Sms { AppId = SmsKey, AppSecret = SmsSecret, To = new List<string>(), Type = MessageType.Text, SenderId = SenderID, ApiUrl = SmsApiUrl };
                    smsService.Text = smsMessage;
                    smsService.To.Add(item.PhoneNumber.Replace(" ", ""));

                    if (await smsService.Send())
                    {
                        #region sms log
                        //var smsLog = new SmsLog().InsertSmsLog(new EntitySmsLog()
                        //{
                        //    ApplicationId = item.Applicationid,
                        //    UserId = item.CurrentUserId,
                        //    PhoneNumber = item.PhoneNumber,
                        //    Message = smsMessage,
                        //    CreatedDate = DateTime.UtcNow,
                        //    IsScheduler = 1
                        //});
                        #endregion
                    }
                }
            }
            catch (Exception ex)
            {
               
                _log.LogError(ex, ex.Message);
            }
            return 1;
        }

        public static string FormatTemplate(string template, NotificationItem model)
        {
            template = Regex.Replace(template, @"\<%.*%>", m => m.ToString().ToLower());
            template = template.Replace("<%orderid%>", model.OrderId);
            template = template.Replace("<%trackingurl%>", model.TrackingUrl);
            return template;
        }
    }
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
