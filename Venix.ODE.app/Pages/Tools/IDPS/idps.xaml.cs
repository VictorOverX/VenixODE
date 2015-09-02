using System;
using System.Collections.Generic;
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
    /// Interaction logic for idps.xaml
    /// </summary>
    public partial class idps : UserControl
    {
        string path = AppDomain.CurrentDomain.BaseDirectory;

        public idps()
        {
            InitializeComponent();
        }
        /// <summary>
        /// GERADOR DE IDPS
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnGerar_Click(object sender, RoutedEventArgs e)
        {
            string hexString = tbIDPS.Text.Trim();
            if (tbIDPS.Text.Count() == 32)
            {
                if (tbIDPS.Text.Trim() != "")
                {
                    if (File.Exists(path + "data\\idps"))
                    {
                        File.Delete(path + "data\\idps");
                    }

                    File.WriteAllBytes(path + "data\\idps", StringToByteArray(hexString));
                    MessageBox.Show("Arquivo Criado com sucesso!");
                    tbIDPS.Text = String.Empty;
                }
            }
            else
            {
                MessageBox.Show("Sequencia numérica invalida!");
            }
        }

        /// <summary>
        /// CLASSE RESPONSAVEL PELA CRIAÇÃO DO ARQUIVO HEXA
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
    }
}
