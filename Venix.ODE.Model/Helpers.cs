using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;


namespace Venix.ODE.Model
{
    public class Helpers
    {
        /// <summary>
        /// VALIDAÇÃO DO E-MAIL
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public static bool Email(string email)
        {
            Regex rg = new Regex(@"^(?("")("".+?""@)|(([0-9a-zA-Z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-zA-Z])@))(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-zA-Z][-\w]*[0-9a-zA-Z]\.)+[a-zA-Z]{2,6}))$");
            if (rg.IsMatch(email)) { return true; } else { return false; }
        }

       


    }
}
