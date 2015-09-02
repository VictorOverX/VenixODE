using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.IO;

namespace Venix.ODE.Model
{
    public class SettingsThemplate
    {
        public string _result;
        #region Constant Variables
        private const string _Root = "Software";
        private const string _Publisher = "Publisher Here";
        private const string _Application = "Application Name Here";
        #endregion Constant Variables

        public static string Link()
        {
            return "http://venix.linexti.com.br/login.php";
        }

        #region Static ApplicationKeys for registry
        /// <summary>
        /// Key to pull the users color settings
        /// </summary>
        public static ApplicationKeys _Color = new ApplicationKeys { Name = "Color", Encrypted = false };
        /// <summary>
        /// Key to pull the users font size setting
        /// </summary>
        public static ApplicationKeys _FontSize = new ApplicationKeys { Name = "FontSize", Encrypted = false };
        /// <summary>
        /// Key to pull the theme from the users settings
        /// </summary>
        public static ApplicationKeys _Theme = new ApplicationKeys { Name = "Theme", Encrypted = false };
        #endregion Static ApplicationKeys for registry

        #region global vars
        string _objResult;
        #endregion

        /// <summary>
        /// Lê e retorna um valor chaves
        /// </summary>
        /// <param name="KeyName">Keyname to read</param>
        /// <returns>string value</returns>
        public static string ReadKey(string config)
        {
            XmlReader reader = XmlReader.Create("Settings\\" + config + ".xml");
            string resultado = "";

            while (reader.Read())
            {
                if (reader.NodeType == XmlNodeType.Element
                && reader.Name == "Settings")
                {
                    while (reader.NodeType != XmlNodeType.EndElement)
                    {
                        reader.Read();
                        if (reader.Name == config)
                        {
                            while (reader.NodeType != XmlNodeType.EndElement)
                            {
                                reader.Read();
                                if (reader.NodeType == XmlNodeType.Text)
                                {
                                    resultado = reader.Value;
                                }
                            }
                        } //end if
                    } //end while
                } //end if
            } //end while

            reader.Close();
            return resultado;
        }

        /// <summary>
        /// Grava os nossos valores para a aplicação, mas exclui DB
        /// </summary>
        /// <param name="keyName"></param>
        /// <param name="value"></param>
        public void WriteKey(ApplicationKeys keyName, string value)
        {
            if (!Directory.Exists("Settings"))
            {
                Directory.CreateDirectory("Settings");
            }

            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Indent = true;

            XmlWriter writer = XmlWriter.Create("Settings\\" + keyName.Name + ".xml");
            writer.WriteStartDocument();

            writer.WriteStartElement("Settings");
            writer.WriteAttributeString("Name", keyName.Name);
            writer.WriteElementString(keyName.Name, value);

            writer.WriteEndElement();
            writer.WriteEndDocument();

            writer.Flush();
            writer.Close();
        }

        /// <summary>
        /// Class to hold the key values to reference back to the registry.
        /// </summary>
        public class ApplicationKeys
        {
            /// <summary>
            /// Name of the registry key
            /// </summary>
            public string Name { get; set; }

            public bool Encrypted { get; set; }
        }
    }
}