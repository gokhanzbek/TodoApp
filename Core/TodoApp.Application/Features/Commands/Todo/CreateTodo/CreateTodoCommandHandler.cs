using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoApp.Application.Repositories;
using TodoApp.Domain.Entities;

namespace TodoApp.Application.Features.Commands.Todo.CreateTodo
{
    public class CreateTodoCommandHandler : IRequestHandler<CreateTodoCommandRequest, CreateTodoCommandResponse>
    {
        private readonly ITodoWriteRepository _todoWriteRepository;

        // Dependency Injection ile Repository'mizi çağırıyoruz
        public CreateTodoCommandHandler(ITodoWriteRepository todoWriteRepository)
        {
            _todoWriteRepository = todoWriteRepository;
        }

        public async Task<CreateTodoCommandResponse> Handle(CreateTodoCommandRequest request, CancellationToken cancellationToken)
        {
            // 1. Yeni Todo nesnemizi oluşturuyoruz
            TodoItem newTodo = new TodoItem
            {
                Id = Guid.NewGuid(),
                Title = request.Title,
                Description = request.Description,
                TodoDate = request.TodoDate,
                IsCompleted = false,

                // DİKKAT: Senin TodoItem sınıfında UserId zorunlu bir alan.
                // EF Core hata fırlatmasın diye buraya veritabanında var olan 
                // bir kullanıcının (örneğin az önce Postman'den oluşturduğun kullanıcının) 
                // ID'sini yazmalısın. İleride bunu doğrudan JWT Token içinden dinamik okuyacağız.
                UserId = Guid.Parse("EA18574B-E26C-4968-A5D1-4473A0325735")
            };

            // 2. Repository üzerinden veritabanına ekliyoruz
            await _todoWriteRepository.AddAsync(newTodo);

            // 3. Değişiklikleri kaydediyoruz (Bunu yazmazsan DB'ye gitmez)
            await _todoWriteRepository.SaveAsync();

            // 4. İşlem bitince geriye başarılı yanıtını dönüyoruz
            return new CreateTodoCommandResponse
            {
                IsSuccess = true,
                Message = "Görev başarıyla eklendi!",
                TodoId = newTodo.Id
            };
        }
    }
}
