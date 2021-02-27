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
available menu:
m > run main program
t > run test
q > quit
");
                Console.ForegroundColor = ConsoleColor.Magenta;
                choice = Console.ReadKey();
                Console.ResetColor();
                Console.Write("\n\n");
                switch (choice.Key)
                {
                    case ConsoleKey.M:
                        RunMainProgram();
                        break;
                    case ConsoleKey.T:
                        RunTest();
                        break;
                    case ConsoleKey.Q:
                        break;
                    default:
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine($"invalid menu: {choice.KeyChar}");
                        Console.ResetColor();
                        break;
                }
            } while (choice.Key != ConsoleKey.Q);
        }

        private static void RunMainProgram()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("[running main program...]\n");
            Console.ResetColor();

            Console.Write("plaintext: ");
            Console.ForegroundColor = ConsoleColor.Magenta;
            var plaintext = Console.ReadLine();
            Console.ResetColor();

            Console.Write("string cipher Key: ");
            Console.ForegroundColor = ConsoleColor.Magenta;
            var key0 = Console.ReadLine();
            Console.ResetColor();

            Console.Write("int cipher Key: ");
            Console.ForegroundColor = ConsoleColor.Magenta;
            var key1 = int.Parse(Console.ReadLine() ?? "0");
            Console.ResetColor();

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\n[encryption stage]\n");
            Console.ResetColor();

            var ciphers = new StringCipher[]
            {
                new VigenereCipher.VigenereCipherAscii(key0),
                new AdditiveCipher.AdditiveCipher(key1),
                new TranspositionCipher.TranspositionCipherAscii(key0)
            };

            var textBefore = plaintext;
            for (var i = 0; i < ciphers.Length; i++)
            {
                textBefore = ciphers[i].Encrypt(textBefore);
                Console.WriteLine($"stage {i + 1} {ciphers[i].GetType().Name} ciphertext ({textBefore.Length}):");
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine($"{textBefore}\n");
                Console.ResetColor();
            }

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("[decryption stage]\n");
            Console.ResetColor();

            for (var i = ciphers.Length - 1; i > -1; i--)
            {
                textBefore = ciphers[i].Decrypt(textBefore);
                Console.WriteLine($"stage {i + 1}^-1 {ciphers[i].GetType().Name} plaintext ({textBefore.Length}):");
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine($"{textBefore}\n");
                Console.ResetColor();
            }

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("[main program finished.]");
            Console.ResetColor();
        }

        private static void RunTest()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("[running test...]\n");
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
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine(plaintext);
            Console.ResetColor();

            Console.Write("string key: ");
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine(keyString);
            Console.ResetColor();

            Console.Write("int key: ");
            Console.ForegroundColor = ConsoleColor.Magenta;
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

                Console.WriteLine($"encrypted ({ciphertext.Length}):");
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine($"{ciphertext}\n");
                Console.ResetColor();

                Console.WriteLine($"decrypted ({ctplaintext.Length}):");
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine($"{ctplaintext}\n");
                Console.ResetColor();

                Console.WriteLine("result:");
                Console.ForegroundColor = plaintext.Equals(ctplaintext) ? ConsoleColor.Green : ConsoleColor.Red;
                Console.WriteLine($"{(plaintext.Equals(ctplaintext) ? "equals" : "not equal")}\n");
                Console.ResetColor();
            }

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("[test finished.]");
            Console.ResetColor();
        }
    }
}