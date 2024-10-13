using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTCG
{
    public class MonsterCard : Card
    {
        public int Damage { get; set; }

        public MonsterCard(string name, ElementType element, int damage) : base(name, element)
        {
            if (damage < 0)
            {
                throw new ArgumentException("Damage cannot be negative!");
            }
            Damage = damage;
        }
        public override string ToString()
        {
            return $"{Name} - {Element} - {Damage} - {AddedDate}";
        }
    }
}
