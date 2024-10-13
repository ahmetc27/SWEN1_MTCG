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
            StackCards.Remove(card);
        }
    }
}
