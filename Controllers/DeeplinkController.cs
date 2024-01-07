using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace PaymentVietQRCallAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DeeplinkController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public DeeplinkController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [HttpGet("Andorid")]
        public async Task<IActionResult> GetDeeplinksAndorid()
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync("https://api.vietqr.io/v2/android-app-deeplinks");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var appList = JsonConvert.DeserializeObject<AppList>(content);
                return Ok(appList);
            }

            return StatusCode((int)response.StatusCode, response.ReasonPhrase);
        }
        [HttpGet("ios")]
        public async Task<IActionResult> GetIosDeeplinks()
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync("https://api.vietqr.io/v2/ios-app-deeplinks");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var appList = JsonConvert.DeserializeObject<AppList>(content);
                return Ok(appList);
            }

            return StatusCode((int)response.StatusCode, response.ReasonPhrase);
        }
    }
}
