using Common;

namespace AdditiveCipher
{
    public class AdditiveCipher : StringCipher
    {
        private readonly int _key;

        public AdditiveCipher(int key)
        {
            _key = key;
        }

        public override string Encrypt(string plaintext)
        {
            char[] ciphertext = new char[plaintext.Length];
            for (int i = 0; i < plaintext.Length; i++)
            {
                char pc = plaintext[i];
                char cc = (char) (Utils.Modulo(pc - 32 + _key, 95) + 32);
                ciphertext[i] = cc;
                // Console.WriteLine($"{(int) pc:D3} {(int) pc:X3} {pc} > {(int) cc:D3} {(int) cc:X3} {cc}");
            }

            return new string(ciphertext);
        }

        public override string Decrypt(string ciphertext)
        {
            char[] plaintext = new char[ciphertext.Length];
            for (int i = 0; i < ciphertext.Length; i++)
            {
                char cc = ciphertext[i];
                char pc = (char) (Utils.Modulo(cc - 32 - _key, 95) + 32);
                plaintext[i] = pc;
                // Console.WriteLine($"{(int) cc:D3} {(int) cc:X3} {cc} > {(int) pc:D3} {(int) pc:X3} {pc}");
            }

            return new string(plaintext);
        }
    }
}