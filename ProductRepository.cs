using System;
using System.Collections.Generic;
using System.Text;
using MySql.Data.MySqlClient;
using inClassAspNetMVC.Models;

namespace inClassAspNetMVC
{
    class ProductRepository
    {
        private const string connStr = "Server=localhost;Database=BestBuy;Uid=root;Pwd=password";


        public void DeleteProduct(int productId)
        {
            MySqlConnection conn = new MySqlConnection(connStr);
            MySqlCommand cmd = conn.CreateCommand();

            cmd.CommandText =
                "DELETE FROM products " +
                $"WHERE ProductID={productId};";

            using (cmd.Connection)
            {
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void UpdateProductName(int productId, string newName)
        {
            MySqlConnection conn = new MySqlConnection(connStr);
            MySqlCommand cmd = conn.CreateCommand();

            cmd.CommandText =
                "UPDATE products " +
                "SET Name=@name " +
                $"WHERE ProductID={productId};";
            cmd.Parameters.AddWithValue("name", newName);

            using (cmd.Connection)
            {
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public List<Product> GetAllProducts()
        {
            MySqlConnection conn = new MySqlConnection(connStr);
            MySqlCommand cmd = conn.CreateCommand();

            cmd.CommandText =
                "SELECT ProductID, Name, Price " +
                "FROM products;";

            List<Product> products = new List<Product>();
            using (cmd.Connection)
            {
                cmd.Connection.Open();

                MySqlDataReader dataReader = cmd.ExecuteReader();

                while (dataReader.Read() == true)
                {
                    Product product = new Product();

                    product.ID = dataReader.GetInt32("ProductID");
                    product.Name = dataReader.GetString("Name");
                 

                    products.Add(product);
                }
            }

            return products;
        }
    }
}