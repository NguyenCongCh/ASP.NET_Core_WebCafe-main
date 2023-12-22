using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebCafe.Models;
using Microsoft.AspNetCore.Hosting;
namespace WebCafe.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AdminNewsController : Controller
    {
        private readonly CuaHangBanCafeContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public AdminNewsController(CuaHangBanCafeContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }


        public async Task<IActionResult> Index()
        {
            return View(await _context.TinTucs.ToListAsync());
        }


        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tinTuc = await _context.TinTucs
                .FirstOrDefaultAsync(m => m.MaTt == id);
            if (tinTuc == null)
            {
                return NotFound();
            }

            return View(tinTuc);
        }


        public IActionResult Create()
        {
            ViewBag.ThongBao = "";
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MaTt,TenTt,AnhTt,Motangan,Motadai,Tacgia,CreateDate,LoaiTin")] TinTuc tinTuc, IFormFile file)
        {
            if (ModelState.IsValid)
            {
                if (!TinTucExists(tinTuc.TenTt))
                {
                    tinTuc.CreateDate = DateTime.Now;
                    // Check if a file is selected
                    if (file != null && file.Length > 0)
                    {
                        // Get the filename and extension
                        var fileName = Path.GetFileName(file.FileName);
                        var fileExt = Path.GetExtension(fileName);

                        // Generate a unique filename
                        var uniqueFileName = Guid.NewGuid().ToString() + fileExt;

                        // Combine the path to save the file
                        var filePath = Path.Combine(_webHostEnvironment.WebRootPath, "img/tintuc", uniqueFileName);

                        // Copy the file to the path
                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            await file.CopyToAsync(stream);
                        }

                        // Save the filename to the model
                        tinTuc.AnhTt = uniqueFileName;
                    }

                    _context.Add(tinTuc);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                // Đưa ra thông báo
                ViewBag.ThongBao = "Tiêu đề tin tức đã tồn tại, thêm tin tức mới không thành công";
            }
            return View(tinTuc);
        }



        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                await Console.Out.WriteLineAsync("id null");
                return NotFound();
            }

            var tinTuc = await _context.TinTucs.FindAsync(id);
            if (tinTuc == null)
            {
                await Console.Out.WriteLineAsync("not found");
                return NotFound();
            }
            ViewBag.ThongBao = "";
            return View(tinTuc);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MaTt,TenTt,AnhTt,Motangan,Motadai,Tacgia,CreateDate,LoaiTin")] TinTuc tinTuc)
        {
            if (id != tinTuc.MaTt)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                // Kiểm tra tin tức đã tồn tại trước đó chưa
                if (!TinTucExists(tinTuc.TenTt))
                {
                    _context.Update(tinTuc);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                // Đưa ra thông báo
                ViewBag.ThongBao = "Tiêu đề tin tức đã tồn tại, sửa tin tức không thành công";
            }
            return View(tinTuc);
        }



        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tinTuc = await _context.TinTucs
                .FirstOrDefaultAsync(m => m.MaTt == id);
            if (tinTuc == null)
            {
                return NotFound();
            }

            return View(tinTuc);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var tinTuc = await _context.TinTucs.FindAsync(id);
            if (tinTuc == null)
                return View(id);
            _context.TinTucs.Remove(tinTuc);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TinTucExists(string TieuDe)
        {
            return _context.TinTucs.Any(e => e.TenTt == TieuDe);
        }
    }
}