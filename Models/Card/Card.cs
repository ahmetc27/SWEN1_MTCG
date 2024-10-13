using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTCG
{
    public enum ElementType
    {
        Fire,
        Water,
        Normal
    };
    public abstract class Card
    {
        public string Name { get; set; }
        public ElementType Element { get; set; }
        public DateTime AddedDate { get; set; }

        public Card(string name, ElementType element)
        {
            Name = name;
            Element = element;
        }
        public abstract override string ToString();
    }
}
