using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoApp.Application.Repositories.TodoApp.Application.Repositories.TodoItems;

namespace TodoApp.Application.Features.Queries.Todo.GetAllTodos
{
    public class GetAllTodosQueryHandler : IRequestHandler<GetAllTodosQueryRequest, GetAllTodosQueryResponse>
    {
        private readonly ITodoReadRepository _todoReadRepository;

        public GetAllTodosQueryHandler(ITodoReadRepository todoReadRepository)
        {
            _todoReadRepository = todoReadRepository;
        }

        public async Task<GetAllTodosQueryResponse> Handle(GetAllTodosQueryRequest request, CancellationToken cancellationToken)
        {
            // tracking=false diyoruz çünkü verilerde değişiklik (Update) yapmayacağız, sadece okuyacağız.
            // Bu sayede Entity Framework Core daha az yorulur ve liste çok daha hızlı gelir.
            var query = _todoReadRepository.GetAll(false);

            // Toplam görev sayısını alıyoruz.
            var totalCount = query.Count();

            // Verileri liste halinde çekiyoruz. User nesnesindeki döngüsel referans 
            // hatalarını (JSON loop) önlemek için sadece ihtiyacımız olan alanları seçiyoruz (Select).
            // İleride buraya Sayfalama (Pagination) kodları da ekleyebiliriz (Skip, Take gibi).
            var todos = query.Select(t => new
            {
                t.Id,
                t.Title,
                t.Description,
                t.TodoDate,
                t.IsCompleted,
                t.CreatedAt
            }).ToList();

            return new GetAllTodosQueryResponse
            {
                TotalCount = totalCount,
                Todos = todos
            };
        }
    }
}