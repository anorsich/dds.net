using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dds.Client;
using NUnit.Framework;
using PBN;

namespace Tests
{
    [TestFixture]
    public class PaerserTest
    {
        public string testPbn = "\r\n[Dealer \"S\"]\r\n[Deal \"S:6.KJ987.AK75.Q96 AQJT93.63.J64.J4 K2.QT42.QT982.K7 8754.A5.3.AT8532\"]\r\n[Auction \"S\"]\r\n4D Pass Pass Pass\r\n[Declarer \"South\"]\r\n[Contract \"4D\"]\r\n[Result \"0\"]\r\n[Play \"W\"]\r\nH6 H2 HA H7\r\nD6 DQ D3 D5\r\nD4 DT C2 DK\r\nDJ D9 C3 DA\r\nSA S2 S5 S6\r\nCJ C7 CA C6\r\nC4 CK C5 C9\r\nS3 SK S4 H8\r\nH3 H4 H5 H9\r\nS9 HT C8 CQ\r\nST HQ CT HJ\r\nSQ D8 S7 D7\r\nSJ D2 S8 HK\r\n\r\n\r\n";
        public string testPbn2 = "\r\n[Dealer \"S\"]\r\n[Deal \"S:K4.AT974.543.AK3 QJT83.K5.KJ72.Q5 A92.62.A.JT97642 765.QJ83.QT986.8\"]\r\n[Contract \"4NT\"]\r\n[Auction \"S\"]\r\n6C Pass Pass Pass\r\n[Play \"W\"]\r\nSQ SA S5 S4\r\nS9 S6 SK S3\r\nHA H5 H2 H8\r\nHT HK H6 H3\r\nSJ S2 S7 C3\r\n";

        [Test]
        public void Test()
        {
            var result = PbnParser.ParseGame(testPbn);
            Assert.AreEqual(7, result.Values.Count);
            Assert.IsTrue(result.Values.ContainsKey("Dealer"));
            Assert.AreEqual(result.Values["Dealer"].Value, "S");
            Assert.IsNullOrEmpty(result.Values["Dealer"].Body);
            Assert.IsNotEmpty(result.Values["Play"].Body);
            Assert.AreEqual("4D Pass Pass Pass", result.Auction);
            Assert.AreEqual("4D", result.Contract);
            Assert.AreEqual("0", result.Result);
            Assert.AreEqual("S:6.KJ987.AK75.Q96 AQJT93.63.J64.J4 K2.QT42.QT982.K7 8754.A5.3.AT8532", result.Deal);
            Assert.AreEqual("H6 H2 HA H7\r\nD6 DQ D3 D5\r\nD4 DT C2 DK\r\nDJ D9 C3 DA\r\nSA S2 S5 S6\r\nCJ C7 CA C6\r\nC4 CK C5 C9\r\nS3 SK S4 H8\r\nH3 H4 H5 H9\r\nS9 HT C8 CQ\r\nST HQ CT HJ\r\nSQ D8 S7 D7\r\nSJ D2 S8 HK", result.Play);
        }
        [Test]
        public void Test2()
        {
            var client = new DdsApiClient();
            var response = client.SolveGame(testPbn2);
            Assert.NotNull(response);
        }
    }
}
