using System;
using System.Collections.Generic;

namespace JournalProgram
{
    public class PromptGenerator
    {
        private List<string> _prompts = new List<string>
        {
            "Who was the most interesting person I interacted with today?",
            "What was the best part of my day?",
            "How did I see the hand of the Lord in my life today?",
            "What was the strongest emotion I felt today?",
            "If I had one thing I could do over today, what would it be?",
            "What am I grateful for today?",
            "What did I learn today?",
            "What made me smile today?",
            "What was a challenge I faced today, and how did I overcome it?",
            "What are my goals for tomorrow?"
        };

        public string GetRandomPrompt()
        {
            Random random = new Random();
            int index = random.Next(_prompts.Count);
            return _prompts[index];
        }
    }
}