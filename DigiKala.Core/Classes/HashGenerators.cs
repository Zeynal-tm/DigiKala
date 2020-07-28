using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;

namespace DigiKala.Core.Classes
{
    public class HashGenerators
    {
        public static string MD5Encoding(string password)
        {
            Byte[] mainBytes;
            Byte[] encodeBytes;

            MD5 md5;

            md5 = new MD5CryptoServiceProvider();

            mainBytes = ASCIIEncoding.Default.GetBytes(password);
            encodeBytes = md5.ComputeHash(mainBytes);

            return BitConverter.ToString(encodeBytes);
        }

    }
}
