using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bridge.Domain;
using Bridge.Domain.Utils;
using Dds.Net.Dto;
using Dds.Net.Integration;
using FutureTricks = Dds.Net.Dto.FutureTricks;

namespace Dds.Net
{
    public class DdsConnect
    {
        public FutureTricks SolveBoardPbn(BridgeGame game, int target, int solutions, int mode)
        {
            if (game.CurrentTrick.Deck.Count > 3)
                throw new ArgumentException("Invalid current trick cards count", "CurrentTrick.Deck.Count");

            var deal = new Integration.DealPbn();
            deal.trump = game.Contract.Trump.Order;
            deal.first = game.CurrentTrick.TrickDealer.Order;

            deal.currentTrickRank = new int[3];
            deal.currentTrickSuit = new int[3];
            for (int i = 0; i < game.CurrentTrick.Deck.Count; i++)
            {
                var card = game.CurrentTrick.Deck.Cards[i];
                deal.currentTrickRank[i] = card.Rank.Score;
                deal.currentTrickSuit[i] = card.Suit.Order;
            }
            deal.remainCards = DdsHelper.PbnStringToChars(BridgeHelper.ToPbn(game));

            var ddsResult = new Integration.FutureTricks();

            //TODO: Support mutiple threads.
            var res = DdsImport.SolveBoardPBN(deal, target, solutions, mode, ref ddsResult, 0);
            if (res != 1)
                throw new DdsSolveBoardException(res);

            var result = new FutureTricks();
            result.Cards = ddsResult.cards;
            result.Nodes = ddsResult.nodes;
            result.Scores = ddsResult.score.ToList();
            for (int i = 0; i < 13; i++)
            {
                if (ddsResult.rank[i] != 0)
                {
                    var rank = new Rank(ddsResult.rank[i]);
                    var suit = new Suit(ddsResult.suit[i]);

                    result.FutureCards.Cards.Add(new Card(rank, suit));
                }
                else
                {
                    break;
                }
            }

            return result;
        }

        public BestCard SolveBoardPbnBestCard(BridgeGame game)
        {
            var result = SolveBoardPbn(game,
                //Parameter ”target” is the number of tricks to be won by the side to play, 
                //    //-1 means that the program shall find the maximum number.
                //    //For equivalent  cards only the highest is returned.
                            -1,
                //    //target=1-13, solutions=1:  Returns only one of the cards. 
                //    //Its returned score is the same as target whentarget or higher tricks can be won. 
                //    //Otherwise, score –1 is returned if target cannot be reached, or score 0 if no tricks can be won. 
                //    //target=-1, solutions=1:  Returns only one of the optimum cards and its score.
                            1, 0);

            return new BestCard() { Card = result.FutureCards.Cards.First(), Score = result.Scores[0] };
        }

        public List<Contract> CalculateMakeableContracts(string pbn)
        {
            var ret = new List<Contract>();
            var results = new DdTableResults();
            var dto = new DdTableDealPbn(DdsHelper.PbnStringToChars(pbn));

            var res = DdsImport.CalcDDtablePBN(dto, ref results);
            if (res != 1)
                throw new DdsCalcDDtableException(res);
            /* 
            *      S   H   D   C   NT
            *  N   0   4   8   12  16
            *  E   1   5   9   13  17
            *  S   2   6   10  14  18
            *  W   3   7   11  15  19
            */

            var index = 0;
            foreach (Trump trump in Trump.Trumps)
            {
                foreach (PlayerPosition player in PlayerPosition.Players)
                {
                    ret.Add(new Contract()
                    {
                        Trump = trump,
                        PlayerPosition = player,
                        Value = results.resTable[index]
                    });
                    index++;
                }
            }

            return ret;
        }
    }
}
