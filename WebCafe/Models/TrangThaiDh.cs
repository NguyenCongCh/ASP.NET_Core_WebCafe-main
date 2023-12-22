using System;
using System.Collections.Generic;

namespace WebCafe.Models;

public partial class TrangThaiDh
{
    public int MaTtdh { get; set; }

    public int MaDh { get; set; }

    public string TrangThai { get; set; } = null!;

    public string? Mota { get; set; }

    public virtual DonHang MaDhNavigation { get; set; } = null!;
}
