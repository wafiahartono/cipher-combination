using System;
using Common;

namespace ConsoleApp
{
    internal static class Program
    {
        private static void Main()
        {
            Test();
            // return;

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

            Console.Write("plaintext: ");
            var plaintext = Console.ReadLine();
            Console.Write("vigenere cipher Key: ");
            var key0 = Console.ReadLine();
            Console.Write("additive cipher Key: ");
            var key1 = int.Parse(Console.ReadLine() ?? "0");
            Console.Write("transposition cipher Key: ");
            var key2 = Console.ReadLine();
            Console.WriteLine();

            var cipher0 = new VigenereCipher.VigenereCipher(key0);
            var cipher1 = new AdditiveCipher.AdditiveCipher(key1);
            var cipher2 = new TranspositionCipher.TranspositionCipher(key2, '$');

            var ciphertext0 = cipher0.Encrypt(plaintext);
            Console.WriteLine($"stage 1 ciphertext: {ciphertext0}");
            var ciphertext1 = cipher1.Encrypt(ciphertext0);
            Console.WriteLine($"stage 2 ciphertext: {ciphertext1}");
            var ciphertext2 = cipher2.Encrypt(ciphertext1);
            Console.WriteLine($"stage 3 ciphertext: {ciphertext2}");

            Console.WriteLine();

            var plaintext2 = cipher2.Decrypt(ciphertext2);
            Console.WriteLine($"stage 3^-1 plaintext: {plaintext2}");
            var plaintext1 = cipher1.Decrypt(plaintext2);
            Console.WriteLine($"stage 2^-1 plaintext: {plaintext1}");
            var plaintext0 = cipher0.Decrypt(plaintext1);
            Console.WriteLine($"stage 1^-1 plaintext: {plaintext0}");
        }

        private static void Test()
        {
            var ciphers = new StringCipher[]
            {
                new AdditiveCipher.AdditiveCipher(10),
                new TranspositionCipher.TranspositionCipher("mykey", '$'),
                new VigenereCipher.VigenereCipher("mykey")
            };
            var plaintext = "informationandsecurityassurance";
            Console.WriteLine($"plaintext: {plaintext}");
            foreach (var cipher in ciphers)
            {
                var ciphertext = cipher.Encrypt(plaintext);
                var ctplaintext = cipher.Decrypt(ciphertext);
                Console.WriteLine($"[{cipher.GetType().Name}]");
                Console.WriteLine($"encrypt : {ciphertext}");
                Console.WriteLine($"decrypt : {ctplaintext}");
                Console.WriteLine($"result  : {(plaintext.Equals(ctplaintext) ? "pass" : "fail")}");
                Console.WriteLine();
            }
        }
    }
}