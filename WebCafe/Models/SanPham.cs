using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebCafe.Models;

public partial class SanPham
{
    [Key]
    public int MaSp { get; set; }

    public int MaDm { get; set; }

    [Display(Name = "TenSp")]
    [Required(ErrorMessage = "Tên Sản Phẩm không được để trống.")]
    [RegularExpression(@"^[aAàÀảẢãÃáÁạẠăĂằẰẳẲẵẴắẮặẶâÂầẦẩẨẫẪấẤậẬbBcCdDđĐeEèÈẻẺẽẼéÉẹẸêÊềỀểỂễỄếẾệỆ fFgGhHiIìÌỉỈĩĨíÍịỊjJkKlLmMnNoOòÒỏỎõÕóÓọỌôÔồỒổỔỗỖốỐộỘơƠờỜởỞỡỠớỚợỢpPqQrRsStTu UùÙủỦũŨúÚụỤưƯừỪửỬữỮứỨựỰvVwWxXyYỳỲỷỶỹỸýÝỵỴzZ]*$", ErrorMessage = "Tên Sản" +
        "Phẩm không được chứa số và kí tự đặc biệt.")]
    [MinLength(2, ErrorMessage = "Tên Sản Phẩm phải dài hơn 2 kí tự.")]
    public string TenSp { get; set; } = null!;

    [Required(ErrorMessage = "Ảnh Sản Phẩm không được để trống.")]
    public string AnhSp { get; set; } = null!;

    public string? VideoSp { get; set; }

    [Required(ErrorMessage = "Giá Sản Phẩm không được để trống")]
    [Range(0, 1000000, ErrorMessage = "Giá Sản Phẩm phải có giá trị từ 0 đến 1 triệu đồng.")]
    public int GiaSp { get; set; }

    public bool TrangThai { get; set; }

    [Required(ErrorMessage = "Số Lượng không được để trống")]
    [Range(0, 1000, ErrorMessage = "Số Lượng phải có giá trị từ 0 đến 1000.")]
    public int SoLuong { get; set; }

    public bool BestSeller { get; set; }

    [DataType(DataType.DateTime)]
    [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy hh:mm:ss tt}", ApplyFormatInEditMode = true)]
    public DateTime CreateDate { get; set; }

    [DataType(DataType.DateTime)]
    [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy hh:mm:ss tt}", ApplyFormatInEditMode = true)]
    public DateTime NgaySua { get; set; }

    public int? MaKm { get; set; }

    [Required(ErrorMessage = "Mô Tả Sản Phẩm không được để trống.")]
    [MinLength(20, ErrorMessage = "Mô Tả Sản Phẩm phải dài hơn 20 kí tự.")]
    public string MotaSp { get; set; } = null!;

    public virtual ICollection<ChiTietDonHang> ChiTietDonHangs { get; } = new List<ChiTietDonHang>();

    public virtual DanhMucSp? MaDmNavigation { get; set; }

    public virtual KhuyenMai? MaKmNavigation { get; set; }
}
