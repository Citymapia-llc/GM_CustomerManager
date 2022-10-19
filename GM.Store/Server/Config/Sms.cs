using System.Net;
using System.Net.Http.Headers;

namespace GM.Store.Server.Config
{
    public class Sms
    {
        public Sms()
        {

        }

        public List<string> To { get; set; }

        public string Text { get; set; }

        public string AppId { get; set; }

        public string AppSecret { get; set; }

        public string SenderId { get; set; }

        public MessageType Type { get; set; }

        public string ApiUrl { get; set; }

        public async Task<bool> Send()
        {

            var receiver = string.Join(",", To);
            To.Select(a => receiver += a);
            var sms = Text;

            if (string.IsNullOrEmpty(ApiUrl))
                throw new Exception("Please set api url for SMS service!");
            var url = "?api_key=" + AppId + "&api_token=" + AppSecret + "&sender=" + SenderId + "&receiver=" + receiver + "&msgtype=1&sms=" + sms;

            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls
                   | SecurityProtocolType.Tls11
                   | SecurityProtocolType.Tls12
                   | SecurityProtocolType.Ssl3;


            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(ApiUrl);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                //GET Method
                HttpResponseMessage response = await client.GetAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

    }

    public enum MessageType
    {
        Text = 1,
        Flash = 2,
        Unicode = 3
    }
}
