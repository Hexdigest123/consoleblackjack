namespace blackjack
{
    public class Player
    {
        // Constructor & Destructor

        public Player(bool isDealer)
        {
            _balance = isDealer ? 1000000 : 1000;
            _cards = new Card[2];
        }
        
        // Functions

        public void ExtendArray() // Pass by reference
        {
            Card[] localArray = new Card[_cards.Length];
            for (int i = 0; i < _cards.Length; i++)
            {
                localArray[i] = _cards[i];
            }

            _cards = new Card[_cards.Length + 1];
            localArray = null;
        }

        public void AddCard(Card card)
        {
            if (_cardsRealLength == _cards.Length - 1)
            {
                ExtendArray();
            }
            _cards[_cardsRealLength] = card;
            _cardsRealLength++;
        }
        
        
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
        private int _cardsRealLength = 0;

    }
}