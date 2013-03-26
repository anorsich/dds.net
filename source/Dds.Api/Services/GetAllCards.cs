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
        public object Any(GetAllCards getAllCards)
        {
            var parseResult = PbnParser.ParseGame(getAllCards.PBN);
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