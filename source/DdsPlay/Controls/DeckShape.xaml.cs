using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Animation;
using DdsPlay.Model;

namespace DdsPlay.Controls
{
    /// <summary>
    /// Interaction logic for DeckShape.xaml
    /// </summary>
    public partial class DeckShape : UserControl
    {
        #region Static Properties

        private static List<DeckShape> playingDecks = new List<DeckShape>();

        #endregion

        #region Properties

        private double cardSpacerX = 0;
        public double CardSpacerX
        {
            get { return cardSpacerX; }
            set { cardSpacerX = value; }
        }

        private double cardSpacerY = 0;
        public double CardSpacerY
        {
            get { return cardSpacerY; }
            set { cardSpacerY = value; }
        }

        private int maxCardsSpace = 0;
        public int MaxCardsSpace
        {
            get
            {
                return maxCardsSpace;
            }
            set
            {
                maxCardsSpace = value;
            }
        }

        private double nextCardX = 0;
        public double NextCardX
        {
            get { return nextCardX; }
            set { nextCardX = value; }
        }

        private double nextCardY = 0;
        public double NextCardY
        {
            get { return nextCardY; }
            set { nextCardY = value; }
        }

        private Deck deck = null;
        public Deck Deck
        {
            get
            {
                return deck;
            }
            set
            {
                if (deck != null)
                {
                    deck.SortChanged -= DeckSortChanged;
                }

                deck = value;
                
                deck.SortChanged += DeckSortChanged;
                UpdateCardShapes();
            }
        }
        
        #endregion

        #region Events

        public event MouseEventHandler DeckMouseEnter;
        public event MouseEventHandler DeckMouseLeave;
        public event MouseEventHandler DeckMouseMove;
        public event MouseButtonEventHandler DeckMouseLeftButtonDown;
        public event MouseButtonEventHandler DeckMouseLeftButtonUp;

        #endregion

        #region Constructors

        public DeckShape()
        {
            InitializeComponent();

            playingDecks.Add(this);
            rectBorder.Visibility = Visibility.Collapsed;
        }

        #endregion

        #region Methods

        public Point GetNextCardPosition()
        {
            Point p = new Point(NextCardX, NextCardY);

            NextCardX += CardSpacerX;
            NextCardY += CardSpacerY;

            return p;
        }

        /// <summary>
        /// Recalculate all the card positions and animate them to the new positions
        /// Should be called when the deck change its cards order or count
        /// </summary>
        public void UpdateCardShapes()
        {
            GameShape game = GameShape.GetGameShape(Deck.Game);
            NextCardX = 0;
            NextCardY = 0;

            double localCardSpacerX = CardSpacerX;
            double localCardSpacerY = CardSpacerY;

            if ((MaxCardsSpace > 0) && (Deck.Cards.Count > MaxCardsSpace))
            {
                //override the spacers values to squeez cards
                localCardSpacerX = (CardSpacerX * MaxCardsSpace) / Deck.Cards.Count;
                localCardSpacerY = (CardSpacerY * MaxCardsSpace) / Deck.Cards.Count;
            }

            ////Create the animation to move the card from one deck to the other
            Duration duration = new Duration(TimeSpan.FromSeconds(0.2));

            Storyboard sb = new Storyboard();
            sb.Duration = duration;

            //Loop on the Deck Cards (not playing cards)
            for (int i = 0; i < Deck.Cards.Count; i++)
            {
                //Get the card shape
                CardShape cardShape = game.GetCardShape(Deck.Cards[i]);
                if (cardShape.Parent != this.LayoutRoot)
                {
                    LayoutRoot.Children.Add(cardShape);
                }

                // set left and top values
                if (double.IsNaN(Canvas.GetLeft(cardShape)))
                {
                    cardShape.SetValue(Canvas.LeftProperty, Convert.ToDouble(0));
                }
                if (double.IsNaN(Canvas.GetTop(cardShape)))
                {
                    cardShape.SetValue(Canvas.TopProperty, Convert.ToDouble(0));
                }

                //Animate card to its correct position
                DoubleAnimation xAnim = new DoubleAnimation();
                xAnim.Duration = duration;
                sb.Children.Add(xAnim);
                Storyboard.SetTarget(xAnim, cardShape);
                Storyboard.SetTargetProperty(xAnim, new PropertyPath("(Canvas.Left)"));
                xAnim.To = NextCardX;

                DoubleAnimation yAnim = new DoubleAnimation();
                yAnim.Duration = duration;
                sb.Children.Add(yAnim);
                Storyboard.SetTarget(yAnim, cardShape);
                Storyboard.SetTargetProperty(yAnim, new PropertyPath("(Canvas.Top)"));
                yAnim.To = NextCardY;

                Canvas.SetZIndex(cardShape, i);

                //Increment the next card position
                NextCardX += localCardSpacerX;
                NextCardY += localCardSpacerY;
            }

            if (LayoutRoot.Resources.Contains("sb"))
                LayoutRoot.Resources.Remove("sb");

            LayoutRoot.Resources.Add("sb", sb);
            sb.Begin();
        }

        #endregion

        #region DeckShape Event Handlers

        /// <summary>
        /// Handles the SortChanged event of the deck control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void DeckSortChanged(object sender, EventArgs e)
        {
            UpdateCardShapes();
        }


        private void RectBorderBackMouseEnter(object sender, MouseEventArgs e)
        {
            if (Deck != null && Deck.Enabled)
                rectBorder.Visibility = Visibility.Visible;

            if (DeckMouseEnter != null)
                DeckMouseEnter(this, e);
        }

        private void RectBorderBackMouseLeave(object sender, MouseEventArgs e)
        {
            if (Deck != null && Deck.Enabled)
                rectBorder.Visibility = Visibility.Collapsed;

            if (DeckMouseLeave != null)
                DeckMouseLeave(this, e);
        }

        private void RectBorderBackMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (DeckMouseLeftButtonDown != null)
                DeckMouseLeftButtonDown(this, e);
        }

        private void RectBorderBackMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (DeckMouseLeftButtonUp != null)
                DeckMouseLeftButtonUp(this, e);
        }

        private void RectBorderBackMouseMove(object sender, MouseEventArgs e)
        {
            if (DeckMouseMove != null)
                DeckMouseMove(this, e);
        }

        #endregion
    }
}
