using System.Linq;
using System.Text;
using Bridge.Domain.Utils;
using Dds.Contract;
using Dds.Net;
using ServiceStack.ServiceInterface;

namespace Dds.Api.Services
{
    public class PlayGameService : Service
    {
        public object Any(PlayGame request)
        {
            var dds = new DdsConnect();
            var game = GameReplayer.Replay(request.PBN);
            var player = BridgeHelper.GetNextPlayerPosition(game.Declarer);
            while (game.Tricks.Count != 13)
            {
                var result = dds.SolveBoard(game);
                var card = result.FutureCards.Cards[0];
                player = game.PlayCard(card, player);
            }
            var play = game.Tricks.Select(x => BridgeHelper.DeckToPbnPlay(x.Deck));
            return new PlayGameResponse
            {
                Play =  string.Join("\n",play)
            };
        }
    }
}