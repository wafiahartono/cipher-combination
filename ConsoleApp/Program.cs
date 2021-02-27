using System;
using System.Linq;
using Common;

namespace ConsoleApp
{
    internal static class Program
    {
        private static void Main()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(@"Cipher Combination
Information Security and Assurance

Anggota kelompok:
1. Christopher Angelo Lomban (160419058)
2. Herliansyah Bagus Priambodo (160419082)
3. Pandu Sanika Satya Wada Nurrahman (160419096)
4. Wafi Azmi Hartono (160419098)
5. David Pratama (160419103)
6. Starif Pahlaurelf Girsang (160419149)");
            Console.ResetColor();
            ConsoleKeyInfo choice;
            do
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine(@"
Available menu:
M > Run main program
T > Run test
Q > Quit
");
                Console.ForegroundColor = ConsoleColor.Magenta;
                choice = Console.ReadKey();
                Console.ResetColor();
                Console.WriteLine();
                switch (choice.Key)
                {
                    case ConsoleKey.M:
                        Console.SetCursorPosition(0, Console.GetCursorPosition().Top - 1);
                        RunMainProgram();
                        break;
                    case ConsoleKey.T:
                        Console.SetCursorPosition(0, Console.GetCursorPosition().Top - 1);
                        RunTest();
                        break;
                    case ConsoleKey.Q:
                        break;
                    default:
                        Console.SetCursorPosition(0, Console.GetCursorPosition().Top - 1);
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine($"Invalid menu: {choice.KeyChar}");
                        Console.ResetColor();
                        break;
                }
            } while (choice.Key != ConsoleKey.Q);
        }

        private static void RunMainProgram()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("[Running main program...]\n");
            Console.ResetColor();

            Console.Write("plaintext: ");
            Console.ForegroundColor = ConsoleColor.Magenta;
            var plaintext = Console.ReadLine();
            Console.ResetColor();

            Console.Write("vigenere cipher Key: ");
            Console.ForegroundColor = ConsoleColor.Magenta;
            var key0 = Console.ReadLine();
            Console.ResetColor();

            Console.Write("additive cipher Key: ");
            Console.ForegroundColor = ConsoleColor.Magenta;
            var key1 = int.Parse(Console.ReadLine() ?? "0");
            Console.ResetColor();

            Console.Write("transposition cipher Key: ");
            Console.ForegroundColor = ConsoleColor.Magenta;
            var key2 = Console.ReadLine();
            Console.ResetColor();

            Console.WriteLine();

            var cipher0 = new VigenereCipher.VigenereCipherAscii(key0);
            var cipher1 = new AdditiveCipher.AdditiveCipher(key1);
            var cipher2 = new TranspositionCipher.TranspositionCipherAscii(key2);

            var ciphertext0 = cipher0.Encrypt(plaintext);
            Console.Write("stage 1 ciphertext: ");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(ciphertext0);
            Console.ResetColor();

            var ciphertext1 = cipher1.Encrypt(ciphertext0);
            Console.Write("stage 2 ciphertext: ");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(ciphertext1);
            Console.ResetColor();

            var ciphertext2 = cipher2.Encrypt(ciphertext1);
            Console.Write("stage 3 ciphertext: ");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(ciphertext2);
            Console.ResetColor();

            Console.WriteLine();

            var plaintext2 = cipher2.Decrypt(ciphertext2);
            Console.Write("stage 3^-1 plaintext: ");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(plaintext2);
            Console.ResetColor();

            var plaintext1 = cipher1.Decrypt(plaintext2);
            Console.Write("stage 2^-1 plaintext: ");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(plaintext1);
            Console.ResetColor();

            var plaintext0 = cipher0.Decrypt(plaintext1);
            Console.Write($"stage 1^-1 plaintext: ");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(plaintext0);
            Console.ResetColor();

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\n[Main program finished.]");
            Console.ResetColor();
        }

        private static void RunTest()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("[Running test...]\n");
            Console.ResetColor();

            const string keyString = "rCzlY0Fs92j7omaPmzG6";
            const int keyInt = 85965636;
            var ciphers = new StringCipher[]
            {
                new AdditiveCipher.AdditiveCipher(keyInt),
                new VigenereCipher.VigenereCipherAscii(keyString),
                new TranspositionCipher.TranspositionCipherAscii(keyString)
            };
            var plaintext = string.Join(null, from i in Enumerable.Range(32, 95) select (char) i);

            Console.Write("plaintext: ");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(plaintext);
            Console.ResetColor();

            Console.Write("string key: ");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(keyString);
            Console.ResetColor();

            Console.Write("int key: ");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(keyInt.ToString());
            Console.ResetColor();

            foreach (var cipher in ciphers)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"\n[{cipher.GetType().Name}]");

                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("encrypting...");
                Console.ResetColor();
                var ciphertext = cipher.Encrypt(plaintext);

                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("decrypting...");
                Console.ResetColor();
                var ctplaintext = cipher.Decrypt(ciphertext);

                Console.WriteLine();

                Console.Write("encrypted: ");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine(ciphertext);
                Console.ResetColor();

                Console.Write("decrypted: ");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine(ctplaintext);
                Console.ResetColor();

                Console.Write("result: ");
                Console.ForegroundColor = plaintext.Equals(ctplaintext) ? ConsoleColor.Green : ConsoleColor.Red;
                Console.WriteLine(plaintext.Equals(ctplaintext) ? "equals" : "not equal");
                Console.ResetColor();
            }

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\n[Test finished.]");
            Console.ResetColor();
        }
    }
}