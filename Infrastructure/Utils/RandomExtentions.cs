using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Utils
{
    public static class RandomExtentions
    {

        static Random rd = new Random();
        public static string CreateString(int stringLength)
        {
            const string allowedChars = "0123456789";
            char[] chars = new char[stringLength];

            for (int i = 0; i < stringLength; i++)
            {
                chars[i] = allowedChars[rd.Next(0, allowedChars.Length)];
            }

            return new string(chars);
        }
    }
}
