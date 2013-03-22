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
        public string testPbn = "\r\n[Dealer \"S\"]\r\n[Deal \"S:K4.AT974.543.AK3 QJT83.K5.KJ72.Q5 A92.62.A.JT97642 765.QJ83.QT986.8\"]\r\n[Contract \"4NT\"]\r\n[Auction \"S\"]\r\n6C Pass Pass Pass\r\n[Play \"W\"]\r\nSQ SA S5 S4\r\nS9 S6 SK S3\r\nHA H5 H2 H8\r\nHT HK H6 H3\r\nSJ S2 S7 C3\r\n";


        public object Any(SolveGame solveGame)
        {
            var response = new SolveGameResponse();
            var parseResult = PbnParser.ParseGame(solveGame.PBN ?? testPbn);
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
                    foreach (var card in trick)
                    {
                        var result = dds.SolveBoard(game);
                        MapCards(result,(suit,rank,score) =>
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