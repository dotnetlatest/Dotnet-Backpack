using System;
using Backpack.Core.Formatters;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Backpack.Core.Tests
{
    [TestClass]
    public class DateFormatterTests
    {
        [TestMethod]
        public void DateTimeisStrippedOfTimeUnits()
        {
            var sampleDateTime = new DateTime(2015, 1, 1, 12, 00, 0);

            DateTime sut = sampleDateTime.StripTime();

            Assert.AreNotSame(sampleDateTime,sut);
        }
    }
}
