using DriveEase.SharedKernel;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace DriveEase.API.Controllers
{
    public class CarController : BaseController
    {
        private readonly ApplicationConfig config;
        public CarController(IMediator mediator, IOptionsSnapshot<ApplicationConfig> config) : base(mediator)
        {
            this.config = config.Value;
        }

        [HttpGet]
        [Route("/cars")]
        public async Task<IActionResult> GetCars()
        {
            return Ok();
        }
    }
}
