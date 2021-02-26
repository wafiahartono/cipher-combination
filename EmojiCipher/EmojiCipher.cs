using System.Globalization;
using System.Linq;

namespace EmojiCipher
{
    public static class Cipher
    {
        private const int Ascii0 = 32;
        private const int Unicode0 = 0x1F420;

        static string Encrypt(string pt)
        {
            return string.Join(null, from c in pt select char.ConvertFromUtf32(c + Unicode0 - Ascii0));
        }

        static string Decrypt(string ct)
        {
            return string.Join(null,
                from i in StringInfo.ParseCombiningCharacters(ct)
                select char.ConvertFromUtf32(char.ConvertToUtf32(ct, i) - Unicode0 + Ascii0)
            );
        }
    }
}