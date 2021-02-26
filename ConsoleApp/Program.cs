using System;

namespace ConsoleApp
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            const string message = @"Cipher Combination

Anggota kelompok:
1. Christopher Angelo Lomban (160419058)
2. Herliansyah Bagus Priambodo (160419082)
3. Pandu Sanika Satya Wada Nurrahman (160419096)
4. Wafi Azmi Hartono (160419098)
5. David Pratama (160419103)
6. Starif Pahlaurelf Girsang (160419149)

Cipher yang digunakan: vigenere > additive > transposition
";
            Console.WriteLine(message);

            Console.Write("plain text: ");
            var plainText = Console.ReadLine();
            Console.Write("vigenere cipher Key: ");
            var key0 = Console.ReadLine();
            Console.Write("additive cipher Key: ");
            var key1 = int.Parse(Console.ReadLine() ?? "0");
            Console.Write("transposition cipher Key: ");
            var key2 = Console.ReadLine();
            Console.WriteLine();

            var cipherText0 = VigenereCipher.Cipher.Encrypt(plainText, key0);
            Console.WriteLine($"stage 1 cipher text: {cipherText0}");
            var cipherText1 = AdditiveCipher.Cipher.Encrypt(cipherText0, key1);
            Console.WriteLine($"stage 2 cipher text: {cipherText1}");
            var cipherText2 = TranspositionCipher.Cipher.Encipher(cipherText1, key2, '$');
            Console.WriteLine($"stage 3 cipher text: {cipherText2}");
            Console.WriteLine();

            var plainText2 = TranspositionCipher.Cipher.Decipher(cipherText2, key2);
            Console.WriteLine($"stage 3^-1 plain text: {plainText2}");
            var plainText1 = AdditiveCipher.Cipher.Decrypt(plainText2, key1);
            Console.WriteLine($"stage 2^-1 plain text: {plainText2}");
            var plainText0 = VigenereCipher.Cipher.Decrypt(plainText1, key0);
            Console.WriteLine($"stage 1^-1 plain text: {plainText0}");
        }
    }
}