using System;
using System.IO;

namespace Hotel_CheckIn.Data
{
    public static class DatabasePaths
    {
        public static string MainDatabasePath =>
            Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "GuestDatabase.db");
    }
}