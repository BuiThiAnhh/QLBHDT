using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BUS;
using Entities;
using GUI;
using Project;

namespace Cao_Van_Dan
{
    public partial class GUI_SanPham : Form
    {
        BUS_SanPham bus_ch = new BUS_SanPham();
        BUS_LoaiSanPham bus_dch = new BUS_LoaiSanPham();
        Bus_NhaCungCap bus_ncc = new Bus_NhaCungCap();
        
        void loaddgv()
        {
            dgvSanPham.DataSource = bus_ch.getData();
            dgvSanPham.Columns[0].HeaderText = "Mã sản phẩm";
            dgvSanPham.Columns[1].HeaderText = "Tên sản phẩm";
            dgvSanPham.Columns[2].HeaderText = "Giá bán";
            dgvSanPham.Columns[3].HeaderText = "Số lượng";
            dgvSanPham.Columns[4].HeaderText = "Ngày tạo";
            dgvSanPham.Columns[5].HeaderText = "Nhà cung cấp";
            dgvSanPham.Columns[6].HeaderText = "Loại SP";
        }
        void loadcbb()
        {
            cbbDayCH.DataSource = bus_dch.getData();
            cbbDayCH.DisplayMember = "TenLoai";
            cbbDayCH.ValueMember = "MaLoai";
            cbbNhaCungCap.DataSource = bus_ncc.getdata();
            cbbNhaCungCap.DisplayMember = "TenNCC";
            cbbNhaCungCap.ValueMember = "MaNCC";
            cb_hedieuhanh.DataSource = bus_ch.GetHeDieuHanh();
            cb_hang.DataSource = bus_ch.GetHang();
            cb_hedieuhanh.DisplayMember = "TenHeDieuHanh";
            cb_hedieuhanh.ValueMember = "HeDieuHanhID";
            cb_hang.DisplayMember = "TenHang";
            cb_hang.ValueMember = "HangID";

        }
        
        public GUI_SanPham()
        {
            InitializeComponent();
        }

        private void FormSP_Load(object sender, EventArgs e)
        {
            loaddgv();
            loadcbb();
           
        }


        private void dgvSanPham_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int i = e.RowIndex;
            txtSoNha.Text = dgvSanPham[0, i].Value.ToString();
            int dchId;
            if (int.TryParse(dgvSanPham[4, i].Value.ToString(), out dchId))
            {
                cbbDayCH.SelectedValue = dchId.ToString(); // sử dụng SelectedValue để chọn giá trị tương ứng trong combobox
            }
            int hangId;
            if (int.TryParse(dgvSanPham[5, i].Value.ToString(), out hangId))
            {
                cb_hang.SelectedValue = hangId.ToString(); // sử dụng SelectedValue để chọn giá trị tương ứng trong combobox
            }

            txtTT.Text = dgvSanPham[3, i].Value.ToString();
            txtDG.Text = dgvSanPham[2, i].Value.ToString();
            txtDienTich.Text = dgvSanPham[1, i].Value.ToString();
           
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                string value = dgvSanPham[e.ColumnIndex, e.RowIndex].Value.ToString();
                DateTime dateTime;
                if (DateTime.TryParse(value, out dateTime))
                {
                    dateTimePicker1.Value = dateTime;
                }
            }
        }


        private void btnMoi_Click(object sender, EventArgs e)
        {
            foreach (Control ctr in groupBox1.Controls)
            {
                if (ctr is TextBox || ctr is ComboBox || ctr is DateTimePicker)
                {
                    ctr.Text = "";
                }
            }
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            try
            {
                SanPham ch = new SanPham();
                ch.MaSP = Int32.Parse(txtSoNha.Text);
                ch.MaLoai = int.Parse(cbbDayCH.SelectedValue.ToString());
                ch.TenSP = txtDienTich.Text;
                ch.MaNCC = int.Parse(cbbNhaCungCap.SelectedValue.ToString());
                ch.GiaBan = float.Parse(txtDG.Text);
                ch.SoLuong = Int32.Parse(txtTT.Text);
                ch.HangID = int.Parse(cb_hang.SelectedValue.ToString());
                ch.HeDieuHanhID = int.Parse(cb_hedieuhanh.SelectedValue.ToString());
              

                string formattedDate = dateTimePicker1.Value.ToString("yyyy/MM/dd");
                ch.NgayNhap = DateTime.Parse(formattedDate);

                if (bus_ch.kiemtramatrung(txtSoNha.Text) == 1)
                {
                    MessageBox.Show("Mã đã tồn tại, vui lòng nhập lại mã!!!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    if (bus_ch.ThemCH(ch) == true)
                    {
                        MessageBox.Show("Thêm thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        loaddgv();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }

        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            try
            {
                SanPham ch = new SanPham();
                ch.MaSP = Int32.Parse(txtSoNha.Text);
                ch.MaLoai = int.Parse(cbbDayCH.SelectedValue.ToString());
                ch.TenSP = txtDienTich.Text;
                ch.HangID = int.Parse(cb_hang.SelectedValue.ToString());
                ch.HeDieuHanhID = int.Parse(cb_hedieuhanh.SelectedValue.ToString());
                ch.GiaBan = float.Parse(txtDG.Text);
                ch.SoLuong = int.Parse(txtTT.Text);
                ch.MaNCC = int.Parse(cbbNhaCungCap.SelectedValue.ToString());
                if (bus_ch.SuaCH(ch) == true)
                {
                    MessageBox.Show("Sửa thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    loaddgv();
                }
            }catch(Exception)
            {
                MessageBox.Show("Lỗi", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }

        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            try
            {
                int ma = Int32.Parse(txtSoNha.Text);
                if (bus_ch.XoaCH(ma) == true)
                {
                    MessageBox.Show("Xoá thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    loaddgv();
                }
            }catch(Exception)
            {
                MessageBox.Show("Lỗi", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

            }

        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Bạn có chắc chắn muốn thoát khỏi ứng dụng không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr == DialogResult.Yes)
                Application.Exit();
        }
        void loaddgvTimKiemSP(string masp, string dientich, string giaban)
        {
            dgvSanPham.DataSource = bus_ch.TimKiem(masp, dientich, giaban);
            if (dgvSanPham.DataSource == null)
            {
                MessageBox.Show("Bạn chưa nhập thông tin tìm kiếm sản phẩm!!!");
            }
            else
            {
                dgvSanPham.Columns[0].HeaderText = "Mã sản phẩm";
                dgvSanPham.Columns[1].HeaderText = "Tên sản phẩm";
                dgvSanPham.Columns[2].HeaderText = "Giá bán";
                dgvSanPham.Columns[3].HeaderText = "Tình trạng";
                dgvSanPham.Columns[4].HeaderText = "Mã loại sản phẩm";
            }
        }
        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            try
            {
                string sonha = txtTimKiemsonha.Text;
                string dientich = txtTimKiemdtch.Text;
                string giaban = txtTimKiemgbch.Text;
                loaddgvTimKiemSP(sonha, dientich, giaban);
            }catch
            {
                MessageBox.Show("Lỗi", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

            }

        }

        private void pbThoat_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Bạn có chắc chắn muốn thoát khỏi ứng dụng không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr == DialogResult.Yes)
                Application.Exit();
        }

        private void dgvSanPham_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void mnTimKiem_Click(object sender, EventArgs e)
        {
            GUI_TimKiem frmTimKiem = new GUI_TimKiem();
            this.Hide();
            frmTimKiem.ShowDialog();
        }

        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            GUI_DangNhap frmDangNhap = new GUI_DangNhap();
            this.Hide();
            frmDangNhap.ShowDialog();
        }

        private void toolStripMenuItem5_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Bạn có chắc chắn muốn thoát khỏi ứng dụng không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr == DialogResult.Yes)
                Application.Exit();
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
            GUI_NhanVien frmNhanVien = new GUI_NhanVien();
            this.Hide();
            frmNhanVien.ShowDialog();
        }

        private void toolStripMenuItem9_Click(object sender, EventArgs e)
        {
            GUI_KhachHang frmKhachHang = new GUI_KhachHang();
            this.Hide();
            frmKhachHang.ShowDialog();
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

        private void txtDG_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtSoNha_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtTT_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void quảnLýThờiTrangToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GUI_SanPham frmCanHo = new GUI_SanPham();
            this.Hide();
            frmCanHo.ShowDialog();
        }

        private void quảnLýNhânViênToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GUI_HoaDon frmHoaDon = new GUI_HoaDon();
            this.Hide();
            frmHoaDon.ShowDialog();
        }

        private void quảnLýNhânViênToolStripMenuItem1_Click(object sender, EventArgs e)
        {

            GUI_NhanVien frmNhanVien = new GUI_NhanVien();
            this.Hide();
            frmNhanVien.ShowDialog();
        }

        private void quảnLýKháchHàngToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GUI_KhachHang frmKhachHang = new GUI_KhachHang();
            this.Hide();
            frmKhachHang.ShowDialog();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void báoCáoThốngKêToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GUI_ThongKe_BaoCao frm_TK_BC = new GUI_ThongKe_BaoCao();
            this.Hide();
            frm_TK_BC.ShowDialog();
        }

        private void quảnLýHóaĐơnNhậpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Gui_HoaDonNhap frmHoaDon = new Gui_HoaDonNhap();
            this.Hide();
            frmHoaDon.ShowDialog();
        }
    }
}
