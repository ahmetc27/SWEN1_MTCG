using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTCG
{
    public class Stack
    {
        public List<Card> StackCards { get; set; }
        public Stack()
        {
            StackCards = new List<Card>();
        }

        public void AddCard(Card card)
        {
            StackCards.Add(card);
            card.AddedDate = DateTime.Now;
        }
        public void RemoveCard(Card card)
        {
            if (StackCards.Contains(card))
            {
                StackCards.Remove(card);
            }
            else
            {
                Console.WriteLine("Card not found in the stack!");
            }
        }

        public Card DrawCard()
        {
            if (StackCards.Count == 0)
            {
                Console.WriteLine("No cards in the stack!");
                return null;
            }
            Card drawnCard = StackCards[0];
            StackCards.RemoveAt(0);  // Entfernt die oberste Karte
            return drawnCard;
        }

        public Card DrawRandomCard()
        {
            if (StackCards.Count == 0)
            {
                Console.WriteLine("No cards in the stack!");
                return null;
            }
            Random rng = new Random();
            int index = rng.Next(StackCards.Count);
            Card drawnCard = StackCards[index];
            StackCards.RemoveAt(index);
            return drawnCard;
        }
    }
}
