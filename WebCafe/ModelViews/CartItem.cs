using WebCafe.Models;

namespace WebCafe.ModelViews
{
    public class CartItem
    {
        public SanPham sanPham { get; set; }
        public int soLuong { get; set; }
        public double TongTien => soLuong * sanPham.GiaSp;
        public double GiamGialoai1 { get; set; }
        public double GiamGialoai2 { get; set; }
        public int MaKm { get; set; }
    }
}
