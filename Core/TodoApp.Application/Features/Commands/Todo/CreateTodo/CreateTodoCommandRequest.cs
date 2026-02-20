using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TodoApp.Application.Features.Commands.Todo.CreateTodo
{
    public class CreateTodoCommandRequest : IRequest<CreateTodoCommandResponse>
    {
        public string Title { get; set; } = null!;
        public string? Description { get; set; }
        public DateTime TodoDate { get; set; }
    }
}
