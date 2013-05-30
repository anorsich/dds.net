using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dds.Contract;
using ServiceStack.ServiceClient.Web;

namespace Dds.Client
{
    public class DdsApiClient
    {
        private readonly JsonServiceClient _client;

        public DdsApiClient()
        {
            _client = new JsonServiceClient(ConfigurationManager.AppSettings["DdsApiUrl"]);
        }

        public SolveGameResponse SolveGame(string pbn)
        {
            return _client.Post(new SolveGame {PBN = pbn});
        }

        public GetCardResponse GetCard(string pbn)
        {
            return _client.Post(new GetCard() {PBN = pbn});
        }

        public PlayGameResponse PlayGame(string pbn)
        {
            return _client.Post(new PlayGame() { PBN = pbn });
        }
    }
}
