using System;
using System.Collections.Generic;
using WebCafe;
using WebCafe.Models;

namespace WebCafe.Repository
{
    public class DanhMucSpRepository : IDanhMucSpRepository
    {
        private readonly CuaHangBanCafeContext _context;
        public DanhMucSpRepository(CuaHangBanCafeContext context)
        {
            _context = context;
        }

        public DanhMucSp Add(DanhMucSp danhMucSp)
        {
            _context.DanhMucSps.Add(danhMucSp);
            _context.SaveChanges();
            return danhMucSp;
        }

        public DanhMucSp Delete(int maDanhMucSp)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<DanhMucSp> GetAllDanhMucSp()
        {
            return _context.DanhMucSps;
        }

        

        public DanhMucSp GetDanhMucSp(int maDanhMucSp)
        {
            return _context.DanhMucSps.Find(maDanhMucSp);
        }

        public DanhMucSp Update(DanhMucSp danhMucSp)
        {
            _context.Update(danhMucSp);
            _context.SaveChanges();
            return danhMucSp;
        }
    }
}
