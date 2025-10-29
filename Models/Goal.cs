using SQLite;

namespace GoalTrackerMVVM.Models
{
    public class Goal
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public string Name { get; set; }
        
        public string Motivation { get; set; }
        
        public string Steps { get; set; }
        
        public string TargetDate { get; set; }
        
        public double Progress { get; set; }
    }
}
