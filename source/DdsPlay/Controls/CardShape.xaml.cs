using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using DdsPlay.Model;

namespace DdsPlay.Controls
{
    public delegate void CardDragEventHandler(CardShape cardShape, DeckShape oldDeckShape, DeckShape newDeckShape);

    /// <summary>
    /// Interaction logic for CardShape.xaml
    /// </summary>
    public partial class CardShape : UserControl
    {
        #region Fields
        private Storyboard _aniFlipStart;
        private Storyboard _aniFlipEnd;
        private Storyboard _animRotate;
        #endregion

        #region Constants

        public const double CardOrigX = 76;
        public const double CardOrigY = 61;
        public const double CardWidth = 72 * 2;
        public const double CardHeight = 97 * 2;
        public const double CardWidthRect = 73 * 2;
        public const double CardHeightRect = 98 * 2;

        #endregion

        #region Properties

        private Card card = null;
        public Card Card
        {
            get
            {
                return card;
            }
            set
            {
                if (card != null)
                {
                    card.VisibleChanged -= new EventHandler(CardVisibleChanged);
                    card.DeckChanged -= new EventHandler(CardDeckChanged);
                }

                card = value;

                //Handle Card Events
                card.VisibleChanged += new EventHandler(CardVisibleChanged);
                card.DeckChanged += new EventHandler(CardDeckChanged);

                //Adjust the clipping of the cards image to reflect the current card
                double x = 0;
                double y = 0;

                if (Card.Visible)
                {
                    //Define the card position in the cards image
                    if (Card.Number <= 10)
                    {
                        x = (Card.Number - 1) % 2;
                        y = (Card.Number - 1) / 2;

                        switch (Card.Suit)
                        {
                            case CardSuit.Spades:
                                x += 6;
                                break;
                            case CardSuit.Hearts:
                                x += 0;
                                break;
                            case CardSuit.Diamonds:
                                x += 2;
                                break;
                            case CardSuit.Clubs:
                                x += 4;
                                break;
                        }
                    }
                    else
                    {
                        int number = (Card.Number - 11);
                        switch (Card.Suit)
                        {
                            case CardSuit.Spades:
                                number += 6;
                                break;
                            case CardSuit.Hearts:
                                number += 9;
                                break;
                            case CardSuit.Diamonds:
                                number += 3;
                                break;
                            case CardSuit.Clubs:
                                number += 0;
                                break;
                        }

                        x = (number % 2) + 8;
                        y = number / 2;
                    }
                }
                else
                {
                    //Show back of the card
                    x = 8;
                    y = 6;
                }

                ((RectangleGeometry)imgCard.Clip).Rect = new Rect(x * CardWidthRect + CardOrigX, y * CardHeightRect + CardOrigY, CardWidth, CardHeight);
                foreach (Transform tran in ((TransformGroup)imgCard.RenderTransform).Children)
                {
                    if (tran.GetType() == typeof(TranslateTransform))
                    {
                        tran.SetValue(TranslateTransform.XProperty, -x * CardWidthRect - CardOrigX);
                        tran.SetValue(TranslateTransform.YProperty, -y * CardHeightRect - CardOrigY);
                    }
                }
                imgCard.RenderTransformOrigin = new Point(0.05 + (x * 0.1), 0.08 + (y * 0.166666));
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

        public void OnCardDrag(DeckShape fromDeckShape, DeckShape toDeckShape)
        {
            if ((fromDeckShape != null) && (CardDrag != null))
            {
                CardDrag(this, fromDeckShape, toDeckShape);
            }
        }

        #endregion

        #region Private Variables

        private Point oldMousePos;
        private bool isDrag = false;

        #endregion

        #region Constructors

        public CardShape()
        {
            // Required to initialize variables
            InitializeComponent();

            _aniFlipStart = (Storyboard)Resources["aniFlipStart"];
            _aniFlipEnd = (Storyboard)Resources["aniFlipEnd"];
            _animRotate = (Storyboard)Resources["animRotate"];

            rectBorder.Visibility = Visibility.Collapsed;
            _aniFlipStart.Completed += new EventHandler(AniFlipStartCompleted);
        }

        #endregion

        #region Card Event Handlers

        /// <summary>
        /// Event handler for the card visible property changed event.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void CardVisibleChanged(object sender, EventArgs e)
        {
            // set left and top values
            var gameShape = GameShape.GetGameShape(this.Card.Deck.Game);
            var cardShape = gameShape.GetCardShape((Model.Card)sender);

            if (double.IsNaN(Canvas.GetLeft(cardShape)))
            {
                cardShape.SetValue(Canvas.LeftProperty, Convert.ToDouble(0));
            }
            if (double.IsNaN(Canvas.GetTop(cardShape)))
            {
                cardShape.SetValue(Canvas.TopProperty, Convert.ToDouble(0));
            }
            _aniFlipStart.Begin();
        }

        /// <summary>
        /// Event handler for the card deck property changed event.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void CardDeckChanged(object sender, EventArgs e)
        {
            //Get Decks Shapes
            GameShape gameShape = GameShape.GetGameShape(this.Card.Deck.Game);
            DeckShape oldDeck = (DeckShape)((Canvas)this.Parent).Parent;
            DeckShape newDeck = gameShape.GetDeckShape(this.Card.Deck);

            //Get the animation positions in relation to the world (background)
            double startX = Canvas.GetLeft(oldDeck) + Canvas.GetLeft(this);
            double startY = Canvas.GetTop(oldDeck) + Canvas.GetTop(this);

            double endX = Canvas.GetLeft(newDeck);
            double endY = Canvas.GetTop(newDeck);

            //Change the card parent
            ((Canvas)this.Parent).Children.Remove(this);
            newDeck.LayoutRoot.Children.Add(this);

            //Maintain the same card position relative to the new parent
            Canvas.SetLeft(this, (double.IsNaN(startX) ? Convert.ToDouble(0) : startX) - endX);
            Canvas.SetTop(this, (double.IsNaN(startY) ? Convert.ToDouble(0) : startY) - endY);

            //Reorder decks
            oldDeck.UpdateCardShapes();
            newDeck.UpdateCardShapes();
        }

        #endregion

        #region CardShape Event Handlers

        /// <summary>
        /// Event handler for the flip start animation completed event.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void AniFlipStartCompleted(object sender, EventArgs e)
        {
            this.Card = this.Card;
            _aniFlipEnd.Begin();
        }

        /// <summary>
        /// Event handler for the image card mouse left button down event.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.Windows.Input.MouseButtonEventArgs"/> instance containing the event data.</param>
        private void ImgCardMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (Card != null && Card.IsDragable)
            {   
                imgCard.CaptureMouse();
                isDrag = true;
                oldMousePos = e.GetPosition(LayoutRoot);
            }

            if (CardMouseLeftButtonDown != null)
                CardMouseLeftButtonDown(this, e);
        }

        /// <summary>
        /// Event handler for the image card mouse left button up.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.Windows.Input.MouseButtonEventArgs"/> instance containing the event data.</param>
        private void ImgCardMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (isDrag)
            {
                imgCard.ReleaseMouseCapture();
                isDrag = false;

                //Get which deck this card was dropped into
                GameShape gameShape = GameShape.GetGameShape(this.Card.Deck.Game);
                DeckShape oldDeckShape = gameShape.GetDeckShape(this.Card.Deck);
                DeckShape nearestDeckShape = null;
                double nearestDistance = double.MaxValue;

                foreach (var deck in gameShape.DeckShapes)
                {
                    if (deck.Deck.Enabled)
                    {
                        //double dx = Canvas.GetLeft(deck) - (Canvas.GetLeft(this) + Canvas.GetLeft((UIElement)((Canvas)this.Parent).Parent));
                        //double dy = Canvas.GetTop(deck) - (Canvas.GetTop(this) + Canvas.GetTop((UIElement)((Canvas)this.Parent).Parent));
                        var offset = VisualTreeHelper.GetOffset(deck);

                        var dx = offset.X - e.GetPosition((UIElement)((Canvas)this.Parent).Parent).X;
                        var dy = offset.Y - e.GetPosition((UIElement)((Canvas)this.Parent).Parent).Y;
                        
                        double distance = Math.Sqrt(dx * dx + dy * dy);

                        if (distance < nearestDistance)
                        {
                            nearestDistance = distance;
                            nearestDeckShape = deck;
                        }
                    }
                }

                if ((nearestDeckShape != null) && (CardDrag != null))
                {
                    CardDrag(this, gameShape.GetDeckShape(this.Card.Deck), nearestDeckShape);
                }

                gameShape.GetDeckShape(this.Card.Deck).UpdateCardShapes();
                Canvas.SetZIndex(oldDeckShape, 0);
            }

            if (CardMouseLeftButtonUp != null)
                CardMouseLeftButtonUp(this, e);
        }

        /// <summary>
        /// Event handler for the image card mouse move event.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.Windows.Input.MouseEventArgs"/> instance containing the event data.</param>
        private void ImgCardMouseMove(object sender, MouseEventArgs e)
        {
            if (isDrag)
            {
                Point newMousePos = e.GetPosition(LayoutRoot);

                double dx = newMousePos.X - oldMousePos.X;
                double dy = newMousePos.Y - oldMousePos.Y;

                GameShape gameShape = GameShape.GetGameShape(this.Card.Deck.Game);

                for (int i = this.Card.Deck.Cards.IndexOf(this.Card); i < this.Card.Deck.Cards.Count; i++)
                {
                    CardShape cardShape = gameShape.GetCardShape(this.Card.Deck.Cards[i]);
                    Canvas.SetLeft(cardShape, Canvas.GetLeft(cardShape) + dx);
                    Canvas.SetTop(cardShape, Canvas.GetTop(cardShape) + dy);
                    Canvas.SetZIndex(gameShape.GetDeckShape(this.Card.Deck), 100);
                }
            }

            if (CardMouseMove != null)
                CardMouseMove(this, e);
        }

        /// <summary>
        /// Event handler for the image card mouse enter event.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.Windows.Input.MouseEventArgs"/> instance containing the event data.</param>
        private void ImgCardMouseEnter(object sender, MouseEventArgs e)
        {
            if (Card != null && Card.Enabled)
                rectBorder.Visibility = Visibility.Visible;

            if (CardMouseEnter != null)
                CardMouseEnter(this, e);
        }

        /// <summary>
        /// Event handler for the Image Card mouse leave event.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.Windows.Input.MouseEventArgs"/> instance containing the event data.</param>
        private void ImgCardMouseLeave(object sender, MouseEventArgs e)
        {
            if (Card != null && Card.Enabled)
                rectBorder.Visibility = Visibility.Collapsed;

            if (CardMouseLeave != null)
                CardMouseLeave(this, e);
        }

        #endregion

        #region Methods

        public void Rotate(double speedRatio)
        {
            _animRotate.SpeedRatio = speedRatio;
            _animRotate.Begin();
        }

        #endregion
    }

}
