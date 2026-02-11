using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoApp.Domain.Entities.Common;

namespace TodoApp.Domain.Entities
{
    public class TodoItem : BaseEntity
    {
        public string Title { get; set; } = null!;
        public string? Description { get; set; }

        public DateTime TodoDate { get; set; }
        public bool IsCompleted { get; set; }

        public Guid UserId { get; set; }
        public User User { get; set; } = null!;
    }


}
