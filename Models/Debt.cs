

public class Debt {

    public string Creditor {get;set;}
    public decimal TotalAmount {get;set;}
    public decimal AmountPaid {get;set;}

    public decimal RemainingAmount => TotalAmount - AmountPaid;
    public decimal PaymentProgress => TotalAmount == 0 ? 0 : (AmountPaid/TotalAmount) * 100;

    public void MakePayment( decimal amount) {
        AmountPaid += amount;
    }

    public override string ToString()
    {
        return $"{Creditor}: Paid {AmountPaid:C} / {TotalAmount:C} ({PaymentProgress:F1}%)" + (RemainingAmount <= 0 ? "Paid Off!" : "There is still a balance");
    }
}