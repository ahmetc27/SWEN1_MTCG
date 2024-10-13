using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTCG
{
    public class Package
    {
        public List<Card> PackageCards { get; set; }
        public Package()
        {
            PackageCards = new List<Card>();
        }

        public void GeneratePackage(List<Card> availableCards)
        {
            Random rng = new Random();
            for (int i = 0; i < 5; i++)
            {
                var card = availableCards[rng.Next(availableCards.Count)];
                PackageCards.Add(card);
            }
        }
    }
}
