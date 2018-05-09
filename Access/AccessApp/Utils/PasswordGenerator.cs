using System;

namespace AccessApp
{
    public static class PasswordGenerator
    {

        public static string Generate(int lenght = 8)
        {
            // Certaines lettres ont été retirées car pouvant rendre le mot de passe 
            //difficile à prononcer ou pouvant être confondues (l et I)
            char[] voyellesArray = "aeuio".ToCharArray();
            char[] consonneArray = "bcdfghjkmnpqrstvz".ToCharArray();
            char[] password = new char[lenght];
            Random rnd = new Random();

            //La premiere lettre sera une voyelle ?
            bool voyelle = rnd.Next(2) == 0;

            //Pour chaque caractere souhaité
            for (int i = 0; i < lenght; i++)
            {
                if (voyelle)//Si voyelle
                {
                    int voyelNumber = rnd.Next(0, 5);//Génère un chiffre aléatoire
                    //Ajoute la lettre au mot de passe en fonction du chiffre généré
                    password[i] = voyellesArray[voyelNumber]; 
                }
                else//Si consonne
                {
                    int consonneNumber = rnd.Next(0, 17);//Génère un chiffre aléatoire
                    //Ajoute la lettre au mot de passe en fonction du chiffre généré
                    password[i] = consonneArray[consonneNumber];
                }
                voyelle = !voyelle;
            }     
            return new string(password);
        }
    }
}
