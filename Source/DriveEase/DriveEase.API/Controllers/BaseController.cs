using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DriveEase.API.Controllers
{
    [Route("[controller]")]
    [Produces("application/json")]
    public class BaseController : ControllerBase
    {
        protected IMediator mediator { get; set; }
        public BaseController(IMediator mediator)
        {
            this.mediator = mediator;
        }

    }
}