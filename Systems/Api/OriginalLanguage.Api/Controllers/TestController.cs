using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace OriginalLanguage.Api.Controllers
{
    //[Route("api/[controller]")]
    [Route("v{version:apiVersion}/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        [ProducesResponseType(typeof(IEnumerable<string>), 200)]  // For OpenApi
        [ApiVersion("1.0")]
        [HttpGet("")]
        public async Task<IEnumerable<string>> GetData1()
        {
            var res = new List<string>()
            {
                "data1",
                "data2",
                "data3",
                "some data4"
            };
            return res;
        }

        [ProducesResponseType(typeof(IEnumerable<string>), 200)]  // For OpenApi
        [ApiVersion("2.0")]
        [HttpGet("")]
        public async Task<IEnumerable<string>> GetData2(string? prefix)
        {
            var res = new List<string>()
            {
                prefix + "_data1",
                prefix + "_data2",
                prefix + "_data3",
                prefix + "_some data4"
            };
            return res;
        }

        [HttpGet("exception")]
        [ApiVersion("1.0")]
        public async Task<string> GetException()
        {
            throw new Exception("Exception!");
        }
    }
}
