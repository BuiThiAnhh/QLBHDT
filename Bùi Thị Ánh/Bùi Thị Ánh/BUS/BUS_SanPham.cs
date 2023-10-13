using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using DAL;
using Entities;

namespace BUS
{
    public class BUS_SanPham
    {
        DAL_SanPham dal_ch = new DAL_SanPham();
        DAL_HangSP dal_hang = new DAL_HangSP();
        DAL_HeDieuHanh dal_hdh = new DAL_HeDieuHanh();

        public DataTable GetHang()
        {
            return dal_hang.getData();
        }
        public DataTable GetHeDieuHanh()
        {
            return dal_hdh.getData();
        }
        public DataTable getData()
        {
            return dal_ch.getData();
        }
        public int kiemtramatrung(string ma)
        {
            return dal_ch.kiemtramatrung(ma);
        }
        public bool ThemCH(SanPham ch)
        {
            return dal_ch.ThemCH(ch);
        }
        public bool SuaCH(SanPham ch)
        {
            return dal_ch.SuaCH(ch);
        }
        public bool XoaCH(int ma)
        {
            return dal_ch.XoaCH(ma);
        }
        public DataTable TimKiem(string sonha, string dientich, string giaban)
        {
            return dal_ch.TimKiemCH(sonha, dientich, giaban);
        }
    }
}
