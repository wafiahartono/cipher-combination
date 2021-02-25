using System;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            const string message = @"Cipher Combination

Anggota kelompok:
1. Christopher Angelo Lomban (160419058)
2. Herliansyah Bagus Priambodo (160419082)
3. Pandu Sanika Satya Wada Nurrahman (160419096)
4. Wafi Azmi Hartono (160419098)
5. Starif Pahlaurelf Girsang (160419149)

Cipher yang digunakan: vigenere, additive, transposition
";
            Console.WriteLine(message);

            Console.Write("Plain text: ");
            var plainText = Console.ReadLine();
            Console.Write("Key: ");
            var key = Console.ReadLine();

            Console.WriteLine();

            var vigenereCipherText = VigenereCipher.Cipher.Encrypt(plainText, key);
            Console.WriteLine($"1. Cipher text of vigenere(plaintext): {vigenereCipherText}");

            var transpositionCipherText = TranspositionCipher.Cipher.Encipher(vigenereCipherText, key, '$');
            Console.WriteLine($"2. Cipher text of transposition(vigenere(plaintext)): {transpositionCipherText}");

            var transpositionPlainText = TranspositionCipher.Cipher.Decipher(transpositionCipherText, key);
            Console.WriteLine($"2^-1 . Plain text of transposition(vigenere(plaintext)): {transpositionPlainText}");

            var vigenerePlainText = VigenereCipher.Cipher.Decrypt(transpositionPlainText, key);
            Console.WriteLine($"1^-1 . Plain text of vigenere(plaintext): {vigenerePlainText}");
        }
    }
}