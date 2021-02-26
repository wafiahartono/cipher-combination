using System;

namespace AdditiveCipher
{
    public static class Cipher
    {
        public static int Modulo(int a, int b) => a < 0 ? a + b : a % b;

        public static string Encrypt(string plainText, int key)
        {
            char[] cipherText = new char[plainText.Length];
            for (int i = 0; i < plainText.Length; i++)
            {
                char pc = plainText[i];
                char cc = (char) (Modulo(pc - 32 + key, 95) + 32);
                cipherText[i] = cc;
                Console.WriteLine($"{(int) pc:D3} {(int) pc:X3} {pc} > {(int) cc:D3} {(int) cc:X3} {cc}");
            }

            return new string(cipherText);
        }

        public static string Decrypt(string cipherText, int key)
        {
            char[] plainText = new char[cipherText.Length];
            for (int i = 0; i < cipherText.Length; i++)
            {
                char cc = cipherText[i];
                char pc = (char) (Modulo(cc - 32 - key, 95) + 32);
                plainText[i] = pc;
                Console.WriteLine($"{(int) cc:D3} {(int) cc:X3} {cc} > {(int) pc:D3} {(int) pc:X3} {pc}");
            }

            return new string(plainText);
        }
    }
}