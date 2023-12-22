using WebCafe.Models;
using Microsoft.AspNetCore.Mvc;
using WebCafe.Repository;
using System.Linq;

namespace WebApplication_Nhap.ViewComponents
{
    public class DanhMucSpMenuViewComponent : ViewComponent
    {
        private readonly IDanhMucSpRepository _danhMucSp;
        public DanhMucSpMenuViewComponent(IDanhMucSpRepository danhmucspRepository)
        {
            _danhMucSp = danhmucspRepository;
        }
        public IViewComponentResult Invoke()
        {
            var danhmuc = _danhMucSp.GetAllDanhMucSp().OrderBy(x => x.TenDm);
            return View(danhmuc);
        }
    }
}