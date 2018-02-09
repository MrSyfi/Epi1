using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace TestApp
{
    public static class PasswordGenerator
    {

        public static string Generate(int lenght)
        {
            // Certaines lettres ont été retirées car pouvant rendre le mot de passe difficile à prononcer ou pouvant être confondues (l et I)
            char[] voyellesArray = "aeuioAEUO".ToCharArray();
            char[] consonneArray = "bcdfghjkmnpqrstvzBCDFGHJKLMNPQRSTVZ".ToCharArray();
            char[] password = new char[lenght];
            

            Random rnd = new Random();

            // La premiere lettre sera elle une voyelle ?
            bool voyelle = rnd.Next(2) == 0;

            // Pour chaque caractere souhaité
            for (int i = 0; i < lenght; i++)
            {
                if (voyelle)
                {
                    int voyelNumber = rnd.Next(0, 9);
                    password[i] = voyellesArray[voyelNumber];
                }
                else
                {
                    int consonneNumber = rnd.Next(0, 33);
                    password[i] = consonneArray[consonneNumber];
                }
                voyelle = !voyelle;
            }
            string pwd = new string(password);
           // MailSender.SendPwdPerEmail(pwd,"sylvain.fissiaux@epicura.be","fissS1512");
            return pwd;
        }
  
       

    }
}
