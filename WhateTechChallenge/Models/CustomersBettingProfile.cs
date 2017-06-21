using System;
using System.Collections.Generic;
using System.Linq;



namespace WhateTechChallenge.Models
{
    public class CustomersBettingProfile
    {

        private readonly IEnumerable<CustomerBet> _unsettledBets;
        private readonly IEnumerable<CustomerBet> _settledBets;

        private class CustomerAverageStake
        {
            public int CustomerId { get; set; }
            public double AverageBet { get; set; }
        }

        private class UnusualBettor
        {
            public int CustomerId { get; set; }
        }

        private List<CustomerAverageStake> _customerAverageStake = new List<CustomerAverageStake>();
        private List<UnusualBettor> _unusualBettors = new List<UnusualBettor>();

        public CustomersBettingProfile(List<CustomerBet> settledBets, List<CustomerBet> unsettledBets)
        {
           if(settledBets ==null) throw new ArgumentException("Please provide settled list");

		   if(unsettledBets ==null) throw new ArgumentException("Please provide unsettled list");

            _settledBets = settledBets;
            _unsettledBets = unsettledBets;

            CustomerBets.AddRange(settledBets);
            DetermineUnUsualBet();

            DetermineIfRisky();

            DetermineAverageStake();

            DetermineTenTimesHigherStake();
            DetermineThirtyTimesHigherStake();

            CustomerBets.AddRange(unsettledBets);

        }

        public void EvaluateBet(CustomerBet bet)
        {
            DetermineIfRisky(bet);

			DetermineAverageStake();
			
			DetermineTenTimesHigherStake(bet);
			DetermineThirtyTimesHigherStake(bet);
        }

        public List<CustomerBet> CustomerBets { get; } = new List<CustomerBet>();

        #region Private Methods

        //Determines if the bet comes from a marked unusuall bettor
        private void DetermineIfRisky()
        {
            foreach (var bet in _unsettledBets)
            {
                DetermineIfRisky(bet);
            }
            
        }

        private void DetermineIfRisky(CustomerBet bet)
        {
            if (_unusualBettors.Exists(x => x.CustomerId == bet.CustomerId)) bet.IsRisky = true;
        }

        private void DetermineAverageStake()
        {
            var customers = from bet in _unsettledBets
                            group bet by new { bet.CustomerId }
                into result
                            select new CustomerAverageStake
                            {
                                CustomerId = result.Key.CustomerId,
                                AverageBet = result.Average(x => x.Stake)
                            };

            _customerAverageStake = customers.ToList();
        }

        private void DetermineTenTimesHigherStake()
        {
            foreach (var bet in _unsettledBets)
            {
                DetermineTenTimesHigherStake(bet);
       
            }
        }

        private void DetermineTenTimesHigherStake(CustomerBet bet)
        {
            
                var averageBet = GetCustomerAverageStake(bet.CustomerId);
                bet.AverageBet = averageBet;
                if (bet.Stake >= averageBet * 10) bet.IsUnusualBet = true;
         
            //CustomerBets.AddRange(_unsettledBets);
        }

        private void DetermineThirtyTimesHigherStake()
        {
            foreach (var bet in _unsettledBets)
            {
                DetermineThirtyTimesHigherStake(bet);
            }

            //CustomerBets.AddRange(_unsettledBets);
        }

        private void DetermineThirtyTimesHigherStake(CustomerBet bet)
        {
            
                var averageBet = GetCustomerAverageStake(bet.CustomerId);
                bet.AverageBet = averageBet;
                if (bet.Stake >= (averageBet * 30) || bet.Win >= 1000) bet.HighlyUnsualBet = true;
            

            //CustomerBets.AddRange(_unsettledBets);
        }

        private double GetCustomerAverageStake(int customerId)
        {
            var customer = _customerAverageStake.Find(c => c.CustomerId == customerId);

            return customer?.AverageBet ?? 0;
        }

        private void DetermineUnUsualBet()
        {
            var betStats = from bet in _settledBets
                group bet by new {bet.CustomerId}
                into result
                select new
                {
                    result.Key.CustomerId,
                    TimesWon = Convert.ToDouble(result.Count(x => x.Win > 0)),
                    NumberOfBets = Convert.ToDouble(result.Count()),

                };

            var unusual = from bet in betStats
                where ((bet.TimesWon / bet.NumberOfBets) * 100) > 60
                group bet by new {bet.CustomerId}
                into result
                select new UnusualBettor { CustomerId = result.Key.CustomerId };

            _unusualBettors = unusual.ToList();
            
            foreach (var bet in _settledBets)
            {
                var ub = _unusualBettors.Find(x => x.CustomerId == bet.CustomerId);
                if(ub!=null) bet.IsUnusualBet = true;
            }
      
        }

        #endregion

    }
}