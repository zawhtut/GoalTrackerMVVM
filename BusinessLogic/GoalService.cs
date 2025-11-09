namespace GoalTrackerMVVM.BusinessLogic;

/// <summary>
/// Business logic service for goal operations
/// Handles validation, business rules, and coordinates between UI and data layers
/// </summary>
public class GoalService : IGoalService
{
    private readonly IGoalDatabase _database;

    public GoalService(IGoalDatabase database)
    {
        _database = database;
    }

    public async Task<List<Goal>> GetAllGoalsAsync()
    {
        var goals = await _database.GetGoalsAsync();
        
        // Business logic: Sort goals by progress (descending) for better UX
        return goals.OrderByDescending(g => g.Progress).ToList();
    }

    public async Task<Goal> GetGoalByIdAsync(int id)
    {
        if (id <= 0)
            throw new ArgumentException("Invalid goal ID", nameof(id));

        var goal = await _database.GetGoalAsync(id);
        
        if (goal is null)
            throw new InvalidOperationException($"Goal with ID {id} not found");

        return goal;
    }

    public async Task<int> CreateGoalAsync(Goal goal)
    {
        var (isValid, errorMessage) = ValidateGoal(goal);
        
        if (!isValid)
            throw new ArgumentException(errorMessage);

        // Business logic: Set ID to 0 to ensure new record
        goal.Id = 0;

        return await _database.SaveGoalAsync(goal);
    }

    public async Task<int> UpdateGoalAsync(Goal goal)
    {
        var (isValid, errorMessage) = ValidateGoal(goal);
        
        if (!isValid)
            throw new ArgumentException(errorMessage);

        if (goal.Id <= 0)
            throw new ArgumentException("Cannot update goal without valid ID");

        return await _database.SaveGoalAsync(goal);
    }

    public async Task<int> DeleteGoalAsync(Goal goal)
    {
        if (goal is null)
            throw new ArgumentNullException(nameof(goal));

        if (goal.Id <= 0)
            throw new ArgumentException("Cannot delete goal without valid ID");

        return await _database.DeleteGoalAsync(goal);
    }

    public (bool IsValid, string ErrorMessage) ValidateGoal(Goal goal)
    {
        if (goal is null)
            return (false, "Goal cannot be null");

        if (string.IsNullOrWhiteSpace(goal.Name))
            return (false, "Goal name is required");

        if (goal.Name.Length > 100)
            return (false, "Goal name must be 100 characters or less");

        if (goal.Progress < 0 || goal.Progress > 1)
            return (false, "Progress must be between 0 and 1");

        return (true, string.Empty);
    }

    public double CalculateInitialProgress(DateTime targetDate)
    {
        var today = DateTime.Today;
        var daysUntilTarget = (targetDate - today).Days;

        double initialProgress;

        // Business rule: closer dates have lower initial progress
        if (daysUntilTarget <= 0)
            initialProgress = 1.0;
        else if (daysUntilTarget <= 30)
            initialProgress = 0.8;
        else if (daysUntilTarget <= 90)
            initialProgress = 0.5;
        else if (daysUntilTarget <= 180)
            initialProgress = 0.3;
        else
            initialProgress = 0.1;

        return RoundProgressToNearestFivePercent(initialProgress);
    }

    public double RoundProgressToNearestFivePercent(double progress)
    {
        // Round to nearest 5% (0.05)
        return Math.Round(progress * 20) / 20;
    }
}
