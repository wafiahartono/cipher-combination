using System;
using System.Globalization;

namespace ConsoleApp
{
    public static class Utils
    {
        public static void PrintStringDiff(string a, string b)
        {
            int aLen = StringInfo.ParseCombiningCharacters(a).Length,
                bLen = StringInfo.ParseCombiningCharacters(b).Length;
            if (aLen > bLen) b += new string('\0', aLen - bLen);
            else if (bLen > aLen) a += new string('\0', bLen - aLen);
            int[] aIndexes = StringInfo.ParseCombiningCharacters(a),
                bIndexes = StringInfo.ParseCombiningCharacters(b);
            for (var i = 0; i < aIndexes.Length; i++)
            {
                int aci = char.ConvertToUtf32(a, aIndexes[i]), bci = char.ConvertToUtf32(b, bIndexes[i]);
                var s = string.Empty;
                s += $"[\u001b[36m{i:D4}\u001b[39m] \u001b[35m{aci:D8}\u001b[39m \u001b[32m0x{aci:X6}\u001b[39m ";
                s += aci == 0 ? ' ' : char.ConvertFromUtf32(aci);
                s += $" \u001b[36m>\u001b[39m ";
                s += $"[\u001b[36m{i:D4}\u001b[39m] \u001b[35m{bci:D8}\u001b[39m \u001b[32m0x{bci:X6}\u001b[39m ";
                s += bci == 0 ? ' ' : char.ConvertFromUtf32(bci);
                Console.WriteLine(s);
            }
        }
    }
}