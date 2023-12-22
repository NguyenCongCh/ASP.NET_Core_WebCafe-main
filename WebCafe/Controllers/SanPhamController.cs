using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using X.PagedList;
using WebCafe.Models;

namespace WebCafe.Controllers
{
    public class SanPhamController : Controller
    {
        private readonly CuaHangBanCafeContext db;

        public SanPhamController(CuaHangBanCafeContext context)
        {
            db = context;
        }

        // Lấy danh sách tất cả sản phẩm
        public async Task<IActionResult> Index(int? page)
        {
            int pageNumber = page == null || page <= 0 ? 1 : page.Value;
            int pageSize = 6;
            var lstSanPham = await db.SanPhams.AsNoTracking().ToListAsync();
            PagedList<SanPham> lst = new PagedList<SanPham>(lstSanPham, pageNumber, pageSize);
            ViewBag.tenDanhMuc = "Sản phẩm";
            return View(lst);
        }
        

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sanPham = await db.SanPhams
                .Include(s => s.MaDmNavigation)
                .FirstOrDefaultAsync(m => m.MaSp == id);
            if (sanPham == null)
            {
                return NotFound();
            }

            return View(sanPham);
        }

        // Danh sách sản phẩm theo danh mục sản phẩm
        public async Task<IActionResult> SanPhamTheoDanhMuc(int maDM, int? page)
        {
            int pageNumber = page == null || page <= 0 ? 1 : page.Value;
            int pageSize = 6;
            var lstLoai = await db.SanPhams.Where(x => x.MaDm == maDM).ToListAsync();
            //var tenDanhMuc = _context.DanhMucSps.
            var tenDM = db.DanhMucSps.FirstOrDefault(x => x.MaDm == maDM);
            ViewBag.tenDanhMuc = tenDM.TenDm;
            PagedList<SanPham> lst = new PagedList<SanPham>(lstLoai, pageNumber, pageSize);
            ViewBag.maDM = maDM;
            return View(lst);
        }
        

        private bool SanPhamExists(int id)
        {
            return db.SanPhams.Any(e => e.MaSp == id);
        }
    }
}
