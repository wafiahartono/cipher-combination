namespace Common
{
    public static class Utils
    {
        public static int Modulo(int a, int b) => a < 0 ? a + b : a % b;
    }
}