using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2023;

internal static class InputLoader
{
    public static string[]? GetLines(int day)
    {
        try
        {
            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, $"inputs\\day{day}.txt");

            return File.ReadAllLines(path);
        }
        catch { }

        return null;
    }
}
