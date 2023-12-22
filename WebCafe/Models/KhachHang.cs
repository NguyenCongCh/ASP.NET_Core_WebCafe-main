using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebCafe.Models;

public partial class KhachHang
{
    public KhachHang()
    {
        DonHangs = new HashSet<DonHang>();
    }
    [Key]
    public int MaKh { get; set; }

    [Required(ErrorMessage = "Tên Người Dùng không được để trống.")]
    [RegularExpression(@"^[aAàÀảẢãÃáÁạẠăĂằẰẳẲẵẴắẮặẶâÂầẦẩẨẫẪấẤậẬbBcCdDđĐeEèÈẻẺẽẼéÉẹẸêÊềỀểỂễỄếẾệỆ fFgGhHiIìÌỉỈĩĨíÍịỊjJkKlLmMnNoOòÒỏỎõÕóÓọỌôÔồỒổỔỗỖốỐộỘơƠờỜởỞỡỠớỚợỢpPqQrRsStTu UùÙủỦũŨúÚụỤưƯừỪửỬữỮứỨựỰvVwWxXyYỳỲỷỶỹỸýÝỵỴzZ]*$", ErrorMessage = "Tên Người" +
        "Dùng không được chứa số và kí tự đặc biệt.")]
    [MinLength(2, ErrorMessage = "Tên Người Dùng phải dài hơn 2 kí tự.")]
    public string TenKh { get; set; } = null!;

    public string? GioiTinh { get; set; }

    public string? AvatarKh { get; set; }

    [Required(ErrorMessage = "Địa Chỉ không được để trống.")]
    public string? Diachi { get; set; }

    [Required(ErrorMessage = "Ngày sinh không được để trống.")]
    public DateTime? Ngaysinh { get; set; }

    [Required(ErrorMessage = "Số Điện Thoại không được để trống")]
    [RegularExpression(@"^(0?)(3[2-9]|5[6|8|9]|7[0|6-9]|8[0-6|8|9]|9[0-4|6-9])[0-9]{7}$", ErrorMessage = "Số Điện Thoại không hợp lệ.")]
    public int Phone { get; set; }

    [Required(ErrorMessage = "Email không được để trống")]
    [RegularExpression(@"^[a-z][a-z0-9_.]{5,32}@[a-z0-9]{2,}(.[a-z0-9]{2,4}){1,2}$", ErrorMessage = "Email không hợp lệ.")]
    public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;

    public DateTime CreateDate { get; set; }

    public int? AccountId { get; set; }

    public virtual Account? Account { get; set; }

    public virtual ICollection<DonHang> DonHangs { get; } = new List<DonHang>();

    public enum Gender
    {
        Nam,
        Nữ
    }
}
