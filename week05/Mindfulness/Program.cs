using System;
using System.Collections.Generic;
using System.Threading;
using System.IO;

public abstract class Activity
{
    private string _name;
    private string _description;
    private int _duration;

    public Activity(string name, string description)
    {
        _name = name;
        _description = description;
    }

    public void StartActivity()
    {
        Console.WriteLine($"Welcome to the {_name}.");
        Console.WriteLine();
        Console.WriteLine(_description);
        Console.WriteLine();
        Console.Write("How long, in seconds, would you like for your session? ");
        _duration = int.Parse(Console.ReadLine());
        Console.Clear();
        Console.WriteLine("Get ready to begin...");
        ShowSpinner(5);
        Console.WriteLine();
    }

    public void EndActivity()
    {
        Console.WriteLine();
        Console.WriteLine("Well done!");
        ShowSpinner(3);
        Console.WriteLine();
        Console.WriteLine($"You have completed another {_duration} seconds of the {_name}.");
        ShowSpinner(3);
        Console.Clear();
    }

    public int GetDuration()
    {
        return _duration;
    }

    public void ShowSpinner(int seconds)
    {
        List<string> animationStrings = new List<string>();
        animationStrings.Add("|");
        animationStrings.Add("/");
        animationStrings.Add("-");
        animationStrings.Add("\\");
        animationStrings.Add("|");
        animationStrings.Add("/");
        animationStrings.Add("-");
        animationStrings.Add("\\");

        DateTime startTime = DateTime.Now;
        DateTime endTime = startTime.AddSeconds(seconds);

        int i = 0;

        while (DateTime.Now < endTime)
        {
            string s = animationStrings[i];
            Console.Write(s);
            Thread.Sleep(250);
            Console.Write("\b \b");

            i++;

            if (i >= animationStrings.Count)
            {
                i = 0;
            }
        }
    }

    public void ShowCountdown(int seconds)
    {
        for (int i = seconds; i > 0; i--)
        {
            Console.Write(i);
            Thread.Sleep(1000);
            Console.Write("\b \b");
        }
    }

    public abstract void Run();
}

public class BreathingActivity : Activity
{
    public BreathingActivity() : base("Breathing Activity", "This activity will help you relax by walking your through breathing in and out slowly. Clear your mind and focus on your breathing.") { }

    public override void Run()
    {
        StartActivity();

        int duration = GetDuration();
        DateTime startTime = DateTime.Now;
        DateTime endTime = startTime.AddSeconds(duration);

        while (DateTime.Now < endTime)
        {
            Console.WriteLine();
            Console.Write("Breathe in...");
            ShowCountdown(4);
            Console.WriteLine();
            Console.Write("Breathe out...");
            ShowCountdown(4);
        }

        EndActivity();
    }
}

public class ReflectionActivity : Activity
{
    private List<string> _prompts = new List<string>
    {
        "Think of a time when you stood up for someone else.",
        "Think of a time when you did something really difficult.",
        "Think of a time when you helped someone in need.",
        "Think of a time when you did something truly selfless."
    };

    private List<string> _questions = new List<string>
    {
        "Why was this experience meaningful to you?",
        "Have you ever done anything like this before?",
        "How did you get started?",
        "How did you feel when it was complete?",
        "What made this time different than other times when you were not as successful?",
        "What is your favorite thing about this experience?",
        "What could you learn from this experience that applies to other situations?",
        "What did you learn about yourself through this experience?",
        "How can you keep this experience in mind in the future?"
    };

    public ReflectionActivity() : base("Reflection Activity", "This activity will help you reflect on times in your life when you have shown strength and resilience. This will help you recognize the power you have and how you can use it in other aspects of your life.") { }

    public override void Run()
    {
        StartActivity();

        Random random = new Random();
        string prompt = _prompts[random.Next(_prompts.Count)];
        Console.WriteLine(prompt);
        Console.WriteLine();

        int duration = GetDuration();
        DateTime startTime = DateTime.Now;
        DateTime endTime = startTime.AddSeconds(duration);

        while (DateTime.Now < endTime)
        {
            string question = _questions[random.Next(_questions.Count)];
            Console.WriteLine(question);
            ShowSpinner(5);
            Console.WriteLine();
        }

        EndActivity();
    }
}

public class ListingActivity : Activity
{
    private List<string> _prompts = new List<string>
    {
        "Who are people that you appreciate?",
        "What are personal strengths of yours?",
        "Who are people that you have helped this week?",
        "When have you felt the Holy Ghost this month?",
        "Who are some of your personal heroes?"
    };

    public ListingActivity() : base("Listing Activity", "This activity will help you reflect on the good things in your life by having you list as many things as you can in a certain area.") { }

    public override void Run()
    {
        StartActivity();

        Random random = new Random();
        string prompt = _prompts[random.Next(_prompts.Count)];
        Console.WriteLine(prompt);
        Console.WriteLine();
        Console.Write("You may begin in: ");
        ShowCountdown(5);
        Console.WriteLine();

        int duration = GetDuration();
        DateTime startTime = DateTime.Now;
        DateTime endTime = startTime.AddSeconds(duration);
        int items = 0;

        while (DateTime.Now < endTime)
        {
            Console.Write(">");
            Console.ReadLine();
            items++;
        }

        Console.WriteLine($"You listed {items} items.");
        EndActivity();
    }
}

public class GratitudeActivity : Activity
{
    private List<string> _gratitudePrompts = new List<string>
    {
        "What are three things you are grateful for today?",
        "Who is someone you appreciate and why?",
        "What is a small moment that brought you joy recently?",
        "What is a skill or talent you have that you are grateful for?"
    };

    public GratitudeActivity() : base("Gratitude Activity", "This activity will help you focus on gratitude and positivity in your life.") { }

    public override void Run()
    {
        StartActivity();

        Random random = new Random();
        string prompt = _gratitudePrompts[random.Next(_gratitudePrompts.Count)];
        Console.WriteLine(prompt);
        Console.WriteLine();

        int duration = GetDuration();
        DateTime startTime = DateTime.Now;
        DateTime endTime = startTime.AddSeconds(duration);

        while (DateTime.Now < endTime)
        {
            Console.Write(">");
            Console.ReadLine();
        }

        EndActivity();
    }
}

public class Program
{
    private static Dictionary<string, int> _activityLog = new Dictionary<string, int>();

    static void Main(string[] args)
    {
        LoadLog();

        while (true)
        {
            Console.WriteLine("Mindfulness Activities");
            Console.WriteLine("1. Breathing Activity");
            Console.WriteLine("2. Reflection Activity");
            Console.WriteLine("3. Listing Activity");
            Console.WriteLine("4. Gratitude Activity");
            Console.WriteLine("5. View Activity Log");
            Console.WriteLine("6. Quit");
            Console.Write("Select a choice from the menu: ");
            int choice = int.Parse(Console.ReadLine());

            switch (choice)
            {
                case 1:
                    RunActivity(new BreathingActivity());
                    break;
                case 2:
                    RunActivity(new ReflectionActivity());
                    break;
                case 3:
                    RunActivity(new ListingActivity());
                    break;
                case 4:
                    RunActivity(new GratitudeActivity());
                    break;
                case 5:
                    ViewLog();
                    break;
                case 6:
                    SaveLog();
                    return;
                default:
                    Console.WriteLine("Invalid choice.");
                    break;
            }
        }
    }

    static void RunActivity(Activity activity)
    {
        activity.Run();
        if (_activityLog.ContainsKey(activity.GetType().Name))
        {
            _activityLog[activity.GetType().Name]++;
        }
        else
        {
            _activityLog[activity.GetType().Name] = 1;
        }
    }

    static void ViewLog()
    {
        Console.WriteLine("\nActivity Log:");
        foreach (var entry in _activityLog)
        {
            Console.WriteLine($"{entry.Key}: {entry.Value} times");
        }
        Console.WriteLine();
    }

    static void SaveLog()
    {
        try
        {
            using (StreamWriter writer = new StreamWriter("activity_log.txt"))
            {
                foreach (var entry in _activityLog)
                {
                    writer.WriteLine($"{entry.Key},{entry.Value}");
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error saving log: {ex.Message}");
        }
    }

    static void LoadLog()
    {
        try
        {
            if (File.Exists("activity_log.txt"))
            {
                using (StreamReader reader = new StreamReader("activity_log.txt"))
                {
                    while (!reader.EndOfStream)
                    {
                        string line = reader.ReadLine();
                        string[] parts = line.Split(',');
                        if (parts.Length == 2)
                        {
                            _activityLog[parts[0]] = int.Parse(parts[1]);
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error loading log: {ex.Message}");
        }
    }
}

/*
Exceeding Requirements:

1. Added a new activity: Gratitude Activity. This activity prompts the user to reflect on things they are grateful for.
2. Implemented an activity log using a dictionary to track how many times each activity has been performed.
3. Added functionality to view the activity log (choice 5 in the menu).
4. Implemented saving and loading the activity log to a file (activity_log.txt), so the log persists between program runs.
*/