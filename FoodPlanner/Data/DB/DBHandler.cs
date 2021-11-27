using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FoodPlanner.Enums;

namespace FoodPlanner.Data.DB
{
    public static class DBHandler
    {
        private static string HOST, USERNAME, PASSWORD, DBNAME;
        private static int PORT;

        public static void RefreshTable(REFRESH_TYPE table)
        {
            // refresh specified table
        }

        public static void UpdateSingleValue(string table, string column, string value, string guid)
        {
            // TODO: Implement
        }
    }
}
