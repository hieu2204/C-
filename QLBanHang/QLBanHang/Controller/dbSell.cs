using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLBanHang.Controller
{
    internal class dbSell
    {
        private Connect connect = new Connect();
        public DataSet GetSell()
        {
            DataSet dataSet = new DataSet();
            SqlConnection dbConnect = connect.GetConnect();
            string sql = "SELECT SellID, EmployeeName, CustomerName, SellPrice, DateOut FROM Sells, Employees, Customers WHERE Sells.EmployeeID = Employees.EmployeeID AND Sells.CustomerID = Customers.CustomerID";
            try
            {
                dbConnect.Open();
                SqlCommand cmd = new SqlCommand(sql, dbConnect);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                adapter.Fill(dataSet);
            }catch (Exception ex)
            {
                Console.WriteLine("Lỗi lấy dữ liệu hóa đơn: " + ex.Message);
            }
            finally
            {
                dbConnect.Close();
            }
            return dataSet;
        }
        public void InsertSell(string id, int employeeid, string customerid, decimal sellprice, DateTime date)
        {
            SqlConnection dbConnect = connect.GetConnect();
            string sql = "INSERT INTO Sells VALUES (@id, @employeeid, @customerid, @sellprice, @date)";
            try
            {
                dbConnect.Open();
                SqlCommand cmd = new SqlCommand(sql, dbConnect);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.Parameters.AddWithValue("@employeeid", employeeid);
                cmd.Parameters.AddWithValue("customerid", customerid);
                cmd.Parameters.AddWithValue("@sellprice", sellprice);
                cmd.Parameters.AddWithValue("@date", date);
                cmd.ExecuteNonQuery();
            }catch (Exception ex)
            {
                Console.WriteLine("Lỗi chèn dữ liệu hóa đơn bán hàng: " +ex.Message);
            }
            finally
            {
                dbConnect.Close();
            }
        }
    }
}
