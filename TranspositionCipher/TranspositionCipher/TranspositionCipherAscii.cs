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
            var r = plaintext.Length / _encryptionKey.Length + (plaintext.Length % _encryptionKey.Length == 0 ? 0 : 1);
            plaintext += new string((char) 0, r * _encryptionKey.Length - plaintext.Length);
            var ctm = new char[r, _encryptionKey.Length];
            for (int m = 0, i = 0; m < r; m++)
            for (var n = 0; n < _encryptionKey.Length; n++, i++)
                ctm[m, _encryptionKey[n] - 1] = plaintext[i];
            var ct = new char[plaintext.Length];
            for (int n = 0, i = 0; n < _encryptionKey.Length; n++)
            for (var m = 0; m < r; m++, i++)
                ct[i] = ctm[m, n];
            return new string(ct);
        }

        public override string Decrypt(string ciphertext)
        {
            var r = ciphertext.Length / _decryptionKey.Length;
            var ptm = new char[r, _decryptionKey.Length];
            for (int n = 0, i = 0; n < _decryptionKey.Length; n++)
            for (var m = 0; m < r; m++, i++)
                ptm[m, _decryptionKey[n] - 1] = ciphertext[i];
            var pt = new char[ciphertext.Length];
            for (int m = 0, i = 0; m < r; m++)
            for (var n = 0; n < _encryptionKey.Length; n++, i++)
                pt[i] = ptm[m, n];
            return new string(pt).Replace("\0", string.Empty);
        }
    }
}