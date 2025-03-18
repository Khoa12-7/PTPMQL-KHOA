using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MvcMovie.Models
{
    public class DaiLy
    {
        [Key]
        [Required]
        public string? MaDaiLy { get; set; }

        [Required]
        [StringLength(255)]
        public string? TenDaiLy { get; set; }

        [Required]
        [StringLength(500)]
        public string? DiaChi { get; set; }

        [Required]
        [StringLength(255)]
        public string? NguoiDaiDien { get; set; }
        [Required]
        [StringLength(20)]
        public string? DienThoai { get; set; }

        [Required]
        public string? MaHTPP { get; set; }

        [ForeignKey("MaHTPP")] // sử dụng Foreignkey để liên kết các thuộc tính bên HeThongPhanPhoi qua DaiLy
        public HeThongPhanPhoi? HeThongPhanPhoi { get; set; }
    }
}