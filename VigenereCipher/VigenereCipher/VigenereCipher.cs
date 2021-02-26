using Common;

namespace VigenereCipher
{
    public class VigenereCipher : StringCipher
    {
        private readonly string _key;

        public VigenereCipher(string key)
        {
            _key = key;
        }

        private static int Mod(int a, int b)
        {
            return (a % b + b) % b;
        }

        private string Process(string input, bool encipher)
        {
            for (int i = 0; i < _key.Length; ++i)
                if (!char.IsLetter(_key[i]))
                    return null; // Error

            string output = string.Empty;
            int nonAlphaCharCount = 0;

            for (int i = 0; i < input.Length; ++i)
            {
                if (char.IsLetter(input[i]))
                {
                    bool cIsUpper = char.IsUpper(input[i]);
                    char offset = cIsUpper ? 'A' : 'a';
                    int keyIndex = (i - nonAlphaCharCount) % _key.Length;
                    int k = (cIsUpper ? char.ToUpper(_key[keyIndex]) : char.ToLower(_key[keyIndex])) - offset;
                    k = encipher ? k : -k;
                    char ch = (char) ((Mod(((input[i] + k) - offset), 26)) + offset);
                    output += ch;
                }
                else
                {
                    output += input[i];
                    ++nonAlphaCharCount;
                }
            }

            return output;
        }

        public override string Encrypt(string plainText) => Process(plainText, true);
        public override string Decrypt(string cipherText) => Process(cipherText, false);
    }
}