using GoalTrackerMVVM.Models;

namespace GoalTrackerMVVM.BusinessLogic;

/// <summary>
/// Interface for goal business logic operations
/// </summary>
public interface IGoalService
{
    /// <summary>
    /// Get all goals
    /// </summary>
    Task<List<Goal>> GetAllGoalsAsync();

    /// <summary>
    /// Get a specific goal by ID with validation
    /// </summary>
    Task<Goal> GetGoalByIdAsync(int id);

    /// <summary>
    /// Create a new goal with validation
    /// </summary>
    Task<int> CreateGoalAsync(Goal goal);

    /// <summary>
    /// Update an existing goal with validation
    /// </summary>
    Task<int> UpdateGoalAsync(Goal goal);

    /// <summary>
    /// Delete a goal with validation
    /// </summary>
    Task<int> DeleteGoalAsync(Goal goal);

    /// <summary>
    /// Validate goal data
    /// </summary>
    (bool IsValid, string ErrorMessage) ValidateGoal(Goal goal);

    /// <summary>
    /// Calculate initial progress based on target date
    /// </summary>
    double CalculateInitialProgress(DateTime targetDate);

    /// <summary>
    /// Round progress to nearest 5%
    /// </summary>
    double RoundProgressToNearestFivePercent(double progress);
}
