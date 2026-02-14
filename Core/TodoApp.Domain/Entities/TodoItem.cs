using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoApp.Domain.Entities.Common;
using TodoApp.Domain.Entities.Identity;

namespace TodoApp.Domain.Entities
{
    public class TodoItem : BaseEntity
    {
        public string Title { get; set; } = null!;
        public DateTime TodoDate { get; set; }
        public string? Description { get; set; }
        public bool IsCompleted { get; set; }

        public Guid UserId { get; set; }
        public AppUser User { get; set; } = null!;
    }


}
