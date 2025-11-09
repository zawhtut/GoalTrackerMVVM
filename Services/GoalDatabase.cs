namespace GoalTrackerMVVM.Services;

public class GoalDatabase : IGoalDatabase
{
    private SQLiteAsyncConnection _database;

    public GoalDatabase()
    {
    }

    private async Task Init()
    {
        if (_database is not null)
            return;

        _database = new SQLiteAsyncConnection(Constants.DatabasePath, Constants.Flags);
        await _database.CreateTableAsync<Goal>();
    }

    public async Task<List<Goal>> GetGoalsAsync()
    {
        await Init();
        return await _database.Table<Goal>().ToListAsync();
    }

    public async Task<Goal> GetGoalAsync(int id)
    {
        await Init();
        return await _database.Table<Goal>().Where(i => i.Id == id).FirstOrDefaultAsync();
    }

    public async Task<int> SaveGoalAsync(Goal goal)
    {
        await Init();
        if (goal.Id != 0)
        {
            return await _database.UpdateAsync(goal);
        }
        else
        {
            return await _database.InsertAsync(goal);
        }
    }

    public async Task<int> DeleteGoalAsync(Goal goal)
    {
        await Init();
        return await _database.DeleteAsync(goal);
    }
}
