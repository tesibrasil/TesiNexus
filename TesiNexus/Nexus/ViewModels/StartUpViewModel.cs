using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TesiNexus.Nexus.Views;
using TesiNexus.ViewModels;
using TesiNexus.Views;

namespace TesiNexus.Nexus.ViewModels
{
    public class StartUpViewModel : ViewModelBase
    {
    
        public StartUpViewModel()
        {
             ShowSplashAsync();
        }

        public async Task ShowSplashAsync()
        {
            await Task.Delay(3000);

            var lifetime = (IClassicDesktopStyleApplicationLifetime)Application.Current.ApplicationLifetime;


            LoginWindowView login = new LoginWindowView
            {
                DataContext = new LoginWindowViewModel(),
            };


            login.Show();


            lifetime.MainWindow.Close();

            if ((IClassicDesktopStyleApplicationLifetime)Application.Current.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                desktop.MainWindow = login; ;
            }

            

        }
    }
}
