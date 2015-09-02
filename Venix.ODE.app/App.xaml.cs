using FirstFloor.ModernUI.Presentation;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using Venix.ODE.Model;

namespace Venix.ODE.app
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            if (SettingsThemplate.ReadKey("FontSize") == "grande")
            {
                AppearanceManager.Current.FontSize = FontSize.Large;
            }
            else if (SettingsThemplate.ReadKey("FontSize") == "pequeno")
            { 
                AppearanceManager.Current.FontSize = FontSize.Small; 
            }
            

            var SelectColor = (Color)ColorConverter.ConvertFromString(SettingsThemplate.ReadKey("Color"));
            AppearanceManager.Current.AccentColor = SelectColor;

            Uri xThemeSource = new Uri(SettingsThemplate.ReadKey("Theme"), UriKind.Relative);
            AppearanceManager.Current.ThemeSource = xThemeSource;           
        }    
    }
}
