using DAL;
using DTO;
using Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BUS
{
    public class BUS_HoaDonNhap
    {
        DAL_HoaDonNhap dal_hdn = new DAL_HoaDonNhap();
        public DataTable getData()
        {
            return dal_hdn.getData();
        }
        public int kiemtramatrung(string ma)
        {
            return dal_hdn.kiemtramatrung(ma);
        }
        public bool ThemHDN(HoaDonNhap kh)
        {
            return dal_hdn.ThemHDN(kh);
        }
        public bool SuaHDN(HoaDonNhap kh)
        {
            return dal_hdn.SuaHDN(kh);
        }
        public bool XoaHDN(string ma)
        {
            return dal_hdn.XoaHDB(ma);
        }
    }
}
