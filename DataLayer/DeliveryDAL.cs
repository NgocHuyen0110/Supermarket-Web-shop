using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;
using BusinessLogic.DAL.InterfacesDAL;
using BusinessLogic.ObjectClasses;
using Microsoft.Data.SqlClient;
using Microsoft.SqlServer.Management.Smo;

namespace DataLayer
{
    public class DeliveryDAL : DatabaseHandler, IDeliveryDAL
    {
        public bool CreateHomeDeliveryAddress(DeliveryOption delivery)
        {
            bool result;
            try
            {
                var sql = "Insert into [dbo].[HomeDeliveryAddress] (Address) Values (@Address)";
                var cmd = new SqlCommand(sql, GetDbConnection());
                cmd.Parameters.AddWithValue("@Address", delivery.Address.Address);
                ExecuteNonQuery(cmd);
                result = true;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

            return result;
        }

        public DeliveryAddress GetHomeDeliveryAddress(DeliveryAddress address)
        {
            DeliveryAddress deliveryAddress = null;
            SqlDataReader? reader = null;
            try
            {
                var sql = "Select * from [dbo].[HomeDeliveryAddress] where Address = @Address";
                var cmd = new SqlCommand(sql, GetDbConnection());
                cmd.Parameters.AddWithValue("@Address", address.Address);
                reader = OpenExecuteReader(cmd);
                while (reader.Read())
                {
                    deliveryAddress = new DeliveryAddress((string)reader["Address"]);
                }
                return deliveryAddress;
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

        public List<DeliveryAddress> GetAllAddress()
        {
            List<DeliveryAddress> deliveryAddresses = new List<DeliveryAddress>();
            SqlDataReader? reader = null;
            try
            {
                var sql = "Select * from [dbo].[PickUpAddress]";
                var cmd = new SqlCommand(sql, GetDbConnection());
                reader = OpenExecuteReader(cmd);
                while (reader.Read())
                {
                    deliveryAddresses.Add(new DeliveryAddress((string)reader["Address"]));
                }
                return deliveryAddresses;
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
        public int CountTimeSlot(DeliveryOption delivery)
        {
            SqlDataReader? reader = null;
            int count = 0;
            try
            {
                if (delivery is HomeDelivery)
                {
                    var sql = "Select COUNT(DeliveryTime) as count From [dbo].[Order] where DeliveryTime = @DeliveryTime and DeliveryDate = @DeliveryDate";
                    var cmd = new SqlCommand(sql, GetDbConnection());
                    cmd.Parameters.AddWithValue("@DeliveryTime", delivery.TimeSlot.Time.ToString(@"hh\:mm\:ss"));
                    cmd.Parameters.AddWithValue("@DeliveryDate", delivery.DeliveryDate.ToString("yyyy-MM-dd"));
                    reader = OpenExecuteReader(cmd);
                    while (reader.Read())
                    {
                        count = (int)reader["count"];
                    }
                    return count;
                }
                else
                {
                    var sql = "Select COUNT(DeliveryTime) as count From [dbo].[Order] as o inner join [dbo].[PickUpAddress] as p On o.PickUpAddressID = p.ID where DeliveryTime = @DeliveryTime and DeliveryDate = @DeliveryDate and Address = @Address";
                    var cmd = new SqlCommand(sql, GetDbConnection());
                    cmd.Parameters.AddWithValue("@DeliveryTime", delivery.TimeSlot.Time.ToString(@"hh\:mm\:ss"));
                    cmd.Parameters.AddWithValue("@DeliveryDate", delivery.DeliveryDate.ToString("yyyy-MM-dd"));
                    cmd.Parameters.AddWithValue("@Address", delivery.Address.Address);
                    reader = OpenExecuteReader(cmd);
                    while (reader.Read())
                    {
                        count = (int)reader["count"];
                    }
                    return count;
                }

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
