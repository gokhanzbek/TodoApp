using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoApp.Application.Repositories;
using TodoApp.Application.Repositories.TodoApp.Application.Repositories.TodoItems;
using TodoApp.Domain.Entities;

namespace TodoApp.Application.Features.Commands.Todo.RemoveTodo
{
    public class RemoveTodoCommandHandler : IRequestHandler<RemoveTodoCommandRequest, RemoveTodoCommandResponse>
    {
        private readonly ITodoReadRepository _todoReadRepository;
        private readonly ITodoWriteRepository _todoWriteRepository;

        // Hem okuma (bulmak için) hem yazma (güncellemek için) repolarını alıyoruz
        public RemoveTodoCommandHandler(ITodoReadRepository todoReadRepository, ITodoWriteRepository todoWriteRepository)
        {
            _todoReadRepository = todoReadRepository;
            _todoWriteRepository = todoWriteRepository;
        }

        public async Task<RemoveTodoCommandResponse> Handle(RemoveTodoCommandRequest request, CancellationToken cancellationToken)
        {
            // 1. Silinecek görevi veritabanından bul (Değiştireceğimiz için tracking = true kalmalı)
            TodoItem todo = await _todoReadRepository.GetByIdAsync(request.Id, tracking: true);

            if (todo == null)
            {
                return new RemoveTodoCommandResponse
                {
                    IsSuccess = false,
                    Message = "Görev bulunamadı!"
                };
            }

            // 2. SOFT DELETE OPERASYONU: Fiziksel olarak silmiyoruz, durumunu değiştiriyoruz!
            todo.IsDeleted = true;
            todo.UpdatedAt = DateTime.UtcNow; // BaseEntity'den gelen güncellenme tarihini de atayalım

            // 3. Veritabanına bu değişikliği yansıt ve kaydet
            _todoWriteRepository.Update(todo);
            await _todoWriteRepository.SaveAsync();

            return new RemoveTodoCommandResponse
            {
                IsSuccess = true,
                Message = "Görev başarıyla silindi!"
            };
        }
    }
}
