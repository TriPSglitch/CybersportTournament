using CybersportTournament;
using CybersportTournament.AddWindows;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace UnitTests
{
    [TestClass]
    public class UnitTest
    {
        [TestMethod]
        public void TestRegistration()
        {
            var page = new RegistrationWindow();

            Assert.IsTrue(page.Registration("TriPS1", "123", "Максим", "Карбышев", "Евгеньевич", "kmacksim@yandex.ru"));
            Assert.IsFalse(page.Registration("", "123", "Максим", "Карбышев", "Евгеньевич", "kmacksim@yandex.ru"));
            Assert.IsFalse(page.Registration("TriPS1", "", "Максим", "Карбышев", "Евгеньевич", "kmacksim@yandex.ru"));
            Assert.IsFalse(page.Registration("TriPS1", "123", "", "Карбышев", "Евгеньевич", "kmacksim@yandex.ru"));
            Assert.IsFalse(page.Registration("TriPS1", "123", "Максим", "", "Евгеньевич", "kmacksim@yandex.ru"));
            Assert.IsTrue(page.Registration("TriPS1", "123", "Максим", "Карбышев", "", "kmacksim@yandex.ru"));
            Assert.IsFalse(page.Registration("TriPS1", "123", "Максим", "Карбышев", "Евгеньевич", ""));
            Assert.IsFalse(page.Registration("TriPS1", "123", "Максим", "Карбышев", "Евгеньевич", "15463"));
        }

        [TestMethod]
        public void TestAuthorization()
        {
            var page = new AuthorizationWindow();

            Assert.IsTrue(page.Authorization("TriPS", "123"));
            Assert.IsFalse(page.Authorization("", "123"));
            Assert.IsFalse(page.Authorization("TriPS", ""));
            Assert.IsFalse(page.Authorization("TriPS", "1156415"));
        }

        [TestMethod]
        public void TestMatchPeriod()
        {
            var page = new AddRoundWindow();
            TimeSpan matchPeriod = new TimeSpan(0, 0, 0);
            TimeSpan result = new TimeSpan(1, 30, 00);

            Assert.AreEqual(page.MatchPeriod("1:30", matchPeriod), result);
        }

        [TestMethod]
        public void TestMatchResult()
        {
            var page = new AddRoundWindow();

            Assert.AreEqual(page.MatchResult("16:0", "0:0"), "1:0");
            Assert.AreEqual(page.MatchResult("0:16", "0:0"), "0:1");
            Assert.AreEqual(page.MatchResult("16:16", "0:0"), "ошибка");
            Assert.AreEqual(page.MatchResult("16:0", "1:0"), "2:0");
            Assert.AreEqual(page.MatchResult("0:16", "1:0"), "1:1");
            Assert.AreEqual(page.MatchResult("16:0", "0:1"), "1:1");
            Assert.AreEqual(page.MatchResult("0:16", "0:1"), "0:2");
        }

        [TestMethod]
        public void TestHash()
        {
            Assert.AreEqual(Encrypt.Hash("123"), "5fa285e1bebe0a6623e33afc04a1fbd5");
        }
    }
}