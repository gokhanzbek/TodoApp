using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoApp.Domain.Entities.Identity;

namespace TodoApp.Application.Abstractions.Token
{
    public interface ITokenHandler
    {
        DTOs.Token CreateAccessToken(int minute, AppUser user);
        //isim çakışması oldu o yüzden DTOs dan geldiğini belirttik
    }
}
