using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Data;
using System.Net;
using System;
using System.Threading.Tasks;
using WebCafe.Models;
using WebCafe.Common;

namespace WebCafe.Areas.Admin.Controllers
{
	[Area("Admin")]
	public class AdminSalesController : Controller
	{
		// GET: QLKhuyenMai
		private readonly CuaHangBanCafeContext _context;
		
		public AdminSalesController(CuaHangBanCafeContext context)
		{
			_context = context;
		}

        public async Task<IActionResult> Index()
        {
            return View(await _context.KhuyenMais.ToListAsync());
        }
        /*
        public IActionResult Index(int error = 0)
		{
			var list = _context.KhuyenMais.Where(x => x.TinhTrang == true).OrderByDescending(y => y.NgayBd);
			List<LoaiKM> listLoai = new List<LoaiKM>();
			LoaiKM LOAI1 = new LoaiKM();
			LOAI1.IdLoai = 1;
			LOAI1.TenLoai = "Giảm phần trăm";
			listLoai.Add(LOAI1);
			LoaiKM LOAI2 = new LoaiKM();
			LOAI2.IdLoai = 2;
			LOAI2.TenLoai = "Giảm trực tiếp";
			listLoai.Add(LOAI2);
			ViewBag.items = new SelectList(listLoai, "MaKm", "TenKm");
			ViewBag.GiaTri = 0;
			ViewBag.DanhSach = list;

			ViewBag.Error = error;


			return View(list);
		}
		
		[HttpPost]
		public IActionResult Index(FormCollection f)
		{
			var kq = f["ddlLoai"];
			List<LoaiKM> listLoai = new List<LoaiKM>();
			LoaiKM LOAI1 = new LoaiKM();
			LOAI1.IdLoai = 1;
			LOAI1.TenLoai = "Giảm phần trăm";
			listLoai.Add(LOAI1);
			LoaiKM LOAI2 = new LoaiKM();
			LOAI2.IdLoai = 2;
			LOAI2.TenLoai = "Giảm trực tiếp";
			listLoai.Add(LOAI2);

			if (kq != "")
			{
				int giatri = int.Parse(kq);
				var list = _context.KhuyenMais.Where(x => x.TinhTrang == true && x.LoaiKm == giatri).OrderByDescending(y => y.NgayBd);
				ViewBag.DanhSach = list;
				ViewBag.items = new SelectList(listLoai, "MaKm", "TenKm", giatri);
				ViewBag.GiaTri = giatri;
				return View(list);
			}
			else
			{
				var list = _context.KhuyenMais.Where(x => x.TinhTrang == true).OrderByDescending(y => y.NgayBd);
				ViewBag.DanhSach = list;
				ViewBag.items = new SelectList(listLoai, "MaKm", "TenKm");
				ViewBag.GiaTri = 0;
				return View(list);
			}
		}
		*/

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var khuyenMai = await _context.KhuyenMais.FirstOrDefaultAsync(m => m.MaKm == id);
            if (khuyenMai == null)
            {
                return NotFound();
            }

            return View(khuyenMai);
        }

        public IActionResult Create()
		{
			List<LoaiKM> listLoai = new List<LoaiKM>();
			LoaiKM LOAI1 = new LoaiKM();
			LOAI1.IdLoai = 1;
			LOAI1.TenLoai = "Giảm phần trăm";
			listLoai.Add(LOAI1);
			LoaiKM LOAI2 = new LoaiKM();
			LOAI2.IdLoai = 2;
			LOAI2.TenLoai = "Giảm trực tiếp";
			listLoai.Add(LOAI2);

			ViewBag.LoaiKM = new SelectList(listLoai, "IdLoai", "TenLoai");
			return View();
		}

        [HttpPost]
        //[ValidateInput(false)]
        //[ValidateAntiForgeryToken]
        [AutoValidateAntiforgeryToken]
        public IActionResult Create(KhuyenMai khuyenmai)
		{
			khuyenmai.TinhTrang = true;
			if (ModelState.IsValid)
			{
				try
				{
					_context.KhuyenMais.Add(khuyenmai);
					_context.SaveChanges();
					return RedirectToAction("Index");
				}
				catch (Exception)
				{
					ModelState.AddModelError("", "Quá trình thực hiện thất bại.");
				}
			}
			else
			{
				ModelState.AddModelError("", "Vui lòng kiểm tra lại thông tin đã nhập.");
			}
			List<LoaiKM> listLoai = new List<LoaiKM>();
			LoaiKM LOAI1 = new LoaiKM();
			LOAI1.IdLoai = 1;
			LOAI1.TenLoai = "Giảm phần trăm";
			listLoai.Add(LOAI1);
			LoaiKM LOAI2 = new LoaiKM();
			LOAI2.IdLoai = 2;
			LOAI2.TenLoai = "Giảm trực tiếp";
			listLoai.Add(LOAI2);

			ViewBag.LoaiKM = new SelectList(listLoai, "IdLoai", "TenLoai", khuyenmai.LoaiKm);
			return View(khuyenmai);
		}

		/*
        [HttpPost]
        [HttpGet]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(KhuyenMai khuyenmai)
        {
            try
            {
                var deletedKhuyenmai = await _context.KhuyenMais.FindAsync(khuyenmai.MaKm);

                if (deletedKhuyenmai == null)
                {
                    return NotFound();
                }

                deletedKhuyenmai.TinhTrang = false;
                _context.Entry(deletedKhuyenmai).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                string message = ex.Message;
                return Content("<script> alert(\"Quá trình thực hiện thất bại.\")</script>");
            }
        }
		*/

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var khuyenmai = await _context.KhuyenMais
                .FirstOrDefaultAsync(m => m.MaKm == id);
            if (khuyenmai == null)
            {
                return NotFound();
            }

            return View(khuyenmai);
        }


        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var khuyenMai = await _context.KhuyenMais.FindAsync(id);
            _context.KhuyenMais.Remove(khuyenMai);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool KhuyenMaiExists(int id)
        {
            return _context.KhuyenMais.Any(e => e.MaKm == id);
        }

        public async Task<IActionResult> Edit(int? id)
		{
			if (id == null)
			{
                return new BadRequestResult();
            }
            var khuyenmai = await _context.KhuyenMais.FindAsync(id);
            
			if (khuyenmai == null)
			{
                return NotFound();
            }
			if (id % 1 == 0)
			{			
				List<LoaiKM> listLoai = new List<LoaiKM>();
				LoaiKM LOAI1 = new LoaiKM();
				LOAI1.IdLoai = 1;
				LOAI1.TenLoai = "Giảm phần trăm";
				listLoai.Add(LOAI1);
				LoaiKM LOAI2 = new LoaiKM();
				LOAI2.IdLoai = 2;
				LOAI2.TenLoai = "Giảm trực tiếp";
				listLoai.Add(LOAI2);

				ViewBag.LoaiKM = new SelectList(listLoai, "IdLoai", "TenLoai", khuyenmai.LoaiKm);
				return View(khuyenmai);
			}
			else
			{
				return RedirectToAction("Index", "AdminSales", new { error = 2 });
			}
		}

		[HttpPost]
        //[ValidateInput(false)]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MaKm, LoaiKm, TenKm, NgayBd, NgayKt, GiaTri, ChiTiet, TinhTrang")] KhuyenMai khuyenmai)
		{          
			if (ModelState.IsValid)
			{
				try
				{
					_context.Entry(khuyenmai).State = EntityState.Modified;
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Index");
				}
				catch (Exception ex)
				{
					ModelState.AddModelError("", "Quá trình thực hiện thất bại.");
				}
			}
			else
			{
				ModelState.AddModelError("", "Vui lòng kiểm tra lại thông tin đã nhập.");
			}

			List<LoaiKM> listLoai = new List<LoaiKM>();
			LoaiKM LOAI1 = new LoaiKM();
			LOAI1.IdLoai = 1;
			LOAI1.TenLoai = "Giảm phần trăm";
			listLoai.Add(LOAI1);
			LoaiKM LOAI2 = new LoaiKM();
			LOAI2.IdLoai = 2;
			LOAI2.TenLoai = "Giảm trực tiếp";
			listLoai.Add(LOAI2);

			ViewBag.LoaiKM = new SelectList(listLoai, "MaKm", "TenKm", khuyenmai.LoaiKm);
			return View(khuyenmai);
		}
	}
}
