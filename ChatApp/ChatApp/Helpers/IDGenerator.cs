using System;
using System.Collections.Generic;
using System.Text;

namespace ChatApp.Helpers
{
    public class IDGenerator
    {
        private const String letters = "abcdefghijklmnopqrstuvwxyz";
        private static char[] alphanumeric = (letters + letters.ToUpper() + "0123456789").ToCharArray();
        public static String generateID()
        {
            StringBuilder generatedID = new StringBuilder();
            Random rand = new Random();

            for(int i = 0; i < 5; i++)
            {
                generatedID.Append(alphanumeric[rand.Next(alphanumeric.Length)]);
            }

            return generatedID.ToString();
        }

    }
}
