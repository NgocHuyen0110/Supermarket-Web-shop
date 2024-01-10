using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLogic.DAL.InterfacesDAL;
using BusinessLogic.ObjectClasses;
using Microsoft.Data.SqlClient;

namespace DataLayer
{
    public class ItemDal : DatabaseHandler, IItemDal
    {
        public bool CreateItem(Item item)
        {
            bool result;
            try
            {
                var sql = "INSERT INTO [dbo].[Item] (ItemName, Unit, Price, AmountInStock, SubcategoryID) VALUES (@ItemName, @Unit, @Price, @AmountInStock, (Select ID from [dbo].[Subcategory] where Subcategory = @Subcategory))";
                var cmd = new SqlCommand(sql, GetDbConnection());
                cmd.Parameters.AddWithValue("@ItemName", item.ItemName);
                cmd.Parameters.AddWithValue("@Price", item.Price);
                cmd.Parameters.AddWithValue("@Unit", item.Unit);
                cmd.Parameters.AddWithValue("@AmountInStock", item.AmountInStock);
                cmd.Parameters.AddWithValue("@Subcategory", item.Subcategory.SubcategoryItem);
                ExecuteNonQuery(cmd);
                result = true;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

            return result;
        }

        public bool DeleteItem(Item item)
        {
            bool result;
            try
            {
                var sql = "Delete from [dbo].[Item] where ItemName = @ItemName";
                var cmd = new SqlCommand(sql, GetDbConnection());
                cmd.Parameters.AddWithValue("@ItemName", item.ItemName);
                ExecuteNonQuery(cmd);
                result = true;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

            return result;
        }

        public List<Item> GetAllItemsByCategory(Category category)
        {
            var items = new List<Item>();
            SqlDataReader? reader = null;
            try
            {
                var sql = "SELECT * FROM [dbo].[Item] as i Inner join [dbo].[Subcategory] as s on i.SubcategoryID = s.ID inner join [dbo].[Category] as c on s.CategoryID = c.ID Where Category = @Category and AmountInStock > 0";
                var cmd = new SqlCommand(sql, GetDbConnection());
                cmd.Parameters.AddWithValue("@Category", category.CatergoryItem);
                reader = OpenExecuteReader(cmd);
                while (reader.Read())
                {
                    var item = new Item((int)reader["ID"], (string)reader["ItemName"], (decimal)reader["Price"], (string)reader["Unit"], (int)reader["AmountInStock"], new Subcategory((string)reader["Subcategory"]));
                    items.Add(item);
                }

                return items;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            finally
            {
                CloseExecuteReader(reader);
            }
        }
        public List<Item> GetAllItems()
        {
            var items = new List<Item>();
            SqlDataReader? reader = null;
            try
            {
                var sql = "SELECT * FROM [dbo].[Item] as i Inner join [dbo].[Subcategory] as s on i.SubcategoryID = s.ID inner join [dbo].[Category] as c on s.CategoryID = c.ID Where AmountInStock > 0";
                var cmd = new SqlCommand(sql, GetDbConnection());
                reader = OpenExecuteReader(cmd);
                while (reader.Read())
                {
                    var item = new Item((int)reader["ID"], (string)reader["ItemName"], (decimal)reader["Price"], (string)reader["Unit"], (int)reader["AmountInStock"], new Subcategory((string)reader["Subcategory"]));
                    items.Add(item);
                }
                return items;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            finally
            {
                CloseExecuteReader(reader);
            }
        }

        public List<Item> GetAllItemsByCategoryIncluOutOfStock(Category category)
        {
            var items = new List<Item>();
            SqlDataReader? reader = null;
            try
            {
                var sql = "SELECT * FROM [dbo].[Item] as i Inner join [dbo].[Subcategory] as s on i.SubcategoryID = s.ID inner join [dbo].[Category] as c on s.CategoryID = c.ID Where Category = @Category";
                var cmd = new SqlCommand(sql, GetDbConnection());
                cmd.Parameters.AddWithValue("@Category", category.CatergoryItem);
                reader = OpenExecuteReader(cmd);
                while (reader.Read())
                {
                    var item = new Item((int)reader["ID"], (string)reader["ItemName"], (decimal)reader["Price"], (string)reader["Unit"], (int)reader["AmountInStock"], new Subcategory((string)reader["Subcategory"]));
                    items.Add(item);
                }

                return items;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            finally
            {
                CloseExecuteReader(reader);
            }
        }


        public Item GetItemByName(string itemName)
        {
            SqlDataReader? reader = null;
            Item item = new Item();
            try
            {
                var sql = "SELECT * FROM [dbo].[Item] as i Inner join [dbo].[Subcategory] as s on i.SubcategoryID = s.ID Where ItemName = @ItemName";
                var cmd = new SqlCommand(sql, GetDbConnection());
                cmd.Parameters.AddWithValue("@ItemName", itemName);
                reader = OpenExecuteReader(cmd);
                while (reader.Read())
                {
                    item = new Item((int)reader["ID"], (string)reader["ItemName"], (decimal)reader["Price"], (string)reader["Unit"], (int)reader["AmountInStock"], new Subcategory((string)reader["Subcategory"]));
                }

                return item;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            finally
            {
                CloseExecuteReader(reader);
            }
        }

        public bool UpdateItem(Item item)
        {
            bool result;
            try
            {
                var sql = "Update [Item] set  Unit = @Unit, Price = @Price, AmountInStock = @AmountInStock Where ItemName = @ItemName";
                var cmd = new SqlCommand(sql, GetDbConnection());
                cmd.Parameters.AddWithValue("@Unit", item.Unit);
                cmd.Parameters.AddWithValue("@Price", item.Price);
                cmd.Parameters.AddWithValue("@AmountInStock", item.AmountInStock);
                cmd.Parameters.AddWithValue("@ItemName", item.ItemName);
                ExecuteNonQuery(cmd);
                result = true;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

            return result;
        }
        public bool UpdateAmountInStock(Item item)
        {
            bool result;
            try
            {
                var sql = "Update [Item] set AmountInStock = @AmountInStock Where ItemName = @ItemName";
                var cmd = new SqlCommand(sql, GetDbConnection());
                cmd.Parameters.AddWithValue("@AmountInStock", item.AmountInStock);
                cmd.Parameters.AddWithValue("@ItemName", item.ItemName);
                ExecuteNonQuery(cmd);
                result = true;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

            return result;
        }


    }


}
