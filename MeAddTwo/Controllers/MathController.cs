using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Net.Http;
using System.Threading.Tasks;

namespace MeAddTwo.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MathController : ControllerBase
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly IConfiguration _configuration;

        public MathController(IHttpClientFactory clientFactory, IConfiguration configuration)
        {
            _clientFactory = clientFactory;
            _configuration = configuration;
        }

        [HttpGet]
        public async Task<int> Get(int number)
        {
            using var client = _clientFactory.CreateClient();
            
            var baseAddress = _configuration.GetValue<string>("me-add-one-uri");
            var response1st = await (await client.GetAsync($"http://{baseAddress}/Math?number={number}")).Content.ReadAsStringAsync();
            var response2st = await (await client.GetAsync($"http://{baseAddress}/Math?number={response1st}")).Content.ReadAsStringAsync();

            return int.Parse(response2st);
        }
    }
}
