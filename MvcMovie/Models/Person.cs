using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MvcMovie.Models
{
    public class Person
    {
        [Key]
        public int PersonId { get; set; } // Giữ kiểu int

        public int? EmployeeId { get; set; } // Chuyển thành int? để phù hợp với Employee

        [Required]
        public string FullName { get; set; } = string.Empty;

        [Required]
        public string Address { get; set; } = string.Empty;

        [Required]
        public string Gmail { get; set; } = string.Empty;

        [Required]
        public string PersonType { get; set; } = string.Empty;

        [ForeignKey("EmployeeId")]
        public Employee? Employee { get; set; } // Liên kết với Employee
    }
}
