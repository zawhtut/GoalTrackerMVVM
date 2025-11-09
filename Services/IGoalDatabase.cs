using GoalTrackerMVVM.Models;

namespace GoalTrackerMVVM.Services;

/// <summary>
/// Interface for Goal database operations
/// </summary>
public interface IGoalDatabase
{
    /// <summary>
    /// Get all goals from the database
    /// </summary>
    Task<List<Goal>> GetGoalsAsync();

    /// <summary>
    /// Get a specific goal by ID
    /// </summary>
    Task<Goal> GetGoalAsync(int id);

    /// <summary>
    /// Save (insert or update) a goal
    /// </summary>
    Task<int> SaveGoalAsync(Goal goal);

    /// <summary>
    /// Delete a goal from the database
    /// </summary>
    Task<int> DeleteGoalAsync(Goal goal);
}
