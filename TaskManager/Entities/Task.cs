using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TaskManager.Entities
{
    public class TaskEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long TaskId { get; set; }
        public string Title { get; set; }
        public DateTime CreationTime { get; set; }
        public string TaskDetails { get; set; }
        public DateTime CompletionTime { get; set; }
        public bool IsComplete { get; set; }
        public bool IsActive { get; set; }
        public DateTime DueDate { get; set; }
    }
}
