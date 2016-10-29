namespace Clarkson.Task
{
    using System;
    using System.Collections.Generic;


    public class Cashmachine : ICashmachine
    {
        static object lockObject = new object();
        readonly ICoinSelecotr coinSelector;
        readonly INoteSelector noteSelector;
        Dictionary<int, int> availableNotes;
        Dictionary<int, int> availableCoins;

        // i always there is a room for improvement
        // instead of passing dictionaries i can think of passing a repository object which will have the functions to save notes and coins 
        // the repository design pattern enable me to save cash / coin in any database such as saving in sql server or no-sql ( mongodb ) or in-memory list
        // i used dictionary because they were quick to demonstrate and this piece of code can be sufficient for first sprint which is a principle of agile 
        // deliver some working code

        public Cashmachine(INoteSelector noteSelecotr, ICoinSelecotr coinSelector, Dictionary<int, int> initialNotes, Dictionary<int, int> initialCoins)
        {
            this.noteSelector = noteSelecotr;
            this.coinSelector = coinSelector;
            this.availableNotes = initialNotes;
            this.availableCoins = initialCoins;
        }

        Dictionary<int, int> getNotes(int amount)
        {
            var returnValue = new Dictionary<int, int>();

            lock (lockObject)
            {
                while (amount != 0)
                {
                    int notes = this.noteSelector.GetNote(amount, this.availableNotes);
                    int remainder = amount % notes;
                    int quotient = amount / notes;
                    if (quotient > this.availableNotes[notes])
                    {
                        quotient -= this.availableNotes[notes];
                        returnValue.Add(notes, this.availableNotes[notes]);
                        this.availableNotes[notes] = 0;
                        quotient *= notes;
                    }
                    else
                    {
                        this.availableNotes[notes] -= quotient;
                        returnValue.Add(notes, quotient);
                        quotient = 0;
                    }

                    amount = remainder + quotient;
                }
            }
            return returnValue;
        }

        Dictionary<int, int> getCoins(int amount)
        {
            var returnValue = new Dictionary<int, int>();

            lock (lockObject)
            {
                while (amount != 0)
                {
                    int coins = this.coinSelector.GetCoin(amount, this.availableCoins);
                    int remainder = amount % coins;
                    int quotient = amount / coins;
                    if (quotient > this.availableCoins[coins])
                    {
                        quotient -= this.availableCoins[coins];
                        returnValue.Add(coins, this.availableCoins[coins]);
                        this.availableCoins[coins] = 0;
                        quotient *= coins;
                    }
                    else
                    {
                        this.availableCoins[coins] -= quotient;
                        returnValue.Add(coins, quotient);
                        quotient = 0;
                    }

                    amount = remainder + quotient;
                }
            }

            return returnValue;
        }

        public DispenseMoney WithdrawCash(decimal amount)
        {
            if (amount <= 0)
            {
                throw ExceptionHelpers.ThrowInvalidAmountException();
            }

            if (amount > HelperMethods.GetBalance(this.availableNotes, this.availableCoins))
            {
                throw ExceptionHelpers.ThrowNotEnoughCashException();
            }
            
            Dictionary<int, int> coins = new Dictionary<int, int>(),
                                 notes = new Dictionary<int, int>();

            Tuple<int, int> parts = amount.GetWholeAndFractionalPart();
            int wholePart = parts.Item1 ,
                fractionalPart = parts.Item2;

            if (fractionalPart > 0)
            {
                coins = this.getCoins(fractionalPart);
            }

            if (wholePart > 0)
            {
                notes = this.getNotes(wholePart);
            }

            return DispenseMoney.CreateDispenseMoneyObject(notes, coins);
        }
    }
}

