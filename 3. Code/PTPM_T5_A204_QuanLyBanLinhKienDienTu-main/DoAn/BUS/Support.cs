using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Security.Cryptography;
using System.Text;
using System.Data.SqlClient;
using System.Data.Sql;

namespace BUS
{
    public static class Support
    {
        public static DataTable ToDataTable<T>(this IList<T> data)
        {
            PropertyDescriptorCollection properties =
                TypeDescriptor.GetProperties(typeof(T));
            DataTable table = new DataTable();
            foreach (PropertyDescriptor prop in properties)
                table.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
            foreach (T item in data)
            {
                DataRow row = table.NewRow();
                foreach (PropertyDescriptor prop in properties)
                    row[prop.Name] = prop.GetValue(item) ?? DBNull.Value;
                table.Rows.Add(row);
            }
            return table;
        }
        public static bool SaveConnection(string connectionString)
        {
            return DAO.Support.SaveConnection(connectionString);
        }
        public static string GetConnectionString()
        {
            return DAO.Support.GetConnectionString();
        }
        public static bool TestConnection(string connectionString)
        {
            return DAO.Support.TestConnection(connectionString);
        }
        public static DataTable GetServerName()
        {
            return DAO.Support.GetServerName();
        }
        public static string ConvertVND(string money)
        {
            return DAO.Support.ConvertVND(money);
        }
        public static DataTable GetDBName(string pServer, string pUser, string pPass)
        {
            return DAO.Support.GetDBName(pServer,  pUser,  pPass);
        }
    }
}
