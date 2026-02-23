using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TodoApp.Application.Features.Queries.Todo.GetAllTodos
{
    public class GetAllTodosQueryRequest : IRequest<GetAllTodosQueryResponse>
    {
        public DateOnly? TargetDate { get; set; }
    }
}
