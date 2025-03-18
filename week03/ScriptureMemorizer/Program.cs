using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

public class Program
{
    public static void Main(string[] args)
    {
        List<Scripture> scriptureLibrary = LoadScripturesFromFile("scriptures.txt");

        if (scriptureLibrary.Count > 0)
        {
            Random random = new Random();
            int randomIndex = random.Next(scriptureLibrary.Count);
            Scripture selectedScripture = scriptureLibrary[randomIndex];

            selectedScripture.Memorize();
        }
        else
        {
            Console.WriteLine("No scriptures found in the library.");
        }
    }

    private static List<Scripture> LoadScripturesFromFile(string filePath)
    {
        string fullFilePath = @"C:\Users\David\OneDrive\Documents\BYU\cse210\cse210-projects\week03\ScriptureMemorizer\scriptures.txt";

        List<Scripture> scriptures = new List<Scripture>();
        try
        {
            string[] lines = File.ReadAllLines(fullFilePath);
            foreach (string line in lines)
            {
                string[] parts = line.Split('|');
                if (parts.Length == 3)
                {
                    string book = parts[0];
                    string referencePortion = parts[1];
                    string text = parts[2];

                    string[] referenceParts = referencePortion.Split(new char[] { ':', '-' }, StringSplitOptions.RemoveEmptyEntries);

                    if (referenceParts.Length == 2)
                    {
                        int chapter = int.Parse(referenceParts[0]);
                        int startVerse = int.Parse(referenceParts[1]);
                        ScriptureReference reference = new ScriptureReference(book, chapter, startVerse);
                        scriptures.Add(new Scripture(reference, text));

                    }
                    else if (referenceParts.Length == 3)
                    {
                        int chapter = int.Parse(referenceParts[0]);
                        int startVerse = int.Parse(referenceParts[1]);
                        int endVerse = int.Parse(referenceParts[2]);
                        ScriptureReference reference = new ScriptureReference(book, chapter, startVerse, endVerse);
                        scriptures.Add(new Scripture(reference, text));
                    }
                }
            }
        }
        catch (FileNotFoundException)
        {
            Console.WriteLine($"File not found: {fullFilePath}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error loading scriptures: {ex.Message}");
        }
        return scriptures;
    }
}

public class Scripture
{
    private ScriptureReference reference;
    private List<Word> words;

    public Scripture(ScriptureReference reference, string text)
    {
        this.reference = reference;
        this.words = text.Split(' ').Select(w => new Word(w)).ToList();
    }

    public void Memorize()
    {
        while (true)
        {
            if (Console.IsOutputRedirected == false)
            {
                Console.Clear();
            }

            Console.WriteLine(reference.ToString());
            Console.WriteLine(GetDisplayedText());

            Console.WriteLine("\nPress Enter to hide more words or type 'quit' to exit.");
            string input = Console.ReadLine();

            if (input.ToLower() == "quit")
            {
                break;
            }

            if (words.All(w => w.IsHidden))
            {
                break;
            }

            HideRandomWords();
        }
    }

    private string GetDisplayedText()
    {
        return string.Join(" ", words.Select(w => w.ToString()));
    }

    private void HideRandomWords()
    {
        Random random = new Random();
        int wordsToHide = Math.Min(3, words.Count(w => !w.IsHidden));

        for (int i = 0; i < wordsToHide; i++)
        {
            int index;
            do
            {
                index = random.Next(words.Count);
            } while (words[index].IsHidden);

            words[index].Hide();
        }
    }
}

public class ScriptureReference
{
    private string book;
    private int chapter;
    private int startVerse;
    private int? endVerse;

    public ScriptureReference(string book, int chapter, int startVerse)
    {
        this.book = book;
        this.chapter = chapter;
        this.startVerse = startVerse;
        this.endVerse = null;
    }

    public ScriptureReference(string book, int chapter, int startVerse, int endVerse)
    {
        this.book = book;
        this.chapter = chapter;
        this.startVerse = startVerse;
        this.endVerse = endVerse;
    }

    public override string ToString()
    {
        if (endVerse.HasValue)
        {
            return $"{book} {chapter}:{startVerse}-{endVerse}";
        }
        else
        {
            return $"{book} {chapter}:{startVerse}";
        }
    }
}

public class Word
{
    private string originalWord;
    private bool isHidden;

    public Word(string word)
    {
        this.originalWord = word;
        this.isHidden = false;
    }

    public void Hide()
    {
        this.isHidden = true;
    }

    public bool IsHidden
    {
        get { return isHidden; }
    }

    public override string ToString()
    {
        if (isHidden)
        {
            return new string('_', originalWord.Length);
        }
        else
        {
            return originalWord;
        }
    }
}