using ReactiveUI;
using System;
using System.Threading.Tasks;
using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;

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
                
                //ToDo: add the logic to initialize the application.
                Task.Delay(10000);
                onInitialized?.Invoke();
            }
            catch (Exception ex)
            {
                onError?.Invoke(ex);
            }
        }
    }
}