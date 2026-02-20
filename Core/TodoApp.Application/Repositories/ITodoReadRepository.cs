using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoApp.Domain.Entities;

namespace TodoApp.Application.Repositories
{
    namespace TodoApp.Application.Repositories.TodoItems
    {
        public interface ITodoReadRepository : IReadRepository<TodoItem>
        {
        }
    }
