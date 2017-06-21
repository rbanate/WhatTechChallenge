using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WhateTechChallenge.Models;


namespace WhatTechChallenge.Test
{
    [TestClass]
    public class BetDataTest
    {

        [TestMethod]
        public void MoreThanSixtyPercentWins()
        {
            var bets = new List<CustomerBet>
            {
                new CustomerBet()
                {
                    CustomerId = 1,
                    Event = 1,
                    Participant = 6,
                    Stake = 50,
                    Win = 250
                }
            };

            var customerBettingProfile = new CustomersBettingProfile(bets,new List<CustomerBet>());
            var evaluatedRecord = customerBettingProfile.CustomerBets.FirstOrDefault();
            Assert.IsTrue(evaluatedRecord.IsUnusualBet);
        }

        [TestMethod]
        public void ThousandOrMoreWin()
        {
            var bets = new List<CustomerBet>
            {
                new CustomerBet()
                {
                    CustomerId = 1,
                    Event = 1,
                    Participant = 6,
                    Stake = 50,
                    Win = 1200
                }
            };

            var customerBettingProfile = new CustomersBettingProfile(bets, new List<CustomerBet>());
            var evaluatedRecord = customerBettingProfile.CustomerBets.FirstOrDefault();
            Assert.IsTrue(evaluatedRecord.IsUnusualBet);
        }

        [TestMethod]
        public void TenTimesHigherStake()
        {
            var bets = new List<CustomerBet>();
            bets.Add(new CustomerBet()
            {
                CustomerId = 1,
                Event = 1,
                Participant = 6,
                Stake = 20,
                Win = 200
            });
            bets.Add(new CustomerBet()
            {
                CustomerId = 1,
                Event = 1,
                Participant = 6,
                Stake = 20,
                Win = 200
            });


            var customerBettingProfile = new CustomersBettingProfile(bets, new List<CustomerBet>());


            var tenX = new CustomerBet()
                {
                    CustomerId = 1,
                    Event = 1,
                    Participant = 6,
                    Stake = 200,
                    Win = 0
                };

            customerBettingProfile.EvaluateBet(tenX);
            Assert.IsTrue(tenX.IsUnusualBet);
        }

        [TestMethod]
        public void ThirtyTimesHigherStake()
        {
            var bets = new List<CustomerBet>();
            bets.Add(new CustomerBet()
            {
                CustomerId = 1,
                Event = 1,
                Participant = 6,
                Stake = 20,
                Win = 200
            });
            bets.Add(new CustomerBet()
            {
                CustomerId = 1,
                Event = 1,
                Participant = 6,
                Stake = 20,
                Win = 200
            });


            var customerBettingProfile = new CustomersBettingProfile(bets, new List<CustomerBet>());


            var thirtyX = new CustomerBet()
            {
                CustomerId = 1,
                Event = 1,
                Participant = 6,
                Stake = 600,
                Win = 0
            };

            customerBettingProfile.EvaluateBet(thirtyX);
            Assert.IsTrue(thirtyX.HighlyUnsualBet);
        }
    }
}
