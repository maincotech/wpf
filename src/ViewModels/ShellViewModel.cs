using MahApps.Metro.IconPacks;
using ReactiveUI;
using System;
using System.Collections.ObjectModel;
using WpfTemplate.Views;

namespace WpfTemplate.ViewModels
{
    public class ShellViewModel : ReactiveObject
    {
        public ObservableCollection<MenuItem> Menu { get; } = new();

        public ObservableCollection<MenuItem> OptionsMenu { get; } = new();

        public ShellViewModel()
        {
            // Build the menus

            this.Menu.Add(new MenuItem()
            {
                Icon = new PackIconFontAwesome() { Kind = PackIconFontAwesomeKind.HomeSolid },
                Label = "Home",
                NavigationType = typeof(HomePage),
                NavigationDestination = new Uri("Views/HomePage.xaml", UriKind.RelativeOrAbsolute)
            });
            this.OptionsMenu.Add(new MenuItem()
            {
                Icon = new PackIconFontAwesome() { Kind = PackIconFontAwesomeKind.CogsSolid },
                Label = "Settings",
                NavigationType = typeof(SettingsPage),
                NavigationDestination = new Uri("Views/SettingsPage.xaml", UriKind.RelativeOrAbsolute)
            });

            this.OptionsMenu.Add(new MenuItem()
            {
                Icon = new PackIconFontAwesome() { Kind = PackIconFontAwesomeKind.InfoCircleSolid },
                Label = "About",
                NavigationType = typeof(AboutPage),
                NavigationDestination = new Uri("Views/AboutPage.xaml", UriKind.RelativeOrAbsolute)
            });
        }
    }
}