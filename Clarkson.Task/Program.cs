namespace Clarkson.Task
{
    using System.Collections.Generic;
    using static System.Console;

    class Program
    {
        // i have used layered architecture , as there is always a room to improve for large applications 
        // i prefer saperate projects for each domain area i.e. for infrastructure a saperate project or core you can have a saperate project

        static void Main(string[] args)
        {
            var amount = 0m;
            var availableNotes = new Dictionary<int, int>
            {
                [50] = 50,
                [20] = 50,
                [10] = 50,
                [5] = 50,
                [2] = 100,
                [1] = 100
            };

            var availableCoins = new Dictionary<int, int>
            {
                [50] = 100,
                [20] = 100,
                [10] = 100,
                [5] = 100,
                [2] = 100,
                [1] = 100
            };

            var coinSelector = new CoinSelector();
            INoteSelector noteSelector;
            var doYouWantToContinue = true;

            do
            {
                WriteLine($"Available balance : {HelperMethods.GetBalance(availableNotes, availableCoins)}");
                WriteLine("enter amount :");
                amount = decimal.Parse(ReadLine());

                WriteLine("enter 1 for all notes and 2 for 20 notes only : ");
                var noteChoice = int.Parse(ReadLine());

                // i have used a choice variable to select an algo , but of course we can introduce Ioc container 
                // which can be used to swap the algos. i have used constructor injection which enables me to implement the Ioc in future very easily

                noteSelector = ((noteChoice == 1) ? new NoteSelectorAlogrithm1() : (INoteSelector)new NoteSelectorAlogrithm2());

                ICashmachine machine1 = new Cashmachine(noteSelector, coinSelector, availableNotes, availableCoins);
                var result = machine1.WithdrawCash(amount);

                WriteLine("\n\ttransaction complete.");

                result.Display();
                WriteLine($"balance : {HelperMethods.GetBalance(availableNotes, availableCoins)}");

                WriteLine(".................................");

                WriteLine("do you want to with draw more cash ? ");
                WriteLine("press y to continue ");
                char choice = char.Parse(ReadLine());
                doYouWantToContinue = (choice == 'y');
                Clear();
            }
            while (doYouWantToContinue);

        }
    }
}