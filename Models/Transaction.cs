

namespace FinancialLifeDash.Models;

public class FinanceTransaction {
    public decimal Amount { get; set; }
    public string Category { get; set; } = "";
    public DateTime Date { get; set; }
    public string Type { get; set; } = ""; // income/expense

    public override string ToString() {
        return $"{Date.ToShortDateString()} - {Type} - {Category} - {Amount:F2}";
    }
}