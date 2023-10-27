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

        [HttpGet("1", Name = "/get01")]
        public IEnumerable<string> GetList1()
        {
            return new List<string>() { "one\n" };
        }

        [HttpGet("2", Name = "/get02")]
        public IEnumerable<string> GetList2()
        {
            return new List<string>() { "one\n", "two\n"};
        }

        [HttpGet("3", Name = "/get03")]
        public IEnumerable<string> GetList3()
        {
            return new List<string>() { "one\n", "two\n", "three\n" };
        }

        [HttpGet("4", Name = "/get04")]
        public IEnumerable<string> GetList4()
        {
            return new List<string>() { "one\n", "two\n", "three\n", "four\n" };
        }
    }
}
