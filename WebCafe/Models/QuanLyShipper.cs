using System;
using System.Collections.Generic;

namespace WebCafe.Models;

public partial class QuanLyShipper
{
    public int MaShipper { get; set; }

    public int MaDh { get; set; }

    public string TenShipper { get; set; } = null!;

    public DateTime NgayLayHang { get; set; }

    public int Phone { get; set; }

    public string TenCongty { get; set; } = null!;

    public virtual DonHang MaDhNavigation { get; set; } = null!;
}
