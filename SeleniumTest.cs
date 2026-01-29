using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChairsLibTests
{
    [TestClass]
    public class ChairTests
    {
        private static IWebDriver _driver;

        // HUSK: Ret portnummeret (5500/5501/etc) så det passer til din Live Server
        private string _url = "http://127.0.0.1:5501/index.html";

        [TestInitialize]
        public void Setup()
        {
            _driver = new ChromeDriver();
            _driver.Manage().Window.Maximize();
        }

        [TestCleanup]
        public void TearDown()
        {
            _driver.Quit();
        }

        [TestMethod]
        public void AddChair_And_Verify_List()
        {
            // 1. Gå til siden
            _driver.Navigate().GoToUrl(_url);

            // Tjek at vi er på den rigtige side
            Assert.AreEqual("Stole Butik", _driver.Title);
            Thread.Sleep(3000); // venter

            // 2. Find input felterne (Model og Vægt)
            IWebElement modelInput = _driver.FindElement(By.Id("modelInput"));
            IWebElement weightInput = _driver.FindElement(By.Id("weightInput"));
            IWebElement createButton = _driver.FindElement(By.Id("createButton"));

            // 3. Indtast data
            string newChairName = "Fed ny stol";

            modelInput.Clear(); 
            modelInput.SendKeys(newChairName);

            Thread.Sleep(3000);
            weightInput.Clear();
            weightInput.SendKeys("150");

            Thread.Sleep(3000); 

            createButton.Click();
            Thread.Sleep(3000);

            // 5. Verificer at stolen er kommet frem i tabellen
            // I stedet for at finde et specifikt output element, henter vi hele sidens tekst
            IWebElement tableBody = _driver.FindElement(By.TagName("tbody"));
            string tableText = tableBody.Text;

            // Tjekker om vores nye navn findes i teksten fra tabellen
            Assert.IsTrue(tableText.Contains(newChairName), "Kunne ikke finde den nye stol i listen");
        }
    }
}