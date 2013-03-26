using System;
using System.Collections.Generic;
using Bridge.Domain;
using Bridge.Domain.Utils;
using Dds.Contract;
using Dds.Net;
using Dds.Net.Dto;
using PBN;
using ServiceStack.ServiceHost;
using ServiceStack.ServiceInterface;

namespace Dds.Api.Services
{

    public class SolveGameService: Service
    {
        public object Any(SolveGame solveGame)
        {
            var response = new SolveGameResponse();
            var parseResult = PbnParser.ParseGame(solveGame.PBN);
            var dds = new DdsConnect();
            var game = BridgeHelper.GetGameFromPbn(parseResult.Deal);
            var player = PlayerPosition.Players.Find(x => x.FirstLetter == parseResult.FirstPlayer);
            if (!string.IsNullOrEmpty(parseResult.Play))
            {
                var play = PbnParser.ParsePlay(parseResult.Play);
                var number = 0;
                foreach (var trick in play)
                {
                    number++;
                    var trickResult = new TrickResult()
                    {
                        Number = number
                    };
                    for (int i = 0; i < 4; i++)
                    {
                        var card = trick[player.PbnIndex];
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
                        player = game.PlayCard(BridgeHelper.GetCard(card), player);
                    }
                    response.Tricks.Add(trickResult);
                }
            }
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