using System.Collections.Generic;
using System.Linq;
using Bridge.Domain;
using Bridge.Domain.Utils;
using Dds.Contract;
using Dds.Net;
using PBN;
using ServiceStack.ServiceHost;
using ServiceStack.ServiceInterface;

namespace Dds.Api.Services
{
  

    public class GetAllCardsService: Service
    {
        public string testPbn = "\r\n[Dealer \"S\"]\r\n[Deal \"S:K4.AT974.543.AK3 QJT83.K5.KJ72.Q5 A92.62.A.JT97642 765.QJ83.QT986.8\"]\r\n[Contract \"4NT\"]\r\n[Auction \"S\"]\r\n6C Pass Pass Pass\r\n[Play \"W\"]\r\nSQ SA S5 S4\r\nS9 S6 SK S3\r\nHA H5 H2 H8\r\nHT HK H6 H3\r\nSJ S2 S7 C3\r\n";


        public object Any(GetAllCards getAllCards)
        {
            var parseResult = PbnParser.ParseGame(testPbn);
            var dds = new DdsConnect();
            var game = BridgeHelper.GetGameFromPbn(parseResult.Deal);
            var player = PlayerPosition.Players.Find(x => x.FirstLetter == parseResult.FirstPlayer);
            if (!string.IsNullOrEmpty(parseResult.Play))
            {
                var play = PbnParser.ParsePlay(parseResult.Play);
                foreach (var trick in play)
                {
                    foreach (var card in trick)
                    {
                        player = game.PlayCard(BridgeHelper.GetCard(card), player);
                    }
                }
            }
            var result = dds.SolveBoard(game);
            var response = new GetAllCardsResponse {Nodes = result.Nodes, Cards = new List<CardResult>()};
            for (int i = 0; i < result.FutureCards.Count; i++)
            {
                var card = result.FutureCards.Cards[i];
                response.Cards.Add(new CardResult{ Rank = card.Rank.ShortName, Suit = card.Suit.ShortName, Score = result.Scores[i]});
            }
            response.Cards = response.Cards.OrderByDescending(x=> x.Score).ToList();
            return response;
        }
    }
}