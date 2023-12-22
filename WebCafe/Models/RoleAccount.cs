using System;
using System.Collections.Generic;

namespace WebCafe.Models;

public partial class RoleAccount
{
    public RoleAccount()
    {
        Accounts = new HashSet<Account>();
    }
    public int RoleId { get; set; }

    public string RoleName { get; set; } = null!;

    public string Mota { get; set; } = null!;

    public virtual ICollection<Account> Accounts { get; } = new List<Account>();
}
