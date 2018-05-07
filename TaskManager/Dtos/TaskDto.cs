using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TaskManager.Dtos
{
    public class TaskDto
    {
        public long TaskId { get; set; }
        public string Title { get; set; }
        public string CreationTime { get; set; }
        public string TaskDetails { get; set; }
        public string CompletionTime { get; set; }
        public bool IsComplete { get; set; }
        public bool IsActive { get; set; }
        public string DueDate { get; set; }
    }

    public class TaskCreateDto
    {
        [Required(ErrorMessage = "Please give the title")]
        public string Title { get; set; }
        [Required(ErrorMessage = "Please give the creation date")]
        public DateTime CreationTime { get; set; }
        [Required(ErrorMessage = "Please give the completion date")]
        public DateTime CompletionTime { get; set; }
        [Required(ErrorMessage = "Please give the due date")]
        public DateTime DueDate { get; set; }        
    }

    public class TaskUpdateDto
    {
        [Required(ErrorMessage = "Please give the title")]
        public string Title { get; set; }
        public DateTime CreationTime { get; set; }
        public DateTime CompletionTime { get; set; }
        public DateTime DueDate { get; set; }
    }
}
