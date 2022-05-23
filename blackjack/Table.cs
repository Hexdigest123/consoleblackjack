using System;

namespace blackjack
{
    public class Table
    {
        
        // Constructor & Destructor

        public Table()
        {
            Console.WriteLine("[DEBUG] reserve memory for Table");
            GameLoop();
        }
        
        ~Table()
        {
            Console.WriteLine("[DEBUG] free memory of Table");
        }
        
        // Functions 

        private int CountCards(Card[] array)
        {
            return 1;
        }
        
        private void PrintCards(Card[] array)
        {
            
        }
        
        private void SortCards(ref Card[] array)
        {
            // selection sort
            Console.WriteLine("Your Cards: ");
            foreach (var card in array)
            {
                Console.Write($"");
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
            }
            
            _player = new Player(false);
            _headlessDealer = new Player(true);
            
            while (_gameStarted)
            {
                ClearConsole();
                Console.WriteLine($"Your current balance: {Convert.ToString(_player.GetBalance())}\n\n");

                Console.Write("Welcome to your seat, how much you want to bet [only integer values]: ");
                try
                {
                    int playerBet = Convert.ToInt32(Console.ReadLine());
                    _player.SetBet(playerBet);
                    _headlessDealer.SetBet(playerBet*2);
                }
                catch (InvalidCastException)
                {
                    Console.WriteLine("!pls read the the instructions!\n");
                    System.Threading.Thread.Sleep(1000);
                    continue;
                }
                
                ClearConsole();
                Console.WriteLine($"Your bet is: {Convert.ToString(_player.GetBet())}\n");

                Random rand = new Random();
                _player.AddCard(new Card(rand.Next(4), rand.Next(12)));
                _player.AddCard(new Card(rand.Next(4), rand.Next(12)));

                _headlessDealer.AddCard(new Card(rand.Next(4), rand.Next(12)));
                _headlessDealer.AddCard(new Card(rand.Next(4), rand.Next(12)));
                
                
                
            }
        }
        
        private void ClearConsole(){ for(var i = 0; i < 150; i++){ Console.WriteLine(); }}
        
        // Private variables

        private bool _gameStarted = false;
        
        private Player _player;
        private Player _headlessDealer;
        
    }
}