namespace Lab2CSharp;

[AttributeUsage(AttributeTargets.Property)]
public class WriteInToStringAttribute : Attribute
{
    public bool IsEnabled { get; set; }
}
