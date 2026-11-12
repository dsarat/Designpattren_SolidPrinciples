// See https://aka.ms/new-console-template for more information
//Console.WriteLine("Hello, World!");

using System.Diagnostics;

//Goal : To demonstrate the Single Responsibility Principle (SRP) in C# by separating the responsibilities of a Journal class and a Persistence class.One reason to change one thing is to add a new feature, like saving the journal to a file. If the Journal class is responsible for both managing entries and saving to a file, then changing the saving functionality would require modifying the Journal class, which violates SRP. By separating the responsibilities into two classes, we can change the saving functionality without affecting the Journal class.
public class Journal
{
    private readonly List<string> entries = new List<string>();
    private static int count = 0;
    public int AddEntry(string text)
    {
        entries.Add($"{++count}: {text}");
        return count; // memento
    }
    public void RemoveEntry(int index)
    {
        entries.RemoveAt(index);
    }
    public override string ToString()
    {
        return string.Join(Environment.NewLine, entries);
    }
    //Here In fure we can add the functionality to save and load the journal from a file, but that would violate the Single Responsibility Principle. So we will create a new class for that.
}

public class Persistence
{
    public void SaveToFile(Journal journal, string filename, bool overwrite = false)
    {
        if (overwrite || !File.Exists(filename))
            File.WriteAllText(filename, journal.ToString());
    }
}
public class SingletonResponsibilityPrinciple
{
    public static void Main(string[] args)
    {
        var j = new Journal();
        j.AddEntry("I cried today.");
        j.AddEntry("I ate a bug.");
        Console.WriteLine(j);
        var p = new Persistence();
        var filename = @"D:\Tech\journal.txt"; // Corrected file path with a proper file name
        p.SaveToFile(j, filename, true);

        // Ensure the file exists before attempting to open it
        if (File.Exists(filename))
        {
            Process.Start(new ProcessStartInfo
            {
                FileName = "notepad.exe", // Changed to open with Notepad for simplicity
                Arguments = filename,
                UseShellExecute = true
            });
        }
        else
        {
            Console.WriteLine("File not found: " + filename);
        }
    }
}