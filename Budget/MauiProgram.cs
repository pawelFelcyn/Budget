using Budget.Data;
using Budget.ViewModels;
using Budget.Views;
using CommunityToolkit.Maui;
using DevExpress.Maui;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SkiaSharp.Views.Maui.Controls.Hosting;

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
                    fonts.AddFont("fas.ttf", "FA-S");
                    fonts.AddFont("far.ttf", "FA-R");
                })
                .UseDevExpress()
                .UseMauiCommunityToolkit()
                .UseSkiaSharp();

            DevExpress.Maui.Charts.Initializer.Init();
            DevExpress.Maui.CollectionView.Initializer.Init();
            DevExpress.Maui.Controls.Initializer.Init();
            DevExpress.Maui.Editors.Initializer.Init();

#if DEBUG
            builder.Logging.AddDebug();
#endif


            var dbPath = Path.Combine(FileSystem.Current.AppDataDirectory, "database.db");
            builder.Services
                .AddDbContext<BudgetDbContext>(options => options.UseSqlite($"Data Source = {dbPath}"), contextLifetime: ServiceLifetime.Transient)
                .AddSingleton<DbContextFactory>(sp => () => sp.GetRequiredService<BudgetDbContext>())
                .AddSingleton<HistoryPage>()
                .AddSingleton<HistoryViewModel>()
                .AddSingleton<CategoriesPage>()
                .AddSingleton<CategoriesViewModel>()
                .AddSingleton<CreateNewTansactionViewModel>()
                .AddSingleton<CreateNewTransactionPage>()
                .AddSingleton<OverviewPage>()
                .AddSingleton<OverviewViewModel>();

            return builder.Build();
        }
    }
}