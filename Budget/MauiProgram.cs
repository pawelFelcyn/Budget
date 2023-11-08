using Budget.Data;
using Budget.ViewModels;
using Budget.Views;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Budget
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

#if DEBUG
		builder.Logging.AddDebug();
#endif


            var dbPath = Path.Combine(FileSystem.Current.AppDataDirectory, "database.db");
            builder.Services
                .AddDbContext<BudgetDbContext>(options => options.UseSqlite($"Data Source = {dbPath}"), contextLifetime: ServiceLifetime.Transient)
                .AddSingleton<DbContextFactory>(sp => () => sp.GetRequiredService<BudgetDbContext>())
                .AddSingleton<CategoriesPage>()
                .AddSingleton<CategoriesViewModel>();

            return builder.Build();
        }
    }
}