using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
using System.Data.SQLite;
using System.IO;
using System.Data;
using xDialog;

namespace Venix.ODE.app.Pages.Tools
{
    /// <summary>
    /// Interaction logic for NovoRap.xaml
    /// </summary>
    public partial class NovoRap : UserControl
    {
        string path = AppDomain.CurrentDomain.BaseDirectory;
        private static string conexao = "Data Source=db.db";

        public NovoRap()
        {
            InitializeComponent();
        }

        /// <summary>
        /// ADICIONANDO UM NOVO RAP
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tbAdd_Click(object sender, RoutedEventArgs e)
        {
            if (tbID.Text != "" && tbNome.Text != "" && tbRapCode.Text != "" && tbRapNome.Text != "")
            {
                SQLiteConnection conn = new SQLiteConnection(conexao);
                if (conn.State == ConnectionState.Closed)
                    conn.Open();

                SQLiteCommand cmd = new SQLiteCommand("INSERT INTO tb_raps (ID, NOME, RAPCODE, RAPNOME) VALUES (@ID, @NOME, @RAPCODE, @RAPNOME)", conn);

                cmd.Parameters.AddWithValue("ID", tbID.Text.Trim());
                cmd.Parameters.AddWithValue("NOME", tbNome.Text.Trim());
                cmd.Parameters.AddWithValue("RAPCODE", tbRapCode.Text.Trim());
                cmd.Parameters.AddWithValue("RAPNOME", tbRapNome.Text.Trim());

                try
                {
                    cmd.ExecuteNonQuery();
                    MsgBox.Show("Rap adicionado com sucesso!", "Sucesso", MsgBox.Buttons.OK);

                    tbID.Text = String.Empty;
                    tbNome.Text = String.Empty;
                    tbRapCode.Text = String.Empty;
                    tbRapNome.Text = String.Empty;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro ao cadastrar:\n" + ex.Message);
                }

                conn.Clone();
            }
            else
            {
                MsgBox.Show("Você precisa preencher todos os campos", "Atenção", MsgBox.Buttons.OK);
            }
        }
    }
}
