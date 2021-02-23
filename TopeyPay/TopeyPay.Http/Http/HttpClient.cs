using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using TopeyPay.Shared.Contracts;

namespace TopeyPay.Http.Http
{
    public class HttpClientService
    {

        private readonly ILogWriter _logWriter;
        public HttpClientService(ILogWriter logWriter)
        {
            _logWriter = logWriter;
        }

        public async Task<bool> CallService(string url , object payload)
        {
            using (var client= new HttpClient())
            {
               
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.Timeout = new System.TimeSpan(0, 0, 1, 0);
                var json = JsonConvert.SerializeObject(payload);
                var stringContent = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
                var resp = client.PostAsync(url, stringContent).Result;
                var responseMessage= await resp.Content.ReadAsStringAsync();
                if (resp.IsSuccessStatusCode)
                {
                    // for the sake of the test if the request was 200 return true else false.
                    return  true;
                }
                //log request, respone and httpstatus
                _logWriter.LogWrite("Request:" + Environment.NewLine + json +Environment.NewLine+"Response"+
                    Environment.NewLine+ responseMessage+Environment.NewLine+"Error:" + resp.ReasonPhrase +
                    Environment.NewLine + "ErrorCode:" + resp.StatusCode);
                return false;
            }
        }
    }
}
