namespace AdventOfCode2023;

[AttributeUsage(AttributeTargets.Class)]
internal class NameAttribute(string name) : Attribute
{
    private readonly string _name = name;

    public static implicit operator string(NameAttribute attribute) => attribute._name;
}