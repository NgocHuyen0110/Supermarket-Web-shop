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
    public class SubcategoryDal : DatabaseHandler, ISubcategoryDal
    {
        public List<Subcategory> GetSubcategoriesByCategory(Category category)
        {
            SqlDataReader? reader = null;
            category.Subcatergories = new List<Subcategory>();

            try
            {
                var sql =
                    "SELECT * FROM [dbo].[Subcategory] as s Inner join [dbo].[Category] as c On s.CategoryID = c.ID WHERE Category = @Category";
                var cmd = new SqlCommand(sql, GetDbConnection());
                cmd.Parameters.AddWithValue("@Category", category.CatergoryItem);
                reader = OpenExecuteReader(cmd);
                while (reader.Read())
                {
                    category.Subcatergories.Add(new Subcategory((int)reader["ID"], (string)reader["Subcategory"]));

                }

                return category.Subcatergories;

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
