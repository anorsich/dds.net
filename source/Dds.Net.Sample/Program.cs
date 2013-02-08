using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dds.Net.Dto;

namespace Dds.Net.Sample
{
    class Program
    {
        static void Main(string[] args)
        {
            var dds = new DdsConnect();

            Console.WriteLine("Initial board in pbn: E:AT5.AJT.A632.KJ7 Q763.KQ9.KQJ94.T 942.87653..98653 KJ8.42.T875.AQ42");
            Console.WriteLine("East start with Diamond 2");
            var remainCards = "E:AT5.AJT.A63.KJ7 Q763.KQ9.KQJ94.T 942.87653..98653 KJ8.42.T875.AQ42";
            Console.WriteLine(remainCards);
            var deal = new DealPbn();
            deal.RemainCardsPbn = remainCards;
            deal.TrickDealer = Player.East;
            deal.Trump = Trump.NoTrump;
            deal.CurrentTrickCards = new List<Card>()
            {
                new Card(Rank.Two, Suit.Diamonds)
            };


            var result = dds.SolveBoardPbn(deal,
                //Parameter ”target” is the number of tricks to be won by the side to play, 
                //    //-1 means that the program shall find the maximum number.
                //    //For equivalent  cards only the highest is returned.
                              -1,
                //    //target=1-13, solutions=1:  Returns only one of the cards. 
                //    //Its returned score is the same as target whentarget or higher tricks can be won. 
                //    //Otherwise, score –1 is returned if target cannot be reached, or score 0 if no tricks can be won. 
                //    //target=-1, solutions=1:  Returns only one of the optimum cards and its score.
                              1, 0);

            var nextCard = result.FutureCards.First();

            Console.WriteLine("Next card: " + nextCard);
            Console.ReadKey();
        }
    }
}