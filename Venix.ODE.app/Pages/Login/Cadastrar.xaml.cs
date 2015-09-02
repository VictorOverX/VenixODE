using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Win32;
using System.Collections.Specialized;
using Venix.ODE.Model;

namespace Venix.ODE.app.Pages
{
    /// <summary>
    /// Interaction logic for Cadastrar.xaml
    /// </summary>
    public partial class Cadastrar : UserControl
    {
        WebClient client = new WebClient();
        private string Link = SettingsThemplate.Link();

        public Cadastrar()
        {
            InitializeComponent();
        }

        private void EnviarUsuario(object sender, RoutedEventArgs e)
        {
            if (tbNome.Text == "" && tbEmail.Text == "" && tbSenha.Password == "")
            {
                MessageBox.Show("Você tem que preencher todos os campos!");
                return;
            }

            if (!Helpers.Email(tbEmail.Text))
            {
                MessageBox.Show("Endereço de e-mail inválido!");
                return;
            }

            NameValueCollection UserInfo = new NameValueCollection();
            UserInfo.Add("user_name", tbNome.Text);
            UserInfo.Add("user_email", tbEmail.Text);

            if (tbTel.Text != "")
            {
                UserInfo.Add("user_tel", tbTel.Text);
            }
            
            UserInfo.Add("user_pass", tbSenha.Password);
            UserInfo.Add("user_nivel", "usuario");
            UserInfo.Add("acao", "cadastrar");

            try
            {
                Byte[] InsertUser = client.UploadValues(Link, "POST", UserInfo);

                if (Encoding.ASCII.GetString(InsertUser) == "sucesso")
                {
                    MessageBox.Show("Seu usuário foi criado com sucesso, entraremos em contato em breve, para liberação de sua conta. Obrigado!");
                    LimparCampos();
                }
                else if (Encoding.ASCII.GetString(InsertUser) == "jaexiste")
                {
                    MessageBox.Show("Esse e-mail já foi cadastrado em nosso sistema!");
                }
                else
                {
                    MessageBox.Show("Ocorreu um erro! Entre em contato com nossa equipe. Obrigado", 
                        "Ocorreu um erro!", 
                        MessageBoxButton.OK, 
                        MessageBoxImage.Error);
                }
            }
            catch(Exception erro)
            {
                MessageBox.Show("Ocorreu um erro!\n" + erro.Message);
            }            
        }

        private void LimparCampos()
        {
            tbNome.Text         = String.Empty;
            tbEmail.Text        = String.Empty;
            tbSenha.Password    = String.Empty;
            tbTel.Text          = String.Empty;
        }

    }
}
