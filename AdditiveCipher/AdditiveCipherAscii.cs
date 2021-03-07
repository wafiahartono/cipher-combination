using System.Linq;
using Common;

namespace AdditiveCipher
{
    public class AdditiveCipherAscii : StringCipher
    {
        private readonly int _key;

        public AdditiveCipherAscii(int key)
        {
            _key = key;
        }

        public override string Encrypt(string plaintext) => string.Join(null,
            from c in plaintext select (char) (Utils.Modulo(c - 32 + _key, 95) + 32));

        public override string Decrypt(string ciphertext) => string.Join(null,
            from c in ciphertext select (char) (Utils.Modulo(c - 32 - _key, 95) + 32));
    }
}