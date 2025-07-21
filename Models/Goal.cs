public class Goal {
    public string Name { get; set;}
    public decimal TargetAmount {get; set;}
    public decimal SavedAmount {get;set;}

    public decimal ProgressPercentage => TargetAmount == 0 ? 0 : (SavedAmount / TargetAmount) * 100;
    public bool IsComplete => SavedAmount >= TargetAmount;
    
    public void AddToGoal(decimal amount) {
        SavedAmount += amount;
    }

    public override string ToString()
    {
        return $"{Name}: {SavedAmount:C} / {TargetAmount:C} ({ProgressPercentage:F1}%)" + (IsComplete ? "Completed" : "");
    }
}