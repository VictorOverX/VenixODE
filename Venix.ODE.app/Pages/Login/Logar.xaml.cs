using System;
using System.Collections.Generic;
using System.Diagnostics;
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
    /// Interaction logic for Logar.xaml
    /// </summary>
    public partial class Logar : UserControl
    {
        WebClient client = new WebClient();
        private string Link = SettingsThemplate.Link();

        public Logar()
        {
            InitializeComponent();
        }

        private void Hyperlink_RequestNavigate(object sender, RequestNavigateEventArgs e)
        {
            Process.Start(new ProcessStartInfo(e.Uri.AbsoluteUri));
            e.Handled = true;
        }

        private void tbLogar_Click(object sender, RoutedEventArgs e)
        {
            if (tbEmail.Text != "" && tbSenha.Password != "")
            {
                NameValueCollection UserInfo = new NameValueCollection();

                UserInfo.Add("user_email", tbEmail.Text.Trim());
                UserInfo.Add("user_pass", tbSenha.Password.Trim());
                UserInfo.Add("acao", "logar");
                try
                {
                    Byte[] InsertUser = client.UploadValues(Link, "POST", UserInfo);

                    if (Encoding.ASCII.GetString(InsertUser) == "sucesso")
                    {
                        MainWindow WinStart = new MainWindow();
                        WinStart.Show();

                        Window parentWindow = Window.GetWindow(this);
                        parentWindow.Close();
                    }
                    else if (Encoding.ASCII.GetString(InsertUser) == "sempremissao")
                    {
                        MessageBox.Show("Você não tem permissão para acessar o nosso software!");
                    }
                    else if (Encoding.ASCII.GetString(InsertUser) == "senhaerrada")
                    {
                        MessageBox.Show("Sua senha está errada, digite novamente!");
                    }
                    else
                    {
                        MessageBox.Show("Ocorreu um erro! Entre em contato com nossa equipe. Obrigado",
                            "Ocorreu um erro!",
                            MessageBoxButton.OK,
                            MessageBoxImage.Error);
                    }
                }
                catch (Exception erro)
                {
                    MessageBox.Show("Ocorreu um erro!\n" + erro.Message);
                }   
            }
            else
            {
                MessageBox.Show("Você precisa preecher os campos para Logar!");
            }
        }

    }
}
