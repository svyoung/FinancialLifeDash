using System;
using FinancialLifeDash.Services;

class Program {
    
    static void Main(string[] args) {
        Console.WriteLine("Welcome to your personal Financial Life Dashboard");

        var dashboard = new FinanceTracker();

        dashboard.ShowMainMenu();
    }
}