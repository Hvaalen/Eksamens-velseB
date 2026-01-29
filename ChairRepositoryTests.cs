using Microsoft.VisualStudio.TestTools.UnitTesting;
using ChairsLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChairsLib.Tests
{
    [TestClass()]
    public class ChairRepositoryTests
    {
        // gemmer repository til brug i tests
        private ChairRepository _repository;

        [TestInitialize]
        public void Init()
        {
            // initialiserer repository før hver test og nulstiller data. 
            _repository = new ChairRepository();
        }

        [TestMethod()]
        public void GetAllTest()
        { 
            _repository.Add(new Chair(0, "TestModel1", 50, true));
            List<Chair> result; 
        }

        [TestMethod()]
        public void GetAllTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void GetByIdTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void AddTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void RemoveTest()
        {
            Assert.Fail();
        }
    }
}

