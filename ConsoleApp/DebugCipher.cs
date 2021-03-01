using System;
using System.Globalization;
using System.Linq;
using System.Text;

namespace ConsoleApp
{
    public static class DebugCipher
    {
        public static void PrintStringDiff(string a, string b)
        {
            // var aIndexes = StringInfo.ParseCombiningCharacters(a);
            // var bIndexes = StringInfo.ParseCombiningCharacters(b);
            for (int ai = 0, bi = 0;
                ai < a.Length && bi < b.Length;
                ai += char.IsSurrogatePair(a, ai) ? 2 : 1, bi += char.IsSurrogatePair(b, bi) ? 2 : 1)
            {
                var acInt = char.ConvertToUtf32(a, ai);
                var bcInt = char.ConvertToUtf32(b, bi);
                Console.WriteLine(
                    $"[\u001b[36m{ai:D4}\u001b[39m] \u001b[35m{acInt:D8}\u001b[39m \u001b[32m0x{acInt:X6}\u001b[39m {char.ConvertFromUtf32(acInt)} \u001b[36m>\u001b[39m \u001b[35m{bcInt:D8}\u001b[39m \u001b[32m0x{bcInt:X6}\u001b[39m {char.ConvertFromUtf32(bcInt)}");
            }
        }
    }
}