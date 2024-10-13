using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTCG
{
    public class User
    {
        public string Username { get; set; }
        public string Password { get; set; }

        public List<Card> Stack { get; set; } = new List<Card>();

        public List<Card> Deck { get; set; } = new List<Card>();

        public int Coins { get; set; } = 20;

        public int ELO { get; set; } = 100;

        public int GamesPlayed { get; set; } = 0;

        public User(string username, string password)
        {
            Username = username;
            Password = password;
        }
        public void AddCardToStack(Card card)
        {
            Stack.Add(card);
        }

        public void AddCardToDeck(Card card)
        {
            if (Deck.Count < 4) // Annahme: Deck darf maximal 4 Karten haben
            {
                Deck.Add(card);
            }
            else
            {
                Console.WriteLine("Deck ist already full!");
            }
        }
    }
}
