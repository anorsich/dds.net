namespace Bridge.Domain
{
    public class Trick
    {
        public Trick()
        {
            Deck = new Deck();
        }

        /// <summary>
        /// Leading hand for the trick
        /// </summary>
        public PlayerPosition TrickDealer { get; set; }
        public Deck Deck { get; set; }
        public PlayerPosition TrickWinner { get; set; }

        public Suit TrickDealerSuit
        {
            get { return Deck.BottomCard.Suit; }
        }
    }
}
