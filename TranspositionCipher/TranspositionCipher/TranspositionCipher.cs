using System;
using System.Collections.Generic;
using System.Text;
using Common;

namespace TranspositionCipher
{
    public class TranspositionCipher : StringCipher
    {
        private readonly string _key;
        private readonly char _padding;

        public TranspositionCipher(string key, char padding)
        {
            _key = key;
            _padding = padding;
        }

        private int[] GetShiftIndexes()
        {
            int keyLength = _key.Length;
            int[] indexes = new int[keyLength];
            List<KeyValuePair<int, char>> sortedKey = new List<KeyValuePair<int, char>>();
            int i;

            for (i = 0; i < keyLength; ++i)
                sortedKey.Add(new KeyValuePair<int, char>(i, _key[i]));

            sortedKey.Sort(
                delegate(KeyValuePair<int, char> pair1, KeyValuePair<int, char> pair2)
                {
                    return pair1.Value.CompareTo(pair2.Value);
                }
            );

            for (i = 0; i < keyLength; ++i)
                indexes[sortedKey[i].Key] = i;

            return indexes;
        }

        public override string Encrypt(string plainText)
        {
            plainText = (plainText.Length % _key.Length == 0)
                ? plainText
                : plainText.PadRight(plainText.Length - (plainText.Length % _key.Length) + _key.Length, _padding);
            StringBuilder output = new StringBuilder();
            int totalChars = plainText.Length;
            int totalColumns = _key.Length;
            int totalRows = (int) Math.Ceiling((double) totalChars / totalColumns);
            char[,] rowChars = new char[totalRows, totalColumns];
            char[,] colChars = new char[totalColumns, totalRows];
            char[,] sortedColChars = new char[totalColumns, totalRows];
            int currentRow, currentColumn, i, j;
            int[] shiftIndexes = GetShiftIndexes();

            for (i = 0; i < totalChars; ++i)
            {
                currentRow = i / totalColumns;
                currentColumn = i % totalColumns;
                rowChars[currentRow, currentColumn] = plainText[i];
            }

            for (i = 0; i < totalRows; ++i)
            for (j = 0; j < totalColumns; ++j)
                colChars[j, i] = rowChars[i, j];

            for (i = 0; i < totalColumns; ++i)
            for (j = 0; j < totalRows; ++j)
                sortedColChars[shiftIndexes[i], j] = colChars[i, j];

            for (i = 0; i < totalChars; ++i)
            {
                currentRow = i / totalRows;
                currentColumn = i % totalRows;
                output.Append(sortedColChars[currentRow, currentColumn]);
            }

            return output.ToString();
        }

        public override string Decrypt(string cipherText)
        {
            StringBuilder output = new StringBuilder();
            int totalChars = cipherText.Length;
            int totalColumns = (int) Math.Ceiling((double) totalChars / _key.Length);
            int totalRows = _key.Length;
            char[,] rowChars = new char[totalRows, totalColumns];
            char[,] colChars = new char[totalColumns, totalRows];
            char[,] unsortedColChars = new char[totalColumns, totalRows];
            int currentRow, currentColumn, i, j;
            int[] shiftIndexes = GetShiftIndexes();

            for (i = 0; i < totalChars; ++i)
            {
                currentRow = i / totalColumns;
                currentColumn = i % totalColumns;
                rowChars[currentRow, currentColumn] = cipherText[i];
            }

            for (i = 0; i < totalRows; ++i)
            for (j = 0; j < totalColumns; ++j)
                colChars[j, i] = rowChars[i, j];

            for (i = 0; i < totalColumns; ++i)
            for (j = 0; j < totalRows; ++j)
                unsortedColChars[i, j] = colChars[i, shiftIndexes[j]];

            for (i = 0; i < totalChars; ++i)
            {
                currentRow = i / totalRows;
                currentColumn = i % totalRows;
                output.Append(unsortedColChars[currentRow, currentColumn]);
            }

            return output.ToString();
        }
    }
}