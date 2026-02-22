using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TodoApp.Application.Features.Queries.Todo.GetAllTodos
{
    public class GetAllTodosQueryResponse
    {
        public int TotalCount { get; set; } // Toplam kaç görev var?
        public object Todos { get; set; }   // Görevlerin listesi
    }
}
