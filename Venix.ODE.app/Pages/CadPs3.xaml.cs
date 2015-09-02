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


namespace Venix.ODE.app.Pages
{
    /// <summary>
    /// Interaction logic for CadPs3.xaml
    /// </summary>
    public partial class CadPs3 : UserControl
    {
        string path = AppDomain.CurrentDomain.BaseDirectory;

        private string Nimage = "";
        private string Cimage = "";

        private string vModo = "";

        private static string conexao = "Data Source=Banco.db";
        private static string nBanco = "Banco.db";

        public class Lista
        {
            public int id { get; set; }
            public string nome { get; set; }
            public string imagem { get; set; }
            public string link { get; set; }
            public string regiao { get; set; }
            public string rapcode { get; set; }
            public string rap { get; set; }
        }

        public CadPs3()
        {
            InitializeComponent();
        }
        /// <summary>
        /// CADASTRAR
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSalvar_Click(object sender, RoutedEventArgs e)
        {
            if(vModo == "novo")
            {
                if (tbNome.Text == "" && tbLink.Text == "" && tbRegiao.Text == "")
                {
                    MessageBox.Show("Você precisa preecher todos os campos!", "OCORREU UM ERRO", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    SQLiteConnection conn = new SQLiteConnection(conexao);
                    if (conn.State == ConnectionState.Closed)
                        conn.Open();

                    SQLiteCommand cmd = new SQLiteCommand("INSERT INTO LISTA (NOME, IMAGEM, LINK, REGIAO, RAPCODE, RAP, OBS) VALUES (@NOME, @IMAGEM, @LINK, @REGIAO, @RAPCODE, @RAP, @OBS)", conn);
                    cmd.Parameters.AddWithValue("NOME", tbNome.Text.Trim());
                    cmd.Parameters.AddWithValue("IMAGEM", tbRegiao.Text.Trim() + ".jpg");
                    cmd.Parameters.AddWithValue("LINK", tbLink.Text.Trim());
                    cmd.Parameters.AddWithValue("REGIAO", tbRegiao.Text.Trim());
                    cmd.Parameters.AddWithValue("RAPCODE", tbRapCode.Text.Trim());
                    cmd.Parameters.AddWithValue("RAP", tbRapNome.Text.Trim());
                    cmd.Parameters.AddWithValue("OBS", tbObs.Text);

                    // FAZENDO UPLOAD DA IMAGEM
                    if (this.Cimage != "")
                    {
                        // MOVER ARQUIVO
                        if (File.Exists(path + "data\\covers\\" + tbRegiao.Text.Trim() + ".jpg"))
                        {
                            File.Delete(path + "data\\covers\\" + tbRegiao.Text.Trim() + ".jpg");
                        }

                        File.Copy(Cimage, path + "data\\covers\\" + tbRegiao.Text.Trim() + ".jpg");
                    }

                    try
                    {
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Registro salvo com sucesso!");
                        DesaCampos();
                    
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Erro ao criar Banco de dados:\n" + ex.Message);
                    }
                }
            }// Cadastrando novo Registro
            else if (vModo == "alterar")
            {
                if (tbNome.Text == "" && tbLink.Text == "" && tbRegiao.Text == "")
                {
                    MessageBox.Show("Você precisa preecher todos os campos!", "OCORREU UM ERRO", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    int IdRegistro = Convert.ToInt32(tbCodigo.Text);

                    if (IdRegistro > 0)
                    {
                        SQLiteConnection conn = new SQLiteConnection(conexao);
                        if (conn.State == ConnectionState.Closed)
                            conn.Open();

                        SQLiteCommand cmd = new SQLiteCommand("UPDATE LISTA SET NOME = @NOME, IMAGEM = @IMAGEM, LINK = @LINK, REGIAO = @REGIAO, RAPCODE = @RAPCODE, RAP = @RAP, OBS = @OBS WHERE ID = @CODIGO", conn);
                        cmd.Parameters.AddWithValue("CODIGO", IdRegistro);
                        cmd.Parameters.AddWithValue("NOME", tbNome.Text.Trim());
                        cmd.Parameters.AddWithValue("IMAGEM", tbRegiao.Text.Trim() + ".jpg");
                        cmd.Parameters.AddWithValue("LINK", tbLink.Text.Trim());
                        cmd.Parameters.AddWithValue("REGIAO", tbRegiao.Text.Trim());
                        cmd.Parameters.AddWithValue("RAPCODE", tbRapCode.Text.Trim());
                        cmd.Parameters.AddWithValue("RAP", tbRapNome.Text.Trim());
                        cmd.Parameters.AddWithValue("OBS", tbObs.Text.Trim());

                        // FAZENDO UPLOAD DA IMAGEM
                        if (this.Cimage != "")
                        {
                            // MOVER ARQUIVO
                            if (File.Exists(path + "data\\covers\\" + tbRegiao.Text.Trim() + ".jpg"))
                            {
                                File.Delete(path + "data\\covers\\" + tbRegiao.Text.Trim() + ".jpg");
                            }

                            File.Copy(Cimage, path + "data\\covers\\" + tbRegiao.Text.Trim() + ".jpg");
                        }

                        try
                        {
                            cmd.ExecuteNonQuery();
                            MessageBox.Show("Registro atualizado com sucesso!");

                            DesaCampos();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Erro ao atualizar o Registro:\n" + ex.Message);
                        }
                    }
                }
            }  //Alterando Registro         
        }

        /// <summary>
        /// CANCELAR
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            DesaCampos();
        }
        
        /// <summary>
        /// HABILITANDO OS CAMPOS
        /// </summary>
        private void HabiCampos()
        {
            tbCodigo.IsReadOnly     = true;
            btnBusca.IsEnabled      = false;

            tbNome.IsReadOnly       = false;
            tbImagem.IsReadOnly     = false;
            tbLink.IsReadOnly       = false;
            tbRegiao.IsReadOnly     = false;
            tbRapNome.IsReadOnly    = false;
            tbRapCode.IsReadOnly    = false;
            tbObs.IsReadOnly        = false;

            btnAbrir.IsEnabled      = true;
            btnCancelar.IsEnabled   = true;
            btnSalvar.IsEnabled     = true;
        }
        /// <summary>
        /// DESABILITANDO E LIMPANDO OS CAMPOS
        /// </summary>
        private void DesaCampos()
        {
            tbNome.Text     = String.Empty;
            tbImagem.Text   = String.Empty;
            tbLink.Text     = String.Empty;
            tbRegiao.Text   = String.Empty;
            tbRapNome.Text  = String.Empty;
            tbRapCode.Text  = String.Empty;
            tbObs.Text      = string.Empty;

            tbCodigo.IsReadOnly     = false;
            btnBusca.IsEnabled      = true;

            tbNome.IsReadOnly       = true;
            tbImagem.IsReadOnly     = true;
            tbLink.IsReadOnly       = true;
            tbRegiao.IsReadOnly     = true;
            tbRapNome.IsReadOnly    = true;
            tbRapCode.IsReadOnly    = true;
            tbObs.IsReadOnly        = true;

            btnAbrir.IsEnabled      = false;
            btnApagar.IsEnabled     = false;
            btnAlterar.IsEnabled    = false;
            btnAbrir.IsEnabled      = false;
            btnCancelar.IsEnabled   = false;
            btnSalvar.IsEnabled     = false;
        }


        /// <summary>
        /// CARREGAMENTO DO LAYOUT
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            if (!File.Exists(nBanco))
            {
                SQLiteConnection.CreateFile(nBanco);
                SQLiteConnection conn = new SQLiteConnection(conexao);
                conn.Open();

                StringBuilder sql = new StringBuilder();
                sql.AppendLine("CREATE TABLE IF NOT EXISTS LISTA ([ID] INTEGER PRIMARY KEY AUTOINCREMENT,");
                sql.AppendLine("[NOME] VARCHAR(50), [IMAGEM] VARCHAR(50),[LINK] VARCHAR(255),");
                sql.AppendLine("[REGIAO] VARCHAR(50), [RAPCODE] VARCHAR(255), [RAP] VARCHAR(255), [OBS] TEXT)");

                SQLiteCommand cmd = new SQLiteCommand(sql.ToString(), conn);
                try
                {
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro ao criar Banco de dados:\n" + ex.Message);
                }
            }
        }

        /// <summary>
        /// ABRINDO JANELA PARA BUSCAR IMAGEM
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
                tbImagem.Text   = OpenArq.FileName;
                this.Cimage     = OpenArq.FileName;
                this.Nimage     = OpenArq.SafeFileName;
            }
        }

        /// <summary>
        /// ADICIONANDO NOVO REGISTRO
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnNovo_Click(object sender, RoutedEventArgs e)
        {
            DesaCampos();
            HabiCampos();
            vModo = "novo";
        }

        /// <summary>
        /// BUSCANDO REGISTRO
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnBusca_Click(object sender, RoutedEventArgs e)
        {
            string ValorSelec       = tbCodigo.Text;
            btnAlterar.IsEnabled    = true;
            btnApagar.IsEnabled     = true;

            if (ValorSelec != "")
            {
                SQLiteConnection conn = new SQLiteConnection(conexao);
                if (conn.State == ConnectionState.Closed)
                    conn.Open();

                SQLiteCommand cmd = new SQLiteCommand("SELECT * FROM LISTA WHERE ID = " + ValorSelec + "", conn);
                SQLiteDataReader dr = cmd.ExecuteReader();

                if(dr.HasRows)
                {
                    while (dr.Read())
                    {
                        tbNome.Text     = dr["NOME"].ToString();
                        tbImagem.Text   = dr["IMAGEM"].ToString();
                        tbLink.Text     = dr["LINK"].ToString();
                        tbRegiao.Text   = dr["REGIAO"].ToString();
                        tbRapNome.Text  = dr["RAP"].ToString();
                        tbRapCode.Text  = dr["RAPCODE"].ToString();
                        tbObs.Text      = dr["OBS"].ToString();
                    }
                }
                else
                {
                    MessageBox.Show("Sem Resultado!");
                    DesaCampos();
                    return;
                }
            }            
        }

        /// <summary>
        /// ALTERANDO O REGISTRO 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAlterar_Click(object sender, RoutedEventArgs e)
        {
            HabiCampos();
            vModo = "alterar";
        }

        /// <summary>
        /// APAGANDO O REGSITRO
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnApagar_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Você tem certeza que deseja deletar esse Registro?",
                "Confirmação", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                SQLiteConnection conn = new SQLiteConnection(conexao);
                if (conn.State == ConnectionState.Closed)
                    conn.Open();

                SQLiteCommand cmd = new SQLiteCommand("DELETE FROM LISTA WHERE ID = @CODIGO", conn);
                cmd.Parameters.AddWithValue("CODIGO", Convert.ToInt32(tbCodigo.Text));
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
    }
}
