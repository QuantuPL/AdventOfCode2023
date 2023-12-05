namespace AdventOfCode2023;

[Name("--- Day 4: Scratchcards ---")]
internal class Day4 : IExecute
{
    #region First
    public string ExecuteFirst(string[] input)
    {
        int sum = 0;

        foreach (var line in input)
        {
            int colonIndex = line.IndexOf(':');

            string idText = line[5..colonIndex];
            int id = int.Parse(idText);

            string data = line[(colonIndex + 2)..];

            List<int> winning = new List<int>();
            List<int> numbers = new List<int>();

            int accu = 0;
            bool isWinning = true;
            foreach (char c in data)
            {
                if (char.IsDigit(c))
                {
                    accu = accu * 10 + (c - '0');
                }
                else if (c == ' ' && accu != 0)
                {
                    if (isWinning)
                    {
                        winning.Add(accu);
                    }
                    else
                    {
                        numbers.Add(accu);
                    }

                    accu = 0;
                }
                else if (c == '|')
                {
                    isWinning = false;
                }
            }
            numbers.Add(accu);

            int val = 0;
            foreach (var n in numbers)
            {
                if (winning.Contains(n))
                {
                    if (val != 0)
                    {
                        val *= 2;
                    }
                    else
                    {
                        val = 1;
                    }
                }
            }
            sum += val;
        }
        return sum.ToString();
    }

    #endregion

    #region Second

    public string ExecuteSecond(string[] input)
    {
        int[] match = GetWinList(input);
        int[] winValue = new int[match.Length];

        int sum = 0;
        for (int i = match.Length - 1; i >= 0; i--)
        {
            sum++;

            int val = match[i];
            if (val == 0)
            {
                continue;
            }

            int localSum = 0;
            for (int j = 1; j <= val; j++)
            {
                int index = i + j;

                if (index >= winValue.Length)
                {
                    break;
                }

                localSum += winValue[index] + 1;
            }

            winValue[i] = localSum;
            sum += localSum;
        }

        return sum.ToString();
    }

    private static int[] GetWinList(string[] lines)
    {
        int[] result = new int[lines.Length];

        for (int i = 0; i < lines.Length; i++)
        {
            string line = lines[i];

            int colonIndex = line.IndexOf(':');

            string data = line[(colonIndex + 2)..];

            List<int> winning = new List<int>();
            List<int> numbers = new List<int>();

            int accu = 0;
            bool isWinningNumbers = true;
            foreach (char c in data)
            {
                if (char.IsDigit(c))
                {
                    accu = accu * 10 + (c - '0');
                }
                else if (c == ' ' && accu != 0)
                {
                    if (isWinningNumbers)
                    {
                        winning.Add(accu);
                    }
                    else
                    {
                        numbers.Add(accu);
                    }

                    accu = 0;
                }
                else if (c == '|')
                {
                    isWinningNumbers = false;
                }
            }
            numbers.Add(accu);

            int val = 0;
            foreach (var n in numbers)
            {
                if (winning.Contains(n))
                {
                    val++;
                }
            }

            result[i] = val;
        }

        return result;
    }

    #endregion
}