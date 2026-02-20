using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoApp.Domain.Entities.Common;

namespace TodoApp.Application.Repositories
{
    public interface IRepository<T> where T : BaseEntity
    {//where T : BaseEntity= T sadece BaseEntity'den türemiş class olabilir.
        DbSet<T> Table { get; }
    }
}
