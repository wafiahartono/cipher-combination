using System;
using System.Globalization;
using System.Linq;

namespace ConsoleApp
{
    public static class DebugCipher
    {
        public static void PrintStringDiff(string a, string b)
        {
            var aIndexes = StringInfo.ParseCombiningCharacters(a);
            var bIndexes = StringInfo.ParseCombiningCharacters(b);
            for (int ai = 0, bi = 0; ai < aIndexes.Length && bi < bIndexes.Length; ai++, bi++)
            {
                var ac = char.ConvertFromUtf32(char.ConvertToUtf32(a, aIndexes[ai]));
                var bc = char.ConvertFromUtf32(char.ConvertToUtf32(b, bIndexes[bi]));
                var acInt = ac.ToCharArray().Select(c => (int) c).Aggregate((acc, i) => acc + i);
                var bcInt = bc.ToCharArray().Select(c => (int) c).Aggregate((acc, i) => acc + i);

                if (acInt == 0)
                {
                    bi--;
                    continue;
                }

                if (bcInt == 0)
                {
                    ai--;
                    continue;
                }

                Console.WriteLine(
                    $"[\u001b[36m{ai:D4}\u001b[39m] \u001b[35m{acInt:D8}\u001b[39m \u001b[32m0x{acInt:X6}\u001b[39m {ac} \u001b[36m>\u001b[39m \u001b[35m{bcInt:D8}\u001b[39m \u001b[32m0x{bcInt:X6}\u001b[39m {bc}");
            }
        }
    }
}