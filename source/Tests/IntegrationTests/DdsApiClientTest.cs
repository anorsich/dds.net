using System;
using System.Diagnostics;
using Dds.Api.Services;
using Dds.Client;
using Dds.Contract;
using NUnit.Framework;

namespace Tests.IntegrationTests
{
    [TestFixture]
    public class DdsApiClientTest
    {
        public string testPbn =
            "\r\n[Dealer \"S\"]\r\n[Deal \"S:AJ3.62.J975.J542 T876.KJT43.KQT.A K9.AQ987.2.K8763 Q542.5.A8643.QT9\"]\r\n[Auction \"S\"]\r\n4C Pass Pass Pass\r\n[Declarer \"South\"]\r\n[Contract \"4C\"]\r\n[Result \"1\"]\r\n[Play \"W\"]\r\nDT D2 DA D5\r\nDK C3 D6 D7\r\n\r\n\r\n";

        [Test]
        public void AppharborSolveGame()
        {
            WithStopwatch(() =>
            {
                var client = new DdsApiClient();
                var response = client.SolveGame(testPbn);
                Assert.NotNull(response);
                Assert.AreEqual(13, response.Tricks.Count);
            });
        }     
        [Test]
        public void AppharborGetCard2()
        {
            WithStopwatch(() =>
            {
                var client = new DdsApiClient();
                var response = client.GetCard(testPbn);
                Assert.NotNull(response);
            });
        }  
        
        [Test]
        public void LocalSolveGame()
        {
            WithStopwatch(() =>
            {
                var service = new SolveGameService();
                var response = (SolveGameResponse) service.Any(new SolveGame() {PBN = testPbn});
                Assert.NotNull(response);
                Assert.AreEqual(13, response.Tricks.Count);
            });
        } 
        [Test]
        public void LocalGetCard()
        {
            WithStopwatch(() =>
            {
                var service = new GetCardService();
                var response = (GetCardResponse) service.Any(new GetCard() {PBN = testPbn});
                Assert.NotNull(response);
            });
        } 
        [Test]
        public void ApphbPlayGame()
        {
            var pbn =
                "\r\n[Deal \"W:A7.J43.AQ6.K9632:KT6542.Q6.J4.A85:QJ983.AT72.KT.T4:.K985.987532.QJ7\"]\r\n[Declarer \"E\"]\r\n[Contract \"3NT\"]\r\n[Play \"S\"]\r\n";

            WithStopwatch(() =>
            {
                var client = new DdsApiClient();
                var response = client.PlayGame(pbn);
                Assert.NotNull(response);
            });

        }

        [Test]
        public void LocalPlayGame()
        {
            var pbn =
                "\r\n[Deal \"W:A7.J43.AQ6.K9632:KT6542.Q6.J4.A85:QJ983.AT72.KT.T4:.K985.987532.QJ7\"]\r\n[Declarer \"E\"]\r\n[Contract \"3NT\"]\r\n[Play \"S\"]\r\n";

            WithStopwatch(() =>
            {
                var service = new PlayGameService();
                var response = (PlayGameResponse)service.Any(new PlayGame() { PBN = pbn });
                Assert.NotNull(response);
            });
        } 

        private void WithStopwatch(Action action)
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            action();
            stopwatch.Stop();
            Assert.Pass("Elspsed: " + stopwatch.ElapsedMilliseconds);
        }
    }
}