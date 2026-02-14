using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TodoApp.Application.Features.Commands.AppUser.CreateUser;

namespace TodoApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        readonly IMediator _mediator;
        public UsersController(IMediator mediator)
        {
            _mediator = mediator;
        }
        public Task<IActionResult> CreateUser(CreateUserCommandRequest createUserCommandRequest)
        {
            return Ok();
        }



    }
}
