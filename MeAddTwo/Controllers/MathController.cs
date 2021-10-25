using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Threading.Tasks;

namespace MeAddTwo.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MathController : ControllerBase
    {
        private readonly IHttpClientFactory _clientFactory;

        public MathController(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        [HttpGet]
        public async Task<int> Get(int number)
        {
            using var client = _clientFactory.CreateClient();
            
            var baseAddress = "me-add-one-service:5011";
            var response1st = await (await client.GetAsync($"http://{baseAddress}/Math?number={number}")).Content.ReadAsStringAsync();
            var response2st = await (await client.GetAsync($"http://{baseAddress}/Math?number={response1st}")).Content.ReadAsStringAsync();

            return int.Parse(response2st) + 1;
        }
    }
}
