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
            "\r\n[Dealer \"S\"]\r\n[Deal \"S:2.AKT62.JT74.765 JT5.J98.A6532.43 AK98.Q7.98.AKJ98 Q7643.543.KQ.QT2\"]\r\n[Auction \"S\"]\r\n4NT Pass Pass Pass\r\n[Declarer \"South\"]\r\n[Contract \"4NT\"]\r\n[Result \"0\"]\r\n[Play \"W\"]\r\nD3\r\n\r\n";
       
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