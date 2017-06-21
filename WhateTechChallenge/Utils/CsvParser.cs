using System;
using System.Collections.Generic;
using System.IO;
using WhateTechChallenge.Models;

namespace WhateTechChallenge.Utils
{
    public class CsvParser
    {
        public static List<CustomerBet> ParseCustomerBetInCsv(string fileName, bool settled = false)
        {
            var customers = new List<CustomerBet>();
            if (!File.Exists(fileName)) throw new FileNotFoundException("File not found {0}", fileName);

            var content = File.ReadAllLines(fileName);
            var index = 0;
            foreach (var line in content)
            {
                if (index == 0)
                {
                    index++;
                    continue;
                }

                var data = line.Split(',');

                var customer = new CustomerBet()
                {
                    CustomerId = Convert.ToInt32(data[0]),
                    Event = Convert.ToInt32(data[1]),
                    Participant = Convert.ToInt32(data[2]),
                    Stake = Convert.ToDouble(data[3]),
                    Win = Convert.ToDouble(data[4]),
                    IsSettledBet = settled,
                };

                customers.Add(customer);
            }


            return customers;
        }


        
    }
}
