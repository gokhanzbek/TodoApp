using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TodoApp.Application.Features.Commands.Todo.UpdateTodo
{
    public class UpdateTodoCommandRequest : IRequest<UpdateTodoCommandResponse>
    {
        public string Id { get; set; } = null!; // Hangi görev güncellenecek?
        public string Title { get; set; } = null!;
        public string? Description { get; set; }
        public DateTime TodoDate { get; set; }
        public bool IsCompleted { get; set; } // Görev tamamlandı mı?
    }
}
