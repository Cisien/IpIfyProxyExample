using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace IpIfyProxyExample.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProxyController : ControllerBase
    {
        private readonly ILogger<ProxyController> _logger;
        private readonly HttpClient _client;

        public ProxyController(ILogger<ProxyController> logger, IHttpClientFactory clientFactory)
        {
            _logger = logger;
            _client = clientFactory.CreateClient();
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var response = await _client.GetAsync("https://api.ipify.org?format=json");
            if(!response.IsSuccessStatusCode)
            {
                return StatusCode((int)response.StatusCode);
            }

            var responseString = await response.Content.ReadAsStringAsync();
            return base.Content(responseString, response.Content.Headers.ContentType.ToString());
        }
    }
}
