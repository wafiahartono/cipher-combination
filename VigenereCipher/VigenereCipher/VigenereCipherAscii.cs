using System.Linq;
using Common;

namespace VigenereCipher
{
    public class VigenereCipherAscii : StringCipher
    {
        private readonly string _key;

        public VigenereCipherAscii(string key)
        {
            _key = key;
        }

        public override string Encrypt(string plaintext) => string.Join(null,
            from x in plaintext.Select((c, i) => new {i, c})
            select (char) (Utils.Modulo(x.c - 32 + _key[x.i % (_key.Length - 1)], 95) + 32));

        public override string Decrypt(string ciphertext) => string.Join(null,
            from x in ciphertext.Select((c, i) => new {i, c})
            select (char) (Utils.Modulo(x.c - 32 - _key[x.i % (_key.Length - 1)], 95) + 32));
    }
}