using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
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
using System.Data;
using xDialog;

namespace Venix.ODE.app.Pages
{
    /// <summary>
    /// Interaction logic for CriarRap.xaml
    /// </summary>
    public partial class CriarRap : UserControl
    {
        string path = AppDomain.CurrentDomain.BaseDirectory;

        private static string conexao = "Data Source=db.db";

        public class Lista
        {
            public string id { get; set; }
            public string nome { get; set; }            
            public string rapcode { get; set; }
            public string rapnome { get; set; }
        }

        public CriarRap()
        {
            InitializeComponent();            
        }

        /// <summary>
        /// CARREGAMENTO
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            LeituraDados();
        }

        /// <summary>
        /// LEITURA
        /// </summary>
        private void LeituraDados(string COND = null)
        {
            SQLiteConnection conn = new SQLiteConnection(conexao);
            if (conn.State == ConnectionState.Closed)
                conn.Open();

            SQLiteCommand cmd = new SQLiteCommand("SELECT * FROM tb_raps " + COND, conn);
            SQLiteDataReader dr = cmd.ExecuteReader();

            List<Lista> lista = new List<Lista>();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    lista.Add(new Lista
                    {
                        id      = dr["ID"].ToString(),
                        nome    = dr["NOME"].ToString(),
                        rapcode = dr["RAPCODE"].ToString(),
                        rapnome = dr["RAPNOME"].ToString()
                    });
                }

                gridRap.ItemsSource = lista;

                gridRap.Columns[0].Header = "ID";
                gridRap.Columns[1].Header = "NOME";
                gridRap.Columns[2].Header = "RAPCODE";
                gridRap.Columns[3].Header = "RAPNOME";
            }
            else
            {
                MessageBox.Show("Nenhum resultado encontrado!");
            }            
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            LeituraDados("WHERE NOME LIKE '%" + tbBusca.Text + "%'");
        }

        private void btnCriarRap_Click(object sender, RoutedEventArgs e)
        {
            try
            { 
                object item         = gridRap.SelectedItem;
                string ValorSelec   = (gridRap.SelectedCells[0].Column.GetCellContent(item) as TextBlock).Text;

                if (ValorSelec != "")
                {
                    SQLiteConnection conn = new SQLiteConnection(conexao);
                    if (conn.State == ConnectionState.Closed)
                        conn.Open();

                    SQLiteCommand cmd = new SQLiteCommand("SELECT * FROM tb_raps WHERE ID = '" + ValorSelec + "'", conn);
                    SQLiteDataReader dr = cmd.ExecuteReader();

                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            System.Windows.Forms.SaveFileDialog saveFile = new System.Windows.Forms.SaveFileDialog();

                            saveFile.Title = "Salvando arquivo rap";
                            saveFile.FileName = dr["RAPNOME"].ToString() + ".rap";
                            saveFile.Filter = "Arquivo rap (*.rap)|*.rap";
                            saveFile.FilterIndex = 2;
                            saveFile.RestoreDirectory = true;

                            if (saveFile.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                            {
                                string caminhoArquivo = saveFile.FileName;
                                string hexString = dr["RAPCODE"].ToString().Trim();
                                File.WriteAllBytes(caminhoArquivo, StringToByteArray(hexString));
                                System.Windows.MessageBox.Show(String.Format("RAP para o jogo '{0}' gerado com sucesso!", dr["NOME"].ToString()), "Sucesso!");
                            }
                        }//end while                        
                    }
                    else
                    {
                        MessageBox.Show("Nenhum resultado encontrado!");
                    }            
                }
                else
                {
                    MessageBox.Show("Você precisa selecionar um jogo antes de criar!");
                }
            }
            catch (Exception) { MessageBox.Show("Você precisa selecionar um rap antes de criar!"); }
            
        }


        /// <summary>
        /// CLASSE RESPONSAVEL PELA CRIAÇÃO DO ARQUIVO EM HEXA
        /// </summary>
        /// <param name="hex"></param>
        /// <returns></returns>
        public static byte[] StringToByteArray(string hex)
        {
            return Enumerable.Range(0, hex.Length)
                             .Where(x => x % 2 == 0)
                             .Select(x => Convert.ToByte(hex.Substring(x, 2), 16))
                             .ToArray();
        }

        /// <summary>
        /// RAP SELECIONADO
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gridRap_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                object item = gridRap.SelectedItem;
                string ValorSelec = (gridRap.SelectedCells[0].Column.GetCellContent(item) as TextBlock).Text;

                if (ValorSelec != "")
                {
                    SQLiteConnection conn = new SQLiteConnection(conexao);
                    if (conn.State == ConnectionState.Closed)
                        conn.Open();

                    SQLiteCommand cmd = new SQLiteCommand("SELECT * FROM tb_raps WHERE ID = '" + ValorSelec + "'", conn);
                    SQLiteDataReader dr = cmd.ExecuteReader();

                    if(dr.HasRows)
                    {
                        while(dr.Read())
                        {
                            tbID.Text       = dr["ID"].ToString();
                            tbNome.Text     = dr["NOME"].ToString();
                            tbRapCode.Text  = dr["RAPCODE"].ToString();
                            tbRapNome.Text  = dr["RAPNOME"].ToString();
                        }

                        HabiCampos();                    
                    }

                    conn.Close();
                }
            }
            catch(Exception)
            {}
        }

        private void HabiCampos()
        {
            //tbID.IsReadOnly         = false;
            tbNome.IsReadOnly       = false;
            tbRapCode.IsReadOnly    = false;
            tbRapNome.IsReadOnly    = false;

            btnAlterar.IsEnabled    = true;
            btnApagar.IsEnabled     = true;
        }

        /// <summary>
        /// ALTERANDO O REGISTRO
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAlterar_Click(object sender, RoutedEventArgs e)
        {
            tbID.IsReadOnly = true;

            SQLiteConnection conn = new SQLiteConnection(conexao);
            if (conn.State == ConnectionState.Closed)
                conn.Open();

            SQLiteCommand cmd = new SQLiteCommand("UPDATE tb_raps SET NOME = @NOME, RAPCODE = @RAPCODE, RAPNOME = @RAPNOME WHERE ID = @ID", conn);

            cmd.Parameters.AddWithValue("ID", tbID.Text.Trim());
            cmd.Parameters.AddWithValue("NOME", tbNome.Text.Trim());
            cmd.Parameters.AddWithValue("RAPCODE", tbRapCode.Text.Trim());
            cmd.Parameters.AddWithValue("RAPNOME", tbRapNome.Text.Trim());

            try
            {
                cmd.ExecuteNonQuery();
                MsgBox.Show("Registro Alterado com sucesso!",
                "Sucesso", MsgBox.Buttons.OK, MsgBox.Icon.Info, MsgBox.AnimateStyle.SlideDown);
                LeituraDados();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao atualizar:\n" + ex.Message);
            }

            conn.Clone();
        }

        /// <summary>
        /// APAGANDO REGISTRO
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

                    SQLiteCommand cmd = new SQLiteCommand("DELETE FROM tb_raps WHERE ID = @ID", conn);
                    cmd.Parameters.AddWithValue("ID", tbID.Text);

                    try
                    {
                        cmd.ExecuteNonQuery();
                        MsgBox.Show("Registro apagado com sucesso!", "Sucesso", MsgBox.Buttons.OK);
                        LeituraDados();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Erro ao apagar o Registro:\n" + ex.Message);
                    }
                }
            }
            else
            {
                MsgBox.Show("Você precisa selecionar um registro para apagar!", "Atenção", MsgBox.Buttons.OK);
            }
        }
        


    }
}
