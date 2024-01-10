using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLogic.DAL.InterfacesDAL;
using BusinessLogic.ObjectClasses;
using Microsoft.Data.SqlClient;
using Microsoft.SqlServer.Management.Smo;

namespace DataLayer
{
    public class AccountDal : DatabaseHandler, IAccountDal
    {
        public bool CreateAccount(Account account)
        {
            bool result;
            try
            {
                var sql = "INSERT INTO [Account] (Email, Password) VALUES (@Email, @Password)";
                var cmd = new SqlCommand(sql, GetDbConnection());
                cmd.Parameters.AddWithValue("@Email", account.Email);
                cmd.Parameters.AddWithValue("@Password", account.Password);
                ExecuteNonQuery(cmd);

                result = true;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

            return result;
        }

        public bool UpdateAccount(Account account)
        {
            bool result;
            try
            {
                var sql =
                    "UPDATE [Account] SET Password =@Password WHERE Email = @Email";
                var cmd = new SqlCommand(sql, GetDbConnection());
                cmd.Parameters.AddWithValue("@Email", account.Email);
                cmd.Parameters.AddWithValue("@Password", account.Password);
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
