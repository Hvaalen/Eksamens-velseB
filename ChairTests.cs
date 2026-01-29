using Microsoft.VisualStudio.TestTools.UnitTesting;
using ChairsLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChairsLib.Tests
{
    [TestClass]
    public class ChairTests
    {
        // --- TEST AF MAX WEIGHT (Grænse er 50) ---

        // Test 1: De værdier der LIGE PRÆCIS skal være lovlige
        [TestMethod]
        [DataRow(50)] // Grænseværdi (Lige på grænsen)
        [DataRow(51)] // Lige over grænsen
        [DataRow(200)] // Langt over grænsen
        public void MaxWeight_SetValidValue_ShouldWork(int weight)
        {
            // Arrange
            Chair chair = new Chair();

            // Act
            chair.MaxWeight = weight;

            // Assert
            Assert.AreEqual(weight, chair.MaxWeight);
        }

        // Test 2: De værdier der LIGE PRÆCIS skal fejle
        [TestMethod]
        [DataRow(49)] // Grænseværdi (Lige under grænsen)
        [DataRow(0)]  // Langt under
        [DataRow(-10)] // Negativ
        public void MaxWeight_SetInvalidValue_ShouldThrowException(int weight)
        {
            // Arrange
            Chair chair = new Chair();

            // Act & Assert
            // Vi forventer at koden kaster en ArgumentOutOfRangeException
            Assert.ThrowsException<ArgumentOutOfRangeException>(() =>
            {
                chair.MaxWeight = weight;
            });
        }

        // --- TEST AF MODEL TEKST (Grænse er 2 tegn) ---

        [TestMethod]
        [DataRow("AB")] // 2 tegn (Grænsen) - Skal virke
        [DataRow("ABC")] // 3 tegn - Skal virke
        public void Model_SetValidModel_ShouldWork(string model)
        {
            // Arrange
            Chair chair = new Chair();

            // Act
            chair.Model = model;

            // Assert
            Assert.AreEqual(model, chair.Model);
        }

        [TestMethod]
        [DataRow("A")]  // 1 tegn - Skal fejle
        [DataRow("")]   // Tom streng - Skal fejle
        public void Model_SetInvalidModel_ShouldThrowException(string model)
        {
            // Arrange
            Chair chair = new Chair();

            // Act & Assert
            // Her forventer vi en ArgumentException (som du definerede i din Chair klasse)
            Assert.ThrowsException<ArgumentException>(() =>
            {
                chair.Model = model;
            });
        }

        // Speciel test for null, da DataRow ikke altid er glad for null i parametre
        [TestMethod]
        public void Model_SetNull_ShouldThrowArgumentNullException()
        {
            Chair chair = new Chair();
            Assert.ThrowsException<ArgumentNullException>(() => chair.Model = null);
        }
    }
}