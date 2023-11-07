using Budget.Data;
using Microsoft.EntityFrameworkCore;

namespace Budget
{
    public partial class App : Application
    {
        public App(DbContextFactory dbContextFactory)
        {
            InitializeComponent();

            var dbContext = dbContextFactory();
            dbContext.Database.Migrate();

            MainPage = new AppShell();
        }
    }
}