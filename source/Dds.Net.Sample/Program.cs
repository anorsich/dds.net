using System;
using System.Linq;
using System.Text;
using Bridge.Domain;
using Bridge.Domain.Utils;

namespace Dds.Net.Sample
{
    class Program
    {
        static void Main(string[] args)
        {
            var dds = new DdsConnect();
            var pbnCode = "E:AT5.AJT.A632.KJ7 Q763.KQ9.KQJ94.T 942.87653..98653 KJ8.42.T875.AQ42";
            Console.WriteLine("Board: " + pbnCode);
            
            var game = BridgeHelper.GetGameFromPbn(pbnCode);
            var res = dds.CalculateMakeableContracts(pbnCode);
            Console.WriteLine("Best results:");
            foreach (var contract in res)
            {
                Console.WriteLine(contract);
            }
            Console.WriteLine("------------- Game Starts ----------------");
            Console.WriteLine("Trump: " + game.Contract.Trump);
            var player = game.Declarer;
            while (game.CardsRemaning > 0)
            {
                var result = dds.SolveBoardPbnBestCard(game);
                Console.WriteLine(player + ": " + result.Card + ". Score: " + result.Score);
                player = game.PlayCard(result.Card, player);

                if (game.CurrentTrick.Deck.Count == 0)
                {
                    Console.WriteLine("Trick Winner: " + game.Tricks.Last().TrickWinner);
                }
            }

            Console.WriteLine("-----------Results----------");
            Console.WriteLine("South/North: " + game.NorthSouthTricksMade + " tricks");
            Console.WriteLine("East/West: " + game.EastWestTricksMade + " tricks");

            Console.ReadKey();
        }
    }
}