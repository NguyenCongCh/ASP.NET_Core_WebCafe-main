using System;
using System.Collections.Generic;

namespace WebCafe.Models;

public partial class Account
{
    public int AccountId { get; set; }

    public string? TaiKhoan { get; set; } = null!;

    public int RoleId { get; set; }

    public DateTime CreateDate { get; set; }

    public virtual ICollection<KhachHang> KhachHangs { get; } = new List<KhachHang>();

    public virtual RoleAccount? Role { get; set; } = null!;
}
