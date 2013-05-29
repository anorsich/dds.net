using System;
using Bridge.Domain;
using Bridge.Domain.Utils;
using PBN;

namespace Dds.Api
{
    public static class GameReplayer
    {
        public static BridgeGame Replay(string pbn, Action<BridgeGame, PlayerPosition, string> onCardPaying = null, Action<int> onTrickPlayed= null)
        {
            var parseResult = PbnParser.ParseGame(pbn);
            var game = BridgeHelper.GetGameFromPbn(parseResult.Deal);
            var player = PlayerPosition.Players.Find(x => x.FirstLetter == parseResult.FirstPlayer);
            if (!string.IsNullOrEmpty(parseResult.Play))
            {
                var play = PbnParser.ParsePlay(parseResult.Play);
                var number = 0;
                foreach (var trick in play)
                {
                    number++;
                    for (int i = 0; i < trick.Length; i++)
                    {
                        var card = trick[trick.Length == 4 ? player.PbnIndex: i];
                        if (onCardPaying != null) onCardPaying(game, player, card);
                        player = game.PlayCard(BridgeHelper.GetCard(card), player);
                    }
                    if (onTrickPlayed != null) onTrickPlayed(number);
                }
            }
            return game;
        }
    }
}