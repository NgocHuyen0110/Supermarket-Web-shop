using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLogic.Enum;
using BusinessLogic.InterfacesDAL;
using BusinessLogic.ObjectClasses;
using Microsoft.Data.SqlClient;
using Microsoft.SqlServer.Management.Smo;
using User = BusinessLogic.ObjectClasses.User;

namespace DataLayer
{
    public class UserDal : DatabaseHandler, IUserDal
    {
        public bool CreateUser(User user)
        {
            bool result;
            try
            {

                var sql =
                    "INSERT INTO [User] (FirstName, LastName, Address,PhoneNr, AccountID) VALUES (@FirstName, @LastName, @Address,@PhoneNr, (Select ID from [dbo].[Account] where Email = @Email))";
                var cmd = new SqlCommand(sql, GetDbConnection());
                cmd.Parameters.AddWithValue("@FirstName", user.FirstName);
                cmd.Parameters.AddWithValue("@LastName", user.LastName);
                cmd.Parameters.AddWithValue("@Address", user.Address);
                cmd.Parameters.AddWithValue("@PhoneNr", user.PhoneNr);
                cmd.Parameters.AddWithValue("@Email", user.Account.Email);
                ExecuteNonQuery(cmd);
                result = true;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

            return result;
        }

        //public bool AssignEmployee(User user)
        //{
        //    bool result;
        //    try
        //    {
        //        var sql = "Update [dbo].[User] set EmployeeID = (Select u.ID from [dbo].[User] as u Inner join [dbo].[Account] as a on u.AccountID = a.ID where Email = @Email) where AccountID = (Select ID from [dbo].[Account] where Email = @Email)";
        //        var cmd = new SqlCommand(sql, GetDbConnection());
        //        cmd.Parameters.AddWithValue("@Email", user.Account.Email);
        //        ExecuteNonQuery(cmd);
        //        result = true;
        //    }
        //    catch (Exception e)
        //    {
        //        throw new Exception(e.Message);
        //    }

        //    return result;
        //}
        public bool AssignEmployee(User user)
        {
            bool result;
            try
            {
                var sql = "Insert into [dbo].[Employee] (UserID) values ((Select u.ID from [dbo].[User] as u Inner join [dbo].[Account] as a on u.AccountID = a.ID where Email = @Email))";
                var cmd = new SqlCommand(sql, GetDbConnection());
                cmd.Parameters.AddWithValue("@Email", user.Account.Email);
                ExecuteNonQuery(cmd);
                result = true;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

            return result;
        }
        public bool AssignCustomer(User user)
        {
            bool result;
            try
            {
                var sql = "Insert into [dbo].[Customer] (UserID) values ((Select u.ID from [dbo].[User] as u Inner join [dbo].[Account] as a on u.AccountID = a.ID where Email = @Email))";
                var cmd = new SqlCommand(sql, GetDbConnection());
                cmd.Parameters.AddWithValue("@Email", user.Account.Email);
                ExecuteNonQuery(cmd);
                result = true;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

            return result;
        }
        public User GetEmployeeByEmail(string email)
        {

            User? user = null;
            SqlDataReader? reader = null;

            try
            {
                var sql =
                    "SELECT * FROM [User] as u Inner join [Account] as a On u.AccountID = a.ID Inner join [dbo].[Employee] as e on e.UserID = u.ID WHERE Email = @Email";
                var cmd = new SqlCommand(sql, GetDbConnection());
                cmd.Parameters.AddWithValue("@Email", email);
                reader = OpenExecuteReader(cmd);
                while (reader.Read())
                {
                    user = new Employee((int)reader["ID"], (string)reader["FirstName"], (string)reader["LastName"], (string)reader["Address"],
                        (int)reader["PhoneNr"], new Account((string)reader["Email"], (string)reader["Password"]));
                }

                return user;
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

        public User GetCustomerByEmail(string email)
        {
            User? user = null;
            SqlDataReader? reader = null;

            try
            {
                var sql =
                    "SELECT * FROM [User] as u Inner join [Account] as a On u.AccountID = a.ID Inner join [dbo].[Customer] as c on c.UserID = u.ID WHERE Email = @Email";
                var cmd = new SqlCommand(sql, GetDbConnection());
                cmd.Parameters.AddWithValue("@Email", email);
                reader = OpenExecuteReader(cmd);
                while (reader.Read())
                {
                    user = new Customer((int)reader["ID"], (string)reader["FirstName"], (string)reader["LastName"], (string)reader["Address"],
                        (int)reader["PhoneNr"], new Account((string)reader["Email"], (string)reader["Password"]));
                }

                return user;
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

        public User CheckUserEmailUnit(string email)
        {
            User? user = null;
            SqlDataReader? reader = null;

            try
            {
                var sql =
                    "SELECT * FROM [User] as u Inner join [Account] as a On u.AccountID = a.ID WHERE Email = @Email";
                var cmd = new SqlCommand(sql, GetDbConnection());
                cmd.Parameters.AddWithValue("@Email", email);
                reader = OpenExecuteReader(cmd);
                while (reader.Read())
                {
                    user = new Customer((int)reader["ID"], (string)reader["FirstName"], (string)reader["LastName"], (string)reader["Address"],
                        (int)reader["PhoneNr"], new Account((string)reader["Email"], (string)reader["Password"]));
                }

                return user;
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
        public bool UpdateUser(User user)
        {
            bool result;
            try
            {
                var sql =
                    "UPDATE [User] SET Address = @Address, PhoneNr = @PhoneNr WHERE ID = @ID";
                var cmd = new SqlCommand(sql, GetDbConnection());
                cmd.Parameters.AddWithValue("@Address", user.Address);
                cmd.Parameters.AddWithValue("@PhoneNr", user.PhoneNr);
                cmd.Parameters.AddWithValue("@ID", user.Id);
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

