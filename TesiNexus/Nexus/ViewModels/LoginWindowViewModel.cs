using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Dapper;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TesiNexus.Nexus.Views;
using TesiNexus.ViewModels;
using TesiNexus.Views;

namespace TesiNexus.Nexus.ViewModels
{
    public class LoginWindowViewModel : ViewModelBase
    {
        public LoginWindowViewModel()
        {
            EntrarCommand = ReactiveCommand.CreateFromTask(async () => { Entrar(); });
            SairCommand = ReactiveCommand.CreateFromTask(async () => { Sair(); });
            User = "admin";
            Password = "nautilus";

        }

        private string _user;
        public string User
        {
            get { return _user; }
            set { this.RaiseAndSetIfChanged(ref _user, value); }
        }

        private string _password;

        public string Password
        {
            get { return _password; }
            set { this.RaiseAndSetIfChanged(ref _password, value); }
        }

        public ICommand EntrarCommand { get; set; }
        public ICommand SairCommand { get; set; }
        public IDbTransaction CurrentTransaction { get; private set; }

        public async Task Entrar()
        {
            if (VerificarUsuario())
            {
                var newlifetime = (IClassicDesktopStyleApplicationLifetime)Application.Current.ApplicationLifetime;


                MainWindow mainWindow = new MainWindow
                {
                    DataContext = new MainWindowViewModel(),
                };

                
                mainWindow.Show();
                newlifetime.MainWindow.Close();

                if ((IClassicDesktopStyleApplicationLifetime)Application.Current.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
                {
                    desktop.MainWindow = mainWindow;
                }


            }
            else
            {
                User = "";
                Password = "";
            }
        }

        public void Sair()
        {
            var lifetime = (IClassicDesktopStyleApplicationLifetime)Application.Current.ApplicationLifetime;

            lifetime.Shutdown();
        }

        public bool VerificarUsuario()
        {

            string conectionString = $@"Data Source={((App)App.Current).RunningNexus.IpAdress};User ID={((App)App.Current).RunningNexus.UserLogin};Password={((App)App.Current).RunningNexus.Password};Initial Catalog=VVAND4;";

            using (SqlConnection conn = new SqlConnection(conectionString))
            {
                conn.Open();

                try
                {

                 var user =  conn.Query<bool>(@"LoginNexusUser", param: new
                 {
                     login = User,
                     password = Password
                 }, transaction: CurrentTransaction
                  , commandType: CommandType.StoredProcedure);

                }
                catch
                {
                    return false;
                }

            }

            return true;

        }
    }
}

