using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using DdsPlay.Model;

namespace DdsPlay
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Deck _dealer;
        private const int CardsPerPlayer = 13;

        public MainWindow()
        {
            InitializeComponent();

            _dealer = new Deck(new Game());
        }

        private void MainWindow_DealButton_Click(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void LayoutRoot_Loaded(object sender, RoutedEventArgs e)
        {
            // create new game instance for our main window gameshape control
            GameShape.Game = new Game();

            // call some setup methods to initialize the game components
            SetupPlayerHandDecks();
            SetupTrickDecks();

            var result = BridgeHelper.ParsePbn("E:AT5.AJT.A632.KJ7 Q763.KQ9.KQJ94.T 942.87653..98653 KJ8.42.T875.AQ42");
        }

        private void Pbn_KeyUp(object sender, KeyEventArgs e)
        {


        }

        private void CollectCards()
        {
            Player1Hand.Deck.Draw(_dealer, Player1Hand.Deck.Cards.Count);
            Player2Hand.Deck.Draw(_dealer, Player2Hand.Deck.Cards.Count);
            Player3Hand.Deck.Draw(_dealer, Player3Hand.Deck.Cards.Count);
            Player4Hand.Deck.FlipAllCards();    // flip user cards face down
            Player4Hand.Deck.Draw(_dealer, Player4Hand.Deck.Cards.Count);
            Player1Trick.Deck.FlipAllCards();
            Player1Trick.Deck.Draw(_dealer, Player1Trick.Deck.Cards.Count);
            Player2Trick.Deck.FlipAllCards();
            Player2Trick.Deck.Draw(_dealer, Player1Trick.Deck.Cards.Count);
            Player3Trick.Deck.FlipAllCards();
            Player3Trick.Deck.Draw(_dealer, Player1Trick.Deck.Cards.Count);
            Player4Trick.Deck.FlipAllCards();
            Player4Trick.Deck.Draw(_dealer, Player1Trick.Deck.Cards.Count);
        }

        private void SetupTrickDecks()
        {
            Player1Trick.Deck = new Deck(GameShape.Game) { Enabled = true };
            Player2Trick.Deck = new Deck(GameShape.Game) { Enabled = true };
            Player3Trick.Deck = new Deck(GameShape.Game) { Enabled = true };
            Player4Trick.Deck = new Deck(GameShape.Game) { Enabled = true };

            Player1Trick.Deck.MakeAllCardsDragable(false);
            Player2Trick.Deck.MakeAllCardsDragable(false);
            Player3Trick.Deck.MakeAllCardsDragable(false);
            Player4Trick.Deck.MakeAllCardsDragable(false);

            GameShape.DeckShapes.Add(Player1Trick);
            GameShape.DeckShapes.Add(Player2Trick);
            GameShape.DeckShapes.Add(Player3Trick);
            GameShape.DeckShapes.Add(Player4Trick);
        }

        private void SetupPlayerHandDecks()
        {
            Player1Hand.Deck = new Deck(GameShape.Game) { Enabled = true };
            Player2Hand.Deck = new Deck(GameShape.Game) { Enabled = true };
            Player3Hand.Deck = new Deck(GameShape.Game) { Enabled = true };
            Player4Hand.Deck = new Deck(GameShape.Game) { Enabled = true };

            Player1Hand.Deck.MakeAllCardsDragable(true);
            Player2Hand.Deck.MakeAllCardsDragable(true);
            Player3Hand.Deck.MakeAllCardsDragable(true);
            Player4Hand.Deck.MakeAllCardsDragable(true);

            GameShape.DeckShapes.Add(Player1Hand);
            GameShape.DeckShapes.Add(Player2Hand);
            GameShape.DeckShapes.Add(Player3Hand);
            GameShape.DeckShapes.Add(Player4Hand);
        }

        private void Start_Click(object sender, RoutedEventArgs e)
        {
            _dealer = new Deck(1, 13, GameShape.Game);

            _dealer.Shuffle(5);
            _dealer.MakeAllCardsDragable(false);
            _dealer.Enabled = true;
            _dealer.FlipAllCards();

            var pbn = Pbn.Text;

            // collection cards if any are out
            if (_dealer.Cards.Count < 52)
            {
                CollectCards();
            }
            _dealer.Shuffle(5);

            // deal 13 cards to each of the four players
            for (var cardCount = 0; cardCount < CardsPerPlayer; cardCount++)
            {
                _dealer.Draw(Player1Hand.Deck, 1);
                _dealer.Draw(Player2Hand.Deck, 1);
                _dealer.Draw(Player3Hand.Deck, 1);
                _dealer.Draw(Player4Hand.Deck, 1);
            }

            // turn over human player [4] hand
            Player4Hand.Deck.Sort();
            Player4Hand.Deck.MakeAllCardsDragable(true);
            Player4Hand.Deck.FlipAllCards();
        }
    }
}
