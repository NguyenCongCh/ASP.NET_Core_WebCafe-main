using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebCafe.Models;

namespace WebCafe.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SanPhamsController : Controller
    {
        private readonly CuaHangBanCafeContext _context;

        public SanPhamsController(CuaHangBanCafeContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var lstSanPham = await _context.SanPhams.Include(s => s.MaDmNavigation).ToListAsync();
            return View(lstSanPham);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sanPham = await _context.SanPhams
                .Include(s => s.MaDmNavigation)
                .FirstOrDefaultAsync(m => m.MaSp == id);
            if (sanPham == null)
            {
                return NotFound();
            }

            return View(sanPham);
        }

        public IActionResult Create()
        {
            ViewBag.ThongBao = "";
            ViewData["MaDm"] = new SelectList(_context.DanhMucSps, "MaDm", "TenDm");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MaSp,MaDm,TenSp,AnhSp,VideoSp,GiaSp,TrangThai,SoLuong,BestSeller,CreateDate,NgaySua,MotaSp")] SanPham sanPham)
        {
            if (ModelState.IsValid)
            {
                // Kiểm tra sản phẩm đã tồn tại chưa?
                if (!SanPhamExists(sanPham.MaDm, sanPham.TenSp))
                {
                    sanPham.NgaySua = sanPham.CreateDate;
                    _context.Add(sanPham);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                // Đưa ra thông báo sản phẩm đã tồn tại
                ViewBag.ThongBao = "Sản phẩm đã tồn tại, thêm mới không thành công";
            }
            ViewData["MaDm"] = new SelectList(_context.DanhMucSps, "MaDm", "TenDm", sanPham.MaDm);
            return View(sanPham);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var sanPham = await _context.SanPhams.FindAsync(id);
            if (sanPham == null)
            {
                return NotFound();
            }
            ViewBag.ThongBao = "";
            ViewData["MaDm"] = new SelectList(_context.DanhMucSps, "MaDm", "TenDm", sanPham.MaDm);
            return View(sanPham);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([Bind("MaSp,MaDm,TenSp,AnhSp,VideoSp,GiaSp,TrangThai," +
            "SoLuong,BestSeller,CreateDate,NgaySua,MotaSp")] SanPham sanPham)
        {
            if (ModelState.IsValid)
            {
                if (!SanPhamExists(sanPham.MaDm, sanPham.TenSp))
                {
                    _context.Update(sanPham);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                // Đưa ra thông báo sản phẩm đã tồn tại
                ViewBag.ThongBao = "Sản phẩm đã tồn tại, sửa sản phẩm không thành công";
            }
            ViewData["MaDm"] = new SelectList(_context.DanhMucSps, "MaDm", "TenDm", sanPham.MaDm);
            return View(sanPham);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sanPham = await _context.SanPhams.FindAsync(id);
            if (sanPham == null)
            {
                return NotFound();
            }
            ViewBag.ThongBao = "";
            return View(sanPham);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var sanPham = await _context.SanPhams.FindAsync(id);
            if (!SanPhamTrongChiTietDonHang(id) && sanPham!=null)
            {
                _context.SanPhams.Remove(sanPham);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            // Đưa ra thông báo sản phẩm đã tồn tại
            ViewBag.ThongBao = "Sản phẩm đã được bán, không được phép xóa";
            return View(sanPham);
        }

        private bool SanPhamExists(int MaDm, string TenSp)
        {
            return _context.SanPhams.Any(e => e.MaDm == MaDm && e.TenSp == TenSp);
        }

        public bool SanPhamTrongChiTietDonHang(int MaSp)
        {
            return _context.ChiTietDonHangs.Any(x => x.MaSp == MaSp);
        }
    }
}
