using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoApp.Application.Repositories;
using TodoApp.Domain.Entities;
using TodoApp.Persistence.Contexts;

namespace TodoApp.Persistence.Repositories.TodoItems
{
    public class TodoWriteRepository : WriteRepository<TodoItem>, ITodoWriteRepository
    {
        public TodoWriteRepository(TodoAppDbContext context) : base(context)
        {
        }
    }
}
