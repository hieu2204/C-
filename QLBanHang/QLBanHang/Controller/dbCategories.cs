using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLBanHang.Controller
{
    internal class dbCategories
    {
        private Connect connect = new Connect();
        #region Lấy dữ liệu danh mục sản phẩm
        public DataSet GetCategory()
        {
            DataSet ds = new DataSet();
            SqlConnection dbConnect = connect.GetConnect();
            string sql = "SELECT * FROM Categories";
            try
            {
                dbConnect.Open();
                SqlCommand cmd = new SqlCommand(sql, dbConnect);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                adapter.Fill(ds);
            }catch (Exception ex)
            {
                Console.WriteLine("Lỗi lấy dữ liệu danh mục sản phẩm: " + ex.Message);
            }
            finally
            {
                dbConnect.Close();
            }
            return ds;
        }
        #endregion
        #region Chèn dữ liệu danh mục sản phẩm
        public void InsertCategory(string id, string name)
        {
            SqlConnection dbConnect = connect.GetConnect();
            string sql = "INSERT INTO Categories VALUES (  @id,  @name)";
            try
            {
                dbConnect.Open();
                SqlCommand cmd = new SqlCommand(sql, dbConnect);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.Parameters.AddWithValue ("@name", name);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi chèn dữ liệu danh mục sản phẩm: " + ex.Message);
            }
            finally
            {
                dbConnect.Close();
            }
        }
        #endregion
        #region Cập nhật dữ liệu danh mục sản phẩm
        public void UpdateCategory(string id, string name)
        {
            SqlConnection dbConnect = connect.GetConnect();
            
            string sql = "UPDATE Categories SET CategoryName =  @name WHERE CategoryID = @id";
            try
            {
                dbConnect.Open();
                SqlCommand cmd = new SqlCommand(sql, dbConnect);
                cmd.Parameters.AddWithValue("@name", name);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.ExecuteNonQuery();

                MessageBox.Show("Cập nhật thành công!", "Thông báo");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi cập nhật liệu danh mục sản phẩm: " + ex.Message);
            }
            finally
            {
                dbConnect.Close();
            }
        }
        #endregion
        #region Cập nhật dữ liệu danh mục sản phẩm
        public void DeleteCategory(string id)
        {
            SqlConnection dbConnect = connect.GetConnect();
            SqlTransaction transaction = null;
            //string sql = "DELETE FROM Categories WHERE CategoryID = @id";
            try
            {
                dbConnect.Open();
                // Bắt đầu giao dịch
                transaction = dbConnect.BeginTransaction();

                // Cập nhật bảng Products để gán CategoryID thành NULL
                string updateProduct = "UPDATE Products SET CategoryID = NULL WHERE CategoryID = @id";
                SqlCommand updateCmd = new SqlCommand(updateProduct, dbConnect, transaction);
                updateCmd.Parameters.AddWithValue("@id", id);
                updateCmd.ExecuteNonQuery();

                // Thực hiện xóa danh mục sản phẩm
                string deleteSql = "DELETE FROM Categories WHERE CategoryID = @id";
                SqlCommand deleteCmd = new SqlCommand(deleteSql, dbConnect, transaction);
                deleteCmd.Parameters.AddWithValue("@id", id);
                deleteCmd.ExecuteNonQuery();

                // Commit giao dịch nếu không có lỗi
                transaction.Commit();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi xóa dữ liệu danh mục sản phẩm: " + ex.Message);
            }
            finally
            {
                dbConnect.Close();
            }
        }
        #endregion
        #region Tìm kiếm danh mục sản phẩm theo mã
        public DataSet FindCategoryID(string id)
        {
            DataSet ds = new DataSet();
            SqlConnection dbConnect = connect.GetConnect();
            string sql = "SELECT * FROM Categories WHERE CategoryID = @id";
            try
            {
                dbConnect.Open();
                SqlCommand cmd = new SqlCommand(sql, dbConnect);
                cmd.Parameters.AddWithValue("@id", id);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                adapter.Fill(ds);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi khi tìm danh mục sản phẩm theo mã: " + ex.Message);
            }
            finally
            {
                dbConnect.Close();
            }
            return ds;
        }
        #endregion
        #region Tìm kiếm danh mục sản phẩm theo tên
        public DataSet FindCategoryName(string name)
        {
            DataSet ds = new DataSet();
            SqlConnection dbConnect = connect.GetConnect();
            string sql = "SELECT * FROM Categories WHERE CategoryName LIKE @id";
            try
            {
                dbConnect.Open();
                SqlCommand cmd = new SqlCommand(sql, dbConnect);
                cmd.Parameters.AddWithValue("@id", "%" + name + "%");
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                adapter.Fill(ds);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi khi tìm danh mục sản phẩm theo tên: " + ex.Message);
            }
            finally
            {
                dbConnect.Close();
            }
            return ds;
        }
        #endregion
    }
}
