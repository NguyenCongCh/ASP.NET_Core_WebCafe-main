using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebCafe.Models;

public partial class DanhMucSp
{
    [Key]
    public int MaDm { get; set; }

    [Required(ErrorMessage = "Tên Danh Mục không được để trống.")]
    public string TenDm { get; set; } = null!;

    public string? AnhDm { get; set; }

    public string? MoTaDm { get; set; }

    public bool? TrangThai { get; set; }

    public virtual ICollection<SanPham> SanPhams { get; } = new List<SanPham>();

    public enum Category
    {
        True,
        False
    }
}
