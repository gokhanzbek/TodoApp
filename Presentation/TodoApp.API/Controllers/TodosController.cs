using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TodoApp.Application.Features.Commands.Todo.CreateTodo;
using TodoApp.Application.Features.Commands.Todo.RemoveTodo;
using TodoApp.Application.Features.Commands.Todo.UpdateTodo;
using TodoApp.Application.Features.Queries.Todo.GetAllTodos;

namespace TodoApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodosController : ControllerBase
    {
        private readonly IMediator _mediator;

        public TodosController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> CreateTodo(CreateTodoCommandRequest request)
        {
            // İsteği MediatR'a gönderiyoruz, o gidip Handler'ı bulup çalıştıracak
            CreateTodoCommandResponse response = await _mediator.Send(request);

            return Ok(response);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllTodos([FromQuery] GetAllTodosQueryRequest request)
        {
            // İsteği MediatR'a gönderiyoruz, o gidip GetAllTodosQueryHandler'ı bulup çalıştıracak
            GetAllTodosQueryResponse response = await _mediator.Send(request);

            return Ok(response);
        }
        [HttpDelete("{Id}")] // ID'yi URL üzerinden (Route) alacağız
        public async Task<IActionResult> RemoveTodo([FromRoute] RemoveTodoCommandRequest request)
        {
            RemoveTodoCommandResponse response = await _mediator.Send(request);

            if (response.IsSuccess)
            {
                return Ok(response);
            }

            return BadRequest(response); // Bulunamazsa 400 hatası dönecek
        }
        [HttpPut]
        public async Task<IActionResult> UpdateTodo([FromBody] UpdateTodoCommandRequest request)
        {
            UpdateTodoCommandResponse response = await _mediator.Send(request);

            if (response.IsSuccess)
            {
                return Ok(response);
            }

            return BadRequest(response); // ID yanlışsa veya bulunamazsa 400 dönecek
        }
    }
}
