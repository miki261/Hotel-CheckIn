using System.Windows;
using Hotel_CheckIn.Data;

namespace Hotel_CheckIn
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            using (var db = new HotelDbContext())
            {
                db.Database.EnsureCreated();
            }

            base.OnStartup(e);
        }
    }
}