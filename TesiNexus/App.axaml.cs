using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using System;
using System.Data.SqlClient;
using System.IO;
using TesiNexus.Helpers;
using TesiNexus.Nexus.Models;
using TesiNexus.Nexus.ViewModels;
using TesiNexus.Nexus.Views;
using TesiNexus.ViewModels;
using TesiNexus.Views;

namespace TesiNexus
{
    public partial class App : Application
    {
        public NexusApp RunningNexus = new NexusApp();

        public override void Initialize()
        {
            AvaloniaXamlLoader.Load(this);
            
        }

        public override void OnFrameworkInitializationCompleted()
        {
            if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
              var bd =  CheckConnectionFile();

                if (!bd)
                {
                    desktop.MainWindow = new DataBaseConfigView
                    {
                        DataContext = new DataBaseConfigViewModel(),
                    };
                }
                else
                {
                    desktop.MainWindow = new StartUpView
                    {
                        DataContext = new StartUpViewModel(),
                    };

                }

            }

            base.OnFrameworkInitializationCompleted();


        }

        private static bool CheckConnectionFile()
        {
            string programData = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData);

            string curFile = programData + "\\NexusConfig\\conexao.json";
            if (File.Exists(curFile))
            {
                string json = File.ReadAllText(curFile);
                json = Crypto.Decrypt(json);

                ((App)App.Current).RunningNexus = Newtonsoft.Json.JsonConvert.DeserializeObject<NexusApp>(json);

                string connStrFonte = $@"Data Source={((App)App.Current).RunningNexus.IpAdress};User ID={((App)App.Current).RunningNexus.UserLogin};Password={((App)App.Current).RunningNexus.Password};Initial Catalog=VVAND4;";
                if(DataBaseHelper.CheckingConnection(connStrFonte))
                {
                    return true;
                }
                else
                {
                    return false;
                }
                
            }
            else
            {
                return false;
            }
        }

    }
}
