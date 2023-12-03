using System.Text;

namespace AdventOfCode2023;

[Name("--- Day 1: Trebuchet?! ---")]
internal class Day1 : IExecute
{
    #region First
    public string ExecuteFirst(string[] input)
    {
        int sum = 0;

        foreach (var line in input)
        {
            int firstDigit = line.First(IsDigit) - '0';
            int secondDigit = line.Last(IsDigit) - '0';

            int value = firstDigit * 10 + secondDigit;

            sum += value;
        }

        return sum.ToString();
    }

    private static bool IsDigit(char c)
    {
        return '0' <= c && c <= '9';
    }

    #endregion

    #region Second

    private static readonly string[] _textDigits =
    {
        "zero", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine"
    };

    public string ExecuteSecond(string[] input)
    {
        int sum = 0;

        foreach (var line in input)
        {
            int firstDigit = -1;

            for (int i = 0; i < line.Length; i++)
            {
                if (IsDigit(line[i]))
                {
                    firstDigit = line[i] - '0';
                    break;
                }

                int digit = GetTextDigit(line, i);

                if (digit != -1)
                {
                    firstDigit = digit;
                    break;
                }
            }

            int secondDigit = -1;

            for (int i = line.Length - 1; i >= 0; i--)
            {
                if (IsDigit(line[i]))
                {
                    secondDigit = line[i] - '0';
                    break;
                }

                int digit = GetTextDigit(line, i, true);

                if (digit != -1)
                {
                    secondDigit = digit;
                    break;
                }
            }

            if (firstDigit == -1 || secondDigit == -1)
            {
                throw new InvalidOperationException();
            }

            sum += firstDigit * 10 + secondDigit;
        }

        return sum.ToString();
    }

    private static int GetTextDigit(string input, int startIndex, bool isReverse = false)
    {
        for (int i = 0; i < _textDigits.Length; i++)
        {
            if (MatchText(input, _textDigits[i], startIndex, isReverse))
            {
                return i;
            }
        }

        return -1;
    }

    private static bool MatchText(string input, string match, int startIndex, bool isReverse = false)
    {
        if (!isReverse)
        {
            if (match.Length + startIndex > input.Length)
            {
                return false;
            }
        }
        else
        {
            startIndex -= match.Length -1;
            if (startIndex < 0)
            {
                return false;
            }
        }

        for (int i = 0; i < match.Length; i++)
        {
            char c = input[startIndex + i];
            if (c != match[i])
            {
                return false;
            }
        }

        return true;
    }


    #endregion
}
