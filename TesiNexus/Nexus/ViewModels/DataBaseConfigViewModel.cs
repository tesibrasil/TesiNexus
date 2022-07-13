using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using Dapper;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TesiNexus.Helpers;
using TesiNexus.ViewModels;

namespace TesiNexus.Nexus.ViewModels
{
    public class DataBaseConfigViewModel : ViewModelBase
    {
        public DataBaseConfigViewModel()
        {
            Testar = ReactiveCommand.CreateFromTask(async () => { TestarConexao(); });
            Salvar = ReactiveCommand.CreateFromTask(async () => { SalvarConexao(); });
            Ambiente = ReactiveCommand.CreateFromTask(async () => { PrepararAmbiente(); });
            Fechar = ReactiveCommand.CreateFromTask(async () => { FecharTela(); });

            TesteOk = false;
            HabAmbiente = false;
        }

        private string _ip;

        public string IP
        {
            get { return _ip; }
            set { this.RaiseAndSetIfChanged(ref _ip, value); }
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

        private string _mensagem;

        public string Mensagem
        {
            get { return _mensagem; }
            set { this.RaiseAndSetIfChanged(ref _mensagem, value); }
        }

        private bool _habAmbiente;

        public bool HabAmbiente
        {
            get { return _habAmbiente; }
            set { this.RaiseAndSetIfChanged(ref _habAmbiente, value); }
        }

        public bool TesteOk { get; set; }
        public ICommand Testar { get; set; }
        public ICommand Ambiente { get; set; }
        public ICommand Salvar { get; set; }
        public ICommand Fechar { get; set; }
        public IDbTransaction CurrentTransaction { get; private set; }

        public void TestarConexao()
        {
            string connStrFonte = $@"Data Source={IP};User ID={User};Password={Password};Initial Catalog=WANDA;";

            Mensagem = "TESTANDO ESSA BOSTA!";

            using (SqlConnection conn = new SqlConnection(connStrFonte))
            {
                if (!IsAvailable(conn))
                {
                    connStrFonte = $@"Data Source={IP};User ID={User};Password={Password};";

                    using (SqlConnection newconn = new SqlConnection(connStrFonte))
                    {
                        if (!IsAvailable(newconn))
                        {
                            Mensagem = "NÃO CONSEGUI ACESSAR ESSA CARALHA DE BANCO DE DADOS!";
                        }
                        else
                        {
                            Mensagem = "SEU AMBIENTE NÃO ESTÁ PRERADO PARA RODAR O TESI NEXUS, CLIQUE EM PREPARAR AMBIENTE E TESTE NOVAMENTE A CONEXÃO.";
                            HabAmbiente = true;
                        }
                    }

                    TesteOk = false;

                }
                else
                {
                    Mensagem = "AI SIM MULEKE!";
                    TesteOk = true;
                }
            }
        }

        public void SalvarConexao()
        {
            if (!TesteOk)
            {
                Mensagem = "SEU ULTIMO TESTE DE CONEXÃO NÃO FOI BEM SUCEDIDO, NÃO VAI ROLAR CRIAR UM ARQUIVO DE CONFIGURAÇÃO";
                return;
            }

            ((App)App.Current).RunningNexus.IpAdress = IP;
            ((App)App.Current).RunningNexus.UserLogin = User;
            ((App)App.Current).RunningNexus.Password = Password;

            string json = Newtonsoft.Json.JsonConvert.SerializeObject(((App)App.Current).RunningNexus);

            json = Crypto.Encrypt(json);

            string folder = "C:\\ProgramData\\NexusConfig";
            string arquivo = Path.Combine(folder, "conexao.json");

            Directory.CreateDirectory(folder);
            File.Create(arquivo).Dispose();
            File.WriteAllText(arquivo, json);

            Mensagem = "SALVOU!";

            CloseApp();

        }

        public void FecharTela()
        {

        }

        public void PrepararAmbiente()
        {

            var connStrFonte = $@"Data Source={IP};User ID={User};Password={Password};";

            using(SqlConnection conn = new SqlConnection(connStrFonte))
            {
                conn.Open();

                try
                {
                    conn.Query($"CREATE DATABASE WANDA"
                                          , transaction: CurrentTransaction
                                          , commandType: CommandType.Text);

                    conn.Query<string>($@"CREATE TABLE WANDA.[dbo].USUARIOS (ID INT IDENTITY (1,1), 
                                          					   Nome  VARCHAR(500), 
                                          					   Login  VARCHAR(50), 
                                          					   Senha  VARCHAR (1024), 
                                          					   Inativo  bit DEFAULT(0)
                                          					   )                                          

                                          CREATE TABLE WANDA.[dbo].DESTINOS ( ID INT IDENTITY (1,1), 
                                          						Nome  VARCHAR(500), 
                                          						Source  VARCHAR(500),
                                          						Usuario VARCHAR(500), 
                                          						Senha  VARCHAR (1024)
                                          						)"
                    ,transaction: CurrentTransaction
                    ,commandType: CommandType.Text);



                    Mensagem = "AGORA SIM LELEK! AMBIENTE CONFIGURADO, TESTE E SALVE A CONEXAO";

                }
                catch (Exception)
                {

                    throw;
                }
            }
        }

        private static bool IsAvailable(SqlConnection connection)
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

        public void CloseApp()
        {
            var lifetime = (IClassicDesktopStyleApplicationLifetime)Application.Current.ApplicationLifetime;

            lifetime.Shutdown();
        }
    }
}
