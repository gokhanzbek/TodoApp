using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoApp.Application.Repositories;
using TodoApp.Application.Repositories.TodoApp.Application.Repositories.TodoItems;
using TodoApp.Domain.Entities;

namespace TodoApp.Application.Features.Commands.Todo.UpdateTodo
{
    public class UpdateTodoCommandHandler : IRequestHandler<UpdateTodoCommandRequest, UpdateTodoCommandResponse>
    {
        private readonly ITodoReadRepository _todoReadRepository;
        private readonly ITodoWriteRepository _todoWriteRepository;

        public UpdateTodoCommandHandler(ITodoReadRepository todoReadRepository, ITodoWriteRepository todoWriteRepository)
        {
            _todoReadRepository = todoReadRepository;
            _todoWriteRepository = todoWriteRepository;
        }

        public async Task<UpdateTodoCommandResponse> Handle(UpdateTodoCommandRequest request, CancellationToken cancellationToken)
        {
            // 1. Güncellenecek kaydı bul (tracking=true olmalı çünkü üzerinde değişiklik yapacağız)
            TodoItem todo = await _todoReadRepository.GetByIdAsync(request.Id, tracking: true);

            if (todo == null)
            {
                return new UpdateTodoCommandResponse
                {
                    IsSuccess = false,
                    Message = "Güncellenecek görev bulunamadı!"
                };
            }

            // 2. Yeni değerleri üzerine yaz
            todo.Title = request.Title;
            todo.Description = request.Description;
            todo.TodoDate = request.TodoDate;
            todo.IsCompleted = request.IsCompleted;
            todo.UpdatedAt = DateTime.UtcNow; // BaseEntity'den gelen güncellenme tarihini de tetikliyoruz

            // 3. EF Core'a "bu kayıt değişti" diyoruz ve veritabanına yansıtıyoruz
            _todoWriteRepository.Update(todo);
            await _todoWriteRepository.SaveAsync();

            return new UpdateTodoCommandResponse
            {
                IsSuccess = true,
                Message = "Görev başarıyla güncellendi!"
            };
        }
    }
}
