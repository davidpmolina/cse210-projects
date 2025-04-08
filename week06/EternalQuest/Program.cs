using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;

public abstract class Goal
{
    public string Name { get; protected set; }
    public int Points { get; protected set; }
    public bool Completed { get; protected set; }

    public Goal(string name, int points)
    {
        Name = name;
        Points = points;
        Completed = false;
    }

    public abstract void RecordEvent();

    public virtual int GetPoints()
    {
        return Points;
    }

    public virtual string Display()
    {
        string status = Completed ? "[X]" : "[ ]";
        return $"{status} {Name}";
    }

    public virtual Dictionary<string, object> ToDictionary()
    {
        return new Dictionary<string, object>
        {
            { "Name", Name },
            { "Points", Points },
            { "Completed", Completed },
            { "Type", GetType().Name }
        };
    }

    public static Goal FromDictionary(Dictionary<string, object> data)
    {
        string type = ((JsonElement)data["Type"]).GetString();
        string name = ((JsonElement)data["Name"]).GetString();
        int points = ((JsonElement)data["Points"]).GetInt32();
        bool completed = ((JsonElement)data["Completed"]).GetBoolean();

        switch (type)
        {
            case "SimpleGoal":
                return new SimpleGoal(name, points, completed);
            case "EternalGoal":
                return new EternalGoal(name, points);
            case "ChecklistGoal":
                int target = ((JsonElement)data["Target"]).GetInt32();
                int count = ((JsonElement)data["Count"]).GetInt32();
                return new ChecklistGoal(name, points, target, count);
            case "ProgressGoal":
                int currentProgress = ((JsonElement)data["CurrentProgress"]).GetInt32();
                int totalProgress = ((JsonElement)data["TotalProgress"]).GetInt32();
                return new ProgressGoal(name, points, totalProgress, currentProgress);
            case "NegativeGoal":
                return new NegativeGoal(name, points);
            default:
                throw new ArgumentException($"Unknown goal type: {type}");
        }
    }
}

public class SimpleGoal : Goal
{
    public SimpleGoal(string name, int points, bool completed = false) : base(name, points)
    {
        Completed = completed;
    }

    public override void RecordEvent()
    {
        Completed = true;
    }

    public override Dictionary<string, object> ToDictionary()
    {
        var data = base.ToDictionary();
        data["Completed"] = Completed;
        return data;
    }
}

public class EternalGoal : Goal
{
    public EternalGoal(string name, int points) : base(name, points)
    {
    }

    public override void RecordEvent()
    {
    }
}

public class ChecklistGoal : Goal
{
    public int Target { get; private set; }
    public int Count { get; private set; }

    public ChecklistGoal(string name, int points, int target, int count = 0) : base(name, points)
    {
        Target = target;
        Count = count;
    }

    public override void RecordEvent()
    {
        Count++;
        if (Count == Target)
        {
            Completed = true;
        }
    }

    public override int GetPoints()
    {
        return Completed ? Points + (Points * 10) : Points;
    }

    public override string Display()
    {
        string status = Completed ? "[X]" : "[ ]";
        return $"{status} {Name} (Completed {Count}/{Target} times)";
    }

    public override Dictionary<string, object> ToDictionary()
    {
        var data = base.ToDictionary();
        data["Target"] = Target;
        data["Count"] = Count;
        return data;
    }
}

public class ProgressGoal : Goal
{
    public int TotalProgress { get; private set; }
    public int CurrentProgress { get; private set; }

    public ProgressGoal(string name, int points, int totalProgress, int currentProgress = 0) : base(name, points)
    {
        TotalProgress = totalProgress;
        CurrentProgress = currentProgress;
    }

    public override void RecordEvent()
    {
        CurrentProgress++;
        if (CurrentProgress >= TotalProgress)
        {
            Completed = true;
        }
    }

    public override string Display()
    {
        string status = Completed ? "[X]" : "[ ]";
        return $"{status} {Name} (Progress {CurrentProgress}/{TotalProgress})";
    }

    public override Dictionary<string, object> ToDictionary()
    {
        var data = base.ToDictionary();
        data["TotalProgress"] = TotalProgress;
        data["CurrentProgress"] = CurrentProgress;
        return data;
    }
}

public class NegativeGoal : Goal
{
    public NegativeGoal(string name, int points) : base(name, -points)
    {
    }

    public override void RecordEvent()
    {
    }

    public override string Display()
    {
        return $"[-] {Name}";
    }
}

public class EternalQuest
{
    private List<Goal> goals = new List<Goal>();
    private int score = 0;
    private int level = 1;

    public void CreateGoal(string goalType, string name, int points, int target = 0, int totalProgress = 0)
    {
        switch (goalType.ToLower())
        {
            case "simple":
                goals.Add(new SimpleGoal(name, points));
                break;
            case "eternal":
                goals.Add(new EternalGoal(name, points));
                break;
            case "checklist":
                goals.Add(new ChecklistGoal(name, points, target));
                break;
            case "progress":
                goals.Add(new ProgressGoal(name, points, totalProgress));
                break;
            case "negative":
                goals.Add(new NegativeGoal(name, points));
                break;
            default:
                throw new ArgumentException("Invalid goal type.");
        }
    }

    public void RecordEvent(string goalName)
    {
        Goal goal = goals.Find(g => g.Name == goalName);
        if (goal == null)
        {
            throw new ArgumentException($"Goal '{goalName}' not found.");
        }
        goal.RecordEvent();
        score += goal.GetPoints();
        CheckLevelUp();
    }

    public void DisplayGoals()
    {
        foreach (Goal goal in goals)
        {
            Console.WriteLine(goal.Display());
        }
    }

    public void DisplayScore()
    {
        Console.WriteLine($"Score: {score}, Level: {level}");
    }

    public void SaveData(string filename = "eternal_quest.json")
    {
        var data = new Dictionary<string, object>
        {
            { "Score", score },
            { "Goals", goals.ConvertAll(g => g.ToDictionary()) },
            { "Level", level }
        };

        string jsonString = JsonSerializer.Serialize(data);
        File.WriteAllText(filename, jsonString);
    }

    public void LoadData(string filename = "eternal_quest.json")
    {
        try
        {
            string jsonString = File.ReadAllText(filename);
            var data = JsonSerializer.Deserialize<Dictionary<string, object>>(jsonString);
            score = ((JsonElement)data["Score"]).GetInt32();

            if (data.ContainsKey("Level"))
            {
                level = ((JsonElement)data["Level"]).GetInt32();
            }
            else
            {
                level = 1;
            }

            var goalJsonElements = ((JsonElement)data["Goals"]).EnumerateArray();
            var goalDataList = new List<object>();

            foreach (var goalElement in goalJsonElements)
            {
                var goalDictionary = JsonSerializer.Deserialize<Dictionary<string, object>>(goalElement.GetRawText());
                goalDataList.Add(goalDictionary);
            }

            goals = goalDataList.ConvertAll(goalData => Goal.FromDictionary((Dictionary<string, object>)goalData));
        }
        catch (FileNotFoundException)
        {
            Console.WriteLine("No saved data found.");
        }
    }

    private void CheckLevelUp()
    {
        if (score >= level * 1000)
        {
            level++;
            Console.WriteLine($"Level Up! You are now level {level}.");
        }
    }
}

public class Program
{
    public static void Main(string[] args)
    {
        // Showing Creativity and Exceeding Requirements:
        // 1. Gamification: Implemented a leveling system where the user's level increases
        //    every time their score reaches a multiple of 1000. A "Level Up!" message is displayed.
        // 2. Additional Goal Type: Introduced "NegativeGoal" where completing it results
        //    in a deduction of points, allowing for tracking and discouraging bad habits.
        EternalQuest quest = new EternalQuest();
        quest.LoadData();

        while (true)
        {
            Console.WriteLine("\nEternal Quest Program");
            Console.WriteLine("1. Create Goal");
            Console.WriteLine("2. Record Event");
            Console.WriteLine("3. Display Goals");
            Console.WriteLine("4. Display Score");
            Console.WriteLine("5. Save and Exit");

            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    Console.Write("Enter goal type (simple, eternal, checklist, progress, negative): ");
                    string goalType = Console.ReadLine();
                    Console.Write("Enter goal name: ");
                    string name = Console.ReadLine();
                    Console.Write("Enter points: ");
                    int points = int.Parse(Console.ReadLine());
                    int target = 0;
                    int totalProgress = 0;
                    if (goalType.ToLower() == "checklist")
                    {
                        Console.Write("Enter target count: ");
                        target = int.Parse(Console.ReadLine());
                    }
                    if (goalType.ToLower() == "progress")
                    {
                        Console.Write("Enter total progress: ");
                        totalProgress = int.Parse(Console.ReadLine());
                    }
                    quest.CreateGoal(goalType, name, points, target, totalProgress);
                    break;
                case "2":
                    Console.Write("Enter goal name to record event: ");
                    string eventName = Console.ReadLine();
                    try
                    {
                        quest.RecordEvent(eventName);
                    }
                    catch (ArgumentException e)
                    {
                        Console.WriteLine(e.Message);
                    }
                    break;
                case "3":
                    quest.DisplayGoals();
                    break;
                case "4":
                    quest.DisplayScore();
                    break;
                case "5":
                    quest.SaveData();
                    return;
                default:
                    Console.WriteLine("Invalid choice.");
                    break;
            }
        }
    }
}