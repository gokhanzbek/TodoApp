using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TodoApp.Application.Features.Commands.Todo.RemoveTodo
{
    public class RemoveTodoCommandRequest : IRequest<RemoveTodoCommandResponse>
    {
        // Sadece silinecek kaydın ID'sini almamız yeterli
        public string Id { get; set; } = null!;
    }
}
