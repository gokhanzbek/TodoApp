using MediatR;
using Microsoft.AspNetCore.Http; // IHttpContextAccessor için
using System;
using System.Security.Claims; // ClaimTypes için
using System.Threading;
using System.Threading.Tasks;
using TodoApp.Application.Abstractions.Services;
using TodoApp.Application.Repositories;
using TodoApp.Domain.Entities;

namespace TodoApp.Application.Features.Commands.Todo.CreateTodo
{
    namespace TodoApp.Application.Features.Commands.Todo.CreateTodo
    {
        public class CreateTodoCommandHandler : IRequestHandler<CreateTodoCommandRequest, CreateTodoCommandResponse>
        {
            private readonly ITodoWriteRepository _todoWriteRepository;
            private readonly ICurrentUser _currentUser; // KENDİ AJANIMIZ

            public CreateTodoCommandHandler(ITodoWriteRepository todoWriteRepository, ICurrentUser currentUser)
            {
                _todoWriteRepository = todoWriteRepository;
                _currentUser = currentUser;
            }

            public async Task<CreateTodoCommandResponse> Handle(CreateTodoCommandRequest request, CancellationToken cancellationToken)
            {
                // Tertemiz, Web'e (HTTP'ye) bağımlı olmayan kod!
                var userIdStr = _currentUser.UserId;

                if (string.IsNullOrEmpty(userIdStr))
                {
                    return new CreateTodoCommandResponse { IsSuccess = false, Message = "Lütfen önce giriş yapın!" };
                }

                TodoItem newTodo = new TodoItem
                {
                    Id = Guid.NewGuid(),
                    Title = request.Title,
                    Description = request.Description,
                    TodoDate = request.TodoDate,
                    IsCompleted = false,
                    UserId = Guid.Parse(userIdStr)
                };

                await _todoWriteRepository.AddAsync(newTodo);
                await _todoWriteRepository.SaveAsync();

                return new CreateTodoCommandResponse
                {
                    IsSuccess = true,
                    Message = "Görev başarıyla eklendi!",
                    TodoId = newTodo.Id
                };
            }
        }
    }
}