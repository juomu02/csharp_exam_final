using System.ComponentModel.DataAnnotations;
using App.Entities;

namespace App.API.Models.Requests
{
    public class UpdateTaskRequest
    {
        [Required(ErrorMessage = "Title is required.")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Title must be between 3 and 100 characters.")]
        public string Title { get; set; }

        [StringLength(500, ErrorMessage = "Description cannot exceed 500 characters.")]
        public string Description { get; set; }

        [EnumDataType(typeof(TaskImportance), ErrorMessage = "Importance must be 1 (High), 2 (Medium), or 3 (Low).")]
        public TaskImportance Importance { get; set; } = TaskImportance.Medium;
        public bool IsCompleted { get; set; } = false;

        [DataType(DataType.DateTime, ErrorMessage = "StartDate must be a valid datetime.")]
        public DateTime? StartDate { get; set; }

        [DataType(DataType.DateTime, ErrorMessage = "EndDate must be a valid datetime.")]
        public DateTime? EndDate { get; set; }

    }
}