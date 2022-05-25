using System;
using System.Linq;
using System.Threading;

namespace blackjack
{
    public class Table
    {
        
        // Constructor & Destructor

        public Table() // constructor to initialize variables for Table
        {
            _player = new Player(false);
            _headlessDealer = new Player(true);
            _cardDeck = new Card[10];
            GameLoop();
        }
        
        // Functions 

        private void ExtendCardDeck() // extend card deck similar to Player.cs "Extend Array"
        {
            var localArray = new Card[_cardDeck.Length];
            for (var i = 0; i < _cardDeck.Length; i++)
            {
                localArray[i] = _cardDeck[i];
            }

            _cardDeck = new Card[_cardDeck.Length + 10];
            for (var i = 0; i < localArray.Length; i++)
            {
                _cardDeck[i] = localArray[i];
            }
        }

        private Card CreateCard() // create Card object 
        {
            Card newCard = null;
            newCard = new Card(_rand.Next(4), _rand.Next(13));
            if(_cardDeckRealLength+1 == _cardDeck.Length) ExtendCardDeck();

            foreach (var card in _cardDeck)
            {
                if (card == null) break;
                if (card.GetIconSymbol() != newCard.GetIconSymbol() &&
                    card.GetValueOfCard() != newCard.GetValueOfCard()) continue;
                
                newCard = new Card(_rand.Next(4), _rand.Next(13));
                
                if (card.GetIconSymbol() != newCard.GetIconSymbol() &&
                    card.GetValueOfCard() != newCard.GetValueOfCard()) continue;
                newCard = new Card(_rand.Next(4), _rand.Next(13));
            }
            
            _cardDeck[_cardDeckRealLength] = newCard;
            _cardDeckRealLength++;
            return newCard;
        }

        private void ResetGame() // reset game if player has no money or dealer has no money
        {
            Console.WriteLine("Reset Game");
            Thread.Sleep(1000);
            _finishedCardPicking = false;
            _player.ClearPlayer();
            _player.ClearPlayer();
        }

        private void ResetRound() // reset round when someone won
        {
            Thread.Sleep(3000);
            _finishedCardPicking = false;
            _player.ClearArray();
            _headlessDealer.ClearArray();
        }
        
        private int CountCards(Card[] array) // count cards to get the value of deck
        {
            int value = 0;
            foreach (var card in array)
            {
                if (card == null)
                {
                    continue;
                }
                (String iconName, int iconValue) cardInfo = card.GetValueOfCard();
                value += cardInfo.iconValue;
            }
            return value;
        }

        private void ShowCards(Card[] array, String name) // display cards on console
        {
            Console.WriteLine($"{name} Deck: ");
            foreach (var card in array)
            {
                if (card == null)
                {
                    continue;
                }
                (String iconName, int iconValue) cardInfo = card.GetValueOfCard();
                String iconSymbol = card.GetIconSymbol();

                Console.WriteLine($"{iconSymbol} {cardInfo.iconName} {cardInfo.iconValue.ToString()}");
            }

            Console.WriteLine("\n");
        }

        private void GameLogic() // whole game logic from blackjack
        {
            if (_finishedCardPicking)
            {
                if (CountCards(_player.GetCards()) == CountCards(_headlessDealer.GetCards()))
                {
                    Console.WriteLine("No one Won!");
                    ResetRound();
                    GameLoop();
                }
            }
            if (CountCards(_player.GetCards()) == 21)
            {
                Console.WriteLine("You Won!\nPlayer Blackjack\n");
                _player.IncreaseMoney(_headlessDealer.GetBet());
                _headlessDealer.DecreaseMoney(_headlessDealer.GetBet());
                
                if (_headlessDealer.GetBalance() <= 0)
                {
                    Console.WriteLine("Dealer has not Enough Money ;(\n HOW IS THIS CASE IMPOSSIBLE");
                    Console.WriteLine("Do you want to play again? [yes/no]: ");
                    if (Console.ReadLine() == "yes")
                    {
                        ResetGame(); 
                    }
                    else
                    {
                        Environment.Exit(0);
                    }
                    
                }
                
                ResetRound();
                GameLoop();
                return;
            }

            if (CountCards(_headlessDealer.GetCards()) == 21)
            {
                Console.WriteLine("You Lost!\nDealer Blackjack\n");
                _headlessDealer.IncreaseMoney(_player.GetBet());
                _player.DecreaseMoney(_player.GetBet());
                
                if (_player.GetBalance() <= 0)
                {
                    Console.WriteLine("Player has not Enough Money ;(");
                    Console.WriteLine("Do you want to play again? [yes/no]: ");
                    if (Console.ReadLine() == "yes")
                    {
                        ResetGame(); 
                    }
                    else
                    {
                        Environment.Exit(0);
                    }
                    
                }
                
                ResetRound();
                GameLoop();
                return;
            }

            if (CountCards(_player.GetCards()) > 21)
            {
                Console.WriteLine("You Lost!\nYou have more than 21\n");
                _headlessDealer.IncreaseMoney(_player.GetBet());
                _player.DecreaseMoney(_player.GetBet());
                if (_player.GetBalance() <= 0)
                {
                    Console.WriteLine("Player has not Enough Money ;(");
                    Console.WriteLine("Do you want to play again? [yes/no]: ");
                    if (Console.ReadLine() == "yes")
                    {
                        ResetGame(); 
                    }
                    else
                    {
                        Environment.Exit(0);
                    }
                    
                }
                ResetRound();
                GameLoop();
            }
            
            if (CountCards(_headlessDealer.GetCards()) > 21)
            {
                Console.WriteLine("You Won!\nDealer has more than 21\n");
                _player.IncreaseMoney(_headlessDealer.GetBet());
                _headlessDealer.DecreaseMoney(_headlessDealer.GetBet());
                if (_headlessDealer.GetBalance() <= 0)
                {
                    Console.WriteLine("Dealer has not Enough Money ;(\n HOW IS THIS CASE IMPOSSIBLE");
                    Console.WriteLine("Do you want to play again? [yes/no]: ");
                    if (Console.ReadLine() == "yes")
                    {
                        ResetGame(); 
                    }
                    else
                    {
                        Environment.Exit(0);
                    }
                    
                }
                ResetRound();
                GameLoop();
            }
            
            if (!_finishedCardPicking) return;
            
            if (CountCards(_player.GetCards()) > CountCards(_headlessDealer.GetCards()))
            {
                Console.WriteLine($"You won!\nYou won with a Deck worth of: {CountCards(_player.GetCards()).ToString()}");
                _player.IncreaseMoney(_headlessDealer.GetBet());
                _headlessDealer.DecreaseMoney(_headlessDealer.GetBet());
                
                if (_player.GetBalance() <= 0)
                {
                    Console.WriteLine("Dealer has not Enough Money ;(\n HOW IS THIS CASE IMPOSSIBLE");
                    Console.WriteLine("Do you want to play again? [yes/no]: ");
                    if (Console.ReadLine() == "yes")
                    {
                        ResetGame(); 
                    }
                    else
                    {
                        Environment.Exit(0);
                    }
                    
                }
                
                ResetRound();
                GameLoop();
            }
            else
            {
                Console.WriteLine($"You Lost!\nYou Lost with a Deck worth of: {CountCards(_player.GetCards()).ToString()}");
                _player.IncreaseMoney(_headlessDealer.GetBet());
                _headlessDealer.DecreaseMoney(_headlessDealer.GetBet());
                
                if (_headlessDealer.GetBalance() <= 0)
                {
                    Console.WriteLine("Dealer has not Enough Money ;(\n HOW IS THIS CASE IMPOSSIBLE");
                    Console.WriteLine("Do you want to play again? [yes/no]: ");
                    if (Console.ReadLine() == "yes")
                    {
                        ResetGame(); 
                    }
                    else
                    {
                        Environment.Exit(0);
                    }
                    
                }
                
                ResetRound();
                GameLoop();
            }

        }
        
        private void SortCards(ref Card[] array) // sort cards for deck
        {
            // selection sort

            var n = array.Length-1;
            
            for (var i = 0; i < n - 1; i++) {
                var smallest = i;
                for (var j = i + 1; j < n; j++) {
                    if (array[j].GetValueOfCard().Item2 < array[smallest].GetValueOfCard().Item2) {
                        smallest = j;
                    }
                }
                (array[smallest], array[i]) = (array[i], array[smallest]);
            }
            
        } 

        private void DealerPickCards() // after player took his cards dealer will also take some cards
        {
            var rand = new Random();
            
            if(CountCards(_headlessDealer.GetCards()) < CountCards(_player.GetCards())) 
                _headlessDealer.AddCard(CreateCard());
            
            if(CountCards(_headlessDealer.GetCards()) >= CountCards(_player.GetCards())) return;

            if (CountCards(_headlessDealer.GetCards()) > 15)
            {
                _headlessDealer.AddCard(CreateCard());
            }else if (CountCards(_headlessDealer.GetCards()) > 10)
            {
                _headlessDealer.AddCard(CreateCard());
                if(CountCards(_headlessDealer.GetCards()) >= CountCards(_player.GetCards())) return;
                if (CountCards(_headlessDealer.GetCards()) >= 20) return;
                _headlessDealer.AddCard(CreateCard());
            }
            else
            {
                _headlessDealer.AddCard(CreateCard());
                if(CountCards(_headlessDealer.GetCards()) >= CountCards(_player.GetCards())) return;
                if (CountCards(_headlessDealer.GetCards()) >= 20) return;
                _headlessDealer.AddCard(CreateCard());
                if(CountCards(_headlessDealer.GetCards()) >= CountCards(_player.GetCards())) return;
                if (CountCards(_headlessDealer.GetCards()) >= 20) return;
                _headlessDealer.AddCard(CreateCard());
            }
        }
        
        private void GameLoop() // game loop to keep game working
        {
            while (!_gameStarted)
            {
                Console.Write("\t\tWelcome to Blackjack\n\nDo you want to play [yes,no]: ");
                if (Console.ReadLine() == "yes")
                {
                    _gameStarted = true;
                }
                else
                {
                    Environment.Exit(0);
                }
            }

            while (_gameStarted)
            {
                ClearConsole();
                Console.WriteLine($"Your current balance: {Convert.ToString(_player.GetBalance())}\n\n");

                Console.Write("Welcome to your seat, how much you want to bet [only positive integer values]: ");
                try
                {
                    var playerBet = Convert.ToInt32(Console.ReadLine());
                    if (playerBet <= 0)
                    {
                        throw new ArgumentException();
                    }
                    if (playerBet > _player.GetBalance())
                    {
                        playerBet = _player.GetBalance();
                    }

                    _player.SetBet(playerBet);
                    _headlessDealer.SetBet(playerBet * 2);
                }
                catch (Exception)
                {
                    Console.WriteLine("!pls read the the instructions!\n");
                    Thread.Sleep(3000);
                    continue;
                }
                
                ClearConsole();
                Console.WriteLine($"Your bet is: {Convert.ToString(_player.GetBet())}\n");
                Thread.Sleep(1000);
                
                _player.AddCard(CreateCard());
                _player.AddCard(CreateCard());

                _headlessDealer.AddCard(CreateCard());
                _headlessDealer.AddCard(CreateCard());

                Card[] playerCards = _player.GetCards();
                Card[] dealerCards = _headlessDealer.GetCards();
                SortCards(ref playerCards);
                SortCards(ref dealerCards);

                while (true)
                {
                    
                    ClearConsole();
                    Console.WriteLine($"Your Cards worth: {CountCards(_player.GetCards()).ToString()}");
                    ShowCards(_player.GetCards(), "Your");
                   Console.WriteLine($"Dealer Cards worth: {CountCards(_headlessDealer.GetCards()).ToString()}");
                   ShowCards(_headlessDealer.GetCards(), "Dealer");
                   GameLogic();

                   Console.WriteLine("[1] Take another Card\n[2] Pass");
                   String choice = Console.ReadLine();
                   switch (choice)
                   {
                       case "1":
                           _player.AddCard(CreateCard());
                           break;
                       case "2":
                           _finishedCardPicking = true;
                           DealerPickCards();
                           break;
                       default:
                           continue;
                   }
                }
            }
        }
        
        private void ClearConsole(){ Console.Clear(); } // just to clear to console
        
        // Private variables

        private readonly Random _rand = new Random();
        
        private Card[] _cardDeck;
        private int _cardDeckRealLength = 0;
        
        private bool _gameStarted;
        private bool _finishedCardPicking;
        
        private readonly Player _player;
        private readonly Player _headlessDealer;
        
    }
}