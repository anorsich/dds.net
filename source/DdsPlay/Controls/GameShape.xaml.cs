using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Input;
using DdsPlay.Model;

namespace DdsPlay.Controls
{
    public partial class GameShape : UserControl
    {
        #region Static Properties

        private static List<GameShape> gameShapes = new List<GameShape>();

        #endregion

        #region Properties

        private List<CardShape> cardShapes = new List<CardShape>();
        public List<CardShape> CardShapes
        {
            get
            {
                return cardShapes;
            }
        }

        private List<DeckShape> deckShapes = new List<DeckShape>();
        public List<DeckShape> DeckShapes
        {
            get
            {
                return deckShapes;
            }
        }

        private Game game = new Game();
        public Game Game
        {
            get
            {
                return game;
            }
            set
            {
                game = value;
            }
        }

        #endregion

        #region Events

        public event MouseEventHandler CardMouseEnter;
        public event MouseEventHandler CardMouseLeave;
        public event MouseEventHandler CardMouseMove;
        public event MouseButtonEventHandler CardMouseLeftButtonDown;
        public event MouseButtonEventHandler CardMouseLeftButtonUp;
        public event CardDragEventHandler CardDrag;
        
        #endregion

        #region Constructors

        public GameShape()
        {
            // Required to initialize variables
            InitializeComponent();

            gameShapes.Add(this);
        }

        #endregion

        #region Cards Event Handlers

        protected void cardShape_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (CardMouseLeftButtonDown != null)
                CardMouseLeftButtonDown(sender, e);
        }


        protected void cardShape_MouseMove(object sender, MouseEventArgs e)
        {
            if (CardMouseMove != null)
                CardMouseMove(sender, e);
        }

        protected void cardShape_MouseLeave(object sender, MouseEventArgs e)
        {
            if (CardMouseLeave != null)
                CardMouseLeave(sender, e);
        }

        protected void cardShape_MouseEnter(object sender, MouseEventArgs e)
        {
            if (CardMouseEnter != null)
                CardMouseEnter(sender, e);
        }

        protected void cardShape_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (CardMouseLeftButtonUp != null)
                CardMouseLeftButtonUp(sender, e);
        }

        protected void cardShape_CardDrag(CardShape cardShape, DeckShape oldDeckShape, DeckShape newDeckShape)
        {
            if (CardDrag != null)
                CardDrag(cardShape, oldDeckShape, newDeckShape);
        }

        #endregion

        #region Static Methods

        public static GameShape GetGameShape(Game game)
        {
            for (int i = 0; i < gameShapes.Count; i++)
            {
                if (gameShapes[i].Game == game)
                    return gameShapes[i];
            }

            return null;
        }

        #endregion

        #region Methods

        public CardShape GetCardShape(Card card)
        {
            for (int i = 0; i < CardShapes.Count; i++)
            {
                if (CardShapes[i].Card == card)
                    return CardShapes[i];
            }

            //If not found, create a new card shape
            CardShape cardShape = new CardShape();
            cardShape.Card = card;
            CardShapes.Add(cardShape);

            cardShape.CardMouseLeftButtonDown += new MouseButtonEventHandler(cardShape_MouseLeftButtonDown);
            cardShape.CardMouseLeftButtonUp += new MouseButtonEventHandler(cardShape_MouseLeftButtonUp);
            cardShape.CardMouseEnter += new MouseEventHandler(cardShape_MouseEnter);
            cardShape.CardMouseLeave += new MouseEventHandler(cardShape_MouseLeave);
            cardShape.CardMouseMove += new MouseEventHandler(cardShape_MouseMove);
            cardShape.CardDrag += new CardDragEventHandler(cardShape_CardDrag);

            return cardShape;
        }

        public DeckShape GetDeckShape(Deck deck)
        {
            for (int i = 0; i < DeckShapes.Count; i++)
            {
                if (DeckShapes[i].Deck == deck)
                    return DeckShapes[i];
            }

            return null;
        }

        #endregion
    }
}
