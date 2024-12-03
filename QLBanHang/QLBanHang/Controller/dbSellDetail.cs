using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLBanHang.Controller
{
    internal class dbSellDetail
    {
        private Connect connect = new Connect();
        public void InsertSellDetail(string id, string productid, int quantity, decimal price)
        {
            SqlConnection dbConnect = connect.GetConnect();
            string sql = "INSERT INTO  SellDetail(SellID, ProductID, Quantity, TotalPrice) VALUES (@id, @productid, @quantity, @price)";
            try
            {
                dbConnect.Open();
                SqlCommand cmd = new SqlCommand(sql, dbConnect);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.Parameters.AddWithValue("@productid", productid);
                cmd.Parameters.AddWithValue("@quantity", quantity);
                cmd.Parameters.AddWithValue("@price", price);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi chèn dữ liệu chi tiết hóa đơn bán hàng: " + ex.Message);
            }
            finally
            {
                dbConnect.Close();
            }
        }
    }
}
