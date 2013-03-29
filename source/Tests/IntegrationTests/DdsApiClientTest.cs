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
        public string testPbn = "\r\n[Dealer \"S\"]\r\n[Deal \"S:T762..AT952.JT94 J95.KT74.KJ64.85 AKQ8.Q8632.7.AQ6 43.AJ95.Q83.K732\"]\r\n[Auction \"S\"]\r\n4S Pass Pass Pass\r\n[Declarer \"South\"]\r\n[Contract \"4S\"]\r\n[Result \"9\"]\r\n[Play \"W\"]\r\nC8 C6 CK C4\r\nH4 H2 HA D2\r\nD4 D7 DQ DA\r\nSJ SQ S3 ST\r\nS5 SK S4 S2\r\nS9 SA C2 S6\r\nC5 CA C3 C9\r\nH7 CQ C7 CT\r\nHT H3 H5 S7\r\nD6 H6 D3 CJ\r\nDJ S8 D8 D9\r\nHK HQ H9 D5\r\nDK H8 HJ DT\r\n\r\n\r\n";
       
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