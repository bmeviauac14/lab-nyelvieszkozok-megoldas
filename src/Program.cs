using System.Diagnostics;
using System.Xml.Serialization;

namespace Lab2CSharp;

internal class Program
{
    static void Main(string[] args)
    {
        var p = new Person()
        {
            Age = 17,
            Name = "Luke",
        };

        // p.AgeChanging = new AgeChangingDelegate(PersonAgeChanging);
        p.AgeChanging += new AgeChangingDelegate(PersonAgeChanging);
        p.AgeChanging += PersonAgeChanging; // Tömörebb szintaktika

        p.Age++;

        p.AgeChanging -= PersonAgeChanging;

        p.Age = 2;

        Console.WriteLine(p.Age);

        Console.WriteLine(p);

        var serializer = new XmlSerializer(typeof(Person));
        var stream = new FileStream("person.txt", FileMode.Create);
        serializer.Serialize(stream, p);
        stream.Close();
        Process.Start(new ProcessStartInfo
        {
            FileName = "person.txt",
            UseShellExecute = true,
        });

        var list = new List<int>
        {
            1,
            2,
            3
        };

        list = list.FindAll(MyFilter);
        foreach (int n in list)
        {
            Console.WriteLine($"Value: {n}");
        }
    }

    private static void PersonAgeChanging(int oldAge, int newAge)
    {
        Console.WriteLine(oldAge + " => " + newAge);
    }

    private static bool MyFilter(int n)
    {
        return n % 2 == 1;
    }
}