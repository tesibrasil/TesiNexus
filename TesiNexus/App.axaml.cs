using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
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
                    desktop.MainWindow = new MainWindow
                    {
                        DataContext = new MainWindowViewModel(),
                    };

                }

            }

            base.OnFrameworkInitializationCompleted();


        }

        private static bool CheckConnectionFile()
        {


            string curFile = "C:/ProgramData/NexusConfig/conexao.json";
            if (File.Exists(curFile))
            {
                string json = File.ReadAllText(curFile);
                json = Crypto.Decrypt(json);

                ((App)App.Current).RunningNexus = Newtonsoft.Json.JsonConvert.DeserializeObject<NexusApp>(json);
                CheckingConnection();
                return true;
            }
            else
            {
                return false;

            }
        }

        private static void CheckingConnection()
        {
            string connStrFonte = $@"Data Source={((App)App.Current).RunningNexus.IpAdress};User ID={((App)App.Current).RunningNexus.UserLogin};Password={((App)App.Current).RunningNexus.Password};Initial Catalog=WANDA;";
       
                    
            using (SqlConnection conn = new SqlConnection(connStrFonte))
            {
                if (!IsAvailable(conn))
                {
                     var msg = MessageBox.Avalonia.MessageBoxManager.GetMessageBoxStandardWindow("CARALHO", "CONFIGURA O BANCO DE DADOS PUTO");
                     msg.Show();
                }
            }

        }

        private static bool IsAvailable( SqlConnection connection)
        {
            try
            {
                connection.Open();
                connection.Close();
            }
            catch (SqlException)
            {
                return false;
            }

            return true;
        }
    }
}
