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

namespace Venix.ODE.app.Pages.Download.PS3
{
    /// <summary>
    /// Interaction logic for cadJogos.xaml
    /// </summary>
    public partial class cadJogos : UserControl
    {
        string path = AppDomain.CurrentDomain.BaseDirectory;

        private string Nimage = "";
        private string Cimage = "";

        private static string conexao = "Data Source=db.db";

        public class Lista
        {
            public string ID { get; set; }
            public string TITULO { get; set; }
            public string TIPO { get; set; }
            public string REGIAO { get; set; }
            public string LINK1 { get; set; }
            public string LINK2 { get; set; }
            public string LINK3 { get; set; }
            public string LINK4 { get; set; }
            public string CAPA { get; set; }
        }

        public cadJogos()
        {
            InitializeComponent();
        }

        private bool VerificarCampos(string COND)
        {
            SQLiteConnection conn = new SQLiteConnection(conexao);
            if (conn.State == ConnectionState.Closed)
                conn.Open();

            SQLiteCommand cmd = new SQLiteCommand("SELECT * FROM tb_games " + COND, conn);
            SQLiteDataReader dr = cmd.ExecuteReader();

            if (dr.HasRows)
            {
                return true;
            }
            else
            {
                return false;
            }

            conn.Clone();
        }

        /// <summary>
        /// ENVIAR OS JOGOS
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnEnviar_Click(object sender, RoutedEventArgs e)
        {
            if (VerificarCampos("WHERE ID = '" + tbID.Text + "'"))
            {
                MessageBox.Show("Você já cadastrou um jogo com esse ID");
                return;
            }

            if (tbID.Text != "" && tbTitulo.Text != "" && tbTipo.Text != "" && tbRegiao.Text != "" && tbLink1.Text != "")
            {
                SQLiteConnection conn = new SQLiteConnection(conexao);
                if (conn.State == ConnectionState.Closed)
                    conn.Open();

                SQLiteCommand cmd = new SQLiteCommand("INSERT INTO tb_games (ID, TITULO, TIPO, REGIAO, LINK1, LINK2, LINK3, LINK4, LINK5, CAPA) VALUES (@ID, @TITULO, @TIPO, @REGIAO, @LINK1, @LINK2, @LINK3, @LINK4, @LINK5, @CAPA)", conn);

                cmd.Parameters.AddWithValue("ID", tbID.Text.Trim());
                cmd.Parameters.AddWithValue("TITULO", tbTitulo.Text.Trim());
                cmd.Parameters.AddWithValue("TIPO", tbTipo.Text.Trim());
                cmd.Parameters.AddWithValue("REGIAO", tbRegiao.Text.Trim());
                cmd.Parameters.AddWithValue("LINK1", tbLink1.Text.Trim());

                if (tbLink2.Text != "")
                {
                    cmd.Parameters.AddWithValue("LINK2", tbLink2.Text.Trim());
                }
                else
                {
                    cmd.Parameters.AddWithValue("LINK2", "");
                }

                if (tbLink3.Text != "")
                {
                    cmd.Parameters.AddWithValue("LINK3", tbLink3.Text.Trim());
                }
                else
                {
                    cmd.Parameters.AddWithValue("LINK3", "");
                }

                if (tbLink4.Text != "")
                {
                    cmd.Parameters.AddWithValue("LINK4", tbLink4.Text.Trim());
                }
                else
                {
                    cmd.Parameters.AddWithValue("LINK4", "");
                }

                if (tbLink5.Text != "")
                {
                    cmd.Parameters.AddWithValue("LINK5", tbLink5.Text.Trim());
                }
                else
                {
                    cmd.Parameters.AddWithValue("LINK5", "");
                }    

                cmd.Parameters.AddWithValue("CAPA", tbID.Text.Trim() + ".jpg");

                // FAZENDO UPLOAD DA IMAGEM
                if (this.Cimage != "")
                {
                    // MOVER ARQUIVO
                    if (File.Exists(path + "data\\covers\\" + tbID.Text.Trim() + ".jpg"))
                    {
                        File.Delete(path + "data\\covers\\" + tbID.Text.Trim() + ".jpg");
                    }

                    File.Copy(Cimage, path + "data\\covers\\" + tbID.Text.Trim() + ".jpg");
                }

                try
                {
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Registro incluido com sucesso!");
                    LimparCampos();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro ao cadastrar:\n" + ex.Message);
                }

                conn.Clone();
            }
            else
            {
                MessageBox.Show("Você precisa preecher todos os campos!", "OCORREU UM ERRO", MessageBoxButton.OK, MessageBoxImage.Error);
            }// Validação campos vazios
        }

        private void btnAbrir_Click(object sender, RoutedEventArgs e)
        {
            var OpenArq = new System.Windows.Forms.OpenFileDialog
            {
                FileName = "imagem.jpg", //Nome do arquivo
                Filter = "capa.jpg (*.jpg)|*.jpg", // Tipos de arquivos permitido
            };

            if (OpenArq.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                tbImagem.Text   = OpenArq.FileName;
                this.Cimage     = OpenArq.FileName;
                this.Nimage     = OpenArq.SafeFileName;
            }
        }

        private void LimparCampos()
        {
            tbID.Text       = String.Empty;
            tbTitulo.Text   = String.Empty;
            tbTipo.Text     = String.Empty;
            tbRegiao.Text   = String.Empty;
            tbImagem.Text   = String.Empty;
            tbLink1.Text    = String.Empty;
            tbLink2.Text    = String.Empty;
            tbLink3.Text    = String.Empty;
            tbLink4.Text    = String.Empty;
        }
    }
}
