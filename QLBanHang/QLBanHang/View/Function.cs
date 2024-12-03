using Guna.UI2.WinForms;
using QLBanHang.Controller;
using QLBanHang.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLBanHang.View
{
    public partial class Function : Form
    {
        private string imageEmployee = "D:\\images-NET\\user (1).png";
        private string imageProduct = "D:\\images-NET\\product.jpg";
        private Employee employee = new Employee();
        public Function(Employee employ)
        {
            InitializeComponent();
            employee = employ;
            PhanQuyen();
        }
        #region Employee
        //Phân quyền
        void PhanQuyen()
        {
            if (employee.Role.Equals("Admin"))
            {
                LoadAllDataEmployee();
            }
            else if(employee.Role.Equals("Quản lý"))
            {
                LoadDataEmployeeRole();
                cboFindEmployee.Enabled = false;
            }
            else
            {
                btnQuanLyNhanVien.Visible = false;
                pnQuanLyNhanVien.Visible = false;
                pnThemNhanVien.Visible = false;
            }
            ptbImageEmployee.Image = Image.FromFile(employee.Employeeimage);
            txtName.Text = employee.EmployeeName;
            txtRole.Text = employee.Role;
        }
        // Load ALL Data Employee
        void LoadAllDataEmployee()
        {
            DbEmployee dbEmployee = new DbEmployee();
            List<Employee> employees = dbEmployee.GetEmployee();
            pnDanhSachNhanVien.Controls.Clear();
            if (employees != null)
            {
                foreach (Employee employee in employees)
                {
                    DisplayEmployee displayEmployee = new DisplayEmployee(pnThemNhanVien, employee);
                    pnDanhSachNhanVien.Controls.Add(displayEmployee);
                }
                pnDanhSachNhanVien.Refresh();
                pnThemNhanVien.Visible = false;
            }
        }
        // Load Data Employee Role
        void LoadDataEmployeeRole()
        {
            DbEmployee dbEmployee = new DbEmployee();
            List<Employee> employees = dbEmployee.GetEmployeeRole();
            pnDanhSachNhanVien.Controls.Clear();
            if (employees != null)
            {
                foreach (Employee employee in employees)
                {
                    DisplayEmployee displayEmployee = new DisplayEmployee(pnThemNhanVien, employee);
                    pnDanhSachNhanVien.Controls.Add(displayEmployee);
                }
                pnDanhSachNhanVien.Refresh();
                pnThemNhanVien.Visible = false;
            }
            cboChucVu.SelectedIndex = 1;
            cboChucVu.Enabled = false;
        }
        //Check Insert Employee
        public bool CheckEmployee()
        {
            if (txtUser.Text.Equals(""))
            {
                lbCheckUser.ForeColor = Color.Red;
                lbCheckUser.Text = "Tài khoản không được để trống";
                lbCheckUser.Visible = true;
                txtUser.Focus();
                return false;
            }
            if (txtPassword.Text.Equals(""))
            {
                lbCheckPass.ForeColor = Color.Red;
                lbCheckPass.Text = "Mật khẩu không được để trống";
                lbCheckPass.Visible = true;
                txtPassword.Focus();
                return false;
            }
            if (txtEmployeeName.Text.Equals(""))
            {
                lbCheckName.ForeColor = Color.Red;
                lbCheckName.Text = "Tên không được để trống";
                lbCheckName.Visible = true;
                txtEmployeeName.Focus();
                return false;
            }
            string[] name = txtEmployeeName.Text.Split(' ');
            for (int i = 0; i < name.Length; i++)
            {
                name[i] = char.ToUpper(name[i][0]) + name[i].Substring(1).ToLower();
            }
            txtEmployeeName.Text = String.Join(" ", name);
            if (txtEmployeePhone.Text.Equals(""))
            {
                lbCheckPhone.ForeColor = Color.Red;
                lbCheckPhone.Text = "Số điện thoại không được để trống";
                lbCheckPhone.Visible = true;
                txtEmployeePhone.Focus();
                return false;
            }
           
            string phone = "";
            foreach (Char c in txtEmployeePhone.Text)
            {
                if (Char.IsDigit(c))
                {
                    phone += c;
                }
            }
            if (phone.Length != 10)
            {
                lbCheckPhone.Text = "Vui lòng nhập đúng định dạng số điện thoại";
                lbCheckPhone.ForeColor = Color.Red;
                lbCheckPhone.Visible = true;
                txtEmployeePhone.Focus();
                return false;
            }
            if (phone[0] != '0')
            {
                txtEmployeePhone.Focus();
                lbCheckPhone.ForeColor = Color.Red;
                lbCheckPhone.Visible = true;
                lbCheckPhone.Text = "Vui lòng nhập đúng định dạng số điện thoại";
                return false;
            }
            if (txtEmployeeAddress.Text.Equals(""))
            {
                lbCheckAdress.Text = "Địa chỉ không được để trống";
                lbCheckAdress.ForeColor = Color.Red;
                lbCheckAdress.Visible = true;
                txtEmployeeAddress.Focus();
                return false;
            }
            string[] address = txtEmployeeAddress.Text.Split(' ');
            for (int i = 0; i < address.Length; i++)
            {
                address[i] = char.ToUpper(address[i][0]) + address[i].Substring(1).ToLower();
            }
            txtEmployeeAddress.Text = String.Join(" ", address);
            if (txtEmployeeEmail.Text.Equals(""))
            {
                lbCheckEmail.Text = "email không được để trống";
                lbCheckEmail.ForeColor = Color.Red;
                lbCheckEmail.Visible = true;
                txtEmployeeEmail.Focus();
                return false;
            }
            try
            {
                MailAddress mailaddress = new MailAddress(txtEmployeeEmail.Text);
            }
            catch
            {
                lbCheckEmail.Text = "Vui lòng nhập đúng định dạng email";
                lbCheckEmail.ForeColor = Color.Red;
                lbCheckEmail.Visible = true;
                txtEmployeeEmail.Focus();
                return false;
            }
            return true;
        }

        // Exit Apllication
        private void ptbClose_Click(object sender, EventArgs e)
        {
            pnThemNhanVien.Visible = false;
            PhanQuyen();
        }
        void ResetTextEmployee()
        {
            txtUser.Text = "";
            txtPassword.Text = "";
            txtEmployeeName.Text = "";
            cboGender.SelectedIndex = 0;
            cboChucVu.SelectedIndex = 0;
            cboBirthday.Value = DateTime.Now;
            txtEmployeePhone.Text = "";
            txtEmployeeAddress.Text = "";
            txtEmployeeEmail.Text = "";
            ptbAnhDaiDien.Image = Image.FromFile(imageEmployee);
        }
        // Display panel Add Employee
        private void btnAddEmployee_Click(object sender, EventArgs e)
        {
            ResetTextEmployee();
            pnThemNhanVien.BringToFront();
            pnThemNhanVien.Visible = true;
            btnUpdateEmployee.Visible = false;
            btnDeleteEmployee.Visible = false;
            btnChonAnh.Visible = false;
            btnEmployeeSave.Visible = true;
            btnAddimageEmployee.Visible = true;
        }
        // Get local Image Employee
        private void btnAddimageEmployee_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog();
            openFile.Title = "Chọn ảnh";
            openFile.Filter = "Ảnh (*.jpg;*.jpeg;*.png;*.bmp)|*.jpg;*.jpeg;*.png;*.bmp"; // Lọc file ảnh
            openFile.InitialDirectory = @"D:\images-NET"; // Thư mục mặc định khi mở
            openFile.Multiselect = false; // Chỉ cho phép chọn 1 ảnh
            if(openFile.ShowDialog() == DialogResult.OK)
            {
                imageEmployee = openFile.FileName; //Lấy đường dẫn của ảnh từ hộp thoại chọn file (openFile.FileName).
                ptbAnhDaiDien.Image = Image.FromFile(imageEmployee); // Tải ảnh từ đường dẫn đó vào PictureBox 
                ptbAnhDaiDien.SizeMode = PictureBoxSizeMode.StretchImage; //Điều chỉnh kích thước ảnh sao cho nó kéo giãn để lấp đầy toàn bộ diện tích của PictureBox (StretchImage).
            }
        }
        // Add Employee 
        private void btnEmployeeSave_Click(object sender, EventArgs e)
        {
            DbEmployee dbEmployee = new DbEmployee();
            
            if (CheckEmployee())
            {
                if (dbEmployee.InsertEmployee(txtUser.Text,
                                              Password.HashPassword(txtPassword.Text), 
                                              txtEmployeeName.Text, 
                                              cboGender.SelectedItem.ToString(), 
                                              cboChucVu.SelectedItem.ToString(), 
                                              txtEmployeePhone.Text,
                                              cboBirthday.Value, 
                                              txtEmployeeAddress.Text, 
                                              txtEmployeeEmail.Text, 
                                              imageEmployee))
                {
                    MessageBox.Show("Thêm nhân viên thành công!", "Thông báo");
                    ResetTextEmployee();
                }
                else
                {
                    MessageBox.Show("Thêm nhân viên thất bại!", "Thông báo");
                }
            }
        }
        private void txtFindEmployee_TextChanged(object sender, EventArgs e)
        {
            DbEmployee dbEmployee = new DbEmployee();
            List<Employee> employees = new List<Employee>();
            if (cboFindEmployee.SelectedIndex == 0)
            {
                employees = dbEmployee.FindEmployeeName(txtFindEmployee.Text, employee.Role);
                pnDanhSachNhanVien.Controls.Clear();
                foreach (Employee emp in employees)
                {
                    DisplayEmployee displayEmployee = new DisplayEmployee(pnThemNhanVien, emp);
                    pnDanhSachNhanVien.Controls.Add(displayEmployee);
                }
                pnDanhSachNhanVien.Refresh();
            }
            else
            {
                employees = dbEmployee.FindEmployeeRole(txtFindEmployee.Text);
                pnDanhSachNhanVien.Controls.Clear();
                foreach (Employee emp in employees)
                {
                    DisplayEmployee displayEmployee = new DisplayEmployee(pnThemNhanVien, emp);
                    pnDanhSachNhanVien.Controls.Add(displayEmployee);
                }
                pnDanhSachNhanVien.Refresh();
            }
            if (string.IsNullOrEmpty(txtFindEmployee.Text))
            {
                PhanQuyen();
            }
        }
        #endregion
        #region UI Employee
        void DisplayPanel(Guna2Panel panel)
        {
            foreach(var item in this.Controls)
            {
                if (item is Guna2Panel){
                    Guna2Panel p = (Guna2Panel)item;
                    if( p != pnFunction)
                    {
                        p.Visible = false;
                    }
                }
            }
            panel.Visible = true;
            panel.BringToFront();
        }
        void DisplayButton(Guna2Button button)
        {
            foreach(var item in pnFunction.Controls)
            {
                if (item is Guna2Button)
                {
                    Guna2Button b = (Guna2Button)item;
                    b.ForeColor = Color.White;
                    b.FillColor = Color.MediumSlateBlue;
                }
                
                button.ForeColor = Color.MediumSlateBlue;
                button.FillColor = Color.White;
            }
        }
        private void txtUser_TextChanged(object sender, EventArgs e)
        {
            lbCheckUser.Visible = false;
        }
        private void txtPassword_TextChanged(object sender, EventArgs e)
        {
            lbCheckPass.Visible = false;
            txtPassword.PasswordChar = '*';
        }
        private void txtPassword_Click(object sender, EventArgs e)
        {
            if (txtPassword.PasswordChar.Equals('*'))
            {
                txtPassword.PasswordChar = '\0';
            }
            else
            {
                txtPassword.PasswordChar = '*';
            }
        }
        private void txtEmployeePhone_TextChanged(object sender, EventArgs e)
        {
            lbCheckPhone.Visible = false;
        }

        private void txtEmployeeAddress_TextChanged(object sender, EventArgs e)
        {
            lbCheckAdress.Visible = false;
        }

        private void txtEmployeeEmail_TextChanged(object sender, EventArgs e)
        {
            lbCheckEmail.Visible = false;
        }

        private void btnQuanLyNhanVien_Click(object sender, EventArgs e)
        {
            DisplayPanel(pnQuanLyNhanVien);
            DisplayButton(btnQuanLyNhanVien);
        }

        private void pnDisplayEmployee_Click(object sender, EventArgs e)
        {
            pnDanhSachNhanVien.Visible = false;
        }
        #endregion
        private void btnTrangChu_Click(object sender, EventArgs e)
        {
            DisplayButton(btnTrangChu);
        }
        #region Đăng xuất tài khoản
        private void btnDangXuat_Click(object sender, EventArgs e)
        {
            DisplayButton(btnDangXuat);
            if(MessageBox.Show("Bạn có muốn đăng xuất không?", "Thông báo", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                this.Hide();
                Login login = new Login();
                login.ShowDialog();
                this.Close();
            }
            else
            {
                btnDangXuat.FillColor = Color.MediumSlateBlue;
                btnDangXuat.ForeColor = Color.White;
            }
        }

        #endregion
        #region Nhà cung cấp
        private void btnQuanLyNhaCungCap_Click(object sender, EventArgs e)
        {
            DisplayPanel(pnQuanLyNhaCungCap);
            DisplayButton(btnQuanLyNhaCungCap);
            btnSupplierSave.Enabled = false;
            LoadDataSupplier();
            dgvSupplier.ReadOnly = true;
        }
        // Reset text supplier
        void ResetTextSupplier()
        {
            txtSupplierID.Text = "";
            txtSupplierName.Text = "";
            txtSupplierAddress.Text = "";
            txtSupplierPhone.Text = "";
            txtSupplierEmail.Text = "";
        }
        // Check insert update supplier
        bool CheckSupplier()
        {
            if(string.IsNullOrEmpty(txtSupplierID.Text))
            {
                lbCheckSupplierID.Visible = true;
                lbCheckSupplierID.Text = "Vui lòng nhập ID";
                lbCheckSupplierID.ForeColor = Color.Red;
                return false;
            }
            if (txtSupplierName.Text.Equals(""))
            {
                lbCheckSupplierName.ForeColor = Color.Red;
                lbCheckSupplierName.Text = "Tên không được để trống";
                lbCheckSupplierName.Visible = true;
                txtSupplierName.Focus();
                return false;
            }
            string[] name = txtSupplierName.Text.Split(' ');
            for (int i = 0; i < name.Length; i++)
            {
                name[i] = char.ToUpper(name[i][0]) + name[i].Substring(1).ToLower();
            }
            txtSupplierName.Text = String.Join(" ", name);
            if (txtSupplierPhone.Text.Equals(""))
            {
                lbCheckSupplierPhone.ForeColor = Color.Red;
                lbCheckSupplierPhone.Text = "Số điện thoại không được để trống";
                lbCheckSupplierPhone.Visible = true;
                txtSupplierPhone.Focus();
                return false;
            }

            string phone = "";
            foreach (Char c in txtSupplierPhone.Text)
            {
                if (Char.IsDigit(c))
                {
                    phone += c;
                }
            }
            if (phone.Length != 10)
            {
                lbCheckSupplierPhone.Text = "Vui lòng nhập đúng định dạng số điện thoại";
                lbCheckSupplierPhone.ForeColor = Color.Red;
                lbCheckSupplierPhone.Visible = true;
                txtSupplierPhone.Focus();
                return false;
            }
            if (phone[0] != '0')
            {
                txtSupplierPhone.Focus();
                lbCheckSupplierPhone.ForeColor = Color.Red;
                lbCheckSupplierPhone.Visible = true;
                lbCheckSupplierPhone.Text = "Vui lòng nhập đúng định dạng số điện thoại";
                return false;
            }
            if (txtSupplierAddress.Text.Equals(""))
            {
                lbCheckSupplierAddress.Text = "Địa chỉ không được để trống";
                lbCheckSupplierAddress.ForeColor = Color.Red;
                lbCheckSupplierAddress.Visible = true;
                txtSupplierAddress.Focus();
                return false;
            }
            string[] address = txtSupplierAddress.Text.Split(' ');
            for (int i = 0; i < address.Length; i++)
            {
                address[i] = char.ToUpper(address[i][0]) + address[i].Substring(1).ToLower();
            }
            txtSupplierAddress.Text = String.Join(" ", address);
            if (txtSupplierEmail.Text.Equals(""))
            {
                lbCheckSupplierEmail.Text = "email không được để trống";
                lbCheckSupplierEmail.ForeColor = Color.Red;
                lbCheckSupplierEmail.Visible = true;
                txtSupplierEmail.Focus();
                return false;
            }
            try
            {
                MailAddress mailaddress = new MailAddress(txtSupplierEmail.Text);
            }
            catch
            {
                lbCheckSupplierEmail.Text = "Vui lòng nhập đúng định dạng email";
                lbCheckSupplierEmail.ForeColor = Color.Red;
                lbCheckSupplierEmail.Visible = true;
                txtSupplierEmail.Focus();
                return false;
            }
            return true;
        }
        // Load Data Supplier in Database
        void LoadDataSupplier()
        {
            dbSupplier dbSupplier = new dbSupplier();
            DataSet ds = dbSupplier.GetSupplier();
            dgvSupplier.DataSource = null;
            dgvSupplier.DataSource = ds.Tables[0];
        }
        private void btnSupplierAdd_Click(object sender, EventArgs e)
        {
            ResetTextSupplier();
            btnSupplierUpdate.Enabled = false;
            btnSupplierAdd.Enabled = false;
            btnSupplierDelete.Enabled = false;
            btnSupplierSave.Enabled = true;
            txtSupplierID.Enabled = true;
        }
        #region Thêm nhà cung cấp
        private void btnSupplierSave_Click(object sender, EventArgs e)
        {
            dbSupplier dbSupp = new dbSupplier();
            DataSet ds = dbSupp.GetSupplier();
            dgvSupplier.DataSource = null;
            dgvSupplier.DataSource= ds.Tables[0];
            if (CheckSupplier())
            {
                if(ds.Tables[0].Rows.Count < 0)
                {
                    dbSupplier dbSupplier = new dbSupplier();
                    dbSupplier.InsertSupplier(txtSupplierID.Text, txtSupplierName.Text, txtSupplierAddress.Text, txtSupplierPhone.Text, txtSupplierEmail.Text);
                    MessageBox.Show("Thêm nhà cung cấp thành công!", "Thông báo");
                    ResetTextSupplier();
                    LoadDataSupplier();
                }
                else
                {
                    foreach (DataGridViewRow row in dgvSupplier.Rows)
                    {
                        if (txtSupplierID.Text.Equals(row.Cells["SupplierID"].Value?.ToString()))
                        {
                            MessageBox.Show("Đã có ID này!", "Thông báo");
                            return; // Thoát nếu phát hiện ID đã tồn tại
                        }
                    }
                    // Thêm nhà cung cấp mới nếu không trùng ID
                    dbSupplier dbSupplier = new dbSupplier();
                    dbSupplier.InsertSupplier(txtSupplierID.Text, txtSupplierName.Text, txtSupplierAddress.Text, txtSupplierPhone.Text, txtSupplierEmail.Text);
                    MessageBox.Show("Thêm nhà cung cấp thành công!", "Thông báo");
                    ResetTextSupplier();
                    LoadDataSupplier();
                }
            }
        }
        #endregion
        private void txtSupplierID_TextChanged(object sender, EventArgs e)
        {
            lbCheckSupplierID.Visible = false;
        }

        private void txtSupplierName_TextChanged(object sender, EventArgs e)
        {
            lbCheckSupplierName.Visible = false;
        }

        private void txtSupplierPhone_TextChanged(object sender, EventArgs e)
        {
            lbCheckSupplierPhone.Visible = false;
        }

        private void txtSupplierEmail_TextChanged(object sender, EventArgs e)
        {
            lbCheckSupplierEmail.Visible=false;
        }

        private void txtSupplierAddress_TextChanged(object sender, EventArgs e)
        {
            lbCheckSupplierAddress.Visible=false;
        }

        private void btnSupplierCancel_Click(object sender, EventArgs e)
        {
            btnSupplierAdd.Enabled = true;
            btnSupplierUpdate.Enabled = true;
            btnSupplierDelete.Enabled = true;
            ResetTextSupplier();
            txtSupplierID.Enabled = true;
        }

        private void dgvSupplier_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if(e.RowIndex >= 0)
            {
                txtSupplierID.Text = dgvSupplier.Rows[e.RowIndex].Cells[0].Value.ToString();
                txtSupplierName.Text = dgvSupplier.Rows[e.RowIndex].Cells[1].Value.ToString();
                txtSupplierAddress.Text = dgvSupplier.Rows[e.RowIndex].Cells[2].Value.ToString();
                txtSupplierPhone.Text = dgvSupplier.Rows[e.RowIndex].Cells[3].Value.ToString();
                txtSupplierEmail.Text = dgvSupplier.Rows[e.RowIndex].Cells[4].Value.ToString();
                txtSupplierID.Enabled = false;
            }
        }
        #region Cập nhật nhà cung cấp
        private void btnSupplierUpdate_Click(object sender, EventArgs e)
        {
            dbSupplier dbSupplier = new dbSupplier();
            if(txtSupplierID.Enabled == false)
            {
                if (CheckSupplier())
                {
                    dbSupplier.UpdateSupplier(txtSupplierID.Text, txtSupplierName.Text, txtSupplierAddress.Text, txtSupplierPhone.Text, txtSupplierEmail.Text);
                    MessageBox.Show("Cập nhật thành công!", "Thông báo");
                    LoadDataSupplier();
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một hàng để cập nhật", "Thông báo");
            }
        }
        #endregion
        #region Xóa nhà cung cấp
        private void btnSupplierDelete_Click(object sender, EventArgs e)
        {
            dbSupplier dbSupplier = new dbSupplier();
            if(txtSupplierID.Enabled == false)
            {
                if(MessageBox.Show("Bạn có muốn xóa nhà cung cấp này không!", "Thông báo!", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    dbSupplier.DeleteSupplier(txtSupplierID.Text);
                    MessageBox.Show("Xóa thành công!", "Thông báo");
                    LoadDataSupplier();
                    ResetTextSupplier();
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một hàng để xóa!", "Thông báo");
            }
        }
        #endregion

        #region Tìm kiếm nhà cung cấp
        private void txtSupplierFind_TextChanged(object sender, EventArgs e)
        {
            dbSupplier dbSupplier = new dbSupplier();
            if(cboFindSupplier.SelectedIndex == 0)
            {
                DataSet ds = dbSupplier.FindSupplierID(txtSupplierFind.Text);
                dgvSupplier.DataSource = null;
                dgvSupplier.DataSource = ds.Tables[0];
            }
            else
            {
                DataSet ds = dbSupplier.FindSupplierName(txtSupplierFind.Text);
                dgvSupplier.DataSource = null;
                dgvSupplier.DataSource = ds.Tables[0];
            }
            if (string.IsNullOrEmpty(txtSupplierFind.Text))
            {
                LoadDataSupplier();
            }
        }
        #endregion
        #endregion
        #region Khách hàng
        private void btnQuanLyKhachHang_Click(object sender, EventArgs e)
        {
            DisplayPanel(pnQuanLyKhachHang);
            DisplayButton(btnQuanLyKhachHang);
            btnCustormerSave.Enabled = false;
            LoadDataCustomer();
            dgvCustomer.ReadOnly = true;
        }
        // Reset text supplier
        void ResetTextCustomer()
        {
            txtCustomerID.Text = "";
            txtCustomerName.Text = "";
            txtCustomerAddress.Text = "";
            txtCustomerPhone.Text = "";
            txtCustomerEmail.Text = "";
        }
        // Check insert update supplier
        bool CheckCustomer()
        {
            if (string.IsNullOrEmpty(txtCustomerID.Text))
            {
                lbCheckCustomerID.Visible = true;
                lbCheckCustomerID.Text = "Vui lòng nhập ID";
                lbCheckCustomerID.ForeColor = Color.Red;
                return false;
            }
            if (txtCustomerName.Text.Equals(""))
            {
                lbCheckCustomerName.ForeColor = Color.Red;
                lbCheckCustomerName.Text = "Tên không được để trống";
                lbCheckCustomerName.Visible = true;
                txtCustomerName.Focus();
                return false;
            }
            string[] name = txtCustomerName.Text.Split(' ');
            for (int i = 0; i < name.Length; i++)
            {
                name[i] = char.ToUpper(name[i][0]) + name[i].Substring(1).ToLower();
            }
            txtCustomerName.Text = String.Join(" ", name);
            if (txtCustomerPhone.Text.Equals(""))
            {
                lbCheckCustomerPhone.ForeColor = Color.Red;
                lbCheckCustomerPhone.Text = "Số điện thoại không được để trống";
                lbCheckCustomerPhone.Visible = true;
                txtCustomerPhone.Focus();
                return false;
            }

            string phone = "";
            foreach (Char c in txtCustomerPhone.Text)
            {
                if (Char.IsDigit(c))
                {
                    phone += c;
                }
            }
            if (phone.Length != 10)
            {
                lbCheckCustomerPhone.Text = "Vui lòng nhập đúng định dạng số điện thoại";
                lbCheckCustomerPhone.ForeColor = Color.Red;
                lbCheckCustomerPhone.Visible = true;
                txtCustomerPhone.Focus();
                return false;
            }
            if (phone[0] != '0')
            {
                txtCustomerPhone.Focus();
                lbCheckCustomerPhone.ForeColor = Color.Red;
                lbCheckCustomerPhone.Visible = true;
                lbCheckCustomerPhone.Text = "Vui lòng nhập đúng định dạng số điện thoại";
                return false;
            }
            if (txtCustomerAddress.Text.Equals(""))
            {
                lbCheckCustomerAddress.Text = "Địa chỉ không được để trống";
                lbCheckCustomerAddress.ForeColor = Color.Red;
                lbCheckCustomerAddress.Visible = true;
                txtCustomerAddress.Focus();
                return false;
            }
            string[] address = txtCustomerAddress.Text.Split(' ');
            for (int i = 0; i < address.Length; i++)
            {
                address[i] = char.ToUpper(address[i][0]) + address[i].Substring(1).ToLower();
            }
            txtCustomerAddress.Text = String.Join(" ", address);
            if (txtCustomerEmail.Text.Equals(""))
            {
                lbCheckCustomerEmail.Text = "email không được để trống";
                lbCheckCustomerEmail.ForeColor = Color.Red;
                lbCheckCustomerEmail.Visible = true;
                txtCustomerEmail.Focus();
                return false;
            }
            try
            {
                MailAddress mailaddress = new MailAddress(txtCustomerEmail.Text);
            }
            catch
            {
                lbCheckCustomerEmail.Text = "Vui lòng nhập đúng định dạng email";
                lbCheckCustomerEmail.ForeColor = Color.Red;
                lbCheckCustomerEmail.Visible = true;
                txtCustomerEmail.Focus();
                return false;
            }
            return true;
        }
        // Load Data Supplier in Database
        void LoadDataCustomer()
        {
            dbCustomer dbCustomer = new dbCustomer();
            DataSet ds = dbCustomer.GetCustomer();
            dgvCustomer.DataSource = null;
            dgvCustomer.DataSource = ds.Tables[0];
        }

        private void btnCustomerAdd_Click(object sender, EventArgs e)
        {
            ResetTextSupplier();
            btnCustomerUpdate.Enabled = false;
            btnCustomerAdd.Enabled = false;
            btnCustomerDelete.Enabled = false;
            btnCustormerSave.Enabled = true;
            txtCustomerID.Enabled = true;
        }

        private void btnCustormerSave_Click(object sender, EventArgs e)
        {
            dbCustomer dbCustomer = new dbCustomer();
            DataSet ds = dbCustomer.GetCustomer();
            dgvCustomer.DataSource = null;
            dgvCustomer.DataSource = ds.Tables[0];
            if (CheckCustomer())
            {
                if (ds == null || ds.Tables.Count == 0 || ds.Tables[0].Rows.Count == 0)
                {
                    dbCustomer dbCusto = new dbCustomer();
                    dbCusto.InsertCustomer(txtCustomerID.Text, txtCustomerName.Text, txtCustomerAddress.Text, txtCustomerPhone.Text, txtCustomerEmail.Text);
                    MessageBox.Show("Thêm khách hàng thành công!", "Thông báo");
                    ResetTextCustomer();
                    LoadDataCustomer(); // Tải lại dữ liệu sau khi chèn
                    return;
                }
                else
                {
                    foreach (DataGridViewRow row in dgvCustomer.Rows)
                    {
                        if (txtCustomerID.Text.Equals(row.Cells["CustomerID"].Value?.ToString()))
                        {
                            MessageBox.Show("Đã có ID này!", "Thông báo");
                            return; // Thoát nếu phát hiện ID đã tồn tại
                        }
                    }
                    // Thêm nhà cung cấp mới nếu không trùng ID
                    dbCustomer dbCus = new dbCustomer();
                    dbCus.InsertCustomer(txtCustomerID.Text, txtCustomerName.Text, txtCustomerAddress.Text, txtCustomerPhone.Text, txtCustomerEmail.Text);
                    MessageBox.Show("Thêm khách hàng thành công!", "Thông báo");
                    ResetTextCustomer();
                    LoadDataCustomer();
                }
            }
        }

        private void txtCustomerID_TextChanged(object sender, EventArgs e)
        {
            lbCheckCustomerID.Visible = false;
        }

        private void txtCustomerName_TextChanged(object sender, EventArgs e)
        {
            lbCheckCustomerName.Visible = false;
        }

        private void txtCustomerPhone_TextChanged(object sender, EventArgs e)
        {
            lbCheckCustomerPhone.Visible = false;
        }

        private void txtCustomerEmail_TextChanged(object sender, EventArgs e)
        {
            lbCheckCustomerEmail.Visible = false;
        }

        private void txtCustomerAddress_TextChanged(object sender, EventArgs e)
        {
            lbCheckCustomerAddress.Visible = false;
        }

        private void btnCustomerCancel_Click(object sender, EventArgs e)
        {
            btnCustomerAdd.Enabled = true;
            btnCustomerUpdate.Enabled = true;
            btnCustomerDelete.Enabled = true;
            ResetTextCustomer();
            txtCustomerID.Enabled = true;
        }

        private void dgvCustomer_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                txtCustomerID.Text = dgvCustomer.Rows[e.RowIndex].Cells[0].Value.ToString();
                txtCustomerName.Text = dgvCustomer.Rows[e.RowIndex].Cells[1].Value.ToString();
                txtCustomerAddress.Text = dgvCustomer.Rows[e.RowIndex].Cells[2].Value.ToString();
                txtCustomerPhone.Text = dgvCustomer.Rows[e.RowIndex].Cells[3].Value.ToString();
                txtCustomerEmail.Text = dgvCustomer.Rows[e.RowIndex].Cells[4].Value.ToString();
                txtCustomerID.Enabled = false;
            }
        }

        private void btnCustomerUpdate_Click(object sender, EventArgs e)
        {
            dbCustomer dbCustomer = new dbCustomer();
            if (txtCustomerID.Enabled == false)
            {
                if (CheckCustomer())
                {
                    dbCustomer.UpdateCustomer(txtCustomerID.Text, txtCustomerName.Text, txtCustomerAddress.Text, txtCustomerPhone.Text, txtCustomerEmail.Text);
                    MessageBox.Show("Cập nhật thành công!", "Thông báo");
                    LoadDataCustomer();
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một hàng để cập nhật", "Thông báo");
            }
        }

        private void btnCustomerDelete_Click(object sender, EventArgs e)
        {
            dbCustomer dbCustomer = new dbCustomer();
            if (txtCustomerID.Enabled == false)
            {
                if (MessageBox.Show("Bạn có muốn xóa khách hàng này không!", "Thông báo!", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    dbCustomer.DeleteCustomer(txtCustomerID.Text);
                    MessageBox.Show("Xóa thành công!", "Thông báo");
                    LoadDataCustomer();
                    ResetTextCustomer();
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một hàng để xóa!", "Thông báo");
            }
        }

        private void txtCustomerFind_TextChanged(object sender, EventArgs e)
        {
            dbCustomer dbCustomer = new dbCustomer();
            
            if (cboCustomerFind.SelectedIndex == 0)
            {
                DataSet ds = dbCustomer.FindCustomerID(txtCustomerFind.Text);
                dgvCustomer.DataSource = null;
                dgvCustomer.DataSource = ds.Tables[0];
            }
            else
            {
                DataSet ds = dbCustomer.FindCustomerName(txtCustomerFind.Text);
                dgvCustomer.DataSource = null;
                dgvCustomer.DataSource = ds.Tables[0];
            }
            if (string.IsNullOrEmpty(txtCustomerFind.Text))
            {
                LoadDataCustomer();
            }
        }
        #endregion
        #region Danh mục sản phẩm
        void LoadDataCategory()
        {
            dbCategories dbCategories = new dbCategories();
            DataSet ds = dbCategories.GetCategory();
            dgvCategory.DataSource = null;
            dgvCategory.DataSource= ds.Tables[0];
        }
        void ResetTextCategory()
        {
            txtCategoryID.Text = "";
            txtCategoryName.Text = "";
        }
        bool CheckCategory()
        {
            if (string.IsNullOrEmpty(txtCategoryID.Text))
            {
                lbCheckCategoryID.Visible = true;
                lbCheckCategoryID.Text = "Vui lòng nhập mã danh mục";
                lbCheckCategoryID.ForeColor = Color.Red;
                return false;
            }
            if (string.IsNullOrEmpty(txtCategoryName.Text))
            {
                lbCheckCategoryName.Visible = true;
                lbCheckCategoryName.ForeColor = Color.Red;
                lbCheckCategoryName.Text = "Vui lòng nhập tên danh mục";
                return false;
            }
            return true;
        }
        private void btnQuanLyDanhMucSanPham_Click(object sender, EventArgs e)
        {
            DisplayPanel(pnDanhMucSanPham);
            DisplayButton(btnQuanLyDanhMucSanPham);
            dgvCategory.ReadOnly = true;
            btnCategorySave.Enabled = false;
            LoadDataCategory();
        }

        private void btnCategoryAdd_Click(object sender, EventArgs e)
        {
            btnCategoryUpdate.Enabled = false;
            btnCategoryDelete.Enabled = false;
            btnCategorySave.Enabled = true;
            btnCategoryAdd.Enabled = false;
        }
        private void txtCategoryID_TextChanged(object sender, EventArgs e)
        {
            lbCheckCategoryID.Visible = false;
        }

        private void txtCategoryName_TextChanged(object sender, EventArgs e)
        {
            lbCheckCategoryName.Visible = false;
        }
        private void btnCategoryUpdate_Click(object sender, EventArgs e)
        {
            if(txtCategoryID.Enabled == false)
            {
                if (CheckCategory())
                {
                    if(MessageBox.Show("Bạn có muốn cập nhật danh mục này!", "Thông báo", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        dbCategories dbCategories = new dbCategories();
                        dbCategories.UpdateCategory(txtCategoryID.Text, txtCategoryName.Text);
                        txtCategoryID.Enabled = true;
                        LoadDataCategory();
                        ResetTextCategory();
                        MessageBox.Show("Cập nhật thành công!", "Thông báo");
                    }
                    
                }
            }
        }

        private void btnCategoryDelete_Click(object sender, EventArgs e)
        {
            if( txtCategoryID.Enabled == false)
            {
                if(MessageBox.Show("Bạn có muốn xóa danh mục sản phẩm này!", "Thông báo", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    dbCategories dbCategories = new dbCategories();
                    dbCategories.DeleteCategory(txtCategoryID.Text);
                    MessageBox.Show("Xóa thành công", "Thông báo");
                    LoadDataCategory();
                    ResetTextCategory();
                    txtCategoryID.Enabled = true;
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một hàng để xóa", "Thông báo");
            }
        }

        private void btnCategorySave_Click(object sender, EventArgs e)
        {
            if (CheckCategory())
            {
                dbCategories dbCategories = new dbCategories();
                DataSet ds = dbCategories.GetCategory();
                if (ds.Tables[0].Rows.Count < 0)
                {
                    dbCategories.InsertCategory(txtCategoryID.Text, txtCategoryName.Text);
                    MessageBox.Show("Thêm danh mục thành công!", "Thông báo");
                    ResetTextCategory();
                    LoadDataCategory();
                }
                else
                {
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        if (row["CategoryID"].ToString().Equals(txtCategoryID.Text))
                        {
                            MessageBox.Show("Đã có ID này", "Thông báo");
                            return;
                        }
                    }
                    dbCategories.InsertCategory(txtCategoryID.Text, txtCategoryName.Text);
                    MessageBox.Show("Thêm danh mục thành công!", "Thông báo");
                    ResetTextCategory();
                    LoadDataCategory();
                }
            }
        }

        private void btnCategoryCancel_Click(object sender, EventArgs e)
        {
            ResetTextCategory();
            btnCategoryAdd.Enabled = true;
            btnCategoryDelete.Enabled = true;
            btnCategoryUpdate.Enabled = true;
            txtCategoryID.Enabled = true;
        }
        // hiện thị thông tin ra textbox
        private void dgvCategory_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if(dgvCategory.Rows.Count > 0)
            {
                txtCategoryID.Text = dgvCategory.Rows[e.RowIndex].Cells[0].Value.ToString();
                txtCategoryName.Text = dgvCategory.Rows[e.RowIndex].Cells[1].Value.ToString();
                txtCategoryID.Enabled = false;
            }
        }
        // tìm kiếm danh mục sản phẩm theo mã và theo tên
        private void txtFindCategory_TextChanged(object sender, EventArgs e)
        {
            dbCategories dbCategories = new dbCategories();
            if(cboCategoryFind.SelectedIndex == 0)
            {
                DataSet ds = dbCategories.FindCategoryID(txtFindCategory.Text);
                dgvCategory.DataSource = null;
                dgvCategory.DataSource = ds.Tables[0];
            }
            else
            {
                DataSet ds = dbCategories.FindCategoryName(txtFindCategory.Text);
                dgvCategory.DataSource = null;
                dgvCategory.DataSource = ds.Tables[0];
            }
            if(string.IsNullOrEmpty(txtFindCategory.Text))
            {
                LoadDataCategory();
            }
        }
        #endregion
        #region Sản phẩm
        void LoadDataProduct()
        {
            dbProduct dbProduct = new dbProduct();
            DataSet ds = dbProduct.GetProduct();
            pnDanhSachSanPham.Controls.Clear();
            foreach(DataRow dr in ds.Tables[0].Rows)
            {
                DisplayProduct displayProduct = new DisplayProduct(dr, pnTaoSanPham);
                pnDanhSachSanPham.Controls.Add(displayProduct);
            }
            pnDanhSachSanPham.Refresh();
        }
        
        bool CheckProduct()
        {
            if (string.IsNullOrEmpty(txtProductID.Text))
            {
                lbCheckProductID.Text = "Vui lòng nhập mã";
                lbCheckProductID.ForeColor = Color.Red;
                lbCheckProductID.Visible = true;
                return false;
            }
            if (string.IsNullOrEmpty(txtProductName.Text))
            {
                lbCheckProductName.Text = "Vui lòng nhập tên";
                lbCheckProductName.ForeColor = Color.Red;
                lbCheckProductName.Visible = true;
                return false;
            }
            if (string.IsNullOrEmpty(txtProductPrice.Text))
            {
                lbCheckProductPrice.Text = "Vui lòng nhập giá";
                lbCheckProductPrice.ForeColor = Color.Red;
                lbCheckProductPrice.Visible = true;
                return false;
            }
            if (!decimal.TryParse(txtProductPrice.Text, out decimal price))
            {
                lbCheckProductPrice.Text = "Vui lòng nhập đúng số";
                lbCheckProductPrice.ForeColor = Color.Red;
                lbCheckProductPrice.Visible = true;
                return false;
            }
            if (price < 0)
            {
                lbCheckProductPrice.Text = "Vui lòng nhập số dương";
                lbCheckProductPrice.ForeColor = Color.Red;
                lbCheckProductPrice.Visible = true;
                return false;
            }
            return true;
        }
        void ResetTextProduct()
        {
            txtProductID.Text = "";
            txtProductName.Text = "";
            ptbImageProduct.Image = Image.FromFile("D:\\images-NET\\product.jpg");
            cboProductCategory.StartIndex = 0;
            cboProductSupplier.StartIndex = 0;
            cboProductStatus.StartIndex = 1;
            txtProductDescription.Text = "";
        }
        private void btnNewProduct_Click(object sender, EventArgs e)
        {
            pnTaoSanPham.BringToFront();
            pnTaoSanPham.Visible = true;
            btnChonAnhSanPham.Visible = false;
            btnProductUpdate.Visible = false;
            btnProductDelete.Visible = false;
            ResetTextProduct();
            txtProductID.Enabled = true;
            btnProductSave.Visible = true;
            btnThemAnhSanPham.Visible = true;
        }

        private void btnQuanLySanPham_Click(object sender, EventArgs e)
        {
            DisplayPanel(pnQuanLySanPham);
            DisplayButton(btnQuanLySanPham);

            // Tải danh mục sản phẩm
            dbCategories dbCategories = new dbCategories();
            DataSet dsCategory = dbCategories.GetCategory();

            if (dsCategory != null && dsCategory.Tables.Count > 0 && dsCategory.Tables[0].Rows.Count > 0)
            {
                cboProductCategory.DataSource = dsCategory.Tables[0]; // gán bảng dữ liệu cho combobox
                cboProductCategory.DisplayMember = "CategoryName"; // Hiển thị tên danh mục
                cboProductCategory.ValueMember = "CategoryID"; // Lưu ID danh mục , Xác định cột nào trong DataSource được sử dụng làm giá trị bên trong khi lựa chọn một mục.
                cboProductCategory.SelectedValue = dsCategory.Tables[0].Rows[0]["CategoryID"]; // Gán giá trị mặc định
                //Lấy giá trị CategoryID từ dòng đầu tiên của bảng danh mục(dsCategory.Tables[0]).
                //ComboBox tự động chọn mục có CategoryID khớp với giá trị được gán.
            }
            else
            {
                MessageBox.Show("Không có danh mục sản phẩm nào. Vui lòng thêm danh mục trước.", "Thông báo");
                cboProductCategory.DataSource = null; // Đặt về null nếu không có dữ liệu
            }

            // Tải nhà cung cấp
            dbSupplier dbSupplier = new dbSupplier();
            DataSet dsSupplier = dbSupplier.GetSupplier();

            if (dsSupplier != null && dsSupplier.Tables.Count > 0 && dsSupplier.Tables[0].Rows.Count > 0)
            {
                cboProductSupplier.DataSource = dsSupplier.Tables[0];
                cboProductSupplier.DisplayMember = "SupplierName"; // Hiển thị tên nhà cung cấp
                cboProductSupplier.ValueMember = "SupplierID"; // Lưu ID nhà cung cấp
                cboProductSupplier.SelectedValue = dsSupplier.Tables[0].Rows[0]["SupplierID"]; // Gán giá trị mặc định
            }
            else
            {
                MessageBox.Show("Không có nhà cung cấp nào. Vui lòng thêm nhà cung cấp trước.", "Thông báo");
                cboProductSupplier.DataSource = null; // Đặt về null nếu không có dữ liệu
            }

            Console.WriteLine("CategoryID: " + cboProductCategory.SelectedValue);
            Console.WriteLine("SupplierID: " + cboProductSupplier.SelectedValue);
            LoadDataProduct();
        }


        private void ptbProductClose_Click(object sender, EventArgs e)
        {
            pnTaoSanPham.Visible =false;
            LoadDataProduct();
            ResetTextProduct();
        }

        private void btnProductSave_Click(object sender, EventArgs e)
        {
            if (CheckProduct())
            {
                dbProduct dbProduct = new dbProduct();
                DataSet ds = dbProduct.GetProduct();
                if(ds.Tables[0].Rows.Count < 0)
                {
                    dbProduct.InsertProduct(txtProductID.Text, txtProductName.Text, 
                        int.Parse(txtProductQuantity.Text), cboProductCategory.SelectedValue.ToString(), 
                        cboProductSupplier.SelectedValue.ToString(), decimal.Parse(txtProductPrice.Text), 
                        cboProductStatus.SelectedItem.ToString(), txtProductDescription.Text, imageProduct);
                }
                else
                {
                    foreach(DataRow dr in ds.Tables[0].Rows)
                    {
                        if (dr["ProductID"].ToString().Equals(txtProductID.Text))
                        {
                            MessageBox.Show("Đã có ID này", "Thông báo");
                            return;
                        }
                    }
                    dbProduct.InsertProduct(txtProductID.Text, txtProductName.Text,
                        int.Parse(txtProductQuantity.Text), cboProductCategory.SelectedValue.ToString(),
                        cboProductSupplier.SelectedValue.ToString(), decimal.Parse(txtProductPrice.Text),
                        cboProductStatus.SelectedItem.ToString(), txtProductDescription.Text, imageProduct);
                    MessageBox.Show("Tạo mới sản phẩm thành công!", "Thông báo");
                }
            }
        }

        private void btnThemAnhSanPham_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog();
            openFile.Title = "Chọn ảnh";
            openFile.Filter = "Ảnh (*.jpg;*.jpeg;*.png;*.bmp)|*.jpg;*.jpeg;*.png;*.bmp"; // Lọc file ảnh
            openFile.InitialDirectory = @"D:\images-NET"; // Thư mục mặc định khi mở
            openFile.Multiselect = false; // Chỉ cho phép chọn 1 ảnh
            if (openFile.ShowDialog() == DialogResult.OK)
            {
                imageProduct = openFile.FileName; //Lấy đường dẫn của ảnh từ hộp thoại chọn file (openFile.FileName).
                ptbImageProduct.Image = Image.FromFile(imageProduct); // Tải ảnh từ đường dẫn đó vào PictureBox 
                ptbImageProduct.SizeMode = PictureBoxSizeMode.StretchImage; //Điều chỉnh kích thước ảnh sao cho nó kéo giãn để lấp đầy toàn bộ diện tích của PictureBox (StretchImage).
            }
        }

        private void txtProductID_TextChanged(object sender, EventArgs e)
        {
            lbCheckProductID.Visible = false;
        }

        private void txtProductName_TextChanged(object sender, EventArgs e)
        {
            lbCheckProductName.Visible = false;
        }

        private void txtProductPrice_TextChanged(object sender, EventArgs e)
        {
            lbCheckProductPrice.Visible = false;
        }

        private void cboFilterProductStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            dbProduct dbProduct = new dbProduct();
            
            if(cboFilterProductStatus.SelectedIndex == 0)
            {
                LoadDataProduct();
            }
            else if(cboFilterProductStatus.SelectedIndex == 1)
            {
                DataSet ds = dbProduct.GetProductStatus();
                pnDanhSachSanPham.Controls.Clear();
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    DisplayProduct displayProduct = new DisplayProduct(dr, pnTaoSanPham);
                    pnDanhSachSanPham.Controls.Add(displayProduct);
                }
                pnDanhSachSanPham.Refresh();
            }
            else
            {
                DataSet ds = dbProduct.GetProductStatus_SELL();
                pnDanhSachSanPham.Controls.Clear();
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    DisplayProduct displayProduct = new DisplayProduct(dr, pnTaoSanPham);
                    pnDanhSachSanPham.Controls.Add(displayProduct);
                }
                pnDanhSachSanPham.Refresh();
            }
        }

        private void txtFindProduct_TextChanged(object sender, EventArgs e)
        {
            dbProduct dbProduct = new dbProduct();
            if (cboProductFind.SelectedIndex == 0)
            {
                DataSet ds = dbProduct.GetProductID(txtFindProduct.Text);
                pnDanhSachSanPham.Controls.Clear();
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    DisplayProduct displayProduct = new DisplayProduct(dr, pnTaoSanPham);
                    pnDanhSachSanPham.Controls.Add(displayProduct);
                }
                pnDanhSachSanPham.Refresh();
            }
            else
            {
                DataSet ds = dbProduct.GetProductName(txtFindProduct.Text);
                pnDanhSachSanPham.Controls.Clear();
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    DisplayProduct displayProduct = new DisplayProduct(dr, pnTaoSanPham);
                    pnDanhSachSanPham.Controls.Add(displayProduct);
                }
                pnDanhSachSanPham.Refresh();
            }
            if (string.IsNullOrEmpty(txtFindProduct.Text))
            {
                LoadDataProduct();
            }
        }
        #endregion
        #region Nhập sản phẩm
        bool CheckAddProduct()
        {
            if(string.IsNullOrEmpty(txtPurchaseInvoiceID.Text))
            {
                lbCheckPurchaseInvoiceID.Visible = true;
                lbCheckPurchaseInvoiceID.ForeColor = Color.Red;
                lbCheckPurchaseInvoiceID.Text = "Vui lòng nhập mã hóa đơn";
                return false;
            }
            if (!int.TryParse(txtProductQuantityInvoice.Text, out int quatity))
            {
                lbCheckProductInvoice.Text = "Vui lòng nhập đúng số";
                lbCheckProductInvoice.ForeColor = Color.Red;
                lbCheckProductInvoice.Visible = true;
                return false;
            }
            if (quatity <= 0)
            {
                lbCheckProductInvoice.Text = "Vui lòng nhập số dương";
                lbCheckProductInvoice.ForeColor = Color.Red;
                lbCheckProductInvoice.Visible = true;
                return false;
            }
            if (!decimal.TryParse(txtProductUnitPrice.Text, out decimal price))
            {
                lbCheckProductUnitPrice.Text = "Vui lòng nhập đúng số";
                lbCheckProductUnitPrice.ForeColor = Color.Red;
                lbCheckProductUnitPrice.Visible = true;
                return false;
            }
            if (price <= 0)
            {
                lbCheckProductUnitPrice.Text = "Vui lòng nhập số dương";
                lbCheckProductUnitPrice.ForeColor = Color.Red;
                lbCheckProductUnitPrice.Visible = true;
                return false;
            }
            if (string.IsNullOrEmpty(txtProductNameInvoice.Text))
            {
                lbCheckProductNameInvoice.Text = "Vui lòng chọn sản phẩm";
                lbCheckProductNameInvoice.ForeColor = Color.Red;
                lbCheckProductNameInvoice.Visible = true;
                return false;
            }
            return true;

        }
        private void btnCancelProduct_Click(object sender, EventArgs e)
        {
            if(MessageBox.Show("Bạn có muốn hủy hóa đơn nhập sản phẩm?", "Thông báo", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                pnNhapSanPham.Visible = false;
            }
        }

        #endregion

        private void btnAddProduct_Click(object sender, EventArgs e)
        {
            // Lấy dòng được chọn từ ComboBox (là DataRowView)
            DataRowView selectedRow = (DataRowView)cboProductID.SelectedItem;

            // Truy cập giá trị ProductID từ DataRowView
            string productID = selectedRow["ProductID"].ToString();

            // Tạo đối tượng Product từ các TextBox
            Product product = new Product(
                productID,
                txtProductNameInvoice.Text,
                txtProductCategoryName.Text,
                txtProductSupplierName.Text,
                Convert.ToDecimal(txtProductUnitPrice.Text),
                int.Parse(txtProductQuantityInvoice.Text)
            );

            // Kiểm tra nếu sản phẩm đã tồn tại trong DataGridView
            bool productExists = false;
            foreach (DataGridViewRow row in dgvProductCart.Rows)
            {
                if (row.Cells["ProductID"].Value != null && row.Cells["ProductID"].Value.ToString() == product.ProductID)
                {
                    // Nếu sản phẩm đã tồn tại, cập nhật số lượng
                    int currentQuantity = Convert.ToInt32(row.Cells["ProductQuantity"].Value);
                    int newQuantity = currentQuantity + product.ProductQuantity;
                    row.Cells["ProductQuantity"].Value = newQuantity; // Cập nhật số lượng
                    row.Cells["GetTotalPrice"].Value = newQuantity * Convert.ToDecimal(row.Cells["ProductPrice"].Value); // Cập nhật giá trị tổng
                    productExists = true;
                    break;
                }
            }

            // Nếu sản phẩm chưa tồn tại trong DataGridView, thêm sản phẩm mới
            if (!productExists)
            {
                dgvProductCart.Rows.Add(product.ProductID, product.ProductName, product.CategoryName, product.SupplierName, product.ProductPrice, product.ProductQuantity, product.GetTotalPrice());
            }

        }

        private void btnNhapSanPham_Click(object sender, EventArgs e)
        {
            DisplayButton(btnNhapSanPham);
            DisplayPanel(pnDanhSachHoaDonNhap);
            dbPurchaseInvoice dbPurchaseInvoice = new dbPurchaseInvoice();
            DataSet ds = dbPurchaseInvoice.GetPurchaseInvoice();
            dgvPurchaseInvoice.DataSource = null;
            dgvPurchaseInvoice.DataSource = ds.Tables[0];
        }

        private void btnNhapHang_Click(object sender, EventArgs e)
        {
            pnNhapSanPham.Visible = true;
            pnNhapSanPham.BringToFront();
            dbProduct dbProduct = new dbProduct();
            DataSet ds = dbProduct.GetProductCategoySupplier();
            cboProductID.DataSource = null;
            cboProductID.DataSource = ds.Tables[0];
            cboProductID.DisplayMember = "ProductID";
            cboProductID.ValueMember = "ProductID";
        }

        private void txtPurchaseInvoiceID_TextChanged(object sender, EventArgs e)
        {
            lbCheckPurchaseInvoiceID.Visible = false;
        }

        private void txtProductQuantityInvoice_TextChanged(object sender, EventArgs e)
        {
            lbCheckProductInvoice.Visible = false;
        }

        private void txtProductUnitPrice_TextChanged(object sender, EventArgs e)
        {
            lbCheckProductUnitPrice.Visible = false;
        }

        private void txtProductNameInvoice_TextChanged(object sender, EventArgs e)
        {
            lbCheckProductNameInvoice.Visible = false;


            //// Lấy giá trị tên sản phẩm từ TextBox
            //string productName = txtProductNameInvoice.Text.Trim();

            //if (!string.IsNullOrEmpty(productName))
            //{
            //    // Tạo đối tượng dbProduct và gọi phương thức lấy dữ liệu
            //    dbProduct dbProduct = new dbProduct();
            //    DataSet ds = dbProduct.GetProductCategoySupplier();

            //    // Kiểm tra nếu DataSet có dữ liệu
            //    if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            //    {
            //        // Lọc DataTable theo tên sản phẩm
            //        DataRow[] foundRows = ds.Tables[0].Select("ProductName LIKE '%" + productName + "%'");

            //        // Nếu có kết quả tìm thấy
            //        if (foundRows.Length > 0)
            //        {
            //            DataRow selectedRow = foundRows[0]; // Chọn dòng đầu tiên (hoặc có thể sử dụng vòng lặp nếu muốn nhiều sản phẩm)

            //            // Cập nhật các TextBox và PictureBox với thông tin sản phẩm
            //            txtProductCategoryName.Text = selectedRow["CategoryName"].ToString();
            //            txtProductSupplierName.Text = selectedRow["SupplierName"].ToString();

            //            // Kiểm tra nếu có hình ảnh
            //            string productImagePath = selectedRow["ProductImage"].ToString();
            //            ptbProductImageInvoice.Image = Image.FromFile(productImagePath);
            //        }
            //    }
            //}

        }
        private void cboProductID_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Kiểm tra xem ComboBox có dữ liệu hay không
            if (cboProductID.SelectedIndex != -1 && cboProductID.DataSource != null)
            {
                // Lấy DataTable từ DataSet
                DataTable dt = (DataTable)cboProductID.DataSource;

                // Lấy chỉ số của mục được chọn
                int selectedIndex = cboProductID.SelectedIndex;

                // Kiểm tra chỉ số hợp lệ
                if (selectedIndex >= 0 && selectedIndex < dt.Rows.Count)
                {
                    // Lấy DataRow tương ứng
                    DataRow selectedRow = dt.Rows[selectedIndex];

                    // Gán giá trị vào các TextBox
                    txtProductNameInvoice.Text = selectedRow["ProductName"].ToString();
                    txtProductCategoryName.Text = selectedRow["CategoryName"].ToString();
                    txtProductSupplierName.Text = selectedRow["SupplierName"].ToString();
                    ptbProductImageInvoice.Image = Image.FromFile(selectedRow["ProductImage"].ToString());
                }
            }
            else
            {
                MessageBox.Show("Không có dữ liệu sản phẩm.");
            }
        }

        private void dgvProductCart_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if(dgvProductCart.Rows.Count > 0 )
            {
                cboProductID.SelectedValue = dgvProductCart.Rows[e.RowIndex].Cells[0].Value;
                txtProductQuantityInvoice.Text = dgvProductCart.Rows[e.RowIndex].Cells[5].Value.ToString();
                txtProductUnitPrice.Text = dgvProductCart.Rows[e.RowIndex].Cells[4].Value.ToString();
            }
        }

        private void btnUpdateProduct_Click(object sender, EventArgs e)
        {
            // Lấy dữ liệu từ các TextBox hoặc ComboBox
            // Lấy dòng được chọn từ ComboBox (là DataRowView)
            DataRowView selectedRow = (DataRowView)cboProductID.SelectedItem;

            // Truy cập giá trị ProductID từ DataRowView
            string productID = selectedRow["ProductID"].ToString();
            int quantity = Convert.ToInt32(txtProductQuantityInvoice.Text); // Lấy số lượng từ TextBox
            decimal unitPrice = Convert.ToDecimal(txtProductUnitPrice.Text); // Lấy giá từ TextBox

            // Tìm hàng trong DataGridView có ID sản phẩm trùng với productID
            foreach (DataGridViewRow row in dgvProductCart.Rows)
            {
                if (row.Cells["ProductID"].Value.ToString() == productID)
                {
                    // Cập nhật giá trị trong các ô tương ứng của dòng đó
                    row.Cells["ProductQuantity"].Value = quantity;
                    row.Cells["ProductPrice"].Value = unitPrice;
                    row.Cells["GetTotalPrice"].Value = unitPrice * quantity;
                    Console.WriteLine("GetToTalPrice:" + row.Cells["GetTotalPrice"].Value);
                    break; // Dừng vòng lặp sau khi tìm thấy dòng cần cập nhật
                }
            }

            // Cập nhật lại DataGridView (nếu cần thiết)
            dgvProductCart.Refresh();
        }

        private void btnDeleteProduct_Click(object sender, EventArgs e)
        {
            // Lấy dữ liệu từ ComboBox (DataRowView)
            DataRowView selectedRow = (DataRowView)cboProductID.SelectedItem;

            // Truy cập giá trị ProductID từ DataRowView
            string productID = selectedRow["ProductID"].ToString();

            // Duyệt qua các dòng trong DataGridView để tìm dòng có ProductID cần xóa
            foreach (DataGridViewRow row in dgvProductCart.Rows)
            {
                if (row.Cells["ProductID"].Value.ToString() == productID)
                {
                    // Xóa dòng tương ứng
                    dgvProductCart.Rows.RemoveAt(row.Index);
                    break; // Dừng vòng lặp sau khi xóa dòng cần tìm
                }
            }

            // Cập nhật lại DataGridView (nếu cần thiết)
            dgvProductCart.Refresh();
        }

        private void btnSaveProduct_Click(object sender, EventArgs e)
        {
            dbPurchaseInvoice dbPurchaseInvoice = new dbPurchaseInvoice();
            dbInvoiceDetails dbInvoiceDetails = new dbInvoiceDetails();
            dbProduct dbProduct = new dbProduct();
            decimal totalPrice = 0;
            foreach (DataGridViewRow row in dgvProductCart.Rows)
            {
                if (!row.IsNewRow) // Bỏ qua hàng trống (nếu có)
                {
                    var cellValue = row.Cells["GetTotalPrice"].Value;
                    // Chuyển đổi sang decimal
                     totalPrice += decimal.Parse(cellValue.ToString());
                }
            }
            Console.WriteLine("GetTotalPrice: " + totalPrice);
            dbPurchaseInvoice.InsertPurchaseInvoice(txtPurchaseInvoiceID.Text, employee.Employeeid, datePurchaseInvoice.Value, totalPrice);
            foreach (DataGridViewRow row in dgvProductCart.Rows)
            {
                if(!row.IsNewRow)
                {
                    Console.WriteLine("ID: " + txtPurchaseInvoiceID.Text);
                    Console.WriteLine("product: " + row.Cells[0].Value.ToString());
                    Console.WriteLine("quantity: " + int.Parse(row.Cells["ProductQuantity"].Value.ToString()));
                    Console.WriteLine("price: " + decimal.Parse(row.Cells["ProductPrice"].Value.ToString()));
                    dbInvoiceDetails.InsertInvoiceDetail(txtPurchaseInvoiceID.Text, row.Cells["ProductID"].Value.ToString(), int.Parse(row.Cells["ProductQuantity"].Value.ToString()), decimal.Parse(row.Cells["ProductPrice"].Value.ToString()));
                    dbProduct.UpdateProductQuantity(row.Cells["ProductID"].Value.ToString(), int.Parse(row.Cells["ProductQuantity"].Value.ToString()));
                }
            }
            MessageBox.Show("Nhập sản phẩm thành công!", "Thông báo");
        }

        
    }
}
