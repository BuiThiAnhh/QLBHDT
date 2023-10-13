using BUS;
using Cao_Van_Dan;
using DTO;
using Entities;
using OfficeOpenXml.Style;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GUI
{
    public partial class Gui_HoaDonNhap : Form
    {

        BUS_HoaDonNhap bus_hd = new BUS_HoaDonNhap();
        BUS_ChiTietHDB bus_cthdb = new BUS_ChiTietHDB();
        BUS_NhanVien bus_nv = new BUS_NhanVien();
        BUS_KhachHang bus_kh = new BUS_KhachHang();
        BUS_SanPham bus_ch = new BUS_SanPham();
        public Gui_HoaDonNhap()
        {
            InitializeComponent();
        }

        private void toolStripMenuItem6_Click(object sender, EventArgs e)
        {
            GUI_SanPham frmCanHo = new GUI_SanPham();
            this.Hide();
            frmCanHo.ShowDialog();
        }

        private void toolStripMenuItem7_Click(object sender, EventArgs e)
        {

            GUI_HoaDon frmHoaDon = new GUI_HoaDon();
            this.Hide();
            frmHoaDon.ShowDialog();
        }

        private void toolStripMenuItem8_Click(object sender, EventArgs e)
        {

            GUI_NhanVien frmHoaDon = new GUI_NhanVien();
            this.Hide();
            frmHoaDon.ShowDialog();
        }

        private void toolStripMenuItem9_Click(object sender, EventArgs e)
        {
            GUI_KhachHang frmHoaDon = new GUI_KhachHang();
            this.Hide();
            frmHoaDon.ShowDialog();
        }

        private void pbThoat_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Bạn có chắc chắn muốn thoát khỏi ứng dụng không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr == DialogResult.Yes)
                Application.Exit();
        }

        private void mnTimKiem_Click(object sender, EventArgs e)
        {
            GUI_TimKiem frmTimKiem = new GUI_TimKiem();
            this.Hide();
            frmTimKiem.ShowDialog();
        }

        private void okToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GUI_ThongKe_BaoCao frm_TK_BC = new GUI_ThongKe_BaoCao();
            this.Hide();
            frm_TK_BC.Show();
        }

        private void menuStrip2_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }
        void loadcbbNV()
        {
            cbbNV.DataSource = bus_nv.getData();
            cbbNV.DisplayMember = "TenNV";
            cbbNV.ValueMember = "MaNV";
           
            cb_sanpham.DataSource = bus_ch.getData();
            cb_sanpham.DisplayMember = "TenSP";
            cb_sanpham.ValueMember = "MaSP";
        }

        private void cbbNV_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void Gui_HoaDonNhap_Load(object sender, EventArgs e)
        {
            loadcbbNV();
            loaddgvHoaDonBan();
        }
        void loaddgvHoaDonBan()
        {
            dgvHoaDonBan.DataSource = bus_hd.getData();
           


        }

        private void dgvHoaDonBan_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                int i = e.RowIndex;
                txtMaHDB.Text = dgvHoaDonBan[0, i].Value.ToString();


                if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
                {
                    string value = dgvHoaDonBan[e.ColumnIndex, e.RowIndex].Value.ToString();
                    DateTime dateTime;
                    if (DateTime.TryParse(value, out dateTime))
                    {
                        dtNgayBan.Value = dateTime;
                    }
                }
                lblTongTien.Text = dgvHoaDonBan[5, i].Value.ToString();
                tb_dongia.Text = dgvHoaDonBan[3, i].Value.ToString();
                tb_sl.Text = dgvHoaDonBan[4, i].Value.ToString();
            }catch(Exception)
            {

            }
          
           
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            try
            {
                HoaDonNhap hd = new HoaDonNhap();
                hd.HoaDonNhapID = Int32.Parse(txtMaHDB.Text);
                hd.MaNV = Int32.Parse(cbbNV.SelectedValue.ToString());
                
                string datebirthday = string.Format("{0}/{1}/{2}", dtNgayBan.Value.Day, dtNgayBan.Value.Month, dtNgayBan.Value.Year);
                hd.NgayNhap = DateTime.Parse(datebirthday);
                lblTongTien.Text = (Int32.Parse(tb_sl.Text) * Int32.Parse(tb_dongia.Text)).ToString();

                hd.TongTien = Int32.Parse(lblTongTien.Text);

                hd.MaSP = Int32.Parse(cb_sanpham.SelectedValue.ToString());
                hd.SoLuong = Int32.Parse(tb_sl.Text);
                hd.DonGia = Int32.Parse(tb_dongia.Text);
               
                if (bus_hd.kiemtramatrung(txtMaHDB.Text) > 0)
                {
                    MessageBox.Show("Mã hoá đơn đã tồn tại! Vui lòng nhập mã khác!!!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    if (bus_hd.ThemHDN(hd))
                    {
                        MessageBox.Show("Thêm thành công!!!");
                        loaddgvHoaDonBan();
                    }
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Lỗi nhập dữ liệu khác ??", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            try
            {
                HoaDonNhap hd = new HoaDonNhap();
                hd.HoaDonNhapID = Int32.Parse(txtMaHDB.Text);
                hd.MaNV = Int32.Parse(cbbNV.SelectedValue.ToString());
                
                hd.NgayNhap = DateTime.Parse(dtNgayBan.Text);
                hd.MaSP = Int32.Parse(cb_sanpham.SelectedValue.ToString());
                hd.SoLuong = Int32.Parse(tb_sl.Text);
                hd.DonGia = Int32.Parse(tb_dongia.Text);
                
                lblTongTien.Text = (Int32.Parse(tb_sl.Text) * Int32.Parse(tb_dongia.Text)).ToString();

                hd.TongTien = Int32.Parse(tb_sl.Text) * Int32.Parse(tb_dongia.Text);


                if (bus_hd.SuaHDN(hd))
                {
                    MessageBox.Show("Sửa thành công!!!");
                    loaddgvHoaDonBan();
                }
            }
            catch
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
                if (bus_hd.XoaHDN(ma))
                {
                    MessageBox.Show("Xóa thành công ");
                    loaddgvHoaDonBan();
                }
            }
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

        private void button1_Click(object sender, EventArgs e)
        {

            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Excel Files|*.xlsx";
            saveFileDialog.Title = "Export to Excel";

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                string filePath = saveFileDialog.FileName;
                ExportToExcel(dgvHoaDonBan, filePath);
                MessageBox.Show("Export completed!");
            }
        }
    }
}
