using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using WebCafe.Models;

namespace WebCafe.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HomeController : Controller
    {
        private readonly CuaHangBanCafeContext _context;
        public HomeController(CuaHangBanCafeContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> AdminProfile()
        {
            var maKH = HttpContext.Session.GetString("MaKh");
            if(maKH == null)
                return RedirectToAction("Login", "Home", new { area = "" });
            var customerUser = await _context.KhachHangs.FindAsync(int.Parse(maKH));
            return View(customerUser);
        }
        [HttpPost]
        public IActionResult AdminProfile(KhachHang customer)
        {
            if (string.IsNullOrEmpty(customer.TenKh))
            {
                ModelState.AddModelError("", "Tên không được để trống");
                return View(customer);
            }
            var khachHangToUpdate = _context.KhachHangs.Include(a => a.Account).SingleOrDefault(k => k.MaKh == customer.MaKh);
            if (khachHangToUpdate != null)
            {
                // Update: Tên, email, phone, Dob, Gender, diaChi
                khachHangToUpdate.TenKh = customer.TenKh;
                khachHangToUpdate.Email = customer.Email;
                khachHangToUpdate.Diachi = customer.Diachi;
                khachHangToUpdate.Phone = customer.Phone;
                khachHangToUpdate.Ngaysinh = customer.Ngaysinh;
                khachHangToUpdate.GioiTinh = customer.GioiTinh;
                // Account.TaiKhoan trùng với KhachHang.Email
                if(khachHangToUpdate.Account != null)
                    khachHangToUpdate.Account.TaiKhoan = customer.Email;
                _context.Update(khachHangToUpdate);
                var check = _context.SaveChanges();
                if (check > 0)
                {
                    return RedirectToAction("Index");
                }
            }
            ModelState.AddModelError("", "Lỗi lưu dữ liệu");
            return View(customer);
        }
        public async Task<IActionResult> AdminChangePassword()
        {
            var maKH = HttpContext.Session.GetString("MaKh");
            if (!int.TryParse(maKH, out int khachHangId))
            {
                // Không tìm thấy khách hàng, xử lý lỗi và trả về view có thông báo lỗi cho người dùng
                ModelState.AddModelError("", "Không tìm thấy khách hàng");
                return View();
            }
            var customerUser = await _context.KhachHangs.FindAsync(khachHangId);
            if (customerUser == null)
            {
                // Không tìm thấy khách hàng, xử lý lỗi và trả về view có thông báo lỗi cho người dùng
                ModelState.AddModelError("", "Không tìm thấy khách hàng");
                return View();
            }
            return View(customerUser);
        }

        [HttpPost]
        public IActionResult AdminChangePassword(KhachHang customer)
        {
            if (string.IsNullOrEmpty(customer.Password))
            {
                ModelState.AddModelError("", "Mật khẩu không được để trống");
                return View(customer);
            }
            var khachHangToUpdate = _context.KhachHangs.SingleOrDefault(k => k.MaKh == customer.MaKh);
            if (khachHangToUpdate == null)
            {
                // Không tìm thấy khách hàng, xử lý lỗi và trả về view có thông báo lỗi cho người dùng
                ModelState.AddModelError("", "Không tìm thấy khách hàng");
                return View(customer);
            }
            // update password
            khachHangToUpdate.Password = customer.Password;
            _context.Update(khachHangToUpdate);
            var check = _context.SaveChanges();
            if (check > 0)
            {
                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError("", "Lỗi lưu dữ liệu");
                return View(customer);
            }
        }

    }
}
