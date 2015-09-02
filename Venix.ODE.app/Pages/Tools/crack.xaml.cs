using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
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
using xDialog;

namespace Venix.ODE.app.Pages.Tools
{
    /// <summary>
    /// Interaction logic for crack.xaml
    /// </summary>
    public partial class crack : UserControl
    {
        List<string> vLista = new List<string>();

        private string ID  = "";
        private string ACT = "";
        private string RAP = "";
        private string RIF = "";
        private string EDAT = "";

        string path = AppDomain.CurrentDomain.BaseDirectory;

        public crack()
        {
            InitializeComponent();
        }

        /// <summary>
        /// CARREGAMENTO DA JANELA
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            Leitura();
        }

        /// <summary>
        /// LEITURA DOS DADOS
        /// </summary>
        private void Leitura()
        {
            try
            {
                StreamReader vLeitor = new StreamReader("data\\builds\\setdata.ini");

                while (!(vLeitor.EndOfStream))
                {
                    vLista.Add(vLeitor.ReadLine());
                }

                tbContentID.Text = vLista[0].Substring(12);
                tbExdata.Text = vLista[5].Substring(26, 8);

                ID = vLista[0].Substring(19, 9);
            }
            catch (Exception erro)
            {
                MessageBox.Show(erro.Message);
            }
        }

        /// <summary>
        /// SALVAR O CONTENT
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSalvar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                vLista[0] = "ContentID = " + tbContentID.Text;
                vLista[5] = "ForcedInstallTo = ../home/" + tbExdata.Text + "/";

                StreamWriter vgravarDados = new StreamWriter("data\\builds\\setdata.ini");

                for (int i = 0; i < vLista.Count; i++)
                {
                    if (vLista[i] != null)
                    {
                        vgravarDados.WriteLine(vLista[i]);
                    }
                }

                vgravarDados.Close();

                vLista.Clear();
                Leitura();

                // VERIFICANDO SE O DIRETORIO EXISTE
                if (!Directory.Exists("data\\builds\\" + ID))
                {
                    Directory.CreateDirectory("data\\builds\\" + ID);
                }
                else
                {
                    Directory.Delete("data\\builds\\" + ID, true);
                    Directory.CreateDirectory("data\\builds\\" + ID);
                }
                
                btnAbrirAct.IsEnabled   = true;
                btnAbrirRap.IsEnabled   = true;
                btnAbrirRif.IsEnabled   = true;
                btnAbrirEdat.IsEnabled  = true;
                btnCriar.IsEnabled      = true;
                btnSalvar.IsEnabled     = false;
                btnCancelar.IsEnabled   = true;
            }
            catch (Exception erro)
            {
                MessageBox.Show(erro.Message);
            }            
        }

        /// <summary>
        /// DELETA AS PASTAS QUANDO TERMINA
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UserControl_Unloaded(object sender, RoutedEventArgs e)
        {
            var pasta = "data\\builds\\";

            try
            { 
                List<string> dirs = new List<string>(Directory.EnumerateDirectories(pasta));

                foreach (var dir in dirs)
                {
                    Directory.Delete(dir.ToString(), true);
                }
            }
            catch(Exception erro)
            {
                //MessageBox.Show(erro.Message);
            }
        }

        /// <summary>
        /// ABRIR ACT
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAbrirAct_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.OpenFileDialog openFile = new System.Windows.Forms.OpenFileDialog();

            openFile.Title = "Abrindo arquivo dat";
            openFile.FileName = "act";
            openFile.Filter = "Arquivo ACT.dat (*.dat)|*.dat";
            openFile.FilterIndex = 2;
            openFile.RestoreDirectory = true;

            if (openFile.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                ACT = openFile.FileName;                
                tbAct.Text = ACT;

                if (!Directory.Exists("data\\builds\\" + ID + "\\exdata"))
                {
                    Directory.CreateDirectory("data\\builds\\" + ID + "\\exdata");
                }

                File.Copy(ACT, "data\\builds\\" + ID + "\\exdata\\" + openFile.SafeFileName);
            }
        }
        /// <summary>
        /// ABRIR RAP
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAbrirRap_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.OpenFileDialog openFile = new System.Windows.Forms.OpenFileDialog();

            openFile.Title = "Abrindo arquivo rap";
            openFile.Filter = "Arquivo rap (*.rap)|*.rap";
            openFile.FilterIndex = 2;
            openFile.RestoreDirectory = true;

            if (openFile.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                RAP = openFile.FileName;
                tbRap.Text = RAP;

                if (!Directory.Exists("data\\builds\\" + ID + "\\exdata"))
                {
                    Directory.CreateDirectory("data\\builds\\" + ID + "\\exdata");
                }

                File.Copy(RAP, "data\\builds\\" + ID + "\\exdata\\" + openFile.SafeFileName);
            }
        }
        /// <summary>
        /// ABRIR RIF
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAbrirRif_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.OpenFileDialog openFile = new System.Windows.Forms.OpenFileDialog();

            openFile.Title = "Abrindo arquivo rif";
            openFile.Filter = "Arquivo rif (*.rif)|*.rif";
            openFile.FilterIndex = 2;
            openFile.RestoreDirectory = true;

            if (openFile.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                RIF = openFile.FileName;
                tbRif.Text = RIF;

                if (!Directory.Exists("data\\builds\\" + ID + "\\exdata"))
                {
                    Directory.CreateDirectory("data\\builds\\" + ID + "\\exdata");
                }

                File.Copy(RIF, "data\\builds\\" + ID + "\\exdata\\" + openFile.SafeFileName);
            }
        }

        /// <summary>
        /// ABRIR EDAT
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAbrirEdat_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.OpenFileDialog openFile = new System.Windows.Forms.OpenFileDialog();

            openFile.Title = "Abrindo arquivo edat";
            openFile.Filter = "Arquivo edat (*.edat)|*.edat";
            openFile.FilterIndex = 2;
            openFile.RestoreDirectory = true;

            if (openFile.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                EDAT = openFile.FileName;
                tbEdat.Text = EDAT;

                if (!Directory.Exists("data\\builds\\" + ID + "\\exdata"))
                {
                    Directory.CreateDirectory("data\\builds\\" + ID + "\\exdata");
                }

                File.Copy(EDAT, "data\\builds\\" + ID + "\\exdata\\" + openFile.SafeFileName);
            }
        }

        private void btnCriar_Click(object sender, RoutedEventArgs e)
        {
            if (tbRif.Text != "")
            {
                string pkgcdir;
                System.Windows.Forms.FolderBrowserDialog folderDir = new System.Windows.Forms.FolderBrowserDialog();

                if (folderDir.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    pkgcdir = folderDir.SelectedPath;

                    // Use ProcessStartInfo class
                    ProcessStartInfo startInfo  = new ProcessStartInfo();
                    startInfo.CreateNoWindow    = true;
                    startInfo.UseShellExecute   = false;

                    startInfo.FileName = "data\\builds\\make_package.exe";
                    startInfo.WindowStyle = ProcessWindowStyle.Hidden;

                    string arg1 = @"data\builds\setdata.ini";
                    string arg2 = @" data\builds\ ";

                    string argfinal = String.Format(" -n {0} {1}{2} -o {3}", arg1, arg2, ID, pkgcdir);
                    startInfo.Arguments = argfinal;

                    try
                    {
                        using (Process exeProcess = Process.Start(startInfo))
                        {
                            MsgBox.Show("Seu crack foi criado com sucesso!", "Sucesso", MsgBox.Buttons.OK);

                            btnAbrirAct.IsEnabled   = false;
                            btnAbrirRap.IsEnabled   = false;
                            btnAbrirRif.IsEnabled   = false;
                            btnCriar.IsEnabled      = false;
                            btnSalvar.IsEnabled     = true;
                            btnCancelar.IsEnabled   = false;
                            btnAbrirEdat.IsEnabled  = false;

                            tbAct.Text  = String.Empty;
                            tbRap.Text  = String.Empty;
                            tbRif.Text  = String.Empty;
                            tbEdat.Text = String.Empty;
                        }

                        if (cbRetail.IsChecked == true)
                        {
                            System.Windows.Forms.OpenFileDialog openFile = new System.Windows.Forms.OpenFileDialog();

                            openFile.Title = "Retail pkg";
                            openFile.Filter = "Arquivo pkg (*.pkg)|*.pkg";
                            openFile.FilterIndex = 2;
                            openFile.RestoreDirectory = true;

                            if (openFile.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                            {
                                string dirFile = openFile.FileName;

                                ProcessStartInfo startSing = new ProcessStartInfo();
                                startSing.CreateNoWindow = true;
                                startSing.UseShellExecute = false;

                                startSing.FileName = "data\\builds\\retail.exe";
                                startSing.WindowStyle = ProcessWindowStyle.Hidden;
                                startSing.Arguments = " " + dirFile;

                                using (Process Process = Process.Start(startSing))
                                {
                                    MsgBox.Show("Seu crack foi finalizado com sucesso!", "Sucesso", MsgBox.Buttons.OK);
                                }
                            }
                        }
                    }
                    catch (Exception erro)
                    {
                        MessageBox.Show("Ocorreu um erro!\n" + erro.Message);
                    }                    
                }               
            }
            else
            {
                MsgBox.Show("Você precisa importa o RAP e RIF para criar o crack!", "Atenção", MsgBox.Buttons.OK, MsgBox.Icon.Exclamation);
            }
        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            btnAbrirAct.IsEnabled = false;
            btnAbrirRap.IsEnabled = false;
            btnAbrirRif.IsEnabled = false;
            btnCriar.IsEnabled      = false;
            btnSalvar.IsEnabled     = true;
            btnCancelar.IsEnabled   = false;
            btnAbrirEdat.IsEnabled  = false;

            tbAct.Text = String.Empty;
            tbRap.Text = String.Empty;
            tbRif.Text = String.Empty;
            tbEdat.Text = String.Empty;

            if (Directory.Exists("data\\builds\\" + ID))
            {
                Directory.Delete("data\\builds\\" + ID, true);
            }
        }

        
    }
}
