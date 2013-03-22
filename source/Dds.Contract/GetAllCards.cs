using System.Collections.Generic;
using ServiceStack.ServiceHost;

namespace Dds.Contract
{
    [Route("/get-all-cards")]
    [Route("/get-all-cards/{PBN}")]
    public class GetAllCards
    {
        public string PBN { get; set; }
    }

    public class GetAllCardsResponse
    {
        /// <summary>
        /// Number of searched nodes 
        /// </summary>
        public int Nodes { get; set; }


        /// <summary>
        /// Possible plays
        /// </summary>
        public List<CardResult> Cards { get; set; }
    }



    public class CardResult
    {
        public string Rank { get; set; }
        public string Suit { get; set; }
        public int Score { get; set; }
    }
}