using ServiceStack.ServiceHost;

namespace Dds.Contract
{
    [Route("/get-card")]
    [Route("/get-card/{PBN}")]
    public class GetCard : IReturn<GetCardResponse>
    {
        public string PBN { get; set; }
    }

    public class GetCardResponse
    {
        public string Rank { get; set; }
        public string Suit { get; set; }
        public int Score { get; set; }
    }
}