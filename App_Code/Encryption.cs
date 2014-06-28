using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

/// <summary>
/// Summary description for Encryption
/// </summary>
public class Encryption
{

    private static byte[] _salt = Encoding.ASCII.GetBytes("o6806642kbM7c5");
    private static string sharedSecret = "klasdfasweqw1234";

	public Encryption()
	{
		
	}

    public static string SifreleAES(string plainText)
    {
        if (string.IsNullOrEmpty(plainText))
            throw new ArgumentNullException("plainText");
        if (string.IsNullOrEmpty(sharedSecret))
            throw new ArgumentNullException("sharedSecret");

        string outStr = null;
        RijndaelManaged aesAlg = null;

        try
        {

            Rfc2898DeriveBytes key = new Rfc2898DeriveBytes(sharedSecret, _salt);
            aesAlg = new RijndaelManaged();
            aesAlg.Key = key.GetBytes(aesAlg.KeySize / 8);


            ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);


            using (MemoryStream msEncrypt = new MemoryStream())
            {

                msEncrypt.Write(BitConverter.GetBytes(aesAlg.IV.Length), 0, sizeof(int));
                msEncrypt.Write(aesAlg.IV, 0, aesAlg.IV.Length);
                using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                {
                    using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                    {
                        swEncrypt.Write(plainText);
                    }
                }
                outStr = Convert.ToBase64String(msEncrypt.ToArray());
            }
        }
        finally
        {

            if (aesAlg != null)
                aesAlg.Clear();
        }


        return outStr;
    }
    public static string SifreyiCozAES(string cipherText)
    {
        if (string.IsNullOrEmpty(cipherText))
            throw new ArgumentNullException("cipherText");
        if (string.IsNullOrEmpty(sharedSecret))
            throw new ArgumentNullException("sharedSecret");


        RijndaelManaged aesAlg = null;


        string plaintext = null;

        try
        {

            Rfc2898DeriveBytes key = new Rfc2898DeriveBytes(sharedSecret, _salt);


            byte[] bytes = Convert.FromBase64String(cipherText);
            using (MemoryStream msDecrypt = new MemoryStream(bytes))
            {

                aesAlg = new RijndaelManaged();
                aesAlg.Key = key.GetBytes(aesAlg.KeySize / 8);
                aesAlg.IV = ReadByteArray(msDecrypt);

                ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);
                using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                {
                    using (StreamReader srDecrypt = new StreamReader(csDecrypt))

                        //uzmanim.net
                        plaintext = srDecrypt.ReadToEnd();
                }
            }
        }
        finally
        {

            if (aesAlg != null)
                aesAlg.Clear();
        }

        return plaintext;
    }
    private static byte[] ReadByteArray(Stream s)
    {
        byte[] rawLength = new byte[sizeof(int)];
        if (s.Read(rawLength, 0, rawLength.Length) != rawLength.Length)
        {
            throw new SystemException("Stream did not contain properly formatted byte array");
        }

        byte[] buffer = new byte[BitConverter.ToInt32(rawLength, 0)];
        if (s.Read(buffer, 0, buffer.Length) != buffer.Length)
        {
            throw new SystemException("Did not read byte array properly");
        }

        return buffer;
    }
}