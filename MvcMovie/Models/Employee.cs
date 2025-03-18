using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MvcMovie.Models
{
    public class Employee : Person
    {
        [Key]
        public new int EmployeeId { get; set; } // Giữ kiểu int

        public int Age { get; set; }

        // Liên kết với Person
        public Person? Person { get; set; }
    }
}
