using System;
using System.Collections.Generic;

public abstract class Activity
{
    private DateTime _date;
    private int _durationMinutes;

    public DateTime Date => _date;
    public int DurationMinutes => _durationMinutes;

    public Activity(DateTime date, int durationMinutes)
    {
        _date = date;
        _durationMinutes = durationMinutes;
    }

    public abstract double GetDistance();
    public abstract double GetSpeed();
    public abstract double GetPace();

    public virtual string GetSummary()
    {
        return $"{_date.ToString("dd MMM yyyy")} {GetType().Name} ({_durationMinutes} min) - " +
               $"Distance {GetDistance():F1} km, Speed {GetSpeed():F1} kph, Pace: {GetPace():F2} min per km";
    }
}

public class Running : Activity
{
    private double _distanceKm;

    public Running(DateTime date, int durationMinutes, double distanceKm) : base(date, durationMinutes)
    {
        _distanceKm = distanceKm;
    }

    public override double GetDistance()
    {
        return _distanceKm;
    }

    public override double GetSpeed()
    {
        if (DurationMinutes > 0)
        {
            return (_distanceKm / DurationMinutes) * 60;
        }
        return 0;
    }

    public override double GetPace()
    {
        if (_distanceKm > 0)
        {
            return (double)DurationMinutes / _distanceKm;
        }
        return 0;
    }
}

public class Cycling : Activity
{
    private double _speedKph;

    public Cycling(DateTime date, int durationMinutes, double speedKph) : base(date, durationMinutes)
    {
        _speedKph = speedKph;
    }

    public override double GetDistance()
    {
        return (_speedKph * DurationMinutes) / 60.0;
    }

    public override double GetSpeed()
    {
        return _speedKph;
    }

    public override double GetPace()
    {
        double speed = GetSpeed();
        if (speed > 0)
        {
            return 60.0 / speed;
        }
        return 0;
    }
}

public class Swimming : Activity
{
    private const double LapDistanceMeters = 50;
    private int _numLaps;

    public Swimming(DateTime date, int durationMinutes, int numLaps) : base(date, durationMinutes)
    {
        _numLaps = numLaps;
    }

    public override double GetDistance()
    {
        return (_numLaps * LapDistanceMeters) / 1000.0;
    }

    public override double GetSpeed()
    {
        double distanceKm = GetDistance();
        if (DurationMinutes > 0)
        {
            return (distanceKm / DurationMinutes) * 60;
        }
        return 0;
    }

    public override double GetPace()
    {
        double distanceKm = GetDistance();
        if (distanceKm > 0)
        {
            return (double)DurationMinutes / distanceKm;
        }
        return 0;
    }
}

public class Program
{
    public static void Main(string[] args)
    {
        List<Activity> activities = new List<Activity>()
        {
            new Running(new DateTime(2025, 4, 17), 30, 4.8),
            new Cycling(new DateTime(2025, 4, 16), 45, 25.0),
            new Swimming(new DateTime(2025, 4, 15), 20, 40)
        };

        foreach (Activity activity in activities)
        {
            Console.WriteLine(activity.GetSummary());
        }
    }
}