using Maincotech;
using Maincotech.Reflection;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.IO;
using System.Reflection;
using System.Windows;
using WpfTemplate.Models;
using WpfTemplate.ViewModels;

namespace WpfTemplate
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private static Maincotech.Logging.ILogger _Logger = AppRuntimeContext.Current.GetLogger<App>();
        private IHost _host;

        protected override void OnStartup(StartupEventArgs e)
        {
            //initialize the splash screen and set it as the application main window
            var vm = new SplashScreenViewModel();
            var splashScreen = new SplashScreenWindow() { DataContext = vm };
            MainWindow = splashScreen;
            splashScreen.Show();

            //Build Host
            _host = new HostBuilder()
                   .ConfigureAppConfiguration((context, configurationBuilder) =>
                   {
                       configurationBuilder.SetBasePath(context.HostingEnvironment.ContentRootPath);
                       configurationBuilder.AddJsonFile("appsettings.json", optional: true);
                   })
                   .ConfigureServices((context, services) =>
                   {
                       //Conifugre database
                       services.AddDbContext<AppDbContext>(optionsBuilder =>
                       {
                           var dataDir = Path.Combine(AppRuntimeContext.ExecutingPath, "data");
                           if(Directory.Exists(dataDir) == false)
                           {
                               Directory.CreateDirectory(dataDir);
                           }
                           var s_migrationSqlitePath = Path.Combine(dataDir, "db.sqlite3");
                           var connectionString = new SqliteConnectionStringBuilder { DataSource = s_migrationSqlitePath }.ToString();
                           optionsBuilder.UseSqlite(new SqliteConnection(connectionString));
                       });
                       //Register ViewModels
                       var vmTypes = Assembly.GetExecutingAssembly().ClassesOf<ReactiveUI.ReactiveObject>();
                       foreach (var vmType in vmTypes)
                       {
                           services.AddSingleton(vmType);
                       }
                       AppRuntimeContext.Current.ConfigureServices(services, context.Configuration);
                   })
                   .Build();

            _host.StartAsync().Wait();

            vm.StartInitialize(() =>
            {
                MainWindow mainView = new MainWindow();
                MainWindow = mainView;
                mainView.Show();
                splashScreen.Close();
            }, ex =>
            {
                _Logger.Error("A fatal error occured.", ex);
                MessageBox.Show("A fatal error occured, please find more information from the log file.", "Error", MessageBoxButton.OK, MessageBoxImage.Stop);
                App.Current.Shutdown();
            });
        }

        protected override async void OnExit(ExitEventArgs e)
        {
            base.OnExit(e);
            using (_host)
            {
                await _host.StopAsync();
            }
        }
    }
}