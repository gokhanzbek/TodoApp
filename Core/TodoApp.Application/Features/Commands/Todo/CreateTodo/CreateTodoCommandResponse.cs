using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TodoApp.Application.Features.Commands.Todo.CreateTodo
{
    public class CreateTodoCommandResponse
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; } = null!;

        // Frontend tarafında (veya Postman'de) eklenen yeni kaydın ID'sine 
        // anında ihtiyaç duyulabilir, o yüzden bunu dönmek çok iyi bir pratiktir.
        public Guid? TodoId { get; set; }
    }
}
