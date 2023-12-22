using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebCafe.Models;

namespace WebCafe.Controllers
{
    public class TinTucController : Controller
    {
        private readonly CuaHangBanCafeContext _context;

        public TinTucController(CuaHangBanCafeContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            ViewBag.tenDanhMuc = "Tin tức";
            return View(await _context.TinTucs.ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tinTuc = await _context.TinTucs.FirstOrDefaultAsync(m => m.MaTt == id);
            if (tinTuc == null)
            {
                return NotFound();
            }

            ViewBag.tenDanhMuc = "Tin tức";
            return View(tinTuc);
        }
    }
}
