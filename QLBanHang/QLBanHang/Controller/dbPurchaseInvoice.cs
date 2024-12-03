using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLBanHang.Controller
{
    internal class dbPurchaseInvoice
    {
        private Connect connect = new Connect();
        public void InsertPurchaseInvoice(string id, int employeeid, DateTime date, decimal price)
        {
            SqlConnection dbConnect = connect.GetConnect();
            string sql = "INSERT INTO PurchaseInvoices VALUES (@id, @employeeid, @date, @price)";
            try
            {
                dbConnect.Open();
                SqlCommand cmd = new SqlCommand(sql, dbConnect);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.Parameters.AddWithValue("@employeeid", employeeid);
                cmd.Parameters.AddWithValue("@date", date);
                cmd.Parameters.AddWithValue("@price", price);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi chèn dữ liệu hóa đơn nhập: " + ex.Message);
            }
            finally
            {
                dbConnect.Close();
            }
        }
        public DataSet GetPurchaseInvoice()
        {
            DataSet ds = new DataSet();
            SqlConnection dbconnect = connect.GetConnect();
            string sql = "SELECT PurchaseInvoiceID, EmployeeName, InvoiceDate, TotalAmount FROM PurchaseInvoices, Employees WHERE PurchaseInvoices.EmployeeID = Employees.EmployeeID";
            try
            {
                dbconnect.Open();
                SqlCommand cmd = new SqlCommand (sql, dbconnect);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                adapter.Fill(ds);
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); }
            finally { dbconnect.Close(); }
            return ds;
        }
    }
}
