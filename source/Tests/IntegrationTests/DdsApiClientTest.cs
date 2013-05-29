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
            "\r\n[Dealer \"S\"]\r\n[Deal \"S:87.AKT72.AT3.754 53.954.J4.KQT862 AKQT64.Q.Q6.AJ93 J92.J863.K98752.\"]\r\n[Auction \"S\"]\r\n6NT Pass Pass Pass\r\n[Declarer \"South\"]\r\n[Contract \"6NT\"]\r\n[Result \"1\"]\r\n[Play \"W\"]\r\nCK CA D2 C5\r\nSA\r\n\r\n";

        [Test]
        public void Appharbor()
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
        public void Appharbor2()
        {
            WithStopwatch(() =>
            {
                var client = new DdsApiClient();
                var response = client.GetCard(testPbn);
                Assert.NotNull(response);
            });
        }  
        
        [Test]
        public void Local()
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
        public void Local2()
        {
            WithStopwatch(() =>
            {
                var service = new GetCardService();
                var response = (GetCardResponse) service.Any(new GetCard() {PBN = testPbn});
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