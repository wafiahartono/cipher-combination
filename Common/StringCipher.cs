namespace Common
{
    public abstract class StringCipher
    {
        public abstract string Encrypt(string plaintext);
        public abstract string Decrypt(string ciphertext);
    }
}