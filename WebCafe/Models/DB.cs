namespace WebCafe.Models
{
    public class DB: CuaHangBanCafeContext
    {
        public DonHang donhang { get; set; }
        public KhachHang khachhang { get; set; }
    }
}
