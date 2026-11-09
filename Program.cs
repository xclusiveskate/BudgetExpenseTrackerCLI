
// //// <summary>A CLI application that allow users to  manage their personal finances </summary>
// using System.Xml.Serialization;
using System.Globalization;
using Microsoft.VisualBasic;

Console.WriteLine("=======================================");
Console.WriteLine("\t PERSONAL FINANCE TRACKER");
Console.WriteLine("=======================================");
Console.WriteLine("Welcome to your personal Finance Manager");

int limit = 10;
string[] listOfDescriptions = new string[limit];
decimal[] listOfAmounts = new decimal[limit];
string[] listOfTransactionType = new string[limit];
string[] listOfCategories = new string[limit];
string[] listOfTransactionReference = new string[limit];
int transactionCount = 0;
int totalAmountByType = 0;
decimal myMonthlyBudget = 0;
// double budgetUsed;
// string tip;
string financialStatus = "";

CultureInfo nigeria = new CultureInfo("en-NG");

string choose;



int choice;
do
{
    //displaying options to user
DisplayOptions();


    //accepting input from user
    string? input = Console.ReadLine();

    //parse , tryparse
    if (int.TryParse(input, out choice))
    {

        switch (choice)
        {
            case 1:
                System.Console.WriteLine("You selectd : Add income ");
                AddTransaction(limit, listOfDescriptions, listOfAmounts, listOfCategories, listOfTransactionReference, ref transactionCount, listOfTransactionType, "Income");
                break;
            case 2:
                // System.Console.WriteLine("You  selected:  Record Expense ");
                AddTransaction(limit, listOfDescriptions, listOfAmounts, listOfCategories, listOfTransactionReference, ref transactionCount, listOfTransactionType, "Expense");
                break;
            case 3:
                System.Console.WriteLine("You selected:  View All Transactions ");
                ViewAllTransactions(transactionCount, listOfDescriptions, listOfAmounts, listOfCategories, listOfTransactionReference, listOfTransactionType);
                break;
            case 4:
                System.Console.WriteLine("You selected: View Total Income ");
                ViewTransactionByType(transactionCount, listOfAmounts, listOfTransactionType, totalAmountByType, "Income");
                break;
            case 5:
                ViewTransactionByType(transactionCount, listOfAmounts, listOfTransactionType, totalAmountByType, "Expense");
                System.Console.WriteLine("You selected: View Total Expenses ");
                break;
            case 6:

                decimal totalIncome = ViewTransactionByType(transactionCount, listOfAmounts, listOfTransactionType, totalAmountByType, "Income");
                decimal totalExpense = ViewTransactionByType(transactionCount, listOfAmounts, listOfTransactionType, totalAmountByType, "Expense");
                CheckCurrentBalance(totalIncome, totalExpense, nigeria);
                System.Console.WriteLine("You selcted: Check Current Balance ");

                break;
            case 7:
                System.Console.WriteLine("You selected: Set Monthly Budget ");
               myMonthlyBudget =  SetMonthlyBudget();
                break;
            case 8:
                System.Console.WriteLine("You selected: View Financial Summary ");
                ViewFinancialSummary(listOfAmounts, listOfTransactionType, transactionCount, totalAmountByType, nigeria, myMonthlyBudget, financialStatus);
                break;
            case 9:
                System.Console.WriteLine("You selected: Generate Random Financial Tip");
                GenerateRandomFinancialTip();
                break;
            case 10:
                System.Console.WriteLine("Thanks for banking with us");
                break;

            default:
                System.Console.WriteLine("Invalid Input or selection");
                break;
        }
        Console.WriteLine();
    }
    else
    {
        System.Console.WriteLine("Invalid Input: Enter a valid input");
        choice = 0;
    }

 
    if (choice >= 1 && choice <= 9)
    {
        Console.Write("Do you want to perform another operation? (Y/N): ");
        choose = Console.ReadLine()!.Trim().ToUpper();

        if (choose == "N")
        {
            choice = 10;
        }
    }

} while (choice != 10);











static void DisplayOptions()
{
    System.Console.WriteLine("1.Add Income");
    System.Console.WriteLine("2.Record Expense");
    System.Console.WriteLine("3.View All Transactions");
    System.Console.WriteLine("4.View Total Income");
    System.Console.WriteLine("5.View Total Expenses");
    System.Console.WriteLine("6.Check Current Balance");
    System.Console.WriteLine("7.Set Monthly Budget");
    System.Console.WriteLine("8.View Financial Summary");
    System.Console.WriteLine("9.Generate Random Financial Tip");
    System.Console.WriteLine("10.Exit");
}




//WORKS FOR CASE 1 AND CASE 2
static void AddTransaction(int limit, string[] listOfDescriptions, decimal[] listOfAmounts, string[] listOfCategories, string[] listOfTransactionReferences, ref int transactionCount, string[] listOfTransactionType, string transactionType)
{
    string description;
    decimal amount;
    string category;

    bool isDescriptionInvalid;
    bool isAmountInvalid;
    bool isCategoryInvalid;

    // Prompt the user to enter the income description

    string descriptionPromptMessage = transactionType == "Income" ? "Enter income description: " : "Enter expense description: ";
    string amountPromptMessage = transactionType == "Income" ? "Enter income amount: " : "Enter expense amount: ";
    string categoryPromptMessage = transactionType == "Income" ? "Select income category: " : "Select expense category: ";




    if (transactionCount >= limit)
    {
        System.Console.WriteLine("Transaction Limit reached");
        return;
    }
    do
    {

        System.Console.WriteLine(descriptionPromptMessage);
        description = Console.ReadLine()!;
        isDescriptionInvalid = string.IsNullOrWhiteSpace(description);
        // Check if the input is invalid and display an error message
        if (isDescriptionInvalid)
        {
            System.Console.WriteLine("invalid description.");
        }
        else
        {
            System.Console.WriteLine("Description is valid.");
            System.Console.WriteLine($"description: {description}");
        }
    } while (isDescriptionInvalid);


    // Prompt the user to enter the income amount
    do
    {
        System.Console.WriteLine(amountPromptMessage);
        string? amountInput = Console.ReadLine();
        // Validate the input to ensure it is a valid decimal number greater than zero
        isAmountInvalid = !decimal.TryParse(amountInput, out amount) || amount <= 0;
        // Check if the input is invalid and display an error message
        if (isAmountInvalid)
        {
            System.Console.WriteLine("Invalid amount. Enter a number greater than zero.");
        }
        else
        {
            System.Console.WriteLine("Amount is valid");
            System.Console.WriteLine($"Amount: #{amount:N2}");
        }
    }
    while (isAmountInvalid);


    // Display the available income categories to the user
    System.Console.WriteLine(categoryPromptMessage);
    string[] categories = { "Monthly Salary", "Business", "Freelancing", "Investment", "Gift", "Others" };
    string[] expenseCategories = { "Food & Groceries",
        "Transportation",
        "Rent & Housing",
        "Utilities",
        "Healthcare",
        "Education",
        "Entertainment",
        "Shopping",
        "Others"
 };

    string[] selectedCategories = transactionType == "Income" ? categories : expenseCategories;
    // Prompt the user to select an income category
    do
    {

        // Display the available categories to the user
        for (int i = 0; i < selectedCategories.Length; i++)
        {

            System.Console.WriteLine($"{i + 1}. {selectedCategories[i]}");
        }
        // Prompt the user to enter their choice
        System.Console.WriteLine($"Enter your choice between (1- {selectedCategories.Length}): ");
        string? input = Console.ReadLine();
        isCategoryInvalid = !int.TryParse(input, out int choice) || choice < 1 || choice > selectedCategories.Length;
        // Check if the input is invalid and display an error message
        if (isCategoryInvalid)
        {
            System.Console.WriteLine($"Enter a valid choice between 1 to {(selectedCategories.Length)}");
        }

        // If the input is valid, assign the selected category to the variable
        category = selectedCategories[choice - 1];
        System.Console.WriteLine($"Category: {category}");


    } while (isCategoryInvalid);

    string reference = $"{(transactionType == "Income" ? "INC" : "EXP")}-{transactionCount + 1:D4}";

    // Store the transaction details in the arrays
    listOfDescriptions[transactionCount] = description;
    listOfAmounts[transactionCount] = amount;
    listOfTransactionType[transactionCount] = transactionType;
    listOfCategories[transactionCount] = category;
    listOfTransactionReferences[transactionCount] = reference;
    transactionCount++;


    // Display the recorded transaction details

    System.Console.WriteLine($"{transactionType} transaction recorded successfully");


}



//WORKS FOR CASE 3
static void ViewAllTransactions(int transactionCount, string[] listOfDescriptions, decimal[] listOfAmounts, string[] listOfCategories, string[] listOfTransactionReferences, string[] listOfTransactionType)
{
    CultureInfo nigeria = new CultureInfo("en-NG");

    //check if the whether any transaction has been recorced or not 
    if (transactionCount == 0)
    {
        System.Console.WriteLine("No transactions to display.");
    }
    else
    {
        System.Console.WriteLine("All Transactions:");
        for (var i = 0; i < transactionCount; i++)
        {

            System.Console.WriteLine($"Transaction #{i + 1}:");
            System.Console.WriteLine($"Reference: {listOfTransactionReferences[i]}");
            System.Console.WriteLine($"Description: {listOfDescriptions[i]}");
            System.Console.WriteLine($"Amount: {listOfAmounts[i].ToString("C", nigeria)}");
            System.Console.WriteLine($"Category: {listOfCategories[i]}");
            System.Console.WriteLine($"Type: {listOfTransactionType[i]}");
            System.Console.WriteLine();
        }
    }
}




//THIS WORKS FOR CASE 4 AND 5
static decimal ViewTransactionByType(int count, decimal[] listofAmount, string[] listOfTransactionType, decimal total, string transactionType)
{

    if (count == 0)
    {
        System.Console.WriteLine("No transactions recorded");
        return total;
    }
    else
    {
        for (var i = 0; i < count; i++)
        {
            if (listOfTransactionType[i] == transactionType)
            {
                total += listofAmount[i];
            }


        }




    }
    return total;
}




//THIS WORKS FOR CASE 6: CHECK CURRENT BALANCE


static void CheckCurrentBalance(decimal totalIncome, decimal totalExpense, CultureInfo nigeria)
{
    decimal currentBalance = totalIncome - totalExpense;

    System.Console.WriteLine($"Current Balance: {currentBalance.ToString("C", nigeria)}");
}


//THIS WORKS FOR CASE 7: SET MONTHLY BUDGET

static decimal SetMonthlyBudget()
{
    decimal monthlyBudget;
    bool isBudgetInvalid;


    System.Console.WriteLine("Enter monthly budget");
    do
    {

        string? input = Console.ReadLine();
        isBudgetInvalid = !decimal.TryParse(input, out monthlyBudget) || monthlyBudget <= 0;


        if (isBudgetInvalid)
        {
            System.Console.WriteLine("Enter a valid amount");
        }

    } while (isBudgetInvalid);

   return monthlyBudget;

}




//Monitor monthly budget and alert user if they exceed their budget
static double MonitorMonthlyBudget(decimal totalExpense, decimal monthlyBudget, CultureInfo nigeria, string financialStatus)
{
    decimal remainingBudget = monthlyBudget - totalExpense;
    double budget;

    if (monthlyBudget > 0)
    {
        budget = (double)(totalExpense / monthlyBudget);
    }
    else
    {

        System.Console.WriteLine("Monthly budget is not set. Please set a monthly budget first.");
        return 0;
    }

    System.Console.WriteLine($"Monthly Budget: {monthlyBudget.ToString("C2", nigeria)}");
    System.Console.WriteLine($"Total Expense: {totalExpense.ToString("C2", nigeria)}");
    if (totalExpense > monthlyBudget)
    {
        financialStatus = "Budget Exceeded";
    }
    else if (totalExpense == monthlyBudget)
    {
        financialStatus = "Budget Exhausted";
    }
    else
    {
        financialStatus = "Within Budget";
    }
    return budget;
}

//THIS WORKS FOR CASE 8: View Financial Summary


static void ViewFinancialSummary(decimal[] listOfAmounts, string[] listOfTransactionType, int transactionCount, int totalAmountByType, CultureInfo nigeria, decimal myMonthlyBudget, string financialStatus)
{


    System.Console.WriteLine("======== FINANCIAL SUMMARY ========");
    // System.Console.WriteLine("\n");
    decimal totalIncome = ViewTransactionByType(transactionCount, listOfAmounts, listOfTransactionType, totalAmountByType, "Income");
    // System.Console.WriteLine(totalIncome);
    decimal totalExpense = ViewTransactionByType(transactionCount, listOfAmounts, listOfTransactionType, totalAmountByType, "Expense");
    // System.Console.WriteLine(totalExpense);
    CheckCurrentBalance(totalIncome, totalExpense, nigeria);

    System.Console.WriteLine("\n");
    System.Console.WriteLine($"Monthly Budget: {myMonthlyBudget.ToString("C2", nigeria)}");

    double budgetMonitor = MonitorMonthlyBudget(totalExpense, myMonthlyBudget, nigeria, financialStatus);
    if (budgetMonitor > 0)
    {
        System.Console.WriteLine($"Budget Used: {budgetMonitor:P}");

    }


    System.Console.WriteLine("\n");
    System.Console.WriteLine($"Financial Status: {financialStatus}");


    System.Console.WriteLine("======================================");
}



//THIS WORKS FOR CASE 9: Generate Random Financial Tip
static void GenerateRandomFinancialTip()
{
    string[] tips = {
    "Create a budget and stick to it.",
    "Track your expenses to identify areas where you can save.",
    "Set financial goals and work towards achieving them.",
    "Avoid unnecessary debt and pay off existing debts.",
    "Save for emergencies and unexpected expenses.",
    "Invest wisely for long-term financial growth.",
    "Review your financial situation regularly.",
    "Cut back on discretionary spending.",
    "Take advantage of discounts and deals when shopping.",
    "Educate yourself about personal finance and money management."
};
    Random random = new Random();
    int index = random.Next(1, tips.Length + 1);

    System.Console.WriteLine($"Financial Tip: tips[index];");
}


