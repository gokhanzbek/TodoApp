using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoApp.Application.Abstractions.Services;
using TodoApp.Application.Repositories.TodoApp.Application.Repositories.TodoItems;

namespace TodoApp.Application.Features.Queries.Todo.GetAllTodos
{
    public class GetAllTodosQueryHandler : IRequestHandler<GetAllTodosQueryRequest, GetAllTodosQueryResponse>
    {
        private readonly ITodoReadRepository _todoReadRepository;
        private readonly ICurrentUser _currentUser; // 1. AJANI ÇAĞIRDIK

        public GetAllTodosQueryHandler(ITodoReadRepository todoReadRepository, ICurrentUser currentUser)
        {
            _todoReadRepository = todoReadRepository;
            _currentUser = currentUser;
        }

        public async Task<GetAllTodosQueryResponse> Handle(GetAllTodosQueryRequest request, CancellationToken cancellationToken)
        {
            // 2. KİMLİK KONTROLÜ (Token'dan okuyoruz, kimse başkasının verisini göremez!)
            var userIdStr = _currentUser.UserId;
            if (string.IsNullOrEmpty(userIdStr))
            {
                throw new UnauthorizedAccessException("Lütfen giriş yapın!");
            }

            Guid userId = Guid.Parse(userIdStr);

            // 3. SADECE BU KULLANICIYA AİT OLANLARI ÇEK (Patronun 1. İsteği)
            var query = _todoReadRepository.GetAll(false).Where(t => t.UserId == userId);

            // 4. TARİHE GÖRE FİLTRELE (Patronun 2. İsteği - Eğer dışarıdan tarih gönderilmişse)
            if (request.TargetDate.HasValue)
            {
                // Artık .Date yazmamıza gerek yok, DateOnly vs DateOnly direkt eşleşir!
                query = query.Where(t => t.TodoDate == request.TargetDate.Value);
            }

            // Toplam görev sayısını alıyoruz.
            var totalCount = query.Count();

            // Verileri seçip listeye çeviriyoruz (Tarihe göre sıralayarak getirmek şık olur)
            var todos = query
                .OrderBy(t => t.TodoDate) // Yakın tarihli olanlar üstte çıksın
                .Select(t => new
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