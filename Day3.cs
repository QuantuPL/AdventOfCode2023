using System;

namespace AdventOfCode2023;

[Name("--- Day 3: Gear Ratios ---")]
internal class Day3 : IExecute
{
    #region First
    public string ExecuteFirst(string[] input)
    {
        string[] fixedInput = ExpandInput(input);

        int sum = 0;
        string[] lines = new string[3];

        int length = fixedInput.Length - 1;
        for (int i = 1; i < length; i++)
        {
            lines[0] = fixedInput[i - 1];
            lines[1] = fixedInput[i];
            lines[2] = fixedInput[i + 1];

            string line = fixedInput[i];

            List<Number> nums = FindAllNumbersInLine(line);
            foreach (var num in nums)
            {
                if (!IsPartNumber(lines, num))
                {
                    continue;
                }

                string numberText = line[num.StartIndex..(num.EndIndex + 1)];

                int number = int.Parse(numberText);

                sum += number;
            }
        }

        return sum.ToString();
    }

    private static bool IsPartNumber(string[] lines, Number num)
    {
        int start = num.StartIndex;

        if (start > 1)
        {
            start--;

            //.......
            //.x123..
            //.......
            if (IsSymbol(lines[1][start]))
            {
                return true;
            }
        }

        int end = num.EndIndex;

        if (end + 1 < lines[0].Length)
        {
            end++;
            //.......
            //..123x.
            //.......
            if (IsSymbol(lines[1][end]))
            {
                return true;
            }

        }

        //.xxxxx.
        //..123..
        //.xxxxx.
        for (int i = start; i <= end; i++)
        {
            if (IsSymbol(lines[0][i]) || IsSymbol(lines[2][i]))
            {
                return true;
            }
        }

        return false;
    }

    private static bool IsSymbol(char c)
    {
        if (IsDigit(c))
        {
            return false;
        }

        return c != '.';
    }

    #endregion

    #region Second
    public string ExecuteSecond(string[] input)
    {
        string[] fixedInput = ExpandInput(input);

        int sum = 0;
        string[] lines = new string[3];
        List<Gear> penultimateGears = new List<Gear>();
        List<Gear> lastGears = new List<Gear>();

        int length = fixedInput.Length - 1;
        for (int i = 1; i < length; i++)
        {
            lines[0] = fixedInput[i - 1];
            lines[1] = fixedInput[i];
            lines[2] = fixedInput[i + 1];

            string line = fixedInput[i];

            List<Gear> currentGears = new List<Gear>();

            List<Number> nums = FindAllNumbersInLine(line);
            foreach (var num in nums)
            {
                string numberText = line[num.StartIndex..(num.EndIndex + 1)];

                int number = int.Parse(numberText);

                if (!IsGearNumber(lines, i, number, num, out Gear gear))
                {
                    continue;
                }

                currentGears.Add(gear);
            }

            for (int j = 0; j < currentGears.Count - 1; j++)
            {
                Gear g1 = currentGears[j];
                Gear g2 = currentGears[j + 1];

                if (g1.Position != g2.Position)
                {
                    continue;
                }

                currentGears.RemoveAt(j);
                currentGears.RemoveAt(j);

                j--;

                sum += g1.Value * g2.Value;
            }

            for (int j = currentGears.Count - 1; j >= 0; j--)
            {
                Gear gear = currentGears[j];

                foreach (var g in penultimateGears)
                {
                    if (gear.Position != g.Position)
                    {
                        continue;
                    }

                    currentGears.RemoveAt(j);

                    sum += gear.Value * g.Value;
                }

                foreach (var g in lastGears)
                {
                    if (gear.Position != g.Position)
                    {
                        continue;
                    }

                    currentGears.RemoveAt(j);

                    sum += gear.Value * g.Value;
                }
            }

            penultimateGears = lastGears;
            lastGears = currentGears;
        }

        return sum.ToString();
    }

    private static bool IsGearNumber(string[] lines, int centerLineIndex, int value, Number num, out Gear gear)
    {
        gear = new Gear() { };


        int start = num.StartIndex;

        if (start > 1)
        {
            start--;

            //.......
            //.x123..
            //.......
            if (IsGear(lines[1][start]))
            {
                gear.Value = value;
                gear.Position = new(centerLineIndex, start);
                return true;
            }
        }

        int end = num.EndIndex;

        if (end + 1 < lines[0].Length)
        {
            end++;
            //.......
            //..123x.
            //.......
            if (IsGear(lines[1][end]))
            {
                gear.Value = value;
                gear.Position = new(centerLineIndex, end);
                return true;
            }

        }

        //.xxxxx.
        //..123..
        //.xxxxx.
        for (int i = start; i <= end; i++)
        {
            if (IsGear(lines[0][i]))
            {
                gear.Value = value;
                gear.Position = new(centerLineIndex - 1, i);
                return true;
            }
            if (IsGear(lines[2][i]))
            {
                gear.Value = value;
                gear.Position = new(centerLineIndex + 1, i);
                return true;
            }
        }

        return false;
    }


    private static bool IsGear(char c)
    {
        return c == '*';
    }

    #endregion

    private static List<Number> FindAllNumbersInLine(string line)
    {
        List<Number> numbers = new List<Number>();
        for (int i = 0; i < line.Length; i++)
        {
            if (IsDigit(line[i]))
            {
                Number number = FindAllDigits(i);

                numbers.Add(number);

                i = number.EndIndex + 1;
            }
        }

        return numbers;

        Number FindAllDigits(int index)
        {
            Number num = new()
            {
                StartIndex = index,
            };

            index++;

            while (index < line.Length)
            {
                if (!IsDigit(line[index]))
                {
                    break;
                }
                index++;
            }

            num.EndIndex = index - 1;

            return num;
        }
    }

    private static bool IsDigit(char c)
    {
        return '0' <= c && c <= '9';
    }

    private static string[] ExpandInput(string[] input)
    {
        char[] charLine = new char[input[0].Length];
        Array.Fill(charLine, '.');
        string blankLine = new string(charLine);

        string[] fixedInput = new string[input.Length + 2];
        fixedInput[0] = blankLine;
        fixedInput[^1] = blankLine;
        Array.Copy(input, 0, fixedInput, 1, input.Length);

        return fixedInput;
    }

    struct Gear
    {
        public int Value;
        public GearPosition Position;
    }

    struct GearPosition(int line, int index)
    {
        public int line = line;
        public int index = index;

        public static bool operator ==(GearPosition g1, GearPosition g2)
        {
            return g1.line == g2.line && g1.index == g2.index;
        }

        public static bool operator !=(GearPosition g1, GearPosition g2)
        {
            return !(g1 == g2);
        }
    }

    struct Number
    {
        public int StartIndex;
        public int EndIndex;
    }
}
