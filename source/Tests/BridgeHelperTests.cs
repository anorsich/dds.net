using Bridge.Domain;
using Bridge.Domain.Utils;
using NUnit.Framework;

namespace Tests
{
    [TestFixture]
    public class BridgeHelperTests
    {
         [Test]
         public void Test()
         {
             var card = BridgeHelper.GetCard("HK");
             Assert.AreEqual(Suit.Hearts, card.Suit);
             Assert.AreEqual(Rank.King, card.Rank);
         }
    }
}