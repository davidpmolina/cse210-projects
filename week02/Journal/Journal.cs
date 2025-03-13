using System;
using System.Collections.Generic;
using System.IO;

namespace JournalProgram
{
    public class Journal
    {
        private List<Entry> _entries = new List<Entry>();
        private PromptGenerator _promptGenerator = new PromptGenerator();

        public void AddEntry(Entry entry)
        {
            _entries.Add(entry);
        }

        public void DisplayEntries()
        {
            if (_entries.Count == 0)
            {
                Console.WriteLine("Your journal is empty.");
                return;
            }
            foreach (var entry in _entries)
            {
                Console.WriteLine(entry);
            }
        }

        public void SaveToFile(string filename)
        {
            try
            {
                using (StreamWriter outputFile = new StreamWriter(filename))
                {
                    foreach (var entry in _entries)
                    {
                        outputFile.WriteLine($"{entry._date}~|~{entry._prompt}~|~{entry._response}");
                    }
                }
                Console.WriteLine($"Journal saved to {filename}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving journal: {ex.Message}");
            }
        }

        public void LoadFromFile(string filename)
        {
            try
            {
                if (File.Exists(filename))
                {
                    _entries.Clear();
                    string[] lines = File.ReadAllLines(filename);

                    foreach (string line in lines)
                    {
                        string[] parts = line.Split(new string[] { "~|~" }, StringSplitOptions.None);

                        if (parts.Length == 3)
                        {
                            _entries.Add(new Entry(parts[1], parts[2], parts[0]));
                        }
                    }
                    Console.WriteLine($"Journal loaded from {filename}");
                }
                else
                {
                    Console.WriteLine($"File {filename} not found.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading journal: {ex.Message}");
            }
        }

        public string GetRandomPrompt()
        {
            return _promptGenerator.GetRandomPrompt();
        }
    }
}