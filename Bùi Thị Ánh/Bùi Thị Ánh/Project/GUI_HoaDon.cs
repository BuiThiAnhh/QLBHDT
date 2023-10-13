using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Entities;
using BUS;
using Project;  
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System.IO;
using GUI;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System.IO;
using System.Reflection;

namespace Cao_Van_Dan
{
    public partial class GUI_HoaDon : Form
    {

       
        BUS_HoaDon bus_hd = new BUS_HoaDon();
        BUS_ChiTietHDB bus_cthdb = new BUS_ChiTietHDB();
        BUS_NhanVien bus_nv = new BUS_NhanVien();
        BUS_KhachHang bus_kh = new BUS_KhachHang();
        BUS_SanPham bus_ch = new BUS_SanPham();
        void loaddgvHoaDonBan()
        {
            dgvHoaDonBan.DataSource = bus_hd.getData();
            dgvHoaDonBan.Columns[0].HeaderText = "Mã hoá đơn";
            dgvHoaDonBan.Columns[1].HeaderText = "Ngày bán";
            dgvHoaDonBan.Columns[2].HeaderText = "Tổng tiền";
            dgvHoaDonBan.Columns[3].HeaderText = "Ghi chú";   
            

        }
        
        void loadcbbNV()
        {
            cbbNV.DataSource = bus_nv.getData();
            cbbNV.DisplayMember = "TenNV";
            cbbNV.ValueMember = "MaNV";
            cbbKH.DataSource = bus_kh.getData();
            cbbKH.DisplayMember = "TenKH";
            cbbKH.ValueMember = "MaKH";
            cb_sanpham.DataSource = bus_ch.getData();
            cb_sanpham.DisplayMember = "TenSP";
            cb_sanpham.ValueMember = "MaSP";
        }
        void loadcbbKH()
        {
            cbbKH.DataSource = bus_kh.getData();
            cbbKH.DisplayMember = "TenKH";
            cbbKH.ValueMember = "MaKH";
        }
        private void ExportToExcel(DataGridView dgvHoaDonBan, string filePath)
        {
            using (ExcelPackage package = new ExcelPackage())
            {
                ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Sheet1");

                // Ghi tiêu đề cột
                for (int i = 0; i < dgvHoaDonBan.Columns.Count; i++)
                {
                    worksheet.Cells[1, i + 1].Value = dgvHoaDonBan.Columns[i].HeaderText;
                    worksheet.Cells[1, i + 1].Style.Font.Bold = true;
                    worksheet.Cells[1, i + 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                }

                // Ghi dữ liệu từ DataGridView
                for (int row = 0; row < dgvHoaDonBan.Rows.Count; row++)
                {
                    for (int column = 0; column < dgvHoaDonBan.Columns.Count; column++)
                    {
                        worksheet.Cells[row + 2, column + 1].Value = dgvHoaDonBan.Rows[row].Cells[column].Value?.ToString();
                        worksheet.Cells[row + 2, column + 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                    }
                }

                // Tự động chỉnh độ rộng cột
                worksheet.Cells.AutoFitColumns();

                // Lưu file Excel
                File.WriteAllBytes(filePath, package.GetAsByteArray());
            }
        }


        public GUI_HoaDon()
        {
            InitializeComponent();
        }

        private void GUI_HoaDon_Load(object sender, EventArgs e)
        {
            loaddgvHoaDonBan();
            loadcbbNV();
            loadcbbKH();
            if (Program.code == 0)
            {
                mnBCTK.Enabled = true;
            }
            else
            {
                mnBCTK.Enabled = false;
            }
        }

        private void pbThoat_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Bạn có chắc chắn muốn thoát khỏi ứng dụng không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr == DialogResult.Yes)
                Application.Exit();
        }

        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            GUI_DangNhap frmDangNhap = new GUI_DangNhap();
            this.Hide();
            frmDangNhap.ShowDialog();
        } 
        private void toolStripMenuItem8_Click(object sender, EventArgs e)
        {
            GUI_NhanVien frmNhanVien = new GUI_NhanVien();
            this.Hide();
            frmNhanVien.ShowDialog();
        }

      

        private void toolStripMenuItem9_Click(object sender, EventArgs e)
        {
            GUI_KhachHang frmKhachHang = new GUI_KhachHang();
            this.Hide();
        }

        private void toolStripMenuItem6_Click(object sender, EventArgs e)
        {
            GUI_SanPham frmCanHo = new GUI_SanPham();
            this.Hide();
            frmCanHo.ShowDialog();
        }

        private void toolStripMenuItem5_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Bạn có chắc chắn muốn thoát khỏi ứng dụng không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr == DialogResult.Yes)
                Application.Exit();
        }

        private void toolStripMenuItem7_Click(object sender, EventArgs e)
        {
            GUI_HoaDon frmHoaDon = new GUI_HoaDon();
            this.Hide();
            frmHoaDon.ShowDialog();
        }

        private void mnTimKiem_Click(object sender, EventArgs e)
        {
            GUI_TimKiem frmTimKiem = new GUI_TimKiem();
            this.Hide();
            frmTimKiem.ShowDialog();
        }

        private void dgvHoaDonBan_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                int rowIndex = e.RowIndex;
                if (rowIndex >= 0 && rowIndex < dgvHoaDonBan.Rows.Count)
                {
                    txtMaHDB.Text = dgvHoaDonBan[0, rowIndex].Value.ToString();

                    // Lấy thông tin từ DataGridView
                    string maKhachHang = dgvHoaDonBan.Rows[e.RowIndex].Cells["MaKH"].Value.ToString();
                    foreach (DataRowView item in cbbKH.Items)
                    {
                        if (item.Row["MaKH"].ToString() == maKhachHang)
                        {
                            cbbKH.SelectedItem = item;
                            break;
                        }
                    }

                    string maNhanVien = dgvHoaDonBan.Rows[e.RowIndex].Cells["MaNV"].Value.ToString();
                    foreach (DataRowView item in cbbNV.Items)
                    {
                        if (item.Row["MaNV"].ToString() == maNhanVien)
                        {
                            cbbNV.SelectedItem = item;
                            break;
                        }
                    }
                    string maSanPham = dgvHoaDonBan.Rows[e.RowIndex].Cells["MaSP"].Value.ToString();

                    // Tìm sản phẩm trong danh sách dữ liệu ComboBox
                    foreach (DataRowView item in cb_sanpham.Items)
                    {
                        if (item.Row["MaSP"].ToString() == maSanPham)
                        {
                            cb_sanpham.SelectedItem = item;
                            break;
                        }
                    }

                    int soLuongMua = Convert.ToInt32(dgvHoaDonBan[7, rowIndex].Value);
                    float giaban;
                    bool success = float.TryParse(dgvHoaDonBan[8, rowIndex].Value.ToString(), out giaban);
                    if (success)
                    {
                        tb_dongia.Text = giaban.ToString();
                    }
                    else
                    {
                        // Xử lý khi chuyển đổi không thành công
                    }
                    decimal tongTien = Convert.ToDecimal(dgvHoaDonBan[2, rowIndex].Value);
                    DateTime ngayMua = Convert.ToDateTime(dgvHoaDonBan[e.ColumnIndex, rowIndex].Value);

                    // Gán thông tin vào các TextBox khác
                    //cbbKH.Text = maKhachHang;
                    tb_sl.Text = soLuongMua.ToString();
                   
                    lblTongTien.Text = tongTien.ToString();
                    dtNgayBan.Value = ngayMua;
                }
            }catch(Exception)
            {
              
            }
            
            //int i = e.RowIndex;
            //txtMaHDB.Text = dgvHoaDonBan[0, i].Value.ToString();


            //if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            //{
            //    string value = dgvHoaDonBan[e.ColumnIndex, e.RowIndex].Value.ToString();
            //    DateTime dateTime;
            //    if (DateTime.TryParse(value, out dateTime))
            //    {
            //        dtNgayBan.Value = dateTime;
            //    }
            //}
            //lblTongTien.Text = dgvHoaDonBan[2, i].Value.ToString();
            //Tb_GhiChu.Text = dgvHoaDonBan[3, i].Value.ToString();
            //tb_sl.Text = dgvHoaDonBan[7, i].Value.ToString();
            //tb_dongia.Text = dgvHoaDonBan[8, i].Value.ToString();

        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            try
            {
                HoaDon hd = new HoaDon();
                hd.MaHD = Int32.Parse(txtMaHDB.Text);
                hd.MaNV = Int32.Parse(cbbNV.SelectedValue.ToString());
                hd.MaKH = Int32.Parse(cbbKH.SelectedValue.ToString());
                string datebirthday = string.Format("{0}/{1}/{2}", dtNgayBan.Value.Day, dtNgayBan.Value.Month, dtNgayBan.Value.Year);
                hd.NgayLap = DateTime.Parse(datebirthday);
                lblTongTien.Text = (Int32.Parse(tb_sl.Text) * Int32.Parse(tb_dongia.Text)).ToString();

                hd.TongTien = Int32.Parse(lblTongTien.Text);

                hd.MaSP = Int32.Parse(cb_sanpham.SelectedValue.ToString());
                hd.SoLuong = Int32.Parse(tb_sl.Text);
                hd.DonGia = Int32.Parse(tb_dongia.Text);
                hd.GhiChu = Tb_GhiChu.Text;
                if (bus_hd.kiemtramatrung(txtMaHDB.Text) > 0)
                {
                    MessageBox.Show("Mã hoá đơn đã tồn tại! Vui lòng nhập mã khác!!!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    if (bus_hd.ThemHD(hd))
                    {
                        MessageBox.Show("Thêm thành công!!!");
                        loaddgvHoaDonBan();
                    }
                }
            }catch(Exception)
            {
                MessageBox.Show("Lỗi nhập dữ liệu khác ??", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }

        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            try
            {
                HoaDon hd = new HoaDon();
                hd.MaHD = Int32.Parse(txtMaHDB.Text);
                hd.MaNV = Int32.Parse(cbbNV.SelectedValue.ToString());
                hd.MaKH = Int32.Parse(cbbKH.SelectedValue.ToString());
                hd.NgayLap = DateTime.Parse(dtNgayBan.Text);
                hd.MaSP = Int32.Parse(cb_sanpham.SelectedValue.ToString());
                hd.SoLuong = Int32.Parse(tb_sl.Text);
                hd.DonGia = Int32.Parse(tb_dongia.Text);
                hd.GhiChu = Tb_GhiChu.Text;
                lblTongTien.Text = (Int32.Parse(tb_sl.Text) * Int32.Parse(tb_dongia.Text)).ToString();

                hd.TongTien = Int32.Parse(tb_sl.Text) * Int32.Parse(tb_dongia.Text);


                if (bus_hd.SuaHD(hd))
                {
                    MessageBox.Show("Sửa thành công!!!");
                    loaddgvHoaDonBan();
                }
        }catch
            {
                MessageBox.Show("Lỗi !!!");
            }

}

        private void btnXoa_Click(object sender, EventArgs e)
        {
            string ma = txtMaHDB.Text;
            DialogResult kq = MessageBox.Show("Bạn có chắc chắn muốn xóa hóa đơn này !", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (kq == System.Windows.Forms.DialogResult.Yes)
            {
                if (bus_hd.XoaHD(ma))
                {
                    MessageBox.Show("Xóa thành công ");
                    loaddgvHoaDonBan();
                }
            }
        }

        

        

       

        
       

        

        private void label1_Click(object sender, EventArgs e)
        {

        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            // Kiểm tra xem người dùng có nhấn nút X không
            if (e.CloseReason == CloseReason.UserClosing)
            {
                // Gọi hàm Exit để đóng ứng dụng
                System.Windows.Forms.Application.Exit();
            }

            base.OnFormClosing(e);
        }

        private void dgvHoaDonBan_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
                 try
            {
                string maHoaDon = txtMaHDB.Text;
                string tenCuaHang = "Điện Thoại BÙI ÁNH"; // Thay thế bằng tên cửa hàng thực tế của bạn
                string diaChi = "Hưng Yên"; // Thay thế bằng địa chỉ cửa hàng thực tế của bạn
                string Tilte = "Hóa Đơn Bán";
                string TenKhachHang = cbbKH.Text;
                string TenNhanVien = cbbNV.Text;
                string tendienthoai = cb_sanpham.Text;
                int dongia = Int32.Parse(tb_dongia.Text);
                int SoLuong = Int32.Parse(tb_sl.Text);
                int TongTien = Int32.Parse(lblTongTien.Text);

                // Hiển thị hộp thoại chọn tệp
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "Excel Files|*.xlsx";
                saveFileDialog.Title = "Chọn vị trí lưu tệp Excel";
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string filePath = saveFileDialog.FileName;

                    // Tạo một gói Excel mới
                    using (ExcelPackage excelPackage = new ExcelPackage())
                    {
                        // Tạo một trang tính mới
                        ExcelWorksheet worksheet = excelPackage.Workbook.Worksheets.Add("HoaDon");

                        // Ghi tên cửa hàng và địa chỉ vào ô
                        worksheet.Cells["A1"].Value = tenCuaHang;
                        worksheet.Cells["A1:B1"].Merge = true;
                        worksheet.Cells["A1"].Style.Font.Bold = true;
                        worksheet.Cells["A1"].Style.Font.Size = 11;
                        worksheet.Cells["A1"].Style.Font.Color.SetColor(System.Drawing.Color.Red);

                        worksheet.Cells["A1"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                        worksheet.Cells["A2"].Value = diaChi;
                        worksheet.Cells["A2:B2"].Merge = true;
                        worksheet.Cells["A2"].Style.Font.Bold = true;
                        worksheet.Cells["A2"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;


                        worksheet.Cells["A3"].Value = Tilte;
                        worksheet.Cells["A3:L3"].Merge = true;

                        // Định dạng ô "tiêu đề"
                        using (ExcelRange range = worksheet.Cells["A3"])
                        {
                            range.Style.Font.Bold = true;
                            range.Style.Font.Size = 16;
                            range.Style.Font.Color.SetColor(System.Drawing.Color.Red);
                            range.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                            range.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                            range.Style.WrapText = true;
                        }

                        // Căn giữa cột chứa ô "tiêu đề"
                        worksheet.Column(1).Width = 30; // Đặt lại độ rộng cột A (nếu cần)
                        worksheet.Column(2).Width = 30; // Đặt lại độ rộng cột B (nếu cần)
                        worksheet.Cells["A3:B3"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        worksheet.Cells["A3:B3"].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                        //worksheet.Cells["A3"].Value = Tilte;
                        //worksheet.Cells["A3:B3"].Merge = true;
                        //worksheet.Cells["A3"].Style.Font.Bold = true;
                        //worksheet.Cells["A3"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;


                        // Ghi mã hóa đơn vào tệp Excel
                        worksheet.Cells["A4"].Value = "Mã hóa đơn";
                        worksheet.Cells["B4"].Value = maHoaDon;

                        // Định dạng ô chứa mã hóa đơn
                        using (ExcelRange range = worksheet.Cells["B3"])
                        {
                            range.Style.Font.Bold = true;
                            range.Style.Font.Size = 12;
                        }

                        worksheet.Cells["A6"].Value = "Tên Khách Hàng";
                        worksheet.Cells["A7"].Value = TenKhachHang;

                        // Định dạng ô chứa mã hóa đơn
                        using (ExcelRange range = worksheet.Cells["A7"])
                        {
                            range.Style.Font.Bold = true;
                            range.Style.Font.Size = 14;
                        }


                        worksheet.Cells["B6"].Value = "Tên nhân viên bán";
                        worksheet.Cells["B7"].Value = TenNhanVien;

                        worksheet.Cells["D6"].Value = "Tên sản phẩm";
                        worksheet.Cells["D7"].Value = tendienthoai;

                        worksheet.Cells["G6"].Value = "Đơn giá";
                        worksheet.Cells["G7"].Value = dongia;

                        worksheet.Cells["J6"].Value = "Số lượng";
                        worksheet.Cells["J7"].Value = SoLuong;

                        worksheet.Cells["L6"].Value = "Tổng tiền";
                        worksheet.Cells["L7"].Value = TongTien;

                        worksheet.Cells["O6"].Value = "Ngày Bán";
                        worksheet.Cells["O7"].Value = dtNgayBan.Value.ToString("dd/MM/yyyy");



                        // Định dạng các ô chứa dữ liệu
                        using (ExcelRange range = worksheet.Cells["B7:D7:G7:J7:L7:O7"])
                        {
                            range.Style.Font.Bold = true;
                            range.Style.Font.Size = 13;
                        }
                        // Tự động điều chỉnh kích thước các cột
                        worksheet.Cells.AutoFitColumns();

                        // Lưu tệp Excel
                        FileInfo excelFile = new FileInfo(filePath);
                        excelPackage.SaveAs(excelFile);
                    }

                    MessageBox.Show("Hóa đơn đã được xuất ra Excel thành công!");
                }
            }catch(Exception)
            {
                MessageBox.Show("Thông tin không đầy đủ không thể in hóa đơn");
            }
            
          


        }

        private void báoCáoThốngKêToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void okToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GUI_ThongKe_BaoCao frm_TK_BC = new GUI_ThongKe_BaoCao();
            this.Hide();
            frm_TK_BC.Show();
        }

        private void quảnLýHóaĐơnNhậpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Gui_HoaDonNhap frmHoaDon = new Gui_HoaDonNhap();
            this.Hide();
            frmHoaDon.ShowDialog();
        }
    }
}
