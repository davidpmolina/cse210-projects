using System;
using System.Collections.Generic;
using System.IO;

namespace JournalProgram
{
    public class Journal
    {
        private List<Entry> entries = new List<Entry>();
        private PromptGenerator promptGenerator = new PromptGenerator();

        public void AddEntry(Entry entry)
        {
            entries.Add(entry);
        }

        public void DisplayEntries()
        {
            foreach (var entry in entries)
            {
                Console.WriteLine(entry);
            }
        }

        public void SaveToFile(string filename)
        {
            using (StreamWriter outputFile = new StreamWriter(filename))
            {
                foreach (var entry in entries)
                {
                    outputFile.WriteLine($"{entry.Date}~|~{entry.Prompt}~|~{entry.Response}");
                }
            }
            Console.WriteLine($"Journal saved to {filename}");
        }

        public void LoadFromFile(string filename)
        {
            if (File.Exists(filename))
            {
                entries.Clear();
                string[] lines = File.ReadAllLines(filename);

                foreach (string line in lines)
                {
                    string[] parts = line.Split(new string[] { "~|~" }, StringSplitOptions.None);

                    if (parts.Length == 3)
                    {
                        entries.Add(new Entry(parts[1], parts[2], parts[0]));
                    }
                }
                Console.WriteLine($"Journal loaded from {filename}");
            }
            else
            {
                Console.WriteLine($"File {filename} not found.");
            }
        }

        public string GetRandomPrompt()
        {
            return promptGenerator.GetRandomPrompt();
        }
    }
}
