using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTCG
{
    public class Deck
    {
        public List<Card> SelectedCards { get; set; }
        public int MaxDeckSize { get; set; } = 4;

        public Deck()
        {
            SelectedCards = new List<Card>(4);
        }
        public void AddCard(Card card)
        {
            if (SelectedCards.Count < MaxDeckSize)
            {
                SelectedCards.Add(card);
            }
            else
            {
                Console.WriteLine("Deck is full!");
            }
        }

        public void DisplayDeck()
        {
            foreach (Card card in SelectedCards)
            {
                Console.WriteLine(card.ToString());
            }
        }
    }
}
