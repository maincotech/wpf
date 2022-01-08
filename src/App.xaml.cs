using System.Windows;
using WpfTemplate.ViewModels;

namespace WpfTemplate
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
         
            //initialize the splash screen and set it as the application main window
            var vm = new SplashScreenViewModel();
            var splashScreen = new SplashScreenWindow() { DataContext = vm };
            MainWindow = splashScreen;
            splashScreen.Show();
            vm.StartInitialize(() =>
            {
                MainWindow mainView = new MainWindow();
                MainWindow = mainView;
                mainView.Show();
                splashScreen.Close();
            }, ex =>
            {
                System.Console.WriteLine(ex);
            });
        }
    }
}