using System.Linq;
using Common;

namespace TranspositionCipher
{
    public class TranspositionCipherAscii : StringCipher
    {
        private readonly int[] _encryptionKey;
        private readonly int[] _decryptionKey;

        public TranspositionCipherAscii(string key)
        {
            _decryptionKey =
                (from x in key.Select((c, i) => new {i, c}) orderby x.c select x.i + 1).ToArray();
            _encryptionKey =
                (from x in _decryptionKey.Select((k, i) => new {i, k}) orderby x.k select x.i + 1).ToArray();
        }

        public override string Encrypt(string plaintext)
        {
            var c = plaintext.Length / _encryptionKey.Length + (plaintext.Length % _encryptionKey.Length == 0 ? 0 : 1);
            plaintext += new string((char) 0, c * _encryptionKey.Length - plaintext.Length);
            var ctm = new char[_encryptionKey.Length, c];
            for (int n = 0, i = 0; n < c; n++)
            for (var m = 0; m < _encryptionKey.Length; m++, i++)
                ctm[_encryptionKey[m] - 1, n] = plaintext[i];
            var ct = new char[plaintext.Length];
            for (int m = 0, i = 0; m < _encryptionKey.Length; m++)
            for (var n = 0; n < c; n++, i++)
                ct[i] = ctm[m, n];
            return new string(ct);
        }

        public override string Decrypt(string ciphertext)
        {
            var c = ciphertext.Length / _decryptionKey.Length;
            var ptm = new char[_decryptionKey.Length, c];
            for (int m = 0, i = 0; m < _decryptionKey.Length; m++)
            for (var n = 0; n < c; n++, i++)
                ptm[_decryptionKey[m] - 1, n] = ciphertext[i];
            var pt = new char[ciphertext.Length];
            for (int n = 0, i = 0; n < c; n++)
            for (var m = 0; m < _encryptionKey.Length; m++, i++)
                pt[i] = ptm[m, n];
            return new string(pt).Replace("\0", string.Empty);
        }
    }
}