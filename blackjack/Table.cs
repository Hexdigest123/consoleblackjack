using System;

namespace blackjack
{
    public class Table
    {
        
        // Constructor & Destructor

        public Table()
        {
            _player = new Player(false);
            _headlessDealer = new Player(true);
            GameLoop();
        }
        
        ~Table()
        {
            Console.WriteLine("[DEBUG] free memory of table");
        }
        
        // Functions 

        private void ResetGame()
        {
            Console.WriteLine("Reset Game");
            System.Threading.Thread.Sleep(3000);
            _finishedCardPicking = false;
            _player.ClearPlayer();
            _player.ClearPlayer();
        }

        private void ResetRound()
        {
            System.Threading.Thread.Sleep(3000);
            _finishedCardPicking = false;
            _player.ClearArray();
            _headlessDealer.ClearArray();
        }
        
        private int CountCards(Card[] array)
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

        private void ShowCards(Card[] array, String name)
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

        private void GameLogic()
        {
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
                        System.Environment.Exit(0);
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
                        System.Environment.Exit(0);
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
                        System.Environment.Exit(0);
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
                        System.Environment.Exit(0);
                    }
                    
                }
                ResetRound();
                GameLoop();
            }
            
            if (!_finishedCardPicking) return;
            if (CountCards(_player.GetCards()) == CountCards(_headlessDealer.GetCards()))
            {
                Console.WriteLine("No one Won!");
                ResetRound();
                GameLoop();
            }
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
                        System.Environment.Exit(0);
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
                        System.Environment.Exit(0);
                    }
                    
                }
                
                ResetRound();
                GameLoop();
            }

        }
        
        private void SortCards(ref Card[] array)
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

        private void DealerPickCards()
        {
            var rand = new Random();
            if (CountCards(_headlessDealer.GetCards()) < 17) return;

            if (CountCards(_headlessDealer.GetCards()) > 15)
            {
                _headlessDealer.AddCard(new Card(rand.Next(3), rand.Next(12)));
            }else if (CountCards(_headlessDealer.GetCards()) > 10)
            {
                _headlessDealer.AddCard(new Card(rand.Next(3), rand.Next(12)));
                _headlessDealer.AddCard(new Card(rand.Next(3), rand.Next(12)));
            }
            else
            {
                _headlessDealer.AddCard(new Card(rand.Next(3), rand.Next(12)));
                _headlessDealer.AddCard(new Card(rand.Next(3), rand.Next(12)));
                _headlessDealer.AddCard(new Card(rand.Next(3), rand.Next(12)));
            }
        }
        
        private void GameLoop()
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
                    System.Environment.Exit(0);
                }
            }

            while (_gameStarted)
            {
                ClearConsole();
                Console.WriteLine($"Your current balance: {Convert.ToString(_player.GetBalance())}\n\n");

                Console.Write("Welcome to your seat, how much you want to bet [only integer values]: ");
                try
                {
                    var playerBet = Convert.ToInt32(Console.ReadLine());
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
                    System.Threading.Thread.Sleep(3000);
                    continue;
                }
                
                ClearConsole();
                Console.WriteLine($"Your bet is: {Convert.ToString(_player.GetBet())}\n");
                System.Threading.Thread.Sleep(1000);
                    
                var rand = new Random();
                _player.AddCard(new Card(rand.Next(3), rand.Next(12)));
                _player.AddCard(new Card(rand.Next(3), rand.Next(12)));

                _headlessDealer.AddCard(new Card(rand.Next(3), rand.Next(12)));
                _headlessDealer.AddCard(new Card(rand.Next(3), rand.Next(12)));

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
                           _player.AddCard(new Card(rand.Next(3), rand.Next(12)));
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
        
        private void ClearConsole(){ System.Console.Clear(); }
        
        // Private variables

        private bool _gameStarted = false;
        private bool _finishedCardPicking = false;
        
        private readonly Player _player;
        private readonly Player _headlessDealer;
        
    }
}