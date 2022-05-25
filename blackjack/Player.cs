using System;

namespace blackjack
{
    public class Player
    {
        // Constructor & Destructor

        public Player(bool isDealer)
        {
            _isDealer = isDealer;   
            _balance = isDealer ? 1000000 : 1000;
            _cards = new Card[2];
        }
        
        // Functions

        private void ExtendArray() // extend Card array
        {
            var localArray = new Card[_cards.Length];
            for (var i = 0; i < _cards.Length; i++)
            {
                localArray[i] = _cards[i];
            }

            _cards = new Card[_cards.Length + 1];
            for (var i = 0; i < localArray.Length; i++)
            {
                _cards[i] = localArray[i];
            }
        }
        
        public void ClearArray() // clear Array after Round
        {
            _cards = new Card[2];
            _cardsRealLength = 0;
            _bet = 0;
        }
        
        public void ClearPlayer() // reset Player to basic stats
        {
            ClearArray();
            _balance = _isDealer ? 1000000 : 1000;
            _bet = 0;
        }
        
        public void AddCard(Card card) // give Card to Player
        {
            if (_cardsRealLength == _cards.Length - 1)
            {
                ExtendArray();
            }
            _cards[_cardsRealLength] = card;
            _cardsRealLength++;
        }

        public Card[] GetCards() { return _cards; }

        // getter & setter
        public void DecreaseMoney(int decreaseBy) { _balance -= decreaseBy; }
        public void IncreaseMoney(int increaseBy) { _balance += increaseBy;}

        public void SetBet(int bet) { _bet = bet; }
        public int GetBet() { return _bet; }
        
        public int GetBalance() { return _balance; }
        public void SetBalance(int balance) { _balance = balance; }
        
        // Private variables

        private int _bet;
        private int _balance;
        private Card[] _cards;
        private int _cardsRealLength;
        private readonly bool _isDealer;

    }
}