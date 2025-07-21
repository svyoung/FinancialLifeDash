using System;
using FinancialLifeDash.Models;
using System.Collections.Generic;
using System.Text.Json;

namespace FinancialLifeDash.Services;

public class FinanceTracker {

    private DataStore data;

    private const string DataFile = "finance_data.json";

    public FinanceTracker()
    {
        LoadData();
    }
    public void ShowMainMenu() {
        while(true) {
            Console.WriteLine("\n==== Main Menu ====");
            Console.WriteLine("1. Add Income");
            Console.WriteLine("2. Add Expense");
            Console.WriteLine("3. View Transaction");
            Console.WriteLine("4. Manage Goals");
            Console.WriteLine("5. Manage Debts");
            Console.WriteLine("Exit");


            Console.Write("Please select an option: ");
            var userInput = Console.ReadLine();

            switch(userInput) {
                case "1": 
                    AddTransaction("Income");
                    break;
                case "2":
                    AddTransaction("Expense");
                    break;
                case "3":
                    ViewTransactions();
                    break;
                case "4":
                    ShowGoalMenu();
                    break;
                case "5":
                    ShowDebtMenu();
                    break;
                case "6":
                    SaveData();
                    Console.WriteLine("Data saved. Goodbye");
                    break;
                default:
                    Console.WriteLine("Invalid selection");
                    break;
            }
        }
    }

    private void AddTransaction(string type) {
        Console.Write($"{type} Amount: ");
        decimal amount;

        while(!decimal.TryParse(Console.ReadLine(), out amount)) {
            Console.WriteLine("Invalid entry. Please enter a valid number.");
        }

        Console.Write("Category: ");
        var category = Console.ReadLine();

        data.Transactions.Add(new FinanceTransaction {
            Amount = amount,
            Category = category ?? "",
            Date = DateTime.Now,
            Type = type
        });

        Console.WriteLine($"{type} added.");
    }

    private void ViewTransactions() {
        Console.WriteLine("\n---- Transactions ----");
        foreach(var tx in data.Transactions) {
            Console.WriteLine(tx);
        }
    }

    // GOALS MENU

    private void ShowGoalMenu() {
        while(true) {
            Console.WriteLine("\n--- Goals ---");
            Console.WriteLine("1. Add Goal");
            Console.WriteLine("2. View Goals");
            // Console.WriteLine("3. Contribute to Goal");
            Console.WriteLine("3. Back");

            Console.Write("Select an option: ");
            var input = Console.ReadLine();

            switch (input) {
                case "1": AddGoal(); break;
                case "2": ViewGoals(); break;
                // case "3": ContributeToGoal(); break;
                case "3": return;
                default: Console.WriteLine("Invalid selection"); break;
            }
        }
    }

    private void AddGoal() {
        Console.Write("Goal name: ");
        var goalName = Console.ReadLine() ?? "";

        Console.Write("Target amount: ");
        if(!decimal.TryParse(Console.ReadLine(), out var goalAmount)) {
            Console.WriteLine("Invalid amount.");
            return;
        }

        data.Goals.Add(new Goal { Name = goalName, TargetAmount = goalAmount});
        Console.WriteLine("Goal added.");
    }

    private void ViewGoals() {
        foreach(var goal in data.Goals) {
            Console.WriteLine(goal);
        }
    }

    // DEBTS MENU
    private void ShowDebtMenu()
    {
        while (true)
        {
            Console.WriteLine("\n--- Debts ---");
            Console.WriteLine("1. Add Debt");
            Console.WriteLine("2. View Debts");
            Console.WriteLine("3. Make Payment");
            Console.WriteLine("4. Back");

            Console.Write("Choose: ");
            var input = Console.ReadLine();

            switch (input)
            {
                case "1": AddDebt(); break;
                case "2": ViewDebts(); break;
                case "3": PayDebt(); break;
                case "4": return;
                default: Console.WriteLine("Invalid."); break;
            }
        }
    }

    private void AddDebt()
    {
        Console.Write("Creditor name: ");
        var creditor = Console.ReadLine();

        Console.Write("Enter debt amount: ");
        if(!decimal.TryParse(Console.ReadLine(), out var debtAmount)) {
            Console.Write("Invalid amount.");
            return;
        }

        data.Debts.Add(new Debt { Creditor = creditor ?? "", TotalAmount = debtAmount });
        Console.WriteLine("Debt added.");
    }

    private void ViewDebts()
    {
        if (data.Debts.Count == 0)
        {
            Console.WriteLine("No debts to view.");
            return;
        }

        foreach (var debt in data.Debts)
            Console.WriteLine(debt);
    }

    private void PayDebt() {
        ViewDebts();
        Console.WriteLine("Creditor to pay: ");
        var creditor = Console.ReadLine();

        var debt = data.Debts.FirstOrDefault(debt => debt.Creditor.Equals(creditor, StringComparison.OrdinalIgnoreCase));
        if(debt == null) {
            Console.WriteLine("No debt found."); return;
        }

        Console.WriteLine("Enter amount to pay: ");
        if(!decimal.TryParse(Console.ReadLine(), out var amountPay)) {
            Console.WriteLine("Invalid amount."); return;
        }

        debt.MakePayment(amountPay);
        Console.WriteLine("Payment recorded.");
    }



    // Save / Load data

    private void SaveData() {
        var options = new JsonSerializerOptions { WriteIndented = true };
        File.WriteAllText(DataFile, JsonSerializer.Serialize(data, options));
    }

    private void LoadData() {
        if(File.Exists(DataFile)) {
            var json = File.ReadAllText(DataFile);
            data = JsonSerializer.Deserialize<DataStore>(json) ?? new DataStore();
        } else { data = new DataStore(); }
    }
}