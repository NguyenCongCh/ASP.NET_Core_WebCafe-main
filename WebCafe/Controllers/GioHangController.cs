using Microsoft.AspNetCore.Mvc;
using WebCafe.Extension;
using WebCafe.Models;
using WebCafe.ModelViews;

namespace WebCafe.Controllers
{
    public class GioHangController : Controller
    {
        private readonly CuaHangBanCafeContext _context;

        public GioHangController(CuaHangBanCafeContext context)
        {
            _context = context;
        }

        CuaHangBanCafeContext db = new CuaHangBanCafeContext();

        public List<CartItem> GioHang
        {
            get
            {
                var gh = HttpContext.Session.Get<List<CartItem>>("GioHang");
                if (gh == default(List<CartItem>))
                {
                    gh = new List<CartItem>();
                }
                return gh;
            }
        }

        public IActionResult Index()
        {
            var listGio = GioHang;
            return View(GioHang);
        }

        [HttpPost]
        [Route("/giohang/add-cart")]
        public IActionResult AddToCart(int maSP, int soLuong)
        {
            List<CartItem> gioHang = GioHang;
            try
            {
                //Thêm vào giỏ
                CartItem item = gioHang.SingleOrDefault(p => p.sanPham.MaSp == maSP);
                if (item != null)
                {
                    item.soLuong = item.soLuong + soLuong;

                    HttpContext.Session.Set<List<CartItem>>("GioHang", gioHang);
                }
                else
                {
                    SanPham sp = _context.SanPhams.SingleOrDefault(p => p.MaSp == maSP);
                    item = new CartItem
                    {
                        soLuong = soLuong,
                        sanPham = sp
                    };
                    gioHang.Add(item);
                }
                HttpContext.Session.Set<List<CartItem>>("GioHang", gioHang);

                return Json(new { succeess = true });

            }
            catch (Exception)
            {
                return Json(new { succeess = false });
            }

        }

        [HttpPost]
        [Route("/giohang/update-cart")]
        public IActionResult UpdateCart(int maSP, int soLuong)
        {
            var gioHang = HttpContext.Session.Get<List<CartItem>>("GioHang");
            try
            {

                if (gioHang != null)
                {
                    CartItem item = gioHang.SingleOrDefault(p => p.sanPham.MaSp == maSP);
                    if (item != null)
                    {
                        item.soLuong = soLuong;
                    }
                    HttpContext.Session.Set<List<CartItem>>("GioHang", gioHang);
                }
                return Json(new
                {
                    soLuong = GioHang.Sum(p => p.soLuong)
                });

            }
            catch (Exception)
            {
                return Json(new { succeess = false });
            }

        }

        [HttpPost]
        [Route("/giohang/remove")]
        public ActionResult Remove(int maSP)
        {
            try
            {
                List<CartItem> gioHang = GioHang;
                CartItem item = gioHang.SingleOrDefault(p => p.sanPham.MaSp == maSP);
                if (item != null)
                {
                    gioHang.Remove(item);
                }
                HttpContext.Session.Set<List<CartItem>>("GioHang", gioHang);
                return Json(new { success = true });
            }
            catch
            {
                return Json(new { success = false });
            }

        }

        public ActionResult CleanCart()
        {
            HttpContext.Session.Remove("GioHang");
            return RedirectToAction("Index");
        }

        [HttpPost]
        [Route("/giohang/sales")]
        public IActionResult GiamGia(string tenKM)
        {
            List<CartItem> gioHang = GioHang;
            TotalPriceAfterDiscounting total = new TotalPriceAfterDiscounting();
            try
            {
                double tongGiam1 = 0, tongGiam2 = 0;
                if (gioHang != null)
                {
                    var km = db.KhuyenMais.FirstOrDefault(predicate: x => x.TenKm == tenKM && x.TinhTrang == true);
                    if (km != null)
                    {
                        //tongGiam2 = 111;
                        if (km.LoaiKm == 1)
                        {
                            for (var i = 0; i < gioHang.Count; i++)
                            {
                                gioHang[i].GiamGialoai1 = gioHang[i].TongTien * ((double)km.GiaTri / 100);
                                tongGiam1 += gioHang[i].TongTien * ((double)km.GiaTri / 100); // Calculate the total discount for this type
                                gioHang[i].MaKm = km.MaKm;
                            }                         
                        } else if (km.LoaiKm == 2)
                        {
                            tongGiam2 = (double)km.GiaTri;
                            for (var i = 0; i < gioHang.Count; i++)
                            {
                                gioHang[i].GiamGialoai2 = tongGiam2 / gioHang.Count();
                                gioHang[i].MaKm = km.MaKm;
                            }
                        }
                    }
                    HttpContext.Session.Set<List<CartItem>>("GioHang", gioHang);
                }
                return Json(new
                {
                    GiamGialoai1 = tongGiam1,
                    GiamGialoai2 = tongGiam2
                });

            }
            catch (Exception)
            {
                return Json(new { success = false });
            }
        }

        [HttpGet]
        [Route("/cart/count")]
        public IActionResult GetCartCount()
        {
            int count = GioHang.Sum(item => item.soLuong);
            return Json(count);
        }
    }
}