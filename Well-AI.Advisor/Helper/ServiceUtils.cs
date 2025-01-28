using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using WellAI.Advisor.DLL.Entity;
using WellAI.Advisor.Model.ServiceCompany.Models;

namespace WellAI.Advisor.Helper
{
    public static class ServiceUtils
    {
        /// <summary>
        /// Generates a Random Password
        /// respecting the given strength requirements.
        /// </summary>
        /// <param name="opts">A valid PasswordOptions object
        /// containing the password strength requirements.</param>
        /// <returns>A random password</returns>
        public static string GenerateRandomPassword(PasswordOptions opts = null)
        {
            if (opts == null) opts = new PasswordOptions()
            {
                RequiredLength = 8,
                RequiredUniqueChars = 4,
                RequireDigit = true,
                RequireLowercase = true,
                RequireNonAlphanumeric = true,
                RequireUppercase = true
            };

            string[] randomChars = new[] {
                "ABCDEFGHJKLMNOPQRSTUVWXYZ",    // uppercase 
                "abcdefghijkmnopqrstuvwxyz",    // lowercase
                "0123456789",                   // digits
                "!@$?_-"                        // non-alphanumeric
            };
            Random rand = new Random(Environment.TickCount);
            List<char> chars = new List<char>();

            if (opts.RequireUppercase)
                chars.Insert(rand.Next(0, chars.Count),
                    randomChars[0][rand.Next(0, randomChars[0].Length)]);

            if (opts.RequireLowercase)
                chars.Insert(rand.Next(0, chars.Count),
                    randomChars[1][rand.Next(0, randomChars[1].Length)]);

            if (opts.RequireDigit)
                chars.Insert(rand.Next(0, chars.Count),
                    randomChars[2][rand.Next(0, randomChars[2].Length)]);

            if (opts.RequireNonAlphanumeric)
                chars.Insert(rand.Next(0, chars.Count),
                    randomChars[3][rand.Next(0, randomChars[3].Length)]);

            for (int i = chars.Count; i < opts.RequiredLength
                || chars.Distinct().Count() < opts.RequiredUniqueChars; i++)
            {
                string rcs = randomChars[rand.Next(0, randomChars.Length)];
                chars.Insert(rand.Next(0, chars.Count),
                    rcs[rand.Next(0, rcs.Length)]);
            }

            return new string(chars.ToArray());
        }

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

        public static DataTable ToDataTable(IList<UserPermission> data, IList<ComponentSRVModelRec> columns)
        {
            DataTable table = new DataTable();
            table.Columns.Add("Id", typeof(int));
            table.Columns.Add("Title", typeof(string));

            table.PrimaryKey = new DataColumn[] { table.Columns["Id"] };

            foreach (var col in columns)
                table.Columns.Add(col.ComponentName, typeof(bool));

            foreach (UserPermission item in data)
            {
                DataRow row = table.NewRow();
                row["Id"] = item.Id;
                row["Title"] = item.Title;

                foreach (var component in item.Components)
                {
                    row[component.Title] = component.IsActive;
                }
                table.Rows.Add(row);
            }
            return table;
        }

        public static DataTable ToDataTable(IList<UserRole> data, IList<RolePermissions> columns)
        {
            DataTable table = new DataTable();
            table.Columns.Add("Id", typeof(string));
            table.Columns.Add("Title", typeof(string));

            table.PrimaryKey = new DataColumn[] { table.Columns["Id"] };

            foreach (var col in columns)
                table.Columns.Add(col.RolePermissionName, typeof(bool));

            foreach (UserRole item in data)
            {
                DataRow row = table.NewRow();
                row["Id"] = item.Id;
                row["Title"] = item.Title;

                foreach (var permission in item.Permissions)
                {
                    row[permission.Title] = permission.IsActive;
                }
                table.Rows.Add(row);
            }
            return table;
        }
    }
}

