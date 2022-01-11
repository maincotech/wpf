using Maincotech;
using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;
using ReactiveUI;
using System;
using System.Linq;
using System.Threading.Tasks;
using WpfTemplate.Models;

namespace WpfTemplate.ViewModels
{
    public class SplashScreenViewModel : ReactiveObject
    {
        public SplashScreenViewModel()
        {
            CurrentStatus = "Loading...";
        }

        private string _CurrentStatus;

        public string CurrentStatus
        {
            get { return _CurrentStatus; }
            set { this.RaiseAndSetIfChanged(ref _CurrentStatus, value); }
        }

        public void StartInitialize(Action onInitialized, Action<Exception> onError)
        {
            try
            {
                AppCenter.Start("{Your App Secret}", typeof(Analytics), typeof(Crashes));

                CreateDatabase();
                //ToDo: add the logic to initialize the application.
                Task.Delay(10000);
                onInitialized?.Invoke();
            }
            catch (Exception ex)
            {
                onError?.Invoke(ex);
            }
        }

        private void CreateDatabase()
        {
            CurrentStatus = "Start to create database";
            var dbContext = AppRuntimeContext.Current.Resolve<AppDbContext>();
            dbContext.Database.EnsureCreated();

            var versionSetting = dbContext.DbSettingInfoes.FirstOrDefault(x => x.Name == "Version");
            if (versionSetting == null)
            {
                dbContext.DbSettingInfoes.Add(new DbSettingInfo { Name = "Version", Value = "V1", Description = "The version of the db." });
                //ToDo: Add your settings here.
                dbContext.SaveChanges();
            }
            CurrentStatus = "The database has been created.";
        }
    }
}