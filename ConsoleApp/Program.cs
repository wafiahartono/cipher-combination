using System;
using System.Linq;
using Common;

namespace ConsoleApp
{
    internal class Program
    {
        private bool _printReadNull = true;
        private bool _verbose;

        private static void Main()
        {
            new Program().Run();
        }

        private void Run()
        {
            PrintWithColor(@"
   ___ _      _               ___           _    _           _   _          
  / __(_)_ __| |_  ___ _ _   / __|___ _ __ | |__(_)_ _  __ _| |_(_)___ _ _  
 | (__| | '_ \ ' \/ -_) '_| | (__/ _ \ '  \| '_ \ | ' \/ _` |  _| / _ \ ' \ 
  \___|_| .__/_||_\___|_|    \___\___/_|_|_|_.__/_|_||_\__,_|\__|_\___/_||_|
        |_|                                                                 

Information Security and Assurance

Anggota kelompok:
1. Christopher Angelo Lomban (160419058)
2. Herliansyah Bagus Priambodo (160419082)
3. Pandu Sanika Satya Wada Nurrahman (160419096)
4. Wafi Azmi Hartono (160419098)
5. David Pratama (160419103)
6. Starif Pahlaurelf Girsang (160419149)",
                ConsoleColor.Green
            );

            ConsoleKeyInfo choice;
            var printMenu = true;
            do
            {
                if (printMenu)
                    PrintWithColor(@"
available menu:
i > run individual test
t > run test
n > print/read null character
v > verbose
q > quit",
                        ConsoleColor.Blue
                    );
                choice = ReadKeyInput("  > ");
                printMenu = true;
                Console.Write(" : ");
                switch (choice.Key)
                {
                    case ConsoleKey.I:
                        RunIndividualTest();
                        break;
                    case ConsoleKey.T:
                        RunTest();
                        break;
                    case ConsoleKey.N:
                        _printReadNull = !_printReadNull;
                        printMenu = false;
                        PrintWithColor($"print/read null character {_printReadNull.ToString().ToLower()}",
                            ConsoleColor.Yellow);
                        break;
                    case ConsoleKey.V:
                        _verbose = !_verbose;
                        printMenu = false;
                        PrintWithColor($"verbose {_verbose.ToString().ToLower()}", ConsoleColor.Yellow);
                        break;
                    case ConsoleKey.Q:
                        PrintWithColor("quitting...", ConsoleColor.Yellow);
                        break;
                    default:
                        printMenu = false;
                        PrintWithColor($"invalid menu: {choice.KeyChar}", ConsoleColor.Red);
                        break;
                }
            } while (choice.Key != ConsoleKey.Q);
        }

        private static void PrintWithColor(string message, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(message);
            Console.ResetColor();
        }

        private static void PrintKeyValuePair(string key, string value, ConsoleColor color)
        {
            Console.Write($"{key}: ");
            Console.ForegroundColor = color;
            Console.WriteLine(value);
            Console.ResetColor();
        }

        private static string ReadStringInput(string message)
        {
            Console.Write(message);
            Console.ForegroundColor = ConsoleColor.Magenta;
            var input = Console.ReadLine();
            Console.ResetColor();
            return input;
        }

        private static ConsoleKeyInfo ReadKeyInput(string message)
        {
            Console.Write(message);
            Console.ForegroundColor = ConsoleColor.Magenta;
            var key = Console.ReadKey();
            Console.ResetColor();
            return key;
        }

        private void RunIndividualTest()
        {
            PrintWithColor("running individual test...", ConsoleColor.Yellow);

            ConsoleKeyInfo choice;
            var invalidKey = false;
            do
            {
                if (!invalidKey)
                    PrintWithColor(@"
  individual test menu:
  e > encrypt a plaintext
  d > decrypt a ciphertext
  q > quit",
                        ConsoleColor.Blue
                    );
                choice = ReadKeyInput("    > ");
                invalidKey = false;
                Console.Write(" : ");
                switch (choice.Key)
                {
                    case ConsoleKey.E:
                        RunEncryptionTest();
                        break;
                    case ConsoleKey.D:
                        RunDecryptionTest();
                        break;
                    case ConsoleKey.Q:
                        PrintWithColor("quitting...", ConsoleColor.Yellow);
                        break;
                    default:
                        invalidKey = true;
                        PrintWithColor($"invalid menu: {choice.KeyChar}", ConsoleColor.Red);
                        break;
                }
            } while (choice.Key != ConsoleKey.Q);

            PrintWithColor("\n> individual test finished", ConsoleColor.Red);
        }

        private void RunEncryptionTest()
        {
            PrintWithColor("running encryption test...\n", ConsoleColor.Yellow);
            var plaintext = ReadStringInput("plaintext: ");
            var key = ReadStringInput("key: ");
            var cipher = GetCpiher(key);
            if (cipher == null) return;
            var ciphertext = cipher.Encrypt(plaintext);
            if (_verbose)
            {
                Utils.PrintStringDiff(plaintext, ciphertext);
                Console.WriteLine();
            }

            PrintKeyValuePair($"ciphertext ({ciphertext.Length})",
                '\n' + (_printReadNull ? Utils.PrintNullCharacter(ciphertext) : ciphertext), ConsoleColor.Cyan);
        }

        private void RunDecryptionTest()
        {
            PrintWithColor("running decryption test...\n", ConsoleColor.Yellow);
            var ciphertext = ReadStringInput("ciphertext: ");
            if (_printReadNull) ciphertext = Utils.ReadNullCharacter(ciphertext);
            var key = ReadStringInput("key: ");
            var cipher = GetCpiher(key);
            if (cipher == null) return;
            var plaintext = cipher.Decrypt(ciphertext);
            if (_verbose)
            {
                Utils.PrintStringDiff(ciphertext, plaintext);
                Console.WriteLine();
            }

            PrintKeyValuePair($"plaintext ({plaintext.Length})",
                '\n' + (_printReadNull ? Utils.PrintNullCharacter(plaintext) : plaintext), ConsoleColor.Cyan);
        }

        private static StringCipher GetCpiher(string key)
        {
            ConsoleKeyInfo choice;
            var invalidKey = false;
            do
            {
                if (!invalidKey)
                    PrintWithColor(@"
    choose a cipher:
    1 > additive cipher
    2 > additive ascii cipher
    3 > emoji cipher
    4 > transposition cipher
    5 > transposition ascii cipher
    6 > vigenere cipher
    7 > vigenere ascii cipher
    c > cancel",
                        ConsoleColor.Blue
                    );
                choice = ReadKeyInput("      > ");
                invalidKey = false;
                Console.Write(" : ");
                switch (choice.Key)
                {
                    case ConsoleKey.D1:
                        PrintWithColor("additive cipher\n", ConsoleColor.Yellow);
                        return new AdditiveCipher.AdditiveCipher(int.Parse(key));
                    case ConsoleKey.D2:
                        PrintWithColor("additive ascii cipher\n", ConsoleColor.Yellow);
                        return new AdditiveCipher.AdditiveCipherAscii(int.Parse(key));
                    case ConsoleKey.D3:
                        PrintWithColor("emoji cipher\n", ConsoleColor.Yellow);
                        return new EmojiCipher.EmojiCipher();
                    case ConsoleKey.D4:
                        PrintWithColor("transposition cipher\n", ConsoleColor.Yellow);
                        return new TranspositionCipher.TranspositionCipher(key, '_');
                    case ConsoleKey.D5:
                        PrintWithColor("transposition ascii cipher\n", ConsoleColor.Yellow);
                        return new TranspositionCipher.TranspositionCipherAscii(key);
                    case ConsoleKey.D6:
                        PrintWithColor("vigenere cipher\n", ConsoleColor.Yellow);
                        return new VigenereCipher.VigenereCipher(key);
                    case ConsoleKey.D7:
                        PrintWithColor("vigenere ascii cipher\n", ConsoleColor.Yellow);
                        return new VigenereCipher.VigenereCipherAscii(key);
                    case ConsoleKey.C:
                        PrintWithColor("cancelling...", ConsoleColor.Yellow);
                        break;
                    default:
                        invalidKey = true;
                        PrintWithColor($"invalid menu: {choice.KeyChar}", ConsoleColor.Red);
                        break;
                }
            } while (choice.Key != ConsoleKey.C);

            return null;
        }

        private void RunTest()
        {
            PrintWithColor("running test...\n", ConsoleColor.Yellow);
            var plaintext = string.Join(null, from i in Enumerable.Range(32, 95) select (char) i);
            const string stringKey = "rCzlY0Fs92j7omaPmzG6";
            const int intKey = 85965636;
            var ciphers = new StringCipher[]
            {
                new AdditiveCipher.AdditiveCipherAscii(intKey),
                new VigenereCipher.VigenereCipherAscii(stringKey),
                new TranspositionCipher.TranspositionCipherAscii(stringKey),
                new EmojiCipher.EmojiCipher()
            };
            PrintKeyValuePair("plaintext", plaintext, ConsoleColor.Magenta);
            PrintKeyValuePair("string key", stringKey, ConsoleColor.Magenta);
            PrintKeyValuePair("int key", intKey.ToString(), ConsoleColor.Magenta);
            foreach (var cipher in ciphers)
            {
                PrintWithColor($"\n | {cipher.GetType().FullName}", ConsoleColor.Green);
                var ciphertext = cipher.Encrypt(plaintext);
                if (_verbose)
                {
                    PrintWithColor("\nencrypting...\n", ConsoleColor.Yellow);
                    Utils.PrintStringDiff(plaintext, ciphertext);
                }

                var ctplaintext = cipher.Decrypt(ciphertext);
                if (_verbose)
                {
                    PrintWithColor("\ndecrypting...\n", ConsoleColor.Yellow);
                    Utils.PrintStringDiff(ciphertext, ctplaintext);
                }

                PrintKeyValuePair($"\n-/ encrypted ({ciphertext.Length})",
                    '\n' + (_printReadNull ? Utils.PrintNullCharacter(ciphertext) : ciphertext), ConsoleColor.Cyan);
                PrintKeyValuePair($"\n-/ decrypted ({ctplaintext.Length})",
                    '\n' + (_printReadNull ? Utils.PrintNullCharacter(ctplaintext) : ctplaintext), ConsoleColor.Cyan);
                PrintKeyValuePair(
                    "\nresult", plaintext.Equals(ctplaintext) ? "equals" : "not equal",
                    plaintext.Equals(ctplaintext) ? ConsoleColor.Green : ConsoleColor.Red
                );
            }

            PrintWithColor("\n> test finished", ConsoleColor.Red);
        }
    }
}