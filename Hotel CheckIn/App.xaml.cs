using System.Windows;
using Hotel_CheckIn.Data;
using Hotel_CheckIn.Services;

namespace Hotel_CheckIn
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            var restoreCoordinator = new DatabaseRestoreCoordinator();

            if (restoreCoordinator.HasPendingRestore())
            {
                restoreCoordinator.ExecutePendingRestore();
            }

            using (var db = new HotelDbContext())
            {
                db.Database.EnsureCreated();
            }

            base.OnStartup(e);
        }
    }
}