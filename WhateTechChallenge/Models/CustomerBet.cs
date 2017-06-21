using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhateTechChallenge.Models
{

    public class CustomerBet
    {
        public int CustomerId { get; set; }
        public int Event { get; set; }
        public int Participant { get; set; }
        public double Stake { get; set; }
        public double Win { get; set; }
        public bool IsSettledBet { get; set; }
        public bool IsUnusualBet { get; set; }
        public bool HighlyUnsualBet { get; set; }

        public bool IsRisky { get; set; }

        public double AverageBet { get; set; }
    }

    public class Customer
    {
        public int CustomerId { get; set; }

        public decimal AverageBet { get; set; }

        public List<CustomerBet> TenTimesHigherBets { get; set; }

        public List<CustomerBet> ThirtyTimesHigherBets { get; set; }
    }
}
