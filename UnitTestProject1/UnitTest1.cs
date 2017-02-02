using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Kundeverwaltung;
namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {

        TestContext testContext;

        public TestContext TestContext
        {
            get
            {
                return testContext;
            }

            set
            {
                testContext = value;
            }
        }

        [TestMethod,
            ExpectedException(typeof(ArgumentNullException))]
        public void TestMethod1()
        {
             Neukunde k = new Neukunde(null, "Nigeria", "Osagie-Street", "06994113896", Kundeverwaltung.Intervall.monatlich);
        }

        [TestMethod,
            ExpectedException(typeof(ArgumentNullException))]
        public void TestMethod2()
        {
            Neukunde k = new Neukunde("Marvis", "Nigeria", "Osagie-Street", null, Kundeverwaltung.Intervall.monatlich);
        }

        [TestMethod]
        public void TestMethod3()
        {
            Stammkunde k = new Stammkunde("Karl", "Lagos", "hanalulu", "069910153630", Kundeverwaltung.Intervall.taeglich);
            Assert.AreEqual(k.Rabatt, 0.95f);
        }
    }
}
