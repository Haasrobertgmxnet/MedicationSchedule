using MedicationSchedule.ViewModels;
using MedicationSchedule.ViewModels.Models;
using MedicationSchedule.Views;
using Prism;
using Prism.Ioc;
using System.Diagnostics;
using Xamarin.Essentials.Implementation;
using Xamarin.Essentials.Interfaces;
using Xamarin.Forms;

namespace MedicationSchedule
{
    public partial class App
    {
        public App(IPlatformInitializer initializer)
            : base(initializer)
        {
        }

        //public App()
        //{
        //}

        protected override async void OnInitialized()
        {
            InitializeComponent();

            var result = await NavigationService.NavigateAsync("NavigationPage/MainPage");

            if (!result.Success)
            {
                System.Diagnostics.Debugger.Break();
            }
        }

        protected override void OnSleep()
        {
            Debug.Print("OnSLEEP");
            MessagingCenter.Send<object, string>(this, "OnSleepKey", "TestString");
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterSingleton<IAppInfo, AppInfoImplementation>();
            containerRegistry.RegisterSingleton<MedicationList>();

            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<MainPage, MainPageViewModel>();
            containerRegistry.RegisterForNavigation<CreateMedicationSchedulePage, CreateMedicationSchedulePageViewModel>();    
            containerRegistry.RegisterForNavigation<MedicationScheduleListViewPage, MedicationScheduleListViewPageViewModel>();
            containerRegistry.RegisterForNavigation<MedicationSchedulePage, MedicationSchedulePageViewModel>();

        }
    }
}
