using SQLite;
using CommunityToolkit.Mvvm.ComponentModel;

namespace GoalTrackerMVVM.Models;

public partial class Goal : ObservableObject
{
    private int _id;
    private string _name;
    private string _motivation;
    private string _steps;
    private string _targetDate;
    private double _progress;

    [PrimaryKey, AutoIncrement]
    public int Id
    {
        get => _id;
        set => SetProperty(ref _id, value);
    }

    public string Name
    {
        get => _name;
        set => SetProperty(ref _name, value);
    }

    public string Motivation
    {
        get => _motivation;
        set => SetProperty(ref _motivation, value);
    }

    public string Steps
    {
        get => _steps;
        set => SetProperty(ref _steps, value);
    }

    public string TargetDate
    {
        get => _targetDate;
        set => SetProperty(ref _targetDate, value);
    }

    public double Progress
    {
        get => _progress;
        set => SetProperty(ref _progress, value);
    }
}
