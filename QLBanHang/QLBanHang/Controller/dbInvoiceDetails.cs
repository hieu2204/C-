using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLBanHang.Controller
{
    internal class dbInvoiceDetails
    {
        private Connect connect = new Connect();
        public void InsertInvoiceDetail(string id, string productid, int quantity, decimal unitprice)
        {
            SqlConnection dbConnect = connect.GetConnect();
            string sql = "INSERT INTO InvoiceDetails (PurchaseInvoiceID, ProductID, Quantity, UnitPrice, TotalPrice) VALUES (@id, @productid, @quantity, @unitprice)";
            try
            {
                dbConnect.Open();
                SqlCommand cmd = new SqlCommand(sql, dbConnect);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.Parameters.AddWithValue("@productid", productid);
                cmd.Parameters.AddWithValue("@quantity", quantity);
                cmd.Parameters.AddWithValue("@unitprice", unitprice);
                cmd.ExecuteNonQuery();
            }catch (Exception ex)
            {
                Console.WriteLine("Lỗi chèn dữ liệu chi tiết hóa đơn nhập: " +ex.Message);
            }
            finally
            {
                dbConnect.Close();
            }
        }
    }
}
