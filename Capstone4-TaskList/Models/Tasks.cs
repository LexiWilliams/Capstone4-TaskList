using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Capstone4_TaskList.Models
{
    public partial class Tasks
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        [Required(ErrorMessage ="Task description is required")]
        public string Description { get; set; }
        [Required(ErrorMessage = "Task due date is required")]
        public DateTime DueDate { get; set; }
        public string Completed { get; set; }
        public string UserEmail { get; set; }

        public virtual AspNetUsers User { get; set; }
    }
}
