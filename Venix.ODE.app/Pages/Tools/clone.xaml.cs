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

namespace Venix.ODE.app.Pages.Tools
{
    /// <summary>
    /// Interaction logic for clone.xaml
    /// </summary>
    public partial class clone : UserControl
    {
        private Byte[] metldr;

        public clone()
        {
            InitializeComponent();
        }

        /// <summary>
        /// ABRINDO A NOR
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tbAbrirNor_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.OpenFileDialog openFile = new System.Windows.Forms.OpenFileDialog();

            openFile.Filter = "BIN Files (.bin)|*.bin|All Files (*.*)|*.*";
            openFile.FilterIndex = 1;
            openFile.Title = ("Abrir arquivo dump.bin");
            openFile.FileName = ("dump.bin");

            if (openFile.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                tbNor.Text = openFile.FileName;

                try
                {
                    BinaryReader read = new BinaryReader(new FileStream(openFile.FileName, FileMode.Open));

                    //Posição da Leitura
                    read.BaseStream.Position = 0x840;

                    //Lendo os offset
                    //metldr = BitConverter.ToString(read.ReadBytes(59680)).Replace("-", null); //reader.ReadBytes(12)
                    metldr = read.ReadBytes(59680);
                    ByteArrayToFile("data\\clone\\dump", metldr);


                }
                catch
                {
                    MessageBox.Show("Sorry the application seems to have encountered a problem", "Error");
                }
            }
        }

        public bool ByteArrayToFile(string _FileName, byte[] _ByteArray)
        {
            try
            {
                // Open file for reading
                System.IO.FileStream _FileStream =
                   new System.IO.FileStream(_FileName, System.IO.FileMode.Create,
                                            System.IO.FileAccess.Write);
                // Writes a block of bytes to this stream using data from
                // a byte array.
                _FileStream.Write(_ByteArray, 0, _ByteArray.Length);

                // close file stream
                _FileStream.Close();

                return true;
            }
            catch (Exception _Exception)
            {
                // Error
                Console.WriteLine("Exception caught in process: {0}",
                                  _Exception.ToString());
            }

            // error occured, return false
            return false;
        }



    }
}
