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
    public class AdminCategoryController : Controller
    {
        private readonly CuaHangBanCafeContext _context;

        public AdminCategoryController(CuaHangBanCafeContext context)
        {
            _context = context;
        }


        public async Task<IActionResult> Index()
        {
            return View(await _context.DanhMucSps.ToListAsync());
        }


        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var danhMucSp = await _context.DanhMucSps
                .FirstOrDefaultAsync(m => m.MaDm == id);
            if (danhMucSp == null)
            {
                return NotFound();
            }

            return View(danhMucSp);
        }


        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MaDm,TenDm,AnhDm,MoTaDm,TrangThai")] DanhMucSp danhMucSp)
        {
            if (ModelState.IsValid)
            {
                _context.Add(danhMucSp);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(danhMucSp);
        }


        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var danhMucSp = await _context.DanhMucSps.FindAsync(id);
            if (danhMucSp == null)
            {
                return NotFound();
            }
            return View(danhMucSp);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MaDm,TenDm,AnhDm,MoTaDm,TrangThai")] DanhMucSp danhMucSp)
        {
            if (id != danhMucSp.MaDm)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    //_context.Update(danhMucSp);
                    //await _context.SaveChangesAsync();
                    //Console.Out.WriteLine(danhMucSp);
                    var existingRecord = await _context.DanhMucSps.FindAsync(id);

                    // Update the fields with the new values
                    existingRecord.TenDm = danhMucSp.TenDm;
                    existingRecord.AnhDm = danhMucSp.AnhDm;
                    existingRecord.MoTaDm = danhMucSp.MoTaDm;
                    existingRecord.TrangThai = danhMucSp.TrangThai;

                    // Save the changes to the database
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DanhMucSpExists(danhMucSp.MaDm))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(danhMucSp);
        }

        private bool DanhMucSpExists(int id)
        {
            return _context.DanhMucSps.Any(e => e.MaDm == id);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var danhmuc = await _context.DanhMucSps.FirstOrDefaultAsync(m => m.MaDm == id);
            if (danhmuc == null)
            {
                return NotFound();
            }

            return View(danhmuc);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var danhmuc = await _context.DanhMucSps.FindAsync(id);
            _context.DanhMucSps.Remove(danhmuc);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SanPhamExists(int id)
        {
            return _context.SanPhams.Any(e => e.MaSp == id);
        }
    }
}