namespace BlazorShop.Web.Server.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    [ApiController]
    [Route("api/[controller]")]
    public abstract class ApiController : ControllerBase
    {
        protected const string Id = "{id}";
        protected const string PathSeparator = "/";
    }
}
