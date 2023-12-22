using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography.Xml;
using System.Threading.Tasks;
using WebCafe.Models;
namespace WebCafe.Controllers
{
    public class HomeController : Controller
    {

        private readonly CuaHangBanCafeContext _context;

        public HomeController(CuaHangBanCafeContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var CuaHangBanCafeContext = _context.SanPhams.Include(s => s.MaDmNavigation);
            return View(await CuaHangBanCafeContext.ToListAsync());
        }

        public async Task<IActionResult> ChangePassword(int id)
        {
            var maKH = HttpContext.Session.GetString("MaKh");
            id = int.Parse(maKH);
            var customerUser = await _context.KhachHangs.FindAsync(id);

            return View(customerUser);
        }

        [HttpPost]
        public IActionResult ChangePassword(KhachHang customer)
        {
            if (string.IsNullOrEmpty(customer.Password))
            {
                ModelState.AddModelError("", "Mật khẩu không được để trống");
                return View(customer);
            }

            try
            {
                // Check if the AccountID value in the KhachHang record exists in the Account table
                var account = _context.Accounts.Find(customer.AccountId);
                if (account == null)
                {
                    ModelState.AddModelError("", "Không tìm thấy tài khoản");
                    return View(customer);
                }

                // Update the KhachHang record with the new password value
                _context.Update(customer);

                // Save the changes to the database
                _context.SaveChanges();

                // Redirect the user to the Index action method
                return RedirectToAction("Index");
            }
            catch (DbUpdateException ex)
            {
                ModelState.AddModelError("", $"Lỗi lưu dữ liệu: {ex.InnerException?.Message ?? ex.Message}");
                return View(customer);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"Lỗi không xác định: {ex.Message}");
                return View(customer);
            }
        }

        public async Task<IActionResult> UserDashboard(int id)
        {
            var maKH = HttpContext.Session.GetString("MaKh");
            id = int.Parse(maKH);
            var customerUser = await _context.KhachHangs.FindAsync(id);
            return View(customerUser);
        }


        //[HttpPost]
        //public async Task<IActionResult> UserDashboard(KhachHang customer1)
        //{
        //    var customer = _context.KhachHangs.FirstOrDefault(x => x.MaKh == customer1.MaKh);
        //    //if (string.IsNullOrEmpty(customer.TenKh) == true)
        //    //{
        //    //    ModelState.AddModelError("", "Tên không được để trống");
        //    //    return View(customer);
        //    //}
        //    //customer.AvatarKh = customer1.AvatarKh;
        //    ////_context.Update(customer);
        //    //await _context.SaveChangesAsync();
        //    //return RedirectToAction("UserDashboard");


        //    if (string.IsNullOrEmpty(customer.TenKh) == true)
        //    {
        //        ModelState.AddModelError("", "Tên không được để trống");
        //        return View(customer);
        //    }
        //    _context.Update(customer);
        //    var check = _context.SaveChanges();
        //    if (check > 0)
        //    {
        //        return RedirectToAction("UserDashboard");
        //    }
        //    else
        //    {
        //        ModelState.AddModelError("", "Lỗi lưu dữ liệu");
        //        return View(customer);
        //    }
        //}

        [HttpPost]
        public IActionResult UserDashboard(KhachHang customer, Account user)
        {
            if (string.IsNullOrEmpty(customer.TenKh) == true)
            {
                ModelState.AddModelError("", "Tên không được để trống");
                return View(customer);
            }
            try
            {
                _context.Update(customer);
                _context.SaveChanges();
                return RedirectToAction("UserDashboard");
            }
            catch
            {

                ModelState.AddModelError("", "Lỗi lưu dữ liệu");
                return View(customer);
            }
        }

        public IActionResult Privacy()
        {
            return View();
        }


        public IActionResult Register()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register([Bind("MaKh,TenKh,GioiTinh,AvatarKh,Diachi,Ngaysinh,Phone,Email,Password,CreateDate")] KhachHang khachhang,Account user)
        {
            if (ModelState.IsValid)
            {
                if (string.IsNullOrEmpty(khachhang.TenKh) == true || string.IsNullOrEmpty(khachhang.GioiTinh) == true || string.IsNullOrEmpty(khachhang.Email) == true/* || khachhang.Phone == null || khachhang.Ngaysinh == null*/)
                {
                    ModelState.AddModelError("", "Thông tin không được để trống");
                    return View(khachhang);
                }
                var checkEmail = _context.KhachHangs.SingleOrDefault(x => x.Email.Trim().ToLower() == khachhang.Email.Trim().ToLower());
                if (checkEmail != null)
                {
                    ModelState.AddModelError("", "Địa chỉ Email đã tồn tại");
                    return View(khachhang);
                }
                var checkPhone = _context.KhachHangs.SingleOrDefault(x => x.Phone == khachhang.Phone);
                if (checkPhone != null)
                {
                    ModelState.AddModelError("", "Số điện thoại đã tồn tại");
                    return View(khachhang);
                }
                
                user.TaiKhoan = khachhang.Email;
                user.RoleId = 3;
                user.CreateDate = DateTime.Now;
                _context.Add(user);
                await _context.SaveChangesAsync();
                khachhang.AccountId = user.AccountId;
                khachhang.CreateDate = user.CreateDate;
                // set avatar default
                khachhang.AvatarKh = "avatarKH.jpg";
                _context.Add(khachhang);
                await _context.SaveChangesAsync();
                //HttpContext.Session.SetString("MaKh", khachhang.MaKh.ToString());
                //HttpContext.Session.SetString("TenKh", khachhang.TenKh.ToString());
                //HttpContext.Session.SetString("Email", khachhang.Email.Trim().ToLower());
                return RedirectToAction("RegisterSuccess");
            }
            return View();
        }
        public IActionResult RegisterSuccess()
        {
            return View();
        }
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(string email, string password)
        {
            var khachhang = _context.KhachHangs.SingleOrDefault(x => x.Email.Trim().ToLower() == email.Trim().ToLower() && x.Password == password);
            if (khachhang != null)
            {
                HttpContext.Session.SetString("MaKh", khachhang.MaKh.ToString());
                HttpContext.Session.SetString("TenKh", khachhang.TenKh.ToString());
                HttpContext.Session.SetString("Email", khachhang.Email.Trim().ToLower());
                var checkAcount = _context.Accounts.SingleOrDefault(x => x.TaiKhoan.Trim().ToLower() == email.Trim().ToLower());
                if (checkAcount != null)
                {
                    HttpContext.Session.SetString("AccountID", checkAcount.AccountId.ToString());
                    HttpContext.Session.SetInt32("RoleID", checkAcount.RoleId);
                    var roleID = HttpContext.Session.GetInt32("RoleID");
                    var checkRole = _context.RoleAccounts.SingleOrDefault(x => x.RoleId == roleID);
                    if (checkRole != null)
                    {
                        if (checkRole.RoleId == 1 || checkRole.RoleId == 2)
                        {
                            return RedirectToAction("Index", "Home", new { area = "Admin" });
                        }
                        else
                        {
                            return RedirectToAction("Index");
                        }
                    }
                }
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ModelState.AddModelError("", "Đăng Nhập Thất Bại! Kiểm Lại Thông Tin Đăng Nhập");
                return View();
            }
        }

        //Logout
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();//remove session
            return RedirectToAction("Index");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}