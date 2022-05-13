using Medevia.Core.Domain;
using Microsoft.AspNetCore.Mvc;

namespace Medevia.API.UI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        #region Fields
        private readonly IWebHostEnvironment? _hostEnvironment;
        #endregion Fields

        #region constructor
        public HomeController(IWebHostEnvironment? hostEnvironment = null)
        {
            _hostEnvironment = hostEnvironment;
        }
        #endregion
        #region Public methods
        [HttpGet]
        [Route("hello")]
        public HelloMessage GetMessage(string name)
        {
            HelloMessage message = new()
            {
                MessageText = "Hello my Dear!",
                UserName = name
            };

            return message;
        }
        #endregion
    }
}
