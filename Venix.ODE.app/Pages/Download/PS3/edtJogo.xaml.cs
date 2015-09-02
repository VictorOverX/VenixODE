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

namespace Venix.ODE.app.Pages.Download.PS3
{
    /// <summary>
    /// Interaction logic for edtJogo.xaml
    /// </summary>
    public partial class edtJogo : UserControl
    {
        string path = AppDomain.CurrentDomain.BaseDirectory;

        private string Nimage = "";
        private string Cimage = "";

        private string vModo  = "";

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

        public edtJogo()
        {
            InitializeComponent();
        }       

        /// <summary>
        /// ALTERANDO O JOGO
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAlterar_Click(object sender, RoutedEventArgs e)
        {
            HabiCampos();
            vModo = "alterar";
        }

        /// <summary>
        /// ABRINDO JANELA
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAbrir_Click(object sender, RoutedEventArgs e)
        {
            var OpenArq = new System.Windows.Forms.OpenFileDialog
            {
                FileName = "imagem.jpg", //Nome do arquivo
                Filter = "capa.jpg (*.jpg)|*.jpg", // Tipos de arquivos permitido
            };

            if (OpenArq.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                tbImagem.Text = OpenArq.FileName;
                this.Cimage = OpenArq.FileName;
                this.Nimage = OpenArq.SafeFileName;
            }
        }
        /// <summary>
        /// APAGANDO O JOGO
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnApagar_Click(object sender, RoutedEventArgs e)
        {
            if (tbID.Text != "")
            {
                System.Windows.Forms.DialogResult result = MsgBox.Show("Tem certeza que deseja apagar esse registro?",
                    "Deletando Registro", MsgBox.Buttons.YesNo, MsgBox.Icon.Question, MsgBox.AnimateStyle.FadeIn);

                if (result == System.Windows.Forms.DialogResult.Yes)
                {
                    SQLiteConnection conn = new SQLiteConnection(conexao);
                    if (conn.State == ConnectionState.Closed)
                        conn.Open();

                    SQLiteCommand cmd = new SQLiteCommand("DELETE FROM tb_games WHERE ID = @CODIGO", conn);
                    cmd.Parameters.AddWithValue("CODIGO", tbID.Text);

                    try
                    {
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Registro apagado com sucesso!");
                        DesaCampos();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Erro ao apagar o Registro:\n" + ex.Message);
                    }
                }
            }
            else
            {
                MsgBox.Show("Você precisa selecionar um registro para apagar!");
            }               
        }

        /// <summary>
        /// CANCELAR O PROCESSO
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            DesaCampos();
        }
        /// <summary>
        /// ALTERANDO REGISTRO
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnEnviar_Click(object sender, RoutedEventArgs e)
        {
            if(vModo == "alterar")
            {
                if (tbID.Text != "" && tbTitulo.Text != "" && tbTipo.Text != "" && tbRegiao.Text != "" && tbLink1.Text != "")
                {
                    SQLiteConnection conn = new SQLiteConnection(conexao);
                    if (conn.State == ConnectionState.Closed)
                        conn.Open();

                    SQLiteCommand cmd = new SQLiteCommand("UPDATE tb_games SET ID = @ID, TITULO = @TITULO, TIPO = @TIPO, REGIAO = @REGIAO, LINK1 = @LINK1, LINK2 = @LINK2, LINK3 = @LINK3, LINK4 = @LINK4, LINK5 = @LINK5, CAPA = @CAPA WHERE ID = @ID", conn);

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
                        MsgBox.Show("Registro Alterado com sucesso!",
                        "Sucesso", MsgBox.Buttons.OK, MsgBox.Icon.Info, MsgBox.AnimateStyle.SlideDown);
                        DesaCampos();
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
        }

        /// <summary>
        /// HABILITAR CAMPOS
        /// </summary>
        private void HabiCampos()
        {
            tbID.IsReadOnly     = true;
            tbTitulo.IsReadOnly = false;
            tbTipo.IsEnabled    = true;
            tbRegiao.IsEnabled  = true;
            tbLink1.IsReadOnly  = false;
            tbLink2.IsReadOnly  = false;
            tbLink3.IsReadOnly  = false;
            tbLink4.IsReadOnly  = false;
            tbLink5.IsReadOnly  = false;
            tbImagem.IsReadOnly = false;

            btnApagar.IsEnabled     = true;
            btnCancelar.IsEnabled   = true;
            btnAbrir.IsEnabled      = true;
            btnEnviar.IsEnabled     = true;

        }
        /// <summary>
        /// DESABILITAR CAMPOS
        /// </summary>
        private void DesaCampos()
        {
            tbID.IsReadOnly     = false;
            tbTitulo.IsReadOnly = true;
            tbTipo.IsEnabled    = false;
            tbRegiao.IsEnabled  = false;
            tbLink1.IsReadOnly  = true;
            tbLink2.IsReadOnly  = true;
            tbLink3.IsReadOnly  = true;
            tbLink4.IsReadOnly  = true;
            tbLink5.IsReadOnly  = true;
            tbImagem.IsReadOnly = true;

            tbID.Text       = String.Empty;
            tbTitulo.Text   = String.Empty;
            tbTipo.Text     = String.Empty;
            tbRegiao.Text   = String.Empty;
            tbLink1.Text    = String.Empty;
            tbLink2.Text    = String.Empty;
            tbLink3.Text    = String.Empty;
            tbLink4.Text    = String.Empty;
            tbLink5.Text    = String.Empty;
            tbImagem.Text   = String.Empty;

            btnApagar.IsEnabled     = false;
            btnAlterar.IsEnabled    = false;
            btnCancelar.IsEnabled   = false;
            btnAbrir.IsEnabled      = false;
            btnEnviar.IsEnabled     = false;
        }

        /// <summary>
        /// BUSCANDO RESULTADO
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnBusca_Click(object sender, RoutedEventArgs e)
        {
            Leitura("WHERE ID = '" + tbID.Text + "'");
        }

        /// <summary>
        /// LEITURA DE DADOS
        /// </summary>
        /// <param name="COND"></param>
        private void Leitura(string COND = null)
        {
            SQLiteConnection conn = new SQLiteConnection(conexao);
            if (conn.State == ConnectionState.Closed)
                conn.Open();

            SQLiteCommand cmd = new SQLiteCommand("SELECT * FROM tb_games " + COND, conn);
            SQLiteDataReader dr = cmd.ExecuteReader();

            List<Lista> lista = new List<Lista>();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    tbID.Text       = dr["ID"].ToString();
                    tbTitulo.Text   = dr["TITULO"].ToString();
                    tbTipo.Text     = dr["TIPO"].ToString();
                    tbRegiao.Text   = dr["REGIAO"].ToString();
                    tbLink1.Text    = dr["LINK1"].ToString();
                    tbLink2.Text    = dr["LINK2"].ToString();
                    tbLink3.Text    = dr["LINK3"].ToString();
                    tbLink4.Text    = dr["LINK4"].ToString();
                    tbLink5.Text    = dr["LINK5"].ToString();
                    tbImagem.Text   = "";
                }

                btnAlterar.IsEnabled    = true;
                btnApagar.IsEnabled     = true;
            }
            else
            {
                MessageBox.Show("Nenhum resultado encontrado!");
            }
        }
    }
}
