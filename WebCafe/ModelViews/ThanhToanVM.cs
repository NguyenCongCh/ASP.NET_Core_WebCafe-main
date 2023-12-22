using System;
using System.ComponentModel.DataAnnotations;

namespace WebCafe.ModelViews
{
    public class ThanhToanVM
    {
        public int MaKh { get; set; }

        [Required(ErrorMessage ="Vui lòng nhập họ tên")]
        public string FullName { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập số điện thoại")]
        public int Phone { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập địa chỉ")]
        public string DiaChi { get; set; }

        [Required(ErrorMessage = "Vui chọn hình thức thanh toán")]
        public bool ThanhToan { get; set; }
        public DateTime NgayThanhToan { get; set; }
        public string Note { get; set; }
    }
}
