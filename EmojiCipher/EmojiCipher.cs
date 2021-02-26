using System.Globalization;
using System.Linq;
using Common;

namespace EmojiCipher
{
    public class EmojiCipher : StringCipher
    {
        private readonly int _codePointStart;

        public EmojiCipher() : this(0x1F420)
        {
        }

        public EmojiCipher(int codePointStart)
        {
            _codePointStart = codePointStart;
        }

        public override string Encrypt(string plainText) =>
            string.Join(null, from c in plainText select char.ConvertFromUtf32(c + _codePointStart - 32));

        public override string Decrypt(string cipherText) =>
            string.Join(null,
                from i in StringInfo.ParseCombiningCharacters(cipherText)
                select char.ConvertFromUtf32(char.ConvertToUtf32(cipherText, i) - _codePointStart + 32)
            );
    }
}