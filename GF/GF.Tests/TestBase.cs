using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using System.Threading.Tasks;
using GF.Data;
using StructureMap;
using GF.Test.DependencyResolution;

namespace GF.Tests
{
    public class TestBase
    {

        public IContainer Container;
        
      [TestFixtureSetUp]
      public void Init()
      {
          Container = IoC.Initialize();
      }
    }
}
