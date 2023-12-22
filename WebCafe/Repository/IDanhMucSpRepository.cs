using System.Collections.Generic;
using WebCafe.Models;
namespace WebCafe.Repository
{
    public interface IDanhMucSpRepository
    {
        DanhMucSp Add(DanhMucSp danhMucSp);
        DanhMucSp Update(DanhMucSp danhMucSp);
        DanhMucSp Delete(int maDm);
        DanhMucSp GetDanhMucSp(int maDm);
        IEnumerable<DanhMucSp> GetAllDanhMucSp();
    }
}
