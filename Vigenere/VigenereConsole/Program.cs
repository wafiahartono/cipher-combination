using System;

namespace Vigenere
{
    class Program
    {
        static void Main(string[] args)
        {
			Console.Write("Enter text to encrypt : ");
			string text = Console.ReadLine();

			Console.Write("Enter key : ");
			string key = Console.ReadLine();

			string cipherText = VigenereCipher.Encrypt(text, key);
			string plainText = VigenereCipher.Decrypt(cipherText, key);

			Console.WriteLine("==========RESULT==========");
			Console.WriteLine("Encrypt (Cipher text) : " + cipherText);
			Console.WriteLine("Decrypt (Plain text) : " + plainText);
		}
    }
}
