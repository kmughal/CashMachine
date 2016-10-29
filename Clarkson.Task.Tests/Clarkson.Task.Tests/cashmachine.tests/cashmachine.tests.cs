namespace Clarkson.Task.Tests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System.Collections.Generic;

    // although more tests can be written and more robust code can be written using Moq and other related ideas 
    // but my intension is to describe that i am able to write tests 

    // best pattern would be having saperate tests even for individual algos even.

    [TestClass]
    public class CashmachineTests
    {
        [TestMethod]
        [ExpectedException(typeof(InvalidAmountException))]
        public void WhenZeroAmountIsEnteredThenItShouldThrowInvalidAmountException()
        {
            var cashMachine = new Cashmachine(null, null, null, null);
            cashMachine.WithdrawCash(0.0m);
        }

        [TestMethod]
        [ExpectedException(typeof(NotEnoughtCashException))]
        public void WhenMoreAmountIsRequestedToWithDrawThenAvailableBalanceThenItShouldThrowNotEnoughCashException()
        {
            var notes = new Dictionary<int, int>();
            notes.Add(50, 2);
            notes.Add(20, 10);
            notes.Add(10, 0);

            var coins = new Dictionary<int, int>();
            var cashMachine = new Cashmachine(null, null, notes, coins);
            cashMachine.WithdrawCash(2000.23m);
        }


        [TestMethod]        
        public void WhenAmountIsWithDrawnThenCashmachineShouldReturnCorrectNotesAndCoinsPairs()
        {
            INoteSelector noteSelector = new NoteSelectorAlogrithm1();
            ICoinSelecotr coinSelector = new CoinSelector();
            var availableNotes = new Dictionary<int, int> {
                                    { 50, 2},
                                    {20, 2 },
                                    {10, 0 },
                                    {5, 0 },
                                    {2, 0 },
                                    {1, 0 }
                                };

            var availableCoins = new Dictionary<int, int> {
                                    { 50, 10},
                                    {20, 10 },
                                    {10, 0 },
                                    {5, 5 },
                                    {2, 5 },
                                    {1, 5 }
                                };

            
            var cashMachine = new Cashmachine(noteSelector, coinSelector, availableNotes, availableCoins);
            var result = cashMachine.WithdrawCash(120.28m);
            
            Assert.AreEqual(result.Notes[50], 2);
            Assert.AreEqual(result.Notes[20], 1);

            Assert.AreEqual(result.Coins[20], 1);
            Assert.AreEqual(result.Coins[5], 1);
            Assert.AreEqual(result.Coins[2], 1);
            Assert.AreEqual(result.Coins[1], 1);

        }


        [TestMethod]
        public void WhenAlgo1IsUseThenCashmachineShouldUse50NotesIfAvailable()
        {
            INoteSelector noteSelector = new NoteSelectorAlogrithm1();
            ICoinSelecotr coinSelector = new CoinSelector();
            var availableNotes = new Dictionary<int, int> {
                                    { 50, 2},
                                    {20, 2 },
                                    {10, 0 },
                                    {5, 0 },
                                    {2, 0 },
                                    {1, 0 }
                                };

            var availableCoins = new Dictionary<int, int> {
                                    { 50, 10},
                                    {20, 10 },
                                    {10, 0 },
                                    {5, 5 },
                                    {2, 5 },
                                    {1, 5 }
                                };


            var cashMachine = new Cashmachine(noteSelector, coinSelector, availableNotes, availableCoins);
            var result = cashMachine.WithdrawCash(120.28m);

            Assert.AreEqual(result.Notes[50], 2);
            Assert.AreEqual(result.Notes[20], 1);

            Assert.AreEqual(result.Coins[20], 1);
            Assert.AreEqual(result.Coins[5], 1);
            Assert.AreEqual(result.Coins[2], 1);
            Assert.AreEqual(result.Coins[1], 1);

        }

        [TestMethod]
        public void WhenAlgo1IsUseThenCashmachineShouldUse20NotesIfAvailable()
        {
            INoteSelector noteSelector = new NoteSelectorAlogrithm2();
            ICoinSelecotr coinSelector = new CoinSelector();
            var availableNotes = new Dictionary<int, int> {
                                    { 50, 2},
                                    {20, 5 },
                                    {10, 1 },
                                    {5, 2 },
                                    {2, 100 },
                                    {1, 100 }
                                };

            var availableCoins = new Dictionary<int, int> {
                                    { 50, 10},
                                    {20, 10 },
                                    {10, 0 },
                                    {5, 5 },
                                    {2, 5 },
                                    {1, 5 }
                                };

            var cashMachine = new Cashmachine(noteSelector, coinSelector, availableNotes, availableCoins);
            var result = cashMachine.WithdrawCash(120.28m);

            Assert.AreEqual(result.Notes[20], 5);
            Assert.AreEqual(result.Notes[10], 1);
            Assert.AreEqual(result.Notes[5], 2);

            Assert.AreEqual(result.Coins[20], 1);
            Assert.AreEqual(result.Coins[5], 1);
            Assert.AreEqual(result.Coins[2], 1);
            Assert.AreEqual(result.Coins[1], 1);

        }


        [TestMethod]
        public void WhenWithdrawCashThenAvailableBalanceMustBeUpdated() {

            INoteSelector noteSelector = new NoteSelectorAlogrithm1();
            ICoinSelecotr coinSelector = new CoinSelector();
            var availableNotes = new Dictionary<int, int> {
                                    { 50, 2},
                                    {20, 5 },
                                    {10, 1 },
                                    {5, 1 },
                                    {2, 1 },
                                    {1, 1 }
                                };

            var availableCoins = new Dictionary<int, int> {
                                    { 50, 1},
                                    {20, 1 },
                                    {10, 1 },
                                    {5, 1 },
                                    {2, 1 },
                                    {1, 1 }
                                };

            decimal beforeWithdrawBalance = (HelperMethods.GetBalance(availableNotes, availableCoins));
            var cashMachine = new Cashmachine(noteSelector, coinSelector, availableNotes, availableCoins);
            var result = cashMachine.WithdrawCash(120.28m);
            decimal afterWithdrawBalance = (HelperMethods.GetBalance(availableNotes, availableCoins));

            Assert.AreEqual(218.88m, beforeWithdrawBalance);
            Assert.AreEqual(98.6m, afterWithdrawBalance);
        }

    }
}
