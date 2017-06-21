using System;
using System.IO;
using System.Net.Mime;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WhateTechChallenge.Utils;

namespace WhatTechChallenge.Test
{
    [TestClass]
    public class SourceFileTests
    {
        [TestMethod]
        [ExpectedException(typeof(FileNotFoundException))]
        public void ShouldThrowFileNotFoundEx()
        {
            CsvParser.ParseCustomerBetInCsv("somefile.txt", false);
        }

        [TestMethod]
        public void ShouldReturnBetList()
        {
            var fileName = Environment.CurrentDirectory + "\\settled.csv";
            Console.WriteLine(fileName);
            var list = CsvParser.ParseCustomerBetInCsv(fileName);
            Console.WriteLine(list.Count);
            Assert.IsTrue(list.Count>0);
        }
    }
}
