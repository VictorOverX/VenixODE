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
using FirstFloor.ModernUI.Windows.Controls;
using FirstFloor.ModernUI.Presentation;
using FirstFloor.ModernUI.Windows.Navigation;
using xDialog;
using System.Net;
using System.Diagnostics;
using System.ComponentModel;

namespace Venix.ODE.app.Pages.Download.PS3
{
    /// <summary>
    /// Interaction logic for bJogos.xaml
    /// </summary>
    public partial class bJogos : UserControl
    {
        string path = AppDomain.CurrentDomain.BaseDirectory;
        private static string conexao = "Data Source=db.db";
        string NomeGame = "";

        WebClient webDownload;           
        Stopwatch sw = new Stopwatch();
        
        public class Lista
        {
            public string ID { get; set; }
            public string TITULO { get; set; }
            public string TIPO { get; set; }
            public string REGIAO { get; set; }
        }

        public bJogos()
        {
            InitializeComponent();
        }
        
        /// <summary>
        /// CARREGANDO
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Panel_Loaded(object sender, RoutedEventArgs e)
        {
            Leitura();

            tbBaixando.Text         = String.Empty;
            veloDonwload.Text       = String.Empty;
            progressBar1.Value      = 0;
            labelPerc.Text          = String.Empty;
            labelDownload.Text      = String.Empty;

            btnCancelar.IsEnabled = false;
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
                    lista.Add(new Lista
                    {
                        ID      = dr["ID"].ToString(),
                        TITULO  = dr["TITULO"].ToString(),
                        TIPO    = dr["TIPO"].ToString(),
                        REGIAO  = dr["REGIAO"].ToString()
                    });
                }

                gridLista.ItemsSource = lista;
                tbTotal.Text = "Total " + lista.Count.ToString();

                gridLista.Columns[0].Header = "ID";
                gridLista.Columns[1].Header = "TITULO";
                gridLista.Columns[2].Header = "TIPO";
                gridLista.Columns[3].Header = "REGIAO";
            }
            else
            {
                ///MessageBox.Show("Nenhum resultado encontrado!");
            }            
        }

        /// <summary>
        /// PESQUISA POR NOME DO JOGO
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tbBusca_TextChanged(object sender, TextChangedEventArgs e)
        {
            Leitura("WHERE TITULO LIKE '%" + tbBusca.Text + "%'");
        }

        private void tbTipo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string Value = "";
            if (tbTipo.SelectedIndex >= 0)
            {
                Value = ((ComboBoxItem)tbTipo.SelectedItem).Content.ToString();
                Leitura("WHERE TIPO = '" + Value + "'");
            }
        }

        /// <summary>
        /// RESULTADO SELECIONADO
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gridLista_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                string ValorSelec = "";
                object item = gridLista.SelectedItem;
                if (gridLista.SelectedIndex != -1)
                ValorSelec = (gridLista.SelectedCells[0].Column.GetCellContent(item) as TextBlock).Text;

                if (ValorSelec != "")
                {
                    SQLiteConnection conn = new SQLiteConnection(conexao);
                    if (conn.State == ConnectionState.Closed)
                        conn.Open();

                    SQLiteCommand cmd = new SQLiteCommand("SELECT * FROM tb_games WHERE ID = '" + ValorSelec.Trim() + "'", conn);
                    SQLiteDataReader dr = cmd.ExecuteReader();

                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            imgCapa.Source  = new BitmapImage(new Uri(path + "data\\covers\\" + dr["CAPA"].ToString()));
                            tbID.Text       = dr["ID"].ToString();
                            tbTitulo.Text   = dr["TITULO"].ToString();
                        }
                    }
                    else
                    {
                        MessageBox.Show("Nenhum resultado encontrado!");
                    }
                }   
            }
            catch(Exception error)
            { 
                ///MessageBox.Show(error.Message); 
            }
        }
        /// <summary>
        /// BAIXANDO O JOGO
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                object item = gridLista.SelectedItem;
                string ValorSelec = (gridLista.SelectedCells[0].Column.GetCellContent(item) as TextBlock).Text;

                if (ValorSelec != "")
                {
                    SQLiteConnection conn = new SQLiteConnection(conexao);
                    if (conn.State == ConnectionState.Closed)
                        conn.Open();

                    SQLiteCommand cmd = new SQLiteCommand("SELECT * FROM tb_games WHERE ID = '" + ValorSelec.Trim() + "'", conn);
                    SQLiteDataReader dr = cmd.ExecuteReader();

                    int i = 0;
                    string res = "";

                    if (dr.HasRows)
                    {
                        
                        while (dr.Read())
                        {
                            NomeGame = dr["TITULO"].ToString();

                            if (dr["LINK1"].ToString() != "") { i = i + 1; }
                            if (dr["LINK2"].ToString() != "") { i = i + 1; }
                            if (dr["LINK3"].ToString() != "") { i = i + 1; }
                            if (dr["LINK4"].ToString() != "") { i = i + 1; }
                            if (dr["LINK5"].ToString() != "") { i = i + 1; }

                            if (i > 1)
                            {
                                System.Windows.Forms.DialogResult result = MsgBox.Show("Voce tem mais de um link para download, Selecione o Link que deseja baixar clicando em SIM, Obrigado!",
                                    "Jogo com mais de um Link", MsgBox.Buttons.OKCancel, MsgBox.Icon.Info, MsgBox.AnimateStyle.FadeIn);

                                if (result == System.Windows.Forms.DialogResult.OK)
                                {
                                    System.Windows.Forms.DialogResult result1 = MsgBox.Show("Você deseja baixar o primeiro Link?",
                                    "Primeiro Link", MsgBox.Buttons.YesNoCancel, MsgBox.Icon.Info, MsgBox.AnimateStyle.FadeIn);

                                    if (result1 == System.Windows.Forms.DialogResult.Yes)
                                    {
                                        res = dr["LINK1"].ToString();
                                    }
                                    else if (result1 == System.Windows.Forms.DialogResult.No)
                                    {
                                        System.Windows.Forms.DialogResult result2 = MsgBox.Show("Você deseja baixar o segundo Link?",
                                        "Segundo Link", MsgBox.Buttons.YesNoCancel, MsgBox.Icon.Info, MsgBox.AnimateStyle.FadeIn);

                                        if (result2 == System.Windows.Forms.DialogResult.Yes)
                                        {
                                            res = dr["LINK2"].ToString();
                                        }
                                        else if (result2 == System.Windows.Forms.DialogResult.No)
                                        {
                                            if (i > 2)
                                            {
                                                System.Windows.Forms.DialogResult result3 = MsgBox.Show("Você deseja baixar o terceiro Link?",
                                                "Terceiro Link", MsgBox.Buttons.YesNoCancel, MsgBox.Icon.Info, MsgBox.AnimateStyle.FadeIn);

                                                if (result3 == System.Windows.Forms.DialogResult.Yes)
                                                {
                                                    res = dr["LINK3"].ToString();
                                                }
                                                else if (result3 == System.Windows.Forms.DialogResult.No)
                                                {
                                                    if (i > 3)
                                                    {
                                                        System.Windows.Forms.DialogResult result4 = MsgBox.Show("Você deseja baixar o quarto Link?",
                                                        "Quarto Link", MsgBox.Buttons.YesNoCancel, MsgBox.Icon.Info, MsgBox.AnimateStyle.FadeIn);

                                                        if (result4 == System.Windows.Forms.DialogResult.Yes)
                                                        {
                                                            res = dr["LINK4"].ToString();
                                                        }
                                                        else if (result4 == System.Windows.Forms.DialogResult.No)
                                                        {
                                                            if (i > 4)
                                                            {
                                                                System.Windows.Forms.DialogResult result5 = MsgBox.Show("Você deseja baixar o quinto Link?",
                                                                "Quinto Link", MsgBox.Buttons.YesNoCancel, MsgBox.Icon.Info, MsgBox.AnimateStyle.FadeIn);

                                                                if (result5 == System.Windows.Forms.DialogResult.Yes)
                                                                {
                                                                    res = dr["LINK5"].ToString();
                                                                } // Quinto Link
                                                            } // Existe o QUinto Link
                                                        } // Quarto Link
                                                    }//Existe o Quarto Link
                                                } // Terceiro Link
                                            } // Existe o Terceiro Link   
                                        } // Segundo Link
                                    } // Primeiro Link
                                }
                            }
                            else
                            {
                                res = dr["LINK1"].ToString();
                            }// Condição verificando quantos links existem e quais baxiar
                        }// End While

                        /** BAIXANDO O JOGO **/
                        System.Windows.Forms.SaveFileDialog saveFile = new System.Windows.Forms.SaveFileDialog();

                        saveFile.Title = "Salvando arquivo pkg";
                        saveFile.FileName = NomeGame;
                        saveFile.Filter = "Arquivo pkg (*.pkg)|*.pkg";
                        saveFile.FilterIndex = 2;
                        saveFile.RestoreDirectory = true;

                        if (saveFile.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                        {
                            string caminhoArquivo = saveFile.FileName;
                            DownloadFile(res, caminhoArquivo);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Nenhum resultado encontrado!");
                    }
                }
            }
            catch (Exception erro)
            {
                MsgBox.Show("Você precisa selecionar um jogo para baixar!\n" + erro.Message,
                        "Atenção", MsgBox.Buttons.OK, MsgBox.Icon.Question, MsgBox.AnimateStyle.SlideDown);
            }
        }

        /// <summary>
        /// CLASSE RESPONSAVEL PELO DOWNLOAD DO ARQUIVO
        /// </summary>
        /// <param name="urlAddress"></param>
        /// <param name="location"></param>
        public void DownloadFile(string urlAddress, string location)
        {
            using (webDownload = new WebClient())
            {
                webDownload.DownloadFileCompleted += new AsyncCompletedEventHandler(Completed);
                webDownload.DownloadProgressChanged += new DownloadProgressChangedEventHandler(ProgressChanged);

                // A variável verifica a url (certificando-se de que começa com http: //)
                Uri URL = urlAddress.StartsWith("http://", StringComparison.OrdinalIgnoreCase) ? new Uri(urlAddress) : new Uri("http://" + urlAddress);

                // Inicie o cronômetro que iremos utilizar para calcular a velocidade de download
                sw.Start();
                btnCancelar.IsEnabled = true;

                try
                {
                    // Iniciar o download do arquivo
                    webDownload.DownloadFileAsync(URL, location);
                    tbBaixando.Text = "Baixando";
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        /// <summary>
        /// DETALHES DO DOWNLOAD
        ///  O evento que dispara sempre que o progresso da WebClient é alterada
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            // Calcule a velocidade de download e mostra a velocidade.
            veloDonwload.Text = string.Format("{0} kb/s", (e.BytesReceived / 1024d / sw.Elapsed.TotalSeconds).ToString("0.00"));

            // Atualize o percentual da barra de progresso somente quando o valor não é o mesmo.         
            progressBar1.Value = e.ProgressPercentage;

            // Mostrar o percentual em na label.
            labelPerc.Text = e.ProgressPercentage.ToString() + "%";

            // Atualize a label com a quantidade de dados que foram baixados até o momento o tamanho total do arquivo atualmente estamos baixando
            labelDownload.Text = string.Format("{0} MB's / {1} MB's",
                (e.BytesReceived / 1024d / 1024d).ToString("0.00"),
                (e.TotalBytesToReceive / 1024d / 1024d).ToString("0.00"));
        }

        /// <summary>
        /// DOWNLOAD COMPLETO
        /// O evento, que será desencadeada quando o WebClient é concluída
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Completed(object sender, AsyncCompletedEventArgs e)
        {
            // Zerar o cronômetro.
            sw.Reset();

            if (e.Cancelled == true)
            {
                MsgBox.Show("Download cancelado com sucesso!",
                        "Atenção", MsgBox.Buttons.OK, MsgBox.Icon.Exclamation, MsgBox.AnimateStyle.SlideDown);

                Limpar();
            }
            else
            {
                MsgBox.Show("Download concluido com sucesso!",
                        "Atenção", MsgBox.Buttons.OK, MsgBox.Icon.Application, MsgBox.AnimateStyle.ZoomIn);

                Limpar();
            }
        }
        /// <summary>
        /// CANCELANDO DOWNLOAD
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            webDownload.CancelAsync();
        }

        private void Limpar()
        {
            tbBaixando.Text = String.Empty;
            veloDonwload.Text = String.Empty;
            progressBar1.Value = 0;
            labelPerc.Text = String.Empty;
            labelDownload.Text = String.Empty;

            btnCancelar.IsEnabled = false;
        }










    }
}
