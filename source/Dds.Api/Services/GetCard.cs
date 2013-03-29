using Dds.Contract;
using Dds.Net;
using ServiceStack.ServiceInterface;

namespace Dds.Api.Services
{
    public class GetCardService:Service
    {
        public object Any(GetCard request)
        {
            var dds = new DdsConnect();
            var game = GameReplayer.Replay(request.PBN);
            var result = dds.SolveBoard(game);
            var card = result.FutureCards.Cards[0];
            return new GetCardResponse
            {
                Rank = card.Rank.ShortName,
                Suit = card.Suit.ShortName,
                Score = result.Scores[0]
            };
        }
    }
}