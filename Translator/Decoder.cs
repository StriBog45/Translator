using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Translator
{
    public static class Decoder
    {
        public static string Decrypt(string code, int[] keyFirst, int[] keySecond)
        {
            var sizeTable = keyFirst.Max();
            var sequenceRecord = new int[sizeTable];
            var sequenceRead = new int[sizeTable];
            for (int i = 0; i < sizeTable; i++)
            {
                sequenceRecord[i] = i + 1;
                sequenceRead[i] = i + 1;
            }

            // Инициализация и заполнение таблицы
            var tableSymbols = new char[sizeTable][];
            for (int i = 0; i < sizeTable; i++)
                tableSymbols[i] = new char[sizeTable];
            for (int i = 0; i < sizeTable; i++)
                for (int j = 0; j < sizeTable; j++)
                    tableSymbols[i][j] = ' ';

            // Чтение
            int l = 0;
            for (int i = 0; i < sizeTable; i++)
                for (int j = 0; j < sizeTable; j++, l++)
                {
                    if (l >= code.Length)
                        break;
                    tableSymbols[i][j] = code[l];
                }

            sequenceRecord = new int[keySecond.Length];
            for (int i = 0; i < keySecond.Length; i++)
                sequenceRecord[i] = keySecond[i];
            keySecond = new int[sequenceRecord.Length];
            for (int i = 0; i < sequenceRecord.Length; i++)
                keySecond[i] = i + 1;

            // перебор строк
            // перебираем ключ
            for (int z = 0; z < keySecond.Length; z++)
                // порядок различается
                if (keySecond[z] != sequenceRecord[z])
                    // ищем нужную строку для перестановки
                    for (int u = z; u < sequenceRecord.Length; u++)
                        // нашли нужный столбец
                        if (sequenceRecord[z] == keySecond[u])
                        {
                            // меняем строки местами
                            Swap(ref keySecond[z], ref keySecond[u]);
                            for (int r = 0; r < sizeTable; r++)
                                Swap(ref tableSymbols[z][r], ref tableSymbols[u][r]);
                        }

            sequenceRecord = new int[keyFirst.Length];
            for (int i = 0; i < keyFirst.Length; i++)
                sequenceRecord[i] = keyFirst[i];

            keyFirst = new int[sequenceRecord.Length];
            for (int i = 0; i < sequenceRecord.Length; i++)
                keyFirst[i] = i + 1;

            // перебор столбцов
            // перебираем ключ
            for (int z = 0; z < keyFirst.Length; z++)
                // порядок различается
                if (keyFirst[z] != sequenceRecord[z])
                    // ищем нужный столбец для перестановки
                    for (int u = z; u < sequenceRecord.Length; u++)
                        // нашли нужный столбец
                        if (sequenceRecord[z] == keyFirst[u])
                        {
                            // меняем столбцы местами
                            Swap(ref keyFirst[z], ref keyFirst[u]);
                            for (int r = 0; r < sizeTable; r++)
                                Swap(ref tableSymbols[r][z], ref tableSymbols[r][u]);
                        }

            string result = "";
            for (int i = 0; i < sizeTable; i++)
                for (int j = 0; j < sizeTable; j++)
                    result += Convert.ToString(tableSymbols[i][j]);
            result = result.Trim();
            return result;
        }
        static void Swap<T>(ref T a, ref T b)
        {
            T c = a;
            a = b;
            b = c;
        }
    }
}
