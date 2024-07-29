using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EMC_DB_Core.Controllers
{
    [ApiController]
    public class ValuesController : ControllerBase
    {
        [Route("api/kaka")]
        public string GetAll()
        {
            return "kekekeke";
        }
    }
}
