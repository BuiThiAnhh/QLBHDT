using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class HoaDonNhap
    {
        public int HoaDonNhapID { get; set; }
        public int MaSP { get; set; }
        public int MaNV { get; set; }
        public int DonGia { get; set; }
        public int SoLuong { get; set; }
        public int TongTien { get; set; }
        public DateTime NgayNhap { get; set; }

    }
}
