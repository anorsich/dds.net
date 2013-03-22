using Dds.Api.Services;
using NUnit.Framework;

namespace Tests
{
    [TestFixture]
    public class GetCardsServiceTests
    {
        public string testPbn = "\r\n[Dealer \"S\"]\r\n[Deal \"S:6.KJ987.AK75.Q96 AQJT93.63.J64.J4 K2.QT42.QT982.K7 8754.A5.3.AT8532\"]\r\n[Auction \"S\"]\r\n4D Pass Pass Pass\r\n[Declarer \"South\"]\r\n[Contract \"4D\"]\r\n[Result \"0\"]\r\n[Play \"W\"]\r\nH6 H2 HA H7\r\nD6 DQ D3 D5\r\nD4 DT C2 DK\r\nDJ D9 C3 DA\r\nSA S2 S5 S6\r\nCJ C7 CA C6\r\nC4 CK C5 C9\r\nS3 SK S4 H8\r\nH3 H4 H5 H9\r\n";

        [Test]
        public void test()
        {
            var service = new GetAllCardsService();
            var result = (GetAllCardsResponse)service.Any(new GetAllCards() {PBN = testPbn});
            Assert.NotNull(result);
        }
    }
}