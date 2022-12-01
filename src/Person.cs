using System.Reflection;
using System.Text;
using System.Xml.Serialization;

namespace Lab2CSharp;

public delegate void AgeChangingDelegate(int oldAge, int newAge);

[XmlRoot("Személy")]
public class Person
{
    public event AgeChangingDelegate AgeChanging;

    private int age;
    [XmlAttribute("Kor")]
    [WriteInToString(IsEnabled = true)]
    public int Age
    {
        get { return age; }
        set
        {
            if (age == value)
                return;

            if (value < 0)
                throw new ArgumentException("Érvénytelen életkor!");

            AgeChanging?.Invoke(age, value);

            age = value;
        }
    }

    [XmlIgnore]
    public string Name { get; set; }

    public int AgeInDogYear => Age * 7;

    public override string ToString()
    {
        var s = new StringBuilder();
        s.AppendLine(base.ToString());

        foreach (var property in typeof(Person).GetProperties())
        {
            if (property.GetCustomAttribute<WriteInToStringAttribute>()?.IsEnabled ?? false)
            {
                s.AppendLine($"\t{property.Name} = {property.GetValue(this)}");
            }
        }

        return s.ToString();
    }
}
