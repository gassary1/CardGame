using System;
using System.Collections.Generic;

namespace CardGame
{
    class Program
    {
        static void Main(string[] args)
        {
            bool isActive = true;
            int countOfCards;
            int rankOfCard;
            int suitOfCard;
            ConsoleKey userOption;

            Player player = new Player();

            while (isActive)
            {
                player.ShowPlayerDecks();
                Console.WriteLine();
                PrintMenu();

                Console.Write("\nВыберите действие: ");
                userOption = Console.ReadKey().Key;

                Console.Clear();

                if (userOption == ConsoleKey.D1)
                {
                    Console.Write("Введите количество карт: ");
                    countOfCards = GetNumber();
                    player.AddRandomCards(countOfCards);
                }
                else if (userOption == ConsoleKey.D2)
                {
                    Console.WriteLine("Введите тип карты: ");
                    ShowRanksOfCardInfo();
                    rankOfCard = GetNumber();

                    Console.WriteLine("Введите масть карты: ");
                    ShowSuitsOfCardInfo();
                    suitOfCard = GetNumber();

                    Console.Write("Введите количество карт: ");
                    countOfCards = GetNumber();

                    player.AddConcreteCards(rankOfCard, suitOfCard, countOfCards);
                }
                else if (userOption == ConsoleKey.D3)
                {
                    isActive = false;
                }
                else
                {
                    Console.WriteLine("Ошибка ввода");
                }
            }
        }

        static void PrintMenu()
        {
            Console.WriteLine("1 - Добавить заданное количество карт одного рандомного типа\n2 - Добавить конкректные карты\n3 - Выход");
        }

        static void ShowRanksOfCardInfo()
        {
            int index = 0;

            foreach (var rank in Enum.GetNames(typeof(RanksOfCard)))
            {
                Console.WriteLine($"{index++} {rank.ToString()}");
            }
        }

        static void ShowSuitsOfCardInfo()
        {
            int index = 0;

            foreach (var suit in Enum.GetNames(typeof(SuitsOfCard)))
            {
                Console.WriteLine($"{index++} {suit.ToString()}");
            }
        }

        static int GetNumber()
        {
            bool isActive = true;
            int result = 0;

            while (isActive)
            {
                string userInput = Console.ReadLine();

                if (int.TryParse(userInput, out result))
                {
                    isActive = false;
                }
                else
                {
                    Console.WriteLine("Ошибка ввода");
                }
            }
            return result;
        }
    }

    enum SuitsOfCard
    {
        Diamonds,
        Hearts,
        Clubs,
        Spades
    }

    enum RanksOfCard
    {
        Joker,
        Ace,
        King,
        Queen,
        Jack,
        Ten,
        Nine,
        Eight,
        Seven,
        Six
    }

    class Card
    {
        private static Random _random;
        private RanksOfCard _rank;
        private SuitsOfCard _suit;

        public RanksOfCard Rank => _rank;
        public SuitsOfCard Suit => _suit;

        public Card(RanksOfCard rank, SuitsOfCard suit)
        {
            _rank = rank;
            _suit = suit;
        }

        public Card()
        {
            _random = new Random();
            _rank = (RanksOfCard)_random.Next(Enum.GetValues(typeof(RanksOfCard)).Length);
            _suit = (SuitsOfCard)_random.Next(Enum.GetValues(typeof(SuitsOfCard)).Length);
        }

        public void ShowCardInfo()
        {
            Console.WriteLine($"Название карты: {Rank} Масть карты: {Suit}");
        }
    }

    class Deck
    {
        private Card _card;
        private int _count;

        public int Count => _count;
        public Card Card => _card;

        public Deck(Card card, int count)
        {
            _card = card;
            _count = count;
        }

        public void ShowDeckINfo()
        {
            Console.WriteLine($"Название карты: {Card.Rank} Масть карты: {Card.Suit} Количество: {Count}");
        }
    }

    class Player
    {
        private List<Deck> _decks;

        public Player()
        {
            _decks = new List<Deck>();
        }

        public void AddRandomCards(int count)
        {
            if (count>0)
            {
                _decks.Add(new Deck(new Card(), count));
            }
            else
            {
                Console.WriteLine("Количество карт некорректно");
            }
        }

        public void AddConcreteCards(int rank, int suit, int count)
        {
            if (rank < Enum.GetNames(typeof(RanksOfCard)).Length && suit < Enum.GetNames(typeof(SuitsOfCard)).Length && count > 0)
            {
                _decks.Add(new Deck(new Card((RanksOfCard)rank, (SuitsOfCard)suit), count));
            }
            else
            {
                Console.WriteLine("Такой карты не существует или количество меньше 0");
            }
        }

        public void ShowPlayerDecks()
        {
            Console.WriteLine("Колода игрока: ");

            foreach (var deck in _decks)
            {
                deck.ShowDeckINfo();
            }

            Console.WriteLine($"Количество карт в колоде игрока: {CountOfCards()}");
        }

        private int CountOfCards()
        {
            int count = 0;

            for (int i = 0; i < _decks.Count; i++)
            {
                count += _decks[i].Count;
            }

            return count;
        }
    }
}
