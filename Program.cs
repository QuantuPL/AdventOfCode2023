using System.Reflection;
using AdventOfCode2023;

Console.WriteLine("Hello, World!");

Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies()!;

List<Type> dayTasks = assemblies.SelectMany(a => a.GetTypes())
                                .Where(p => p.GetCustomAttribute(typeof(NameAttribute)) != null).ToList();
dayTasks.Sort((a, b) => a.Name.CompareTo(b.Name));

for (int i = 0; i < dayTasks.Count; i++)
{
    int dayNumber = i + 1;
    Console.WriteLine();

    string name = (dayTasks[i].GetCustomAttribute(typeof(NameAttribute))! as NameAttribute)!;

    Console.WriteLine(name);

    string[]? input = InputLoader.GetLines(dayNumber);

    if (input == null)
    {
        Console.WriteLine("Can't load input data");
        continue;
    }

    IExecute solution = (IExecute)Activator.CreateInstance(dayTasks[i])!;

    string output1 = solution.ExecuteFirst(input);

    Console.WriteLine(output1);

    Console.WriteLine("-----------------");

    string output2 = solution.ExecuteSecond(input);

    Console.WriteLine(output2);
}