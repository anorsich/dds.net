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
            "\r\n[Dealer \"S\"]\r\n[Deal \"S:QT3.AK3.AQ8.8764 KJ9.JT8762.JT62. A652.Q.9743.AQT3 874.954.K5.KJ952\"]\r\n[Auction \"S\"]\r\n3NT Pass Pass Pass\r\n[Declarer \"South\"]\r\n[Contract \"3NT\"]\r\n[Result \"1\"]\r\n[Play \"W\"]\r\nHJ HQ H5 H3\r\nD4\r\n\r\n";       
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