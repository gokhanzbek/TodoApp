using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoApp.Application.Repositories.TodoApp.Application.Repositories.TodoItems;
using TodoApp.Domain.Entities;
using TodoApp.Persistence.Contexts;

namespace TodoApp.Persistence.Repositories.TodoItems
{
    public class TodoReadRepository : ReadRepository<TodoItem>, ITodoReadRepository
    {
        public TodoReadRepository(TodoAppDbContext context) : base(context)
        {
        }
    }
}
