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

namespace Venix.ODE.app.Pages
{
    /// <summary>
    /// Interaction logic for CriarRif.xaml
    /// </summary>
    public partial class CriarRif : UserControl
    {
        string raprifName;
        string path = AppDomain.CurrentDomain.BaseDirectory;

        public CriarRif()
        {
            InitializeComponent();
        }
        /// <summary>
        /// CLICK IDPS
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAbrirIDPS_Click(object sender, RoutedEventArgs e)
        {
            var idps = new System.Windows.Forms.OpenFileDialog
            {
                FileName = "IDPS", //Nome do arquivo
                //Filter = "cpukey.txt (*.txt)|*.txt", // Tipos de arquivos permitido
            };

            if (idps.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                tbIDPS.Text = idps.FileName;

                // MOVER ARQUIVO
                if (File.Exists(path + "data\\idps"))
                {
                    File.Delete(path + "data\\idps");
                }

                File.Copy(idps.FileName, path + "data\\idps");
            }
        }
        /// <summary>
        /// CLICK ACT
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAbrirAct_Click(object sender, RoutedEventArgs e)
        {
            var act = new System.Windows.Forms.OpenFileDialog
            {
                FileName = "ACT.DATA", //Nome do arquivo
                Filter = "act.dat (*.dat)|*.dat", // Tipos de arquivos permitido
            };

            if (act.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                tbAct.Text = act.FileName;
                // MOVER ARQUIVO
                if (File.Exists(path + "data\\act.dat"))
                {
                    File.Delete(path + "data\\act.dat");
                }

                File.Copy(act.FileName, path + "data\\act.dat");
            }
        }
        /// <summary>
        /// CLICK RAP/RIF
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAbrirRapRif_Click(object sender, RoutedEventArgs e)
        {
            var raprif = new System.Windows.Forms.OpenFileDialog
            {
                FileName = "RAF / RIF", //Nome do arquivo
                //Filter = "act.dat (*.dat)|*.dat", // Tipos de arquivos permitido
            };

            if (raprif.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                tbRapRif.Text = raprif.FileName;

                // MOVER ARQUIVO
                if (File.Exists(raprif.SafeFileName))
                {
                    File.Delete(raprif.SafeFileName);
                }

                File.Copy(raprif.FileName, raprif.SafeFileName);
                this.raprifName = raprif.SafeFileName;
            }
        }
        /// <summary>
        /// CLICK CRIAR
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCriar_Click(object sender, RoutedEventArgs e)
        {
            if (!File.Exists(path + "data\\act.dat"))
            {
                MessageBox.Show("Você precisa selecionar um arquivo act.dat!", "ERRO", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (!File.Exists(path + "data\\idps"))
            {
                MessageBox.Show("Você precisa selecionar um arquivo idps!", "ERRO", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (tbRapRif.Text != "")
            {
                // Use ProcessStartInfo class
                ProcessStartInfo startInfo = new ProcessStartInfo();
                startInfo.CreateNoWindow = true;
                startInfo.UseShellExecute = false;

                startInfo.FileName = "R2R.exe";
                startInfo.WindowStyle = ProcessWindowStyle.Hidden;
                startInfo.Arguments = " " + this.raprifName;

                try
                {
                    using (Process exeProcess = Process.Start(startInfo))
                    {
                        MessageBox.Show("Arquivo Criado com sucesso!");

                        tbAct.Text = "";
                        tbIDPS.Text = "";
                        tbRapRif.Text = "";
                    }
                }
                catch
                {
                    MessageBox.Show("Ocorreu um erro!");
                }
            }
            else
            {
                MessageBox.Show("Você precisa selecionar RIF ou RAP!", "ERRO", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
