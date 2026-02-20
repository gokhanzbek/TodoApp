using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoApp.Domain.Entities;

namespace TodoApp.Application.Repositories
{
    public interface ITodoWriteRepository : IWriteRepository<TodoItem>
    {
    }
}
