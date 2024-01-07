using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using RestSharp;
using System.Text;

namespace PaymentVietQRCallAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PaymentController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public PaymentController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        [HttpPost("generateVietQR")]
        public async Task<IActionResult> GenerateVietQR(string clientId, string apiKey, ApiRequest request)
        {
            var client = _httpClientFactory.CreateClient();

            var jsonRequest = JsonConvert.SerializeObject(request);
            var content = new StringContent(jsonRequest, Encoding.UTF8, "application/json");

            client.DefaultRequestHeaders.Add("x-client-id", clientId);
            client.DefaultRequestHeaders.Add("x-api-key", apiKey);

            var response = await client.PostAsync("https://api.vietqr.io/v2/generate", content);

            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                return Ok(responseContent); // hoặc phân tích cú pháp theo cấu trúc dữ liệu cần thiết
            }
            else
            {
                return StatusCode((int)response.StatusCode, response.ReasonPhrase);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetBanks()
        {
            using (var client = _httpClientFactory.CreateClient())
            {
                var response = await client.GetAsync("https://api.vietqr.io/v2/banks");
                var content = await response.Content.ReadAsStringAsync();
                var banks = JsonConvert.DeserializeObject<Bank>(content);

                // Trả về danh sách ngân hàng dưới dạng JSON hoặc theo format mong muốn
                return Json(banks);
            }
        }
    }
}
