using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class SanPham
    {
        public int MaSP { get; set; }
        public string TenSP { get; set; }
        public float GiaBan { get; set; }
        public DateTime NgayNhap { get; set; }
        public int MaNCC { get; set; }
        public int MaLoai { get; set; }
        public int SoLuong { get; set; }
        public int HeDieuHanhID { get; set; }
        public int HangID { get; set; }
    }
}
