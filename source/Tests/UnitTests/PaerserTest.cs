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

        [Test]
        public void ParseGame()
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
        public void ParseHand()
        {
            const string hands = "S:K7.KQT632.A3.AKQ AJ93.954.42.8765 T862.J87.QJ87.T2 Q54.A.KT965.J943";
            var result = PbnParser.ParseHands(hands).ToList();
            Assert.NotNull(result);
            Assert.AreEqual("S",result[0].Position);
            foreach (var handParseResult in result)
            {
                Assert.AreEqual(13, handParseResult.Cards.Count());
            }
            var cards = result[0].Cards;
            Assert.AreEqual("SK", cards[0]);
            Assert.AreEqual("S7", cards[1]);
            Assert.AreEqual("HK", cards[2]);
            Assert.AreEqual("HQ", cards[3]);
            Assert.AreEqual("HT", cards[4]);
            Assert.AreEqual("H6", cards[5]);
            Assert.AreEqual("H3", cards[6]);
            Assert.AreEqual("H2", cards[7]);
            Assert.AreEqual("DA", cards[8]);
            Assert.AreEqual("D3", cards[9]);
            Assert.AreEqual("CA", cards[10]);
            Assert.AreEqual("CK", cards[11]);
            Assert.AreEqual("CQ", cards[12]);
        }

        [Test]
        public void ParseHandWithEmptySuit()
        {
            const string hands = "S:.AKQJT632.A3.AKQ AJ93.954.42.8765 T8762.87.QJ87.T2 KQ54..KT965.J943";
            var result = PbnParser.ParseHands(hands).ToList();
            Assert.NotNull(result);
            Assert.AreEqual("S",result[0].Position);
            foreach (var handParseResult in result)
            {
                Assert.AreEqual(13, handParseResult.Cards.Count());
            }
            var cards = result[0].Cards;
            Assert.AreEqual("HA", cards[0]);
            Assert.AreEqual("HK", cards[1]);
            Assert.AreEqual("HQ", cards[2]);
            Assert.AreEqual("HJ", cards[3]);
            Assert.AreEqual("HT", cards[4]);
            Assert.AreEqual("H6", cards[5]);
            Assert.AreEqual("H3", cards[6]);
            Assert.AreEqual("H2", cards[7]);
            Assert.AreEqual("DA", cards[8]);
            Assert.AreEqual("D3", cards[9]);
            Assert.AreEqual("CA", cards[10]);
            Assert.AreEqual("CK", cards[11]);
            Assert.AreEqual("CQ", cards[12]);
        }
    }
}
