using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using WebCafe.Models;

namespace WebCafe.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class DonHangsController : Controller
    {
        private readonly CuaHangBanCafeContext _context;
        public DonHangsController(CuaHangBanCafeContext context)
        {
            _context=context;
        }
        public async Task<IActionResult> Index()
        {
            var lstdonhang = from donhang in _context.DonHangs
                             join khachhang in _context.KhachHangs on donhang.MaKh equals khachhang.MaKh
                             select new
                             {
                                 donhang.MaDh,
                                 donhang.MaKh,
                                 donhang.TongTien,
                                 donhang.NgayTao,
                                 khachhang.TenKh
                             };
            return View(lstdonhang);
        }
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var lstdonhang = from donhang in _context.DonHangs
                             join khachhang in _context.KhachHangs on donhang.MaKh equals khachhang.MaKh
                             join chitietdonhang in _context.ChiTietDonHangs on donhang.MaDh equals chitietdonhang.MaDh
                             join sanpham in _context.SanPhams on chitietdonhang.MaSp equals sanpham.MaSp
                             join khuyenmai in _context.KhuyenMais on sanpham.MaKm equals khuyenmai.MaKm
                             select new
                             {
                                 donhang.MaDh,
                                 donhang.NgayTao,
                                 donhang.MaKh,
                                 donhang.TongTien,
                                 sanpham.AnhSp,
                                 sanpham.TenSp,
                                 sanpham.MaSp,
                                 chitietdonhang.SoLuong,
                                 khachhang.TenKh,
                                 khachhang.Phone,
                                 khachhang.Diachi,
                                 khuyenmai.MaKm,
                                 khuyenmai.GiaTri
                             };
            var dh = lstdonhang.FirstOrDefaultAsync(x => x.MaDh == id);
            if (dh == null)
            {
                return NotFound();
            }
            return View(dh);
        }
    }
}
