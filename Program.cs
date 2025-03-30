using System.Security.Cryptography;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp
{
    class Program
    {
        private static Dictionary<string, string> userDatabase = new Dictionary<string, string>();

        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("Menu:");
                Console.WriteLine("1. Registre");
                Console.WriteLine("2. Verificació de dades");
                Console.WriteLine("3. Encriptació i desencriptació amb RSA");
                Console.WriteLine("4. Sortir");
                Console.Write("Selecciona una opció: ");
                string option = Console.ReadLine();

                switch (option)
                {
                    case "1":
                        RegisterUser();
                        break;
                    case "2":
                        VerifyUser();
                        break;
                    case "3":
                        EncryptDecryptRSA();
                        break;
                    case "4":
                        return;
                    default:
                        Console.WriteLine("Opció no vàlida. Torna-ho a intentar.");
                        break;
                }
            }
        }

        private static void RegisterUser()
        {
            Console.Write("Introdueix el username: ");
            string username = Console.ReadLine();
            Console.Write("Introdueix la password: ");
            string password = Console.ReadLine();

            string combined = username + password;
            string hash = ComputeSha256Hash(combined);

            userDatabase[username] = hash;
            Console.WriteLine($"Registre complet. Hash: {hash}");
        }

        private static void VerifyUser()
        {
            Console.Write("Introdueix el username: ");
            string username = Console.ReadLine();
            Console.Write("Introdueix la password: ");
            string password = Console.ReadLine();

            string combined = username + password;
            string hash = ComputeSha256Hash(combined);

            if (userDatabase.ContainsKey(username) && userDatabase[username] == hash)
            {
                Console.WriteLine("Les dades són correctes.");
            }
            else
            {
                Console.WriteLine("Les dades no són correctes.");
            }
        }

        private static void EncryptDecryptRSA()
        {
            using (RSA rsa = RSA.Create())
            {
                Console.Write("Introdueix el text a encriptar: ");
                string text = Console.ReadLine();
                byte[] data = Encoding.UTF8.GetBytes(text);

                byte[] encryptedData = rsa.Encrypt(data, RSAEncryptionPadding.Pkcs1);
                string encryptedText = Convert.ToBase64String(encryptedData);
                Console.WriteLine($"Text encriptat: {encryptedText}");

                byte[] decryptedData = rsa.Decrypt(encryptedData, RSAEncryptionPadding.Pkcs1);
                string decryptedText = Encoding.UTF8.GetString(decryptedData);
                Console.WriteLine($"Text desencriptat: {decryptedText}");
            }
        }

        private static string ComputeSha256Hash(string rawData)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(rawData));
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2")); // Fet 
                }
                return builder.ToString();
            }
        }
    }
}