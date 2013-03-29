using Dds.Api.Services;
using Dds.Client;
using Dds.Contract;
using NUnit.Framework;

namespace Tests.IntegrationTests
{
    [TestFixture]
    public class DdsApiClientTest
    {
        public string testPbn2 = "\r\n[Dealer \"S\"]\r\n[Deal \"S:K4.AT974.543.AK3 QJT83.K5.KJ72.Q5 A92.62.A.JT97642 765.QJ83.QT986.8\"]\r\n[Contract \"4NT\"]\r\n[Auction \"S\"]\r\n6C Pass Pass Pass\r\n[Play \"W\"]\r\nSQ SA S5 S4\r\nS9 S6 SK S3\r\nHA H5 H2 H8\r\nHT HK H6 H3\r\nSJ S2 S7 C3\r\n";
        public string testPbn3 = "\r\n[Dealer \"S\"]\r\n[Deal \"S:AJ32.2.A4.QT8754 7654.AJ965.Q9.K3 K9.Q743.8752.AJ6 QT8.KT8.KJT63.92\"]\r\n[Auction \"S\"]\r\n4NT Pass Pass Pass\r\n[Play \"W\"]\r\nS7 S9 SQ SA\r\nS2 S4 SK S8\r\nD2 D3 DA D9\r\nSJ S5 H3 ST\r\nS3 S6 H4 C2\r\nDQ D5 DK D4\r\nDJ H2 H5 D7\r\nDT C4 H6 D8\r\nD6 C5 H9 C6\r\nC9 C7 CK CA\r\nCJ H8 CQ C3\r\nCT HJ H7 HT\r\nC8 HA HQ HK\r\n\r\n\r\n";
        public string testPbn4 = "\r\n[Dealer \"S\"]\r\n[Deal \"S:5.AK873.T98.9843 J643.T4.J65.AQT6 KQT8.Q96.K732.K2 A972.J52.AQ4.J75\"]\r\n[Auction \"S\"]\r\n3H Pass Pass Pass\r\n[Play \"W\"]\r\nS3 ST SA S5\r\nC5 C4 CA C2\r\nDJ D2 D4 D8\r\nD6 DK DA D9\r\nDQ DT D5 D3\r\nC7 C3 C6 CK\r\nSK S2 C8 S4\r\nHQ H2 H3 H4\r\nH9 H5 HK HT\r\nC9 CT H6 CJ\r\nD7 HJ H7 S6\r\nS7 HA SJ S8\r\nH8 CQ SQ S9\r\n\r\n\r\n";
        public string testPbn5 = "\r\n[Dealer \"S\"]\r\n[Deal \"S:T762..AT952.JT94 J95.KT74.KJ64.85 AKQ8.Q8632.7.AQ6 43.AJ95.Q83.K732\"]\r\n[Auction \"S\"]\r\n4S Pass Pass Pass\r\n[Declarer \"South\"]\r\n[Contract \"4S\"]\r\n[Result \"9\"]\r\n[Play \"W\"]\r\nC8 C6 CK C4\r\nH4 H2 HA D2\r\nD4 D7 DQ DA\r\nSJ SQ S3 ST\r\nS5 SK S4 S2\r\nS9 SA C2 S6\r\nC5 CA C3 C9\r\nH7 CQ C7 CT\r\nHT H3 H5 S7\r\nD6 H6 D3 CJ\r\nDJ S8 D8 D9\r\nHK HQ H9 D5\r\nDK H8 HJ DT\r\n\r\n\r\n";
        [Test]
        public void Test2()
        {
            var client = new DdsApiClient();
            var response = client.SolveGame(testPbn3);
            Assert.NotNull(response);
            Assert.AreEqual(5,response.Tricks.Count);
        }  
        
        [Test]
        public void Test6()
        {
            var service = new SolveGameService();
            var response = (SolveGameResponse)service.Any(new SolveGame(){PBN = testPbn5});
            Assert.NotNull(response);
            Assert.AreEqual(13,response.Tricks.Count);
        } 
    }
}