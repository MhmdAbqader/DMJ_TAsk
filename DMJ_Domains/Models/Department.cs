using System.ComponentModel.DataAnnotations;

namespace DMJ_Domains.Models
{
    public class Department
    {
        public int Id { get; set; }

        [MaxLength(100)]
        [MinLength(2)] // such as HR is consisted from two character
        public required string Name { get; set; }
        public string? Description { get; set; }
    }
}