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
using GUI;

namespace Cao_Van_Dan
{
    public partial class GUI_NhanVien : Form
    {
        BUS_NhanVien bus_nv = new BUS_NhanVien();
        public GUI_NhanVien()
        {
            InitializeComponent();
        }
        void loaddgvNhanVien()
        {
            dgvNhanVien.DataSource = bus_nv.getData();
            dgvNhanVien.Columns[0].HeaderText = "Mã Nhân Viên";
            dgvNhanVien.Columns[1].HeaderText = "Tên Nhân Viên";
            dgvNhanVien.Columns[2].HeaderText = "Địa chỉ";
            dgvNhanVien.Columns[3].HeaderText = "Số điện thoại";
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

        private void toolStripMenuItem5_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Bạn có chắc chắn muốn thoát khỏi ứng dụng không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr == DialogResult.Yes)
                Application.Exit();
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            string manv = txtTimKiemMaNV.Text;
            string tennv = txtTimKiemTenNV.Text;
            string diachi = txtTimKiemDC.Text;
            dgvNhanVien.DataSource = bus_nv.TimKiem(manv, tennv, diachi);
            dgvNhanVien.Columns[0].HeaderText = "Mã Nhân Viên";
            dgvNhanVien.Columns[1].HeaderText = "Tên Nhân Viên";
            dgvNhanVien.Columns[2].HeaderText = "Địa chỉ";
            dgvNhanVien.Columns[3].HeaderText = "Số điện thoại";
        }

        private void GUI_NhanVien_Load(object sender, EventArgs e)
        {
            loaddgvNhanVien();
            
        }

        private void dgvNhanVien_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int i = e.RowIndex;
            txtMaNV.Text = dgvNhanVien[0, i].Value.ToString();
            txtTenNV.Text = dgvNhanVien[1, i].Value.ToString();
            txtDiaChi.Text = dgvNhanVien[2, i].Value.ToString();
            txtSDT.Text = dgvNhanVien[3, i].Value.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            foreach (Control ctr in groupBox2.Controls)
            {
                if (ctr is TextBox)
                {
                    ctr.Text = "";
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                NhanVien nv = new NhanVien();
                nv.MaNV = Int32.Parse(txtMaNV.Text);
                nv.TenNV = txtTenNV.Text;
                nv.DiaChi = txtDiaChi.Text;
                nv.SoDienThoai = txtSDT.Text;
                if (bus_nv.kiemtramatrung(txtMaNV.Text) > 0)
                {
                    MessageBox.Show("Mã nhân viên đã tồn tại! Vui lòng nhập mã khác!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    if (bus_nv.ThemNV(nv))
                    {
                        MessageBox.Show("Thêm nhân viên thành công!!!");
                        loaddgvNhanVien();
                    }
                }
            }catch(Exception)
            {
                MessageBox.Show("Dữ liệu không hợp lệ ? Thử lại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }

        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            try
            {
                NhanVien nv = new NhanVien();
                nv.MaNV = Int32.Parse(txtMaNV.Text);
                nv.TenNV = txtTenNV.Text;
                nv.DiaChi = txtDiaChi.Text;
                nv.SoDienThoai = txtSDT.Text;
                if (bus_nv.SuaNV(nv))
                {
                    MessageBox.Show("Sửa thông tin nhân viên thành công !!");
                    loaddgvNhanVien();
                }
            }catch(Exception)
            {
                MessageBox.Show("Lỗi !!");

            }

        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            try
            {
                string ma = txtMaNV.Text;
                DialogResult dr = MessageBox.Show("Bạn có chắc chắn muốn xoá nhân viên này?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (dr == DialogResult.Yes)
                {
                    if (bus_nv.XoaNV(ma))
                    {
                        MessageBox.Show("Xoá thành công!!!");
                        loaddgvNhanVien();
                    }
                }
            }catch(Exception)
            {
                MessageBox.Show("Vui lòng nhập đúng dữ liệu!!!");

            }

        }

        private void dgvNhanVien_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int i = e.RowIndex;
            txtMaNV.Text = dgvNhanVien[0, i].Value.ToString();
            txtTenNV.Text = dgvNhanVien[1, i].Value.ToString();
            txtDiaChi.Text = dgvNhanVien[2, i].Value.ToString();
            txtSDT.Text = dgvNhanVien[3, i].Value.ToString();
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

        private void toolStripMenuItem9_Click(object sender, EventArgs e)
        {
            GUI_KhachHang frm_KhachHang = new GUI_KhachHang();
            this.Hide();
            frm_KhachHang.ShowDialog();
        }

        private void mnTimKiem_Click(object sender, EventArgs e)
        {
            GUI_TimKiem frmTimKiem = new GUI_TimKiem();
            this.Hide();
            frmTimKiem.ShowDialog();
        }

        private void mnBCTK_Click(object sender, EventArgs e)
        {
            GUI_ThongKe_BaoCao frm_TK_BC = new GUI_ThongKe_BaoCao();
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

        private void báoCáoThốngKêToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GUI_ThongKe_BaoCao frmTimKiem = new GUI_ThongKe_BaoCao();
            this.Hide();
            frmTimKiem.ShowDialog();
        }

        private void quảnLýHóaĐơnNhậpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Gui_HoaDonNhap frmHoaDon = new Gui_HoaDonNhap();
            this.Hide();
            frmHoaDon.ShowDialog();
        }
    }
}
