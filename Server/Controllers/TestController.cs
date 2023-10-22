using Microsoft.AspNetCore.Mvc;

namespace CodeRoute.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TestController : ControllerBase
    {
        public TestController()
        {

        }

        [HttpGet("1", Name = "/get1")]
        public IEnumerable<string> GetList1()
        {
            return new List<string>() { "one\n" };
        }

        [HttpGet("2", Name = "/get2")]
        public IEnumerable<string> GetList2()
        {
            return new List<string>() { "one\n", "two\n"};
        }

        [HttpGet("3", Name = "/get3")]
        public IEnumerable<string> GetList3()
        {
            return new List<string>() { "one\n", "two\n", "three\n" };
        }

        [HttpGet("4", Name = "/get4")]
        public IEnumerable<string> GetList4()
        {
            return new List<string>() { "one\n", "two\n", "three\n", "four\n" };
        }
    }
}
