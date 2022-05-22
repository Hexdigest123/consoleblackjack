using System;

namespace blackjack
{
    public class Card
    {

        // Constructor & Destructor
        
        public Card(int iconIndex, int cardIndex)
        {
            switch (iconIndex)
            {
                case 0:
                    _iconSymbol = Heart;
                    break;
                case 1:
                    _iconSymbol = Spade;
                    break;
                case 2:
                    _iconSymbol = Pic;
                    break;
                case 3:
                    _iconSymbol = Cross;
                    break;
            }
            
            switch (cardIndex)
            {
                case 0:
                    _valueOfCard = Two;
                    break;
                case 1:
                    _valueOfCard = Three;
                    break;
                case 2:
                    _valueOfCard = Four;
                    break;
                case 3:
                    _valueOfCard = Five;
                    break;
                case 4:
                    _valueOfCard = Six;
                    break;
                case 5:
                    _valueOfCard = Seven;
                    break;
                case 6:
                    _valueOfCard = Eight;
                    break;
                case 7:
                    _valueOfCard = Nine;
                    break;
                case 8:
                    _valueOfCard = Ten;
                    break;
                case 9:
                    _valueOfCard = Jack;
                    break;
                case 10:
                    _valueOfCard = Queen;
                    break;
                case 11:
                    _valueOfCard = King;
                    break;
                case 12:
                    _valueOfCard = Ace;
                    break;
            }
        }

        // Functions

        public (String, int) GetValueOfCard() { return _valueOfCard; }
        public String GetIconSymbol() { return _iconSymbol; }
        
        // Public Variables

        public static readonly String Heart = "♥";
        public static readonly String Spade = "♦";
        public static readonly String Pic = "♠";
        public static readonly String Cross = "♣";
        
        public static readonly (String, int) Two = ("Two", 2);
        public static readonly (String, int) Three = ("Three", 3);
        public static readonly (String, int) Four = ("Four", 4);
        public static readonly (String, int) Five = ("Five", 5);
        public static readonly (String, int) Six = ("Six", 6);
        public static readonly (String, int) Seven = ("Seven", 7);
        public static readonly (String, int) Eight = ("Eight", 8);
        public static readonly (String, int) Nine = ("Nine", 9);
        public static readonly (String, int) Ten = ("Ten", 10);
        public static readonly (String, int) Jack = ("Jack", 10);
        public static readonly (String, int) Queen = ("Queen", 10);
        public static readonly (String, int) King = ("King", 10);
        public static readonly (String, int) Ace = ("Ace", 11);
        

        private readonly (String, int) _valueOfCard;
        private readonly String _iconSymbol;
    }
}