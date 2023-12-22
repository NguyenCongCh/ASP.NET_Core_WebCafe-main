using System;
using System.Collections.Generic;

namespace WebCafe.Models;

public partial class KhuyenMai
{
    public int MaKm { get; set; }

    public int? LoaiKm { get; set; }

    public string TenKm { get; set; } = null!;

    public DateTime? NgayBd { get; set; }

    public DateTime? NgayKt { get; set; }

    public int? GiaTri { get; set; }

    public string? ChiTiet { get; set; }

    public bool? TinhTrang { get; set; }

    public virtual ICollection<DonHang> DonHangs { get; } = new List<DonHang>();

    public virtual ICollection<SanPham> SanPhams { get; } = new List<SanPham>();

    public enum Sales
    {
        True,
        False
    }
}
