namespace AdventOfCode2023;

[Name("--- Day 2: Cube Conundrum ---")]
internal class Day2 : IExecute
{
    #region First
    public string ExecuteFirst(string[] input)
    {
        GameSet gameSet = new() { Blue = 14, Green = 13, Red = 12 };

        int sum = 0;

        for (int i = 0; i < input.Length; i++)
        {
            Game game = ParseGame(input[i]);

            GameSet[] sets = game.Sets;

            bool isGood = true;
            foreach (var set in sets)
            {
                if (set.Red > gameSet.Red || set.Green > gameSet.Green || set.Blue > gameSet.Blue)
                {
                    isGood = false;
                    break;
                }
            }

            if (isGood)
            {
                sum += game.ID;
            }
        }

        return sum.ToString();
    }

    #endregion

    #region Second
    public string ExecuteSecond(string[] input)
    {
        int sum = 0;

        for (int i = 0; i < input.Length; i++)
        {
            Game game = ParseGame(input[i]);

            GameSet result = new();

            GameSet[] sets = game.Sets;
            foreach (var set in sets)
            {
                result = MaxSet(result, set);
            }

            int power = result.Blue * result.Red * result.Green;

            sum += power;
        }

        return sum.ToString();
    }

    private static GameSet MaxSet(GameSet set1, GameSet set2)
    {
        set1.Blue = Math.Max(set1.Blue, set2.Blue);
        set1.Green = Math.Max(set1.Green, set2.Green);
        set1.Red = Math.Max(set1.Red, set2.Red);

        return set1;
    }

    #endregion

    private static Game ParseGame(string line)
    {
        int colonIndex = line.IndexOf(':');

        int id = int.Parse(line[5..colonIndex]);

        Game game = new()
        {
            ID = id
        };

        line = line[(colonIndex + 2)..];
        string[] setsData = line.Split(';', StringSplitOptions.RemoveEmptyEntries);

        GameSet[] sets = new GameSet[setsData.Length];
        for (int i = 0; i < sets.Length; i++)
        {
            GameSet set = new GameSet();

            string[] cubes = setsData[i].Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);

            for (int j = 0; j < cubes.Length; j++)
            {
                string cube = cubes[j];

                int spaceIndex = cube.IndexOf(' ');
                string numberText = cube[..spaceIndex];
                string color = cube[(spaceIndex + 1)..];

                int number = int.Parse(numberText);
                switch (color)
                {
                    case "blue":
                        set.Blue = number;
                        break;
                    case "red":
                        set.Red = number;
                        break;
                    case "green":
                        set.Green = number;
                        break;
                    default:
                        throw new Exception($"Unknown color: {color}");
                }
            }

            sets[i] = set;
        }

        game.Sets = sets;

        return game;
    }

    struct Game
    {
        public int ID;
        public GameSet[] Sets;
    }

    struct GameSet
    {
        public int Blue;
        public int Red;
        public int Green;
    }
}