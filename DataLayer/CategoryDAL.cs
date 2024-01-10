using BusinessLogic.Enum;
using BusinessLogic.ObjectClasses;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLogic.DAL.InterfacesDAL;

namespace DataLayer
{
    public class CategoryDal : DatabaseHandler, ICategoryDal
    {
        public List<Category> GetCategories()
        {
            SqlDataReader? reader = null;
            List<Category> categories = new List<Category>();

            try
            {
                var sql =
                    "SELECT * FROM [Category]";
                var cmd = new SqlCommand(sql, GetDbConnection());
                reader = OpenExecuteReader(cmd);
                while (reader.Read())
                {
                    var category = new Category((string)reader["Category"]);
                    categories.Add(category);
                }

                return categories;

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
       
    }
}