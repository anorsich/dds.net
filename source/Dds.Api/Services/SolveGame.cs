using System;
using Bridge.Domain;
using Dds.Contract;
using Dds.Net;
using Dds.Net.Dto;
using ServiceStack.ServiceInterface;

namespace Dds.Api.Services
{

    public class SolveGameService: Service
    {
        public object Any(SolveGame solveGame)
        {
            var response = new SolveGameResponse();
            var dds = new DdsConnect();
            var trickResult = new TrickResult();
            GameReplayer.Replay(solveGame.PBN,
                             (game, player, card) =>
                             {
                                 var result = dds.SolveBoard(game);
                                 MapCards(result, (suit, rank, score) =>
                                 {
                                     trickResult[player.FirstLetter].Add(new CardResult
                                     {
                                         Suit = suit.ShortName,
                                         Rank = rank.ShortName,
                                         Score = score
                                     });
                                 });
                             },
                             (number) =>
                             {
                                 trickResult.Number = number;
                                 response.Tricks.Add(trickResult);
                                 trickResult = new TrickResult();
                             }
                );
            return response;
        }

        private void MapCards(FutureTricks data, Action<Suit, Rank, int> mapper)
        {
            for (int i = 0; i < data.FutureCards.Count; i++)
            {
                var card = data.FutureCards.Cards[i];
                mapper(card.Suit, card.Rank, data.Scores[i]);
            }
        }
    }
}