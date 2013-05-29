using System.Collections.Generic;
using System.Linq;
using Dds.Contract;
using Dds.Net;
using ServiceStack.ServiceInterface;

namespace Dds.Api.Services
{
    public class GetAllCardsService: Service
    {
        public object Any(GetAllCards getAllCards)
        {
            var dds = new DdsConnect();
            var game = GameReplayer.Replay(getAllCards.PBN);
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