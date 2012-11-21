using System;
using System.Collections.Generic;
using NUnit.Framework;
using System.Linq;
using GF.Data;
using System.Text;
using System.Threading.Tasks;
using GF.Data.Models;

namespace GF.Tests
{
    [TestFixture]
    public class SimpleTest : TestBase
    {
          
        [Test]
        public void Test1() 
        { 
            var repo = Container.GetInstance<IGFRepository>();
            var rolls = repo.GetOrderRolls(1);
            Console.WriteLine("ROLLS " + rolls.Count);
            Assert.IsTrue(rolls.Count > 0);
 
        }
    }
}
