using System.ComponentModel.DataAnnotations;

namespace MvcMovie.Models
{
    public class HeThongPhanPhoi
    {
        [Key]
        [Required]
        public string? MaHTPP { get; set; }  // Mã hệ thống phân phối (Primary Key)

        [Required]
        [StringLength(255)]
        public string? TenHTPP { get; set; }  // Tên hệ thống phân phối
    }
}
